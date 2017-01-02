using GenFx.ComponentModel;
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
    /// <typeparam name="TAlgorithm">Type of the deriving algorithm class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    public abstract class GeneticAlgorithm<TAlgorithm, TConfiguration> : GeneticComponent<TAlgorithm, TConfiguration>, IGeneticAlgorithm
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
        where TConfiguration : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
    {
        private int currentGeneration;
        private GeneticEnvironment environment;
        private List<IStatistic> statistics = new List<IStatistic>();
        private List<IPlugin> plugins = new List<IPlugin>();
        private ComponentConfigurationSet config;
        private AlgorithmOperators operators = new AlgorithmOperators();
        private bool isInitialized;

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
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configurationSet">Contains the component configuration for the algorithm.</param>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected GeneticAlgorithm(ComponentConfigurationSet configurationSet)
            : base(GetAlgorithmConfiguration(configurationSet))
        {
            if (configurationSet == null)
            {
                throw new ArgumentNullException(nameof(configurationSet));
            }

            this.environment = new GeneticEnvironment(this);

            // We want to ensure the config set cannot be changed once it's being used by an algorithm.  To enforce this, we
            // freeze the state of the config set when the algorithm is created.  By cloning the config set that is passed in, we allow
            // the caller to modify their unfrozen instance so it can be configured appropriately between instantiations of an algorithm.
            this.config = configurationSet.Clone();
            this.config.Freeze();
            this.ValidateConfiguration();
            this.config.CompileExternalValidatorMapping();
            this.config.Validate(this);

            this.CreateComponents();
        }

        private static TConfiguration GetAlgorithmConfiguration(ComponentConfigurationSet configurationSet)
        {
            if (configurationSet == null)
            {
                throw new ArgumentNullException(nameof(configurationSet));
            }

            if (!(configurationSet.GeneticAlgorithm is TConfiguration))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_ComponentConfigurationTypeMismatch,
                  typeof(TConfiguration).FullName, typeof(TAlgorithm).FullName), nameof(configurationSet));
            }

            return (TConfiguration)configurationSet.GeneticAlgorithm;
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfigurationSet"/> containing the configuration for this class.
        /// </summary>
        public ComponentConfigurationSet ConfigurationSet
        {
            get { return this.config; }
        }

        /// <summary>
        /// Gets the collection of statistics being calculated for the genetic algorithm.
        /// </summary>
        public IEnumerable<IStatistic> Statistics
        {
            get { return this.statistics; }
        }

        /// <summary>
        /// Gets the collection of plugins being used by the genetic algorithm.
        /// </summary>
        public IEnumerable<IPlugin> Plugins
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
        /// Gets the <see cref="AlgorithmOperators"/> to be used.
        /// </summary>
        public AlgorithmOperators Operators
        {
            get { return this.operators; }
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
                formatter.Serialize(stream, this.SaveState());
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

        private void CreateComponents()
        {
            IConfigurationForComponentWithAlgorithm componentConfig = null;
            try
            {
                // Set optional operators
                if (this.config.CrossoverOperator != null)
                {
                    componentConfig = this.config.CrossoverOperator;
                    this.operators.CrossoverOperator = (ICrossoverOperator)componentConfig.CreateComponent(this);
                }
                if (this.config.ElitismStrategy != null)
                {
                    componentConfig = this.config.ElitismStrategy;
                    this.operators.ElitismStrategy = (IElitismStrategy)componentConfig.CreateComponent(this);
                }
                if (this.config.MutationOperator != null)
                {
                    componentConfig = this.config.MutationOperator;
                    this.operators.MutationOperator = (IMutationOperator)componentConfig.CreateComponent(this);
                }
                if (this.config.FitnessScalingStrategy != null)
                {
                    componentConfig = this.config.FitnessScalingStrategy;
                    this.operators.FitnessScalingStrategy = (IFitnessScalingStrategy)componentConfig.CreateComponent(this);
                }

                // Set required operators
                componentConfig = this.config.SelectionOperator;
                this.operators.SelectionOperator = (ISelectionOperator)componentConfig.CreateComponent(this);
                componentConfig = this.config.FitnessEvaluator;
                this.operators.FitnessEvaluator = (IFitnessEvaluator)componentConfig.CreateComponent(this);

                componentConfig = this.config.Terminator;
                this.operators.Terminator = (ITerminator)componentConfig.CreateComponent(this);

                this.statistics.Clear();

                foreach (IStatisticConfiguration statConfig in this.config.Statistics)
                {
                    componentConfig = statConfig;
                    IStatistic stat = (IStatistic)componentConfig.CreateComponent(this);
                    this.statistics.Add(stat);
                }

                this.plugins.Clear();

                foreach (IPluginConfiguration pluginConfig in this.ConfigurationSet.Plugins)
                {
                    componentConfig = pluginConfig;
                    this.plugins.Add((IPlugin)componentConfig.CreateComponent(this));
                }
            }
            catch (TargetInvocationException e)
            {
                throw new InvalidOperationException(StringUtil.GetFormattedString(Resources.ErrorMsg_ErrorCreatingComponent, componentConfig.GetType().FullName, e.InnerException.Message), e.InnerException);
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

            this.config.RestoreState((KeyValueMap)state[nameof(this.config)]);

            CreateComponents();

            this.environment.RestoreState((KeyValueMap)state[nameof(this.environment)]);
            this.currentGeneration = (int)state[nameof(this.currentGeneration)];
            this.isInitialized = (bool)state[nameof(this.isInitialized)];

            KeyValueMapCollection statisticStates = (KeyValueMapCollection)state[nameof(this.statistics)];
            for (int i = 0; i < statisticStates.Count; i++)
            {
                this.statistics[i].RestoreState(statisticStates[i]);
            }

            this.Operators.CrossoverOperator?.RestoreState((KeyValueMap)state[nameof(this.Operators.CrossoverOperator)]);
            this.Operators.ElitismStrategy?.RestoreState((KeyValueMap)state[nameof(this.Operators.ElitismStrategy)]);
            this.Operators.FitnessEvaluator?.RestoreState((KeyValueMap)state[nameof(this.Operators.FitnessEvaluator)]);
            this.Operators.FitnessScalingStrategy?.RestoreState((KeyValueMap)state[nameof(this.Operators.FitnessScalingStrategy)]);
            this.Operators.MutationOperator?.RestoreState((KeyValueMap)state[nameof(this.Operators.MutationOperator)]);
            this.Operators.SelectionOperator?.RestoreState((KeyValueMap)state[nameof(this.Operators.SelectionOperator)]);
            this.Operators.Terminator?.RestoreState((KeyValueMap)state[nameof(this.Operators.Terminator)]);
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

            state[nameof(this.config)] = this.config.SaveState();
            state[nameof(this.environment)] = this.environment.SaveState();
            state[nameof(this.currentGeneration)] = this.currentGeneration;
            state[nameof(this.isInitialized)] = this.isInitialized;
            state[nameof(this.statistics)] = new KeyValueMapCollection(this.statistics.Select(s => s.SaveState()).Cast<KeyValueMap>());

            state[nameof(this.Operators.CrossoverOperator)] = this.Operators.CrossoverOperator?.SaveState();
            state[nameof(this.Operators.ElitismStrategy)] = this.Operators.ElitismStrategy?.SaveState();
            state[nameof(this.Operators.FitnessEvaluator)] = this.Operators.FitnessEvaluator?.SaveState();
            state[nameof(this.Operators.FitnessEvaluator)] = this.Operators.FitnessEvaluator?.SaveState();
            state[nameof(this.Operators.FitnessScalingStrategy)] = this.Operators.FitnessScalingStrategy?.SaveState();
            state[nameof(this.Operators.MutationOperator)] = this.Operators.MutationOperator?.SaveState();
            state[nameof(this.Operators.SelectionOperator)] = this.Operators.SelectionOperator?.SaveState();
            state[nameof(this.Operators.Terminator)] = this.Operators.Terminator?.SaveState();
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

            if (this.operators.ElitismStrategy != null)
            {
                return this.operators.ElitismStrategy.GetEliteEntities(currentPopulation);
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

            IGeneticEntity entity1 = this.operators.SelectionOperator.SelectEntity(population);
            IGeneticEntity entity2 = this.operators.SelectionOperator.SelectEntity(population);

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
            ICrossoverOperator crossoverOperator = this.operators.CrossoverOperator;
            if (crossoverOperator != null)
            {
                childEntities = crossoverOperator.Crossover(entity1, entity2);
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
                IMutationOperator mutationOperator = this.operators.MutationOperator;
                IGeneticEntity newEntity = entity;
                if (mutationOperator != null)
                {
                    newEntity = mutationOperator.Mutate(entity);
                }
                mutants.Add(newEntity);
            }
            return mutants;
        }

        /// <summary>
        /// Validates that the <see cref="ComponentConfigurationSet"/> is properly set.
        /// </summary>
        /// <exception cref="InvalidOperationException">The configuration for a required component has been set.</exception>
        /// <remarks>
        /// This only validates that the <see cref="ComponentConfigurationSet"/> is correct as a whole.  It does not validate the state
        /// of each of the configuration objects.
        /// </remarks>
        protected virtual void ValidateConfiguration()
        {
            string missingComponent = null;
            if (this.config.GeneticAlgorithm == null)
            {
                missingComponent = nameof(ComponentConfigurationSet.GeneticAlgorithm);
            }
            else if (this.config.SelectionOperator == null)
            {
                missingComponent = nameof(ComponentConfigurationSet.SelectionOperator);
            }
            else if (this.config.FitnessEvaluator == null)
            {
                missingComponent = nameof(ComponentConfigurationSet.FitnessEvaluator);
            }
            else if (this.config.Population == null)
            {
                missingComponent = nameof(ComponentConfigurationSet.Population);
            }
            else if (this.config.Entity == null)
            {
                missingComponent = nameof(ComponentConfigurationSet.Entity);
            }

            if (missingComponent != null)
            {
                throw new InvalidOperationException(
                  StringUtil.GetFormattedString(Resources.ErrorMsg_MissingOperatorType,
                    typeof(ComponentConfigurationSet).FullName, missingComponent));
            }

            if (this.config.CrossoverOperator != null)
            {
                this.ValidateRequiredComponents(this.config.CrossoverOperator.ComponentType);
            }

            if (this.config.ElitismStrategy != null)
            {
                this.ValidateRequiredComponents(this.config.ElitismStrategy.ComponentType);
            }

            if (this.config.FitnessScalingStrategy != null)
            {
                this.ValidateRequiredComponents(this.config.FitnessScalingStrategy.ComponentType);
            }

            if (this.config.MutationOperator != null)
            {
                this.ValidateRequiredComponents(this.config.MutationOperator.ComponentType);
            }

            foreach (IStatisticConfiguration statConfig in this.config.Statistics)
            {
                this.ValidateRequiredComponents(statConfig.ComponentType);
            }

            foreach (IPluginConfiguration pluginConfig in this.config.Plugins)
            {
                this.ValidateRequiredComponents(pluginConfig.ComponentType);
            }

            if (this.config.Terminator != null)
            {
                this.ValidateRequiredComponents(this.config.Terminator.ComponentType);
            }

            this.ValidateRequiredComponents(this.config.FitnessEvaluator.ComponentType);
            this.ValidateRequiredComponents(this.config.SelectionOperator.ComponentType);
            this.ValidateRequiredComponents(this.config.Entity.ComponentType);
            this.ValidateRequiredComponents(this.config.Population.ComponentType);
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

                string configurableTypeCommonName = null;
                string configuredType = null;

                if (attribs[i] is RequiredCrossoverOperatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.CrossoverOperator.ComponentType))
                    {
                        configurableTypeCommonName = Resources.CrossoverCommonName;
                        configuredType = this.config.CrossoverOperator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredElitismStrategyAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.ElitismStrategy.ComponentType))
                    {
                        configurableTypeCommonName = Resources.ElitismCommonName;
                        configuredType = this.config.ElitismStrategy.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredFitnessEvaluatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.FitnessEvaluator.ComponentType))
                    {
                        configurableTypeCommonName = Resources.FitnessEvaluatorCommonName;
                        configuredType = this.config.FitnessEvaluator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredFitnessScalingStrategyAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.FitnessScalingStrategy.ComponentType))
                    {
                        configurableTypeCommonName = Resources.FitnessScalingCommonName;
                        configuredType = this.config.FitnessScalingStrategy.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredGeneticAlgorithmAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.GetType()))
                    {
                        configurableTypeCommonName = Resources.GeneticAlgorithmCommonName;
                        configuredType = this.GetType().FullName;
                    }
                }
                else if (attribs[i] is RequiredEntityAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.Entity.ComponentType))
                    {
                        configurableTypeCommonName = Resources.EntityCommonName;
                        configuredType = this.config.Entity.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredMutationOperatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.MutationOperator.ComponentType))
                    {
                        configurableTypeCommonName = Resources.MutationCommonName;
                        configuredType = this.config.MutationOperator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredPopulationAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.Population.ComponentType))
                    {
                        configurableTypeCommonName = Resources.PopulationCommonName;
                        configuredType = this.config.Population.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredSelectionOperatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.SelectionOperator.ComponentType))
                    {
                        configurableTypeCommonName = Resources.SelectionCommonName;
                        configuredType = this.config.SelectionOperator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredStatisticAttribute)
                {
                    bool foundRequiredType = false;
                    foreach (IStatisticConfiguration statConfig in this.config.Statistics)
                    {
                        if (!attribs[i].RequiredType.IsAssignableFrom(statConfig.ComponentType))
                        {
                            foundRequiredType = true;
                            break;
                        }
                    }

                    if (!foundRequiredType)
                    {
                        configuredType = this.config.Statistics
                            .Select(s => s.ComponentType.FullName)
                            .Aggregate((type1, type2) => type1 + ", " + type2);

                        configurableTypeCommonName = Resources.StatisticCommonName;
                    }
                }
                else if (attribs[i] is RequiredTerminatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.Terminator.ComponentType))
                    {
                        configurableTypeCommonName = Resources.TerminatorCommonName;
                        configuredType = this.config.Terminator.ComponentType.FullName;
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

            if (!this.operators.Terminator.IsComplete())
            {
                isCanceled = await this.CreateNextGenerationAsync();
                if (!isCanceled)
                {
                    isAlgorithmComplete = this.operators.Terminator.IsComplete();
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
    }
}
