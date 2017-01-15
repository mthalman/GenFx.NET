using GenFx.Contracts;
using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a type of genetic algorithm.
    /// </summary>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    public abstract class GeneticAlgorithm : GeneticComponent, IGeneticAlgorithm
    {
        private const int DefaultEnvironmentSize = 1;

        private int currentGeneration;
        private GeneticEnvironment environment;
        private List<IStatistic> statistics = new List<IStatistic>();
        private List<IPlugin> plugins = new List<IPlugin>();
        private bool isInitialized;
        private IFitnessEvaluator fitnessEvaluator;
        private ITerminator terminator;
        private IFitnessScalingStrategy fitnessScalingStrategy;
        private ISelectionOperator selectionOperator;
        private IMutationOperator mutationOperator;
        private ICrossoverOperator crossoverOperator;
        private IElitismStrategy elitismStrategy;
        private IGeneticEntity geneticEntitySeed;
        private IPopulation populationSeed;
        private int environmentSize = DefaultEnvironmentSize;

        // Mapping of component properties to Validator objects as described by external components.
        private Dictionary<PropertyInfo, List<Validator>> externalValidationMapping;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected GeneticAlgorithm()
        {
        }

        /// <summary>
        /// Occurs when the fitness of an environment has been evaluated.
        /// </summary>
        public event EventHandler<EnvironmentFitnessEvaluatedEventArgs> FitnessEvaluated;

        /// <summary>
        /// Occurs when a new generation has been created (but its fitness has not yet been evaluated).
        /// </summary>
        public event EventHandler GenerationCreated;

        /// <summary>
        /// Occurs when execution of the algorithm completes.
        /// </summary>
        public event EventHandler AlgorithmCompleted;

        /// <summary>
        /// Occurs when the algorithm is about to begin execution.
        /// </summary>
        /// <remarks>
        /// This event only occurs when the algorithm is first started after having been initialized.
        /// It does not occur when resuming execution after a pause.
        /// </remarks>
        public event EventHandler AlgorithmStarting;

        /// <summary>
        /// Gets or sets the number of <see cref="IPopulation"/> objects that are contained by the <see cref="GeneticEnvironment"/>.
        /// </summary>
        /// <value>
        /// The number of populations that are contained by the <see cref="GeneticEnvironment"/>.
        /// This value is defaulted to 1 and must be greater or equal to 1.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int EnvironmentSize
        {
            get { return this.environmentSize; }
            set { this.SetProperty(ref this.environmentSize, value); }
        }

        /// <summary>
        /// Gets the <see cref="IFitnessEvaluator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        [RequiredValidator]
        public IFitnessEvaluator FitnessEvaluator
        {
            get { return this.fitnessEvaluator; }
            set { this.SetProperty(ref this.fitnessEvaluator, value); }
        }

        /// <summary>
        /// Gets the <see cref="ITerminator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public ITerminator Terminator
        {
            get { return this.terminator; }
            set { this.SetProperty(ref this.terminator, value); }
        }

        /// <summary>
        /// Gets the <see cref="IFitnessScalingStrategy"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public IFitnessScalingStrategy FitnessScalingStrategy
        {
            get { return this.fitnessScalingStrategy; }
            set { this.SetProperty(ref this.fitnessScalingStrategy, value); }
        }

        /// <summary>
        /// Gets the <see cref="ISelectionOperator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        [RequiredValidator]
        public ISelectionOperator SelectionOperator
        {
            get { return this.selectionOperator; }
            set { this.SetProperty(ref this.selectionOperator, value); }
        }

        /// <summary>
        /// Gets the <see cref="IMutationOperator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public IMutationOperator MutationOperator
        {
            get { return this.mutationOperator; }
            set { this.SetProperty(ref this.mutationOperator, value); }
        }

        /// <summary>
        /// Gets the <see cref="ICrossoverOperator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public ICrossoverOperator CrossoverOperator
        {
            get { return this.crossoverOperator; }
            set { this.SetProperty(ref this.crossoverOperator, value); }
        }

        /// <summary>
        /// Gets the <see cref="IElitismStrategy"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public IElitismStrategy ElitismStrategy
        {
            get { return this.elitismStrategy; }
            set { this.SetProperty(ref this.elitismStrategy, value); }
        }

        /// <summary>
        /// Gets the <see cref="IGeneticEntity"/> to be used by the algorithm.
        /// </summary>
        /// <remarks>
        /// This instance is only used for its configuration property values and to generate
        /// additional genetic entities.
        /// </remarks>
        [ConfigurationProperty]
        [RequiredValidator]
        public IGeneticEntity GeneticEntitySeed
        {
            get { return this.geneticEntitySeed; }
            set { this.SetProperty(ref this.geneticEntitySeed, value); }
        }

        /// <summary>
        /// Gets the <see cref="IPopulation"/> to be used by the algorithm.
        /// </summary>
        /// <remarks>
        /// This instance is only used for its configuration property values and to generate
        /// additional populations.
        /// </remarks>
        [ConfigurationProperty]
        [RequiredValidator]
        public IPopulation PopulationSeed
        {
            get { return this.populationSeed; }
            set { this.SetProperty(ref this.populationSeed, value); }
        }

        /// <summary>
        /// Gets the collection of statistics to be calculated for the genetic algorithm.
        /// </summary>
        [ConfigurationProperty]
        public IList<IStatistic> Statistics
        {
            get { return this.statistics; }
        }

        /// <summary>
        /// Gets the collection of plugins to be used by the genetic algorithm.
        /// </summary>
        [ConfigurationProperty]
        public IList<IPlugin> Plugins
        {
            get { return this.plugins; }
        }

        /// <summary>
        /// Gets the <see cref="GeneticEnvironment"/> being used by this object.
        /// </summary>
        public GeneticEnvironment Environment
        {
            get { return this.environment; }
        }

        /// <summary>
        /// Gets the index of the generation reached so far during execution of the genetic algorithm.
        /// </summary>
        /// <value>
        /// The index of the generation reached so far during execution of the genetic algorithm.
        /// </value>
        public int CurrentGeneration
        {
            get { return this.currentGeneration; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the algorithm is initialized.
        /// </summary>
        public bool IsInitialized
        {
            get { return this.isInitialized; }
        }

        /// <summary>
        /// Initializes the genetic algorithm with a starting <see cref="GeneticEnvironment"/>.
        /// </summary>
        /// <exception cref="ValidationException">The state of a component's configuration is invalid.</exception>
        /// <exception cref="InvalidOperationException">The configuration for a required component has not been set.</exception>
        /// <exception cref="InvalidOperationException">An exception occured while instantiating a component.</exception>
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public async Task InitializeAsync()
        {
            this.environment = new GeneticEnvironment(this);

            this.CompileExternalValidatorMapping();
            this.Validate(this);
            this.ValidateConfiguration();
            

            foreach (IGeneticComponentWithAlgorithm component in this.GetAllComponents())
            {
                component.Initialize(this);
                this.Validate(component);
            }

            this.environment.Populations.Clear();

            this.currentGeneration = 0;

            foreach (IPlugin plugin in this.Plugins)
            {
                plugin.OnAlgorithmStarting();
            }

            await this.environment.InitializeAsync();
            this.OnGenerationCreated();
            await this.environment.EvaluateFitnessAsync();
            this.RaiseFitnessEvaluatedEvent();
            this.isInitialized = true;
        }

        /// <summary>
        /// Saves the state of the algorithm.
        /// </summary>
        /// <returns>The saved state of the algorithm.</returns>
        public byte[] SaveState()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, GeneticComponentExtensions.SaveState(this));
                stream.Flush();
                return stream.GetBuffer();
            }
        }

        /// <summary>
        /// Restores the state of the algorithm.
        /// </summary>
        /// <param name="savedState">The previously saved algorithm state to be restored.</param>
        public void RestoreState(byte[] savedState)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(savedState))
            {
                KeyValueMap state = (KeyValueMap)formatter.Deserialize(stream);
                RestoreState(state);
            }
        }
        
        /// <summary>
        /// Restores the state of the algorithm.
        /// </summary>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);
            
            this.environment.RestoreState((KeyValueMap)state[nameof(this.environment)]);
            this.currentGeneration = (int)state[nameof(this.currentGeneration)];
            this.isInitialized = (bool)state[nameof(this.isInitialized)];
            this.EnvironmentSize = (int)state[nameof(this.EnvironmentSize)];

            KeyValueMapCollection statisticStates = (KeyValueMapCollection)state[nameof(this.Statistics)];
            for (int i = 0; i < statisticStates.Count; i++)
            {
                this.statistics[i].RestoreState(statisticStates[i]);
            }

            KeyValueMapCollection pluginStates = (KeyValueMapCollection)state[nameof(this.Plugins)];
            for (int i = 0; i < pluginStates.Count; i++)
            {
                this.plugins[i].RestoreState(pluginStates[i]);
            }

            this.CrossoverOperator?.RestoreState((KeyValueMap)state[nameof(this.CrossoverOperator)]);
            this.ElitismStrategy?.RestoreState((KeyValueMap)state[nameof(this.ElitismStrategy)]);
            this.FitnessEvaluator?.RestoreState((KeyValueMap)state[nameof(this.FitnessEvaluator)]);
            this.FitnessScalingStrategy?.RestoreState((KeyValueMap)state[nameof(this.FitnessScalingStrategy)]);
            this.MutationOperator?.RestoreState((KeyValueMap)state[nameof(this.MutationOperator)]);
            this.SelectionOperator?.RestoreState((KeyValueMap)state[nameof(this.SelectionOperator)]);
            this.Terminator?.RestoreState((KeyValueMap)state[nameof(this.Terminator)]);
            this.PopulationSeed?.RestoreState((KeyValueMap)state[nameof(this.PopulationSeed)]);
            this.GeneticEntitySeed?.RestoreState((KeyValueMap)state[nameof(this.GeneticEntitySeed)]);
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.environment)] = this.environment.SaveState();
            state[nameof(this.currentGeneration)] = this.currentGeneration;
            state[nameof(this.isInitialized)] = this.isInitialized;
            state[nameof(this.EnvironmentSize)] = this.EnvironmentSize;

            state[nameof(this.Statistics)] = new KeyValueMapCollection(this.statistics.Select(s => s.SaveState()).Cast<KeyValueMap>());
            state[nameof(this.Plugins)] = new KeyValueMapCollection(this.plugins.Select(s => s.SaveState()).Cast<KeyValueMap>());

            state[nameof(this.CrossoverOperator)] = this.CrossoverOperator?.SaveState();
            state[nameof(this.ElitismStrategy)] = this.ElitismStrategy?.SaveState();
            state[nameof(this.FitnessEvaluator)] = this.FitnessEvaluator?.SaveState();
            state[nameof(this.FitnessEvaluator)] = this.FitnessEvaluator?.SaveState();
            state[nameof(this.FitnessScalingStrategy)] = this.FitnessScalingStrategy?.SaveState();
            state[nameof(this.MutationOperator)] = this.MutationOperator?.SaveState();
            state[nameof(this.SelectionOperator)] = this.SelectionOperator?.SaveState();
            state[nameof(this.Terminator)] = this.Terminator?.SaveState();
            state[nameof(this.GeneticEntitySeed)] = this.GeneticEntitySeed?.SaveState();
            state[nameof(this.PopulationSeed)] = this.PopulationSeed?.SaveState();
        }

        /// <summary>
        /// Executes the genetic algorithm.
        /// </summary>
        /// <remarks>
        /// The execution will continue until the <see cref="ITerminator.IsComplete()"/> method returns
        /// true or <see cref="CancelEventArgs.Cancel"/> property is set to true.
        /// </remarks>
        /// <exception cref="InvalidOperationException">The algorithm has not been initialized.</exception>
        public async Task RunAsync()
        {
            this.CheckAlgorithmIsInitialized();

            bool cancelRun = false;
            while (!cancelRun)
            {
                cancelRun = await this.StepCoreAsync();
            }
        }



        /// <summary>
        /// Executes one generation of the genetic algorithm.
        /// </summary>
        /// <returns>True if the genetic algorithm has completed its execution; otherwise, false.</returns>
        /// <remarks>
        /// Subsequent calls to either <see cref="RunAsync()"/> or <see cref="StepAsync()"/> will resume execution from
        /// where the previous <see cref="StepAsync()"/> call left off.
        /// </remarks>
        /// <exception cref="InvalidOperationException">The algorithm has not been initialized.</exception>
        public Task<bool> StepAsync()
        {
            this.CheckAlgorithmIsInitialized();
            return this.StepCoreAsync();
        }

        /// <summary>
        /// Completes the execution of the algorithm.
        /// </summary>
        public void Complete()
        {
            this.OnAlgorithmCompleted();
        }

        /// <summary>
        /// When overriden in a derived class, modifies <paramref name="population"/> to become the next generation
        /// of entities.
        /// </summary>
        /// <param name="population">The current <see cref="IPopulation"/> to be modified.</param>
        protected abstract Task CreateNextGenerationAsync(IPopulation population);

        /// <summary>
        /// Helper method used to apply the <see cref="IElitismStrategy"/>, if one is set, to the <paramref name="currentPopulation"/>.
        /// </summary>
        /// <param name="currentPopulation"><see cref="IPopulation"/> from which to select the
        /// elite entities.</param>
        /// <returns>The collection of entities, if any, that are elite.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="currentPopulation"/> is null.</exception>
        protected IList<IGeneticEntity> ApplyElitism(IPopulation currentPopulation)
        {
            if (currentPopulation == null)
            {
                throw new ArgumentNullException(nameof(currentPopulation));
            }

            if (this.ElitismStrategy != null)
            {
                return this.ElitismStrategy.GetEliteEntities(currentPopulation);
            }
            else
            {
                return new List<IGeneticEntity>();
            }
        }

        /// <summary>
        /// Selects two entities from the population and applies
        /// the <see cref="ICrossoverOperator"/> and <see cref="IMutationOperator"/>, if they exist, to the selected
        /// entities.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> from which to select the entities.</param>
        /// <returns>
        /// List of entities that were selected from the <paramref name="population"/>
        /// and potentially modified through crossover and mutation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected IList<IGeneticEntity> SelectGeneticEntitiesAndApplyCrossoverAndMutation(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            IGeneticEntity entity1 = this.SelectionOperator.SelectEntity(population);
            IGeneticEntity entity2 = this.SelectionOperator.SelectEntity(population);

            IList<IGeneticEntity> childGeneticEntities = this.ApplyCrossover(entity1, entity2);
            childGeneticEntities = this.ApplyMutation(childGeneticEntities);
            return childGeneticEntities;
        }

        /// <summary>
        /// Applies the <see cref="ICrossoverOperator"/>, if one is set, to the genetic entities.
        /// </summary>
        /// <param name="entity1"><see cref="IGeneticEntity"/> to be potentially crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="IGeneticEntity"/> to be potentially crossed over with <paramref name="entity1"/>.</param>
        /// <returns>List of entities after the crossover was applied.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="entity2"/> is null.</exception>
        protected IList<IGeneticEntity> ApplyCrossover(IGeneticEntity entity1, IGeneticEntity entity2)
        {
            if (entity1 == null)
            {
                throw new ArgumentNullException(nameof(entity1));
            }

            if (entity2 == null)
            {
                throw new ArgumentNullException(nameof(entity2));
            }

            IList<IGeneticEntity> childEntities;
            
            if (this.CrossoverOperator != null)
            {
                childEntities = this.CrossoverOperator.Crossover(entity1, entity2);
            }
            else
            {
                childEntities = new List<IGeneticEntity>();
                childEntities.Add(entity1);
                childEntities.Add(entity2);
            }
            return childEntities;
        }

        /// <summary>
        /// Applies the <see cref="IMutationOperator"/>, if one is set, to the <paramref name="entities"/>.
        /// </summary>
        /// <param name="entities">List of entities to be potentially mutated.</param>
        /// <returns>List of entities after the mutation was applied.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entities"/> is null.</exception>
        protected IList<IGeneticEntity> ApplyMutation(IList<IGeneticEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            List<IGeneticEntity> mutants = new List<IGeneticEntity>();
            foreach (IGeneticEntity entity in entities)
            {
                IGeneticEntity newEntity = entity;
                if (this.MutationOperator != null)
                {
                    newEntity = this.MutationOperator.Mutate(entity);
                }
                mutants.Add(newEntity);
            }
            return mutants;
        }

        /// <summary>
        /// Validates the correctness of the algorithm's configuration.
        /// </summary>
        /// <exception cref="InvalidOperationException">The configuration for a required component has been set.</exception>
        /// <remarks>
        /// This only validates that the algorithm object is correct.  It does not validate the state
        /// of each of the referenced components.
        /// </remarks>
        private void ValidateConfiguration()
        {
            foreach (IGeneticComponent component in this.GetAllComponents())
            {
                this.ValidateRequiredComponents(component.GetType());
            }
        }

        private IEnumerable<IGeneticComponent> GetAllComponents()
        {
            if (this.CrossoverOperator != null)
            {
                yield return this.CrossoverOperator;
            }

            if (this.ElitismStrategy != null)
            {
                yield return this.ElitismStrategy;
            }

            if (this.FitnessScalingStrategy != null)
            {
                yield return this.FitnessScalingStrategy;
            }

            if (this.MutationOperator != null)
            {
                yield return this.MutationOperator;
            }

            foreach (IStatistic stat in this.Statistics)
            {
                yield return stat;
            }

            foreach (IPlugin pluginConfig in this.Plugins)
            {
                yield return pluginConfig;
            }

            if (this.Terminator != null)
            {
                yield return terminator;
            }
            
            yield return this.FitnessEvaluator;
            yield return this.SelectionOperator;
            yield return this.GeneticEntitySeed;
            yield return this.PopulationSeed;
        }

        /// <summary>
        /// Validates that the <see cref="IGeneticAlgorithm"/> is configured to use all the types 
        /// required by <paramref name="type"/> via the <see cref="RequiredComponentAttribute"/>.
        /// </summary>
        /// <param name="type">Type whose dependencies are to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="type"/> has a required type defined via
        /// the <see cref="RequiredComponentAttribute"/> that the <see cref="IGeneticAlgorithm"/> is not
        /// configured to use.
        /// </exception>
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected internal void ValidateRequiredComponents(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            RequiredComponentAttribute[] attribs = (RequiredComponentAttribute[])type.GetCustomAttributes(typeof(RequiredComponentAttribute), true);
            
            for (int i = 0; i < attribs.Length; i++)
            {
                RequiredComponentAttribute attrib = attribs[i];
                string configurableTypeCommonName = null;
                string configuredType = null;

                if (attrib is RequiredCrossoverOperatorAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.CrossoverOperator.GetType()))
                    {
                        configurableTypeCommonName = Resources.CrossoverCommonName;
                        configuredType = this.CrossoverOperator.GetType().FullName;
                    }
                }
                else if (attrib is RequiredElitismStrategyAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.ElitismStrategy.GetType()))
                    {
                        configurableTypeCommonName = Resources.ElitismCommonName;
                        configuredType = this.ElitismStrategy.GetType().FullName;
                    }
                }
                else if (attrib is RequiredFitnessEvaluatorAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.FitnessEvaluator.GetType()))
                    {
                        configurableTypeCommonName = Resources.FitnessEvaluatorCommonName;
                        configuredType = this.FitnessEvaluator.GetType().FullName;
                    }
                }
                else if (attrib is RequiredFitnessScalingStrategyAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.FitnessScalingStrategy.GetType()))
                    {
                        configurableTypeCommonName = Resources.FitnessScalingCommonName;
                        configuredType = this.FitnessScalingStrategy.GetType().FullName;
                    }
                }
                else if (attrib is RequiredGeneticAlgorithmAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.GetType()))
                    {
                        configurableTypeCommonName = Resources.GeneticAlgorithmCommonName;
                        configuredType = this.GetType().FullName;
                    }
                }
                else if (attrib is RequiredEntityAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.GeneticEntitySeed.GetType()))
                    {
                        configurableTypeCommonName = Resources.EntityCommonName;
                        configuredType = this.GeneticEntitySeed.GetType().FullName;
                    }
                }
                else if (attrib is RequiredMutationOperatorAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.MutationOperator.GetType()))
                    {
                        configurableTypeCommonName = Resources.MutationCommonName;
                        configuredType = this.MutationOperator.GetType().FullName;
                    }
                }
                else if (attrib is RequiredPopulationAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.PopulationSeed.GetType()))
                    {
                        configurableTypeCommonName = Resources.PopulationCommonName;
                        configuredType = this.PopulationSeed.GetType().FullName;
                    }
                }
                else if (attrib is RequiredSelectionOperatorAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.SelectionOperator.GetType()))
                    {
                        configurableTypeCommonName = Resources.SelectionCommonName;
                        configuredType = this.SelectionOperator.GetType().FullName;
                    }
                }
                else if (attrib is RequiredStatisticAttribute)
                {
                    bool foundRequiredType = false;
                    foreach (IStatistic stat in this.Statistics)
                    {
                        if (!attribs[i].RequiredType.IsAssignableFrom(stat.GetType()))
                        {
                            foundRequiredType = true;
                            break;
                        }
                    }

                    if (!foundRequiredType)
                    {
                        configuredType = this.Statistics
                            .Select(s => s.GetType().FullName)
                            .Aggregate((type1, type2) => type1 + ", " + type2);

                        configurableTypeCommonName = Resources.StatisticCommonName;
                    }
                }
                else if (attrib is RequiredTerminatorAttribute)
                {
                    if (!attrib.RequiredType.IsAssignableFrom(this.Terminator.GetType()))
                    {
                        configurableTypeCommonName = Resources.TerminatorCommonName;
                        configuredType = this.Terminator.GetType().FullName;
                    }
                }

                if (configurableTypeCommonName != null)
                {
                    throw new InvalidOperationException(
                        StringUtil.GetFormattedString(Resources.ErrorMsg_NoRequiredConfigurableType,
                          type.FullName, configurableTypeCommonName.ToLower(CultureInfo.CurrentCulture), attribs[i].RequiredType.FullName, configuredType));
                }
            }
        }
        
        /// <summary>
        /// Throws an exception if the algorithm is not initialized.
        /// </summary>
        private void CheckAlgorithmIsInitialized()
        {
            if (!this.isInitialized)
            {
                throw new InvalidOperationException(
                  StringUtil.GetFormattedString(Resources.ErrorMsg_AlgorithmNotInitialized, "Initialize"));
            }
        }

        /// <summary>
        /// Raises the <see cref="FitnessEvaluated"/> event.
        /// </summary>
        /// <returns>True if the user has canceled continued execution of the genetic algorithm; otherwise, false.</returns>
        private bool RaiseFitnessEvaluatedEvent()
        {
            foreach (IPlugin plugin in this.Plugins)
            {
                plugin.OnFitnessEvaluated(this.environment, this.currentGeneration);
            }

            EnvironmentFitnessEvaluatedEventArgs e = new EnvironmentFitnessEvaluatedEventArgs(this.environment, this.currentGeneration);
            this.OnFitnessEvaluated(e);
            return e.Cancel;
        }

        /// <summary>
        /// Raises the <see cref="FitnessEvaluated"/> event.
        /// </summary>
        /// <param name="e"><see cref="EnvironmentFitnessEvaluatedEventArgs"/> to be passed to the <see cref="FitnessEvaluated"/> event.</param>
        private void OnFitnessEvaluated(EnvironmentFitnessEvaluatedEventArgs e)
        {
            this.CalculateStats(e.Environment, e.GenerationIndex);

            this.FitnessEvaluated?.Invoke(this, e);
        }

        private void OnGenerationCreated()
        {
            this.GenerationCreated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="AlgorithmStarting"/> event.
        /// </summary>
        private void OnAlgorithmStarting()
        {
            this.AlgorithmStarting?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Executes the genetic algorithm until the <see cref="ITerminator.IsComplete()"/> method returns
        /// true or <see cref="CancelEventArgs.Cancel"/> property is set to true.
        /// </summary>
        /// <returns>true if the genetic algorithm has completed its execution; otherwise, false.</returns>
        private async Task<bool> StepCoreAsync()
        {
            if (this.currentGeneration == 1)
            {
                this.OnAlgorithmStarting();
            }

            bool isAlgorithmComplete = false;
            bool isCanceled = false;

            if (!this.Terminator.IsComplete())
            {
                isCanceled = await this.CreateNextGenerationAsync();
                if (!isCanceled)
                {
                    isAlgorithmComplete = this.Terminator.IsComplete();
                    if (isAlgorithmComplete)
                    {
                        this.isInitialized = false;
                    }
                }
            }
            else
            {
                isAlgorithmComplete = true;
            }

            if (isAlgorithmComplete)
            {
                this.Complete();
            }

            return isAlgorithmComplete || isCanceled;
        }

        private void OnAlgorithmCompleted()
        {
            this.AlgorithmCompleted?.Invoke(this, EventArgs.Empty);

            foreach (IPlugin plugin in this.Plugins)
            {
                plugin.OnAlgorithmCompleted();
            }
        }

        /// <summary>
        /// Calculates the statistics for a generation.
        /// </summary>
        private void CalculateStats(GeneticEnvironment geneticEnvironment, int generationIndex)
        {
            foreach (IStatistic statistic in this.statistics)
            {
                statistic.Calculate(geneticEnvironment, generationIndex);
            }
        }
        
        /// <summary>
        /// Creates the next generation.
        /// </summary>
        private async Task<bool> CreateNextGenerationAsync()
        {
            List<Task> createGenerationTasks = new List<Task>();

            for (int i = 0; i < this.environment.Populations.Count; i++)
            {
                IPopulation population = this.environment.Populations[i];

                // Increment the age of all the genetic entities in the population.
                Parallel.ForEach(population.Entities, e => e.Age++);

                createGenerationTasks.Add(this.CreateNextGenerationAsync(population));
            }

            await Task.WhenAll(createGenerationTasks);
            this.currentGeneration++;
            this.OnGenerationCreated();

            await this.environment.EvaluateFitnessAsync();
            return this.RaiseFitnessEvaluatedEvent();
        }

        /// <summary>
        /// Validates the component.
        /// </summary>
        /// <param name="component">The <see cref="IGeneticComponent"/> to validate.</param>
        private void Validate(IGeneticComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            component.Validate();

            IEnumerable<PropertyInfo> properties = component.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            foreach (PropertyInfo propertyInfo in properties)
            {
                // Check that the property is valid using the validators described by external components.
                List<Validator> externalValidators;
                if (externalValidationMapping.TryGetValue(propertyInfo, out externalValidators))
                {
                    object propValue = propertyInfo.GetValue(this, null);
                    foreach (Validator validator in externalValidators)
                    {
                        validator.EnsureIsValid(component.GetType().Name + Type.Delimiter + propertyInfo.Name, propValue, component);
                    }
                }
            }
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="Validator"/> objects as described by external components.
        /// </summary>
        private void CompileExternalValidatorMapping()
        {
            this.externalValidationMapping = new Dictionary<PropertyInfo, List<Validator>>();

            foreach (IGeneticComponent component in this.GetAllComponents())
            {
                this.CompileExternalValidatorMapping(component);
            }
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="Validator"/> objects as described by the specified component.
        /// </summary>
        /// <param name="component">The component to check whether it has defined validators for a configuration property.</param>
        private void CompileExternalValidatorMapping(IGeneticComponent component)
        {
            if (component == null)
            {
                return;
            }

            IExternalConfigurationValidatorAttribute[] attribs = (IExternalConfigurationValidatorAttribute[])component.GetType().GetCustomAttributes(typeof(IExternalConfigurationValidatorAttribute), true);
            foreach (IExternalConfigurationValidatorAttribute attrib in attribs)
            {
                PropertyInfo prop = ExternalValidatorAttributeHelper.GetTargetPropertyInfo(attrib.TargetComponentType, attrib.TargetProperty);
                List<Validator> validators;
                if (!this.externalValidationMapping.TryGetValue(prop, out validators))
                {
                    validators = new List<Validator>();
                    this.externalValidationMapping.Add(prop, validators);
                }
                validators.Add(attrib.Validator);
            }
        }
    }
}
