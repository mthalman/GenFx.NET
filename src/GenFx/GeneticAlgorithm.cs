using GenFx.ComponentModel;
using GenFx.Properties;
using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    public abstract class GeneticAlgorithm : GeneticComponent
    {
        private int currentGeneration;
        private GeneticEnvironment environment;
        private ObservableCollection<Statistic> statistics = new ObservableCollection<Statistic>();
        private ObservableCollection<Plugin> plugins = new ObservableCollection<Plugin>();
        private ComponentConfigurationSet config = new ComponentConfigurationSet();
        private AlgorithmOperators operators = new AlgorithmOperators();
        private bool isInitialized;
        private Dictionary<PropertyInfo, List<Validator>> externalValidationMapping = new Dictionary<PropertyInfo, List<Validator>>();

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
        /// Initializes a new instance of the <see cref="GeneticAlgorithm"/> class.
        /// </summary>
        protected GeneticAlgorithm()
        {
            this.environment = new GeneticEnvironment(this);
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfigurationSet"/> containing the configuration for this <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public ComponentConfigurationSet ConfigurationSet
        {
            get { return this.config; }
        }

        /// <summary>
        /// Gets the collection of statistics being calculated for the genetic algorithm.
        /// </summary>
        public ObservableCollection<Statistic> Statistics
        {
            get { return this.statistics; }
        }

        /// <summary>
        /// Gets the collection of plugins being used by the genetic algorithm.
        /// </summary>
        public ObservableCollection<Plugin> Plugins
        {
            get { return this.plugins; }
        }

        /// <summary>
        /// Gets the <see cref="GeneticEnvironment"/> being used by this <see cref="GeneticEnvironment"/>.
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
        /// Gets the <see cref="AlgorithmOperators"/> to be used by the <see cref="GeneticAlgorithm"/>.
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
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.ConfigurationSet.GeneticAlgorithm; }
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
            this.ValidateConfiguration();
            this.CompileExternalValidatorMapping();
            this.ValidateComponentConfiguration(this);

            this.currentGeneration = 0;

            this.environment.Populations.Clear();
            this.CreateComponents();

            foreach (Plugin plugin in this.Plugins)
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
                formatter.Serialize(stream, SaveStateCore());
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
            Type componentType = null;

            try
            {
                // Set optional operators
                if (this.config.CrossoverOperator != null)
                {
                    componentType = this.config.CrossoverOperator.ComponentType;
                    this.operators.CrossoverOperator = (CrossoverOperator)Activator.CreateInstance(componentType, this);
                }
                if (this.config.ElitismStrategy != null)
                {
                    componentType = this.config.ElitismStrategy.ComponentType;
                    this.operators.ElitismStrategy = (ElitismStrategy)Activator.CreateInstance(componentType, this);
                }
                if (this.config.MutationOperator != null)
                {
                    componentType = this.config.MutationOperator.ComponentType;
                    this.operators.MutationOperator = (MutationOperator)Activator.CreateInstance(componentType, this);
                }
                if (this.config.FitnessScalingStrategy != null)
                {
                    componentType = this.config.FitnessScalingStrategy.ComponentType;
                    this.operators.FitnessScalingStrategy = (FitnessScalingStrategy)Activator.CreateInstance(componentType, this);
                }

                // Set required operators
                componentType = this.config.SelectionOperator.ComponentType;
                this.operators.SelectionOperator = (SelectionOperator)Activator.CreateInstance(componentType, this);
                componentType = this.config.FitnessEvaluator.ComponentType;
                this.operators.FitnessEvaluator = (FitnessEvaluator)Activator.CreateInstance(componentType, this);

                if (this.config.Terminator == null)
                {
                    // If the user doesn't want a terminator, use the EmptyTerminator instead which 
                    // never terminates.
                    componentType = typeof(EmptyTerminator);
                    this.config.Terminator = new EmptyTerminatorConfiguration();
                    this.operators.Terminator = new EmptyTerminator(this);
                }
                else
                {
                    componentType = this.config.Terminator.ComponentType;
                    this.operators.Terminator = (Terminator)Activator.CreateInstance(componentType, this);
                }

                if (this.ConfigurationSet.GeneticAlgorithm.StatisticsEnabled)
                {
                    this.statistics.Clear();

                    for (int i = 0; i < this.config.Statistics.Count; i++)
                    {
                        componentType = this.config.Statistics[i].ComponentType;
                        Statistic stat = (Statistic)Activator.CreateInstance(componentType, this);
                        this.statistics.Add(stat);
                    }
                }

                foreach (PluginConfiguration pluginConfig in this.ConfigurationSet.Plugins)
                {
                    Plugin plugin = (Plugin)Activator.CreateInstance(pluginConfig.ComponentType, this);
                    this.plugins.Add(plugin);
                }
            }
            catch (TargetInvocationException e)
            {
                throw new InvalidOperationException(StringUtil.GetFormattedString(FwkResources.ErrorMsg_ErrorCreatingComponent, componentType.FullName, e.InnerException.Message), e.InnerException);
            }
        }

        /// <summary>
        /// Restores the state of the algorithm.
        /// </summary>
        protected internal override void RestoreState(KeyValueMap state)
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
        protected override void SetSaveState(KeyValueMap state)
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
            state[nameof(this.statistics)] = new KeyValueMapCollection(this.statistics.Select(s => s.SaveStateCore()).Cast<KeyValueMap>());

            state[nameof(this.Operators.CrossoverOperator)] = this.Operators.CrossoverOperator?.SaveStateCore();
            state[nameof(this.Operators.ElitismStrategy)] = this.Operators.ElitismStrategy?.SaveStateCore();
            state[nameof(this.Operators.FitnessEvaluator)] = this.Operators.FitnessEvaluator?.SaveStateCore();
            state[nameof(this.Operators.FitnessEvaluator)] = this.Operators.FitnessEvaluator?.SaveStateCore();
            state[nameof(this.Operators.FitnessScalingStrategy)] = this.Operators.FitnessScalingStrategy?.SaveStateCore();
            state[nameof(this.Operators.MutationOperator)] = this.Operators.MutationOperator?.SaveStateCore();
            state[nameof(this.Operators.SelectionOperator)] = this.Operators.SelectionOperator?.SaveStateCore();
            state[nameof(this.Operators.Terminator)] = this.Operators.Terminator?.SaveStateCore();
        }
        
        /// <summary>
        /// Executes the genetic algorithm.
        /// </summary>
        /// <remarks>
        /// The execution will continue until the <see cref="Terminator.IsComplete()"/> method returns
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
        /// of <see cref="GeneticEntity"/> objects.
        /// </summary>
        /// <param name="population">The current <see cref="Population"/> to be modified.</param>
        protected abstract Task CreateNextGenerationAsync(Population population);

        /// <summary>
        /// Helper method used to apply the <see cref="ElitismStrategy"/>, if one is set, to the <paramref name="currentPopulation"/>.
        /// </summary>
        /// <param name="currentPopulation"><see cref="Population"/> from which to select the
        /// elite <see cref="GeneticEntity"/> objects.</param>
        /// <returns>The collection of <see cref="GeneticEntity"/> objects, if any, that are elite.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="currentPopulation"/> is null.</exception>
        protected IList<GeneticEntity> ApplyElitism(Population currentPopulation)
        {
            if (currentPopulation == null)
            {
                throw new ArgumentNullException(nameof(currentPopulation));
            }

            if (this.operators.ElitismStrategy != null)
            {
                return this.operators.ElitismStrategy.GetElitistGeneticEntities(currentPopulation);
            }
            else
            {
                return new List<GeneticEntity>();
            }
        }

        /// <summary>
        /// Selects two <see cref="GeneticEntity"/> objects from the population and applies
        /// the <see cref="CrossoverOperator"/> and <see cref="MutationOperator"/>, if they exist, to the selected
        /// <see cref="GeneticEntity"/> objects.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to select the <see cref="GeneticEntity"/> objects.</param>
        /// <returns>
        /// List of <see cref="GeneticEntity"/> objects that were selected from the <paramref name="population"/>
        /// and potentially modified through crossover and mutation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected IList<GeneticEntity> SelectGeneticEntitiesAndApplyCrossoverAndMutation(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            GeneticEntity entity1 = this.operators.SelectionOperator.Select(population);
            GeneticEntity entity2 = this.operators.SelectionOperator.Select(population);

            IList<GeneticEntity> childGeneticEntities = this.ApplyCrossover(entity1, entity2);
            childGeneticEntities = this.ApplyMutation(childGeneticEntities);
            return childGeneticEntities;
        }

        /// <summary>
        /// Applies the <see cref="CrossoverOperator"/>, if one is set, to the genetic entities.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be potentially crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be potentially crossed over with <paramref name="entity1"/>.</param>
        /// <returns>List of <see cref="GeneticEntity"/> objects after the crossover was applied.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="entity2"/> is null.</exception>
        protected IList<GeneticEntity> ApplyCrossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            if (entity1 == null)
            {
                throw new ArgumentNullException(nameof(entity1));
            }

            if (entity2 == null)
            {
                throw new ArgumentNullException(nameof(entity2));
            }

            IList<GeneticEntity> childEntities;
            CrossoverOperator crossoverOperator = this.operators.CrossoverOperator;
            if (crossoverOperator != null)
            {
                childEntities = crossoverOperator.Crossover(entity1, entity2);
            }
            else
            {
                childEntities = new List<GeneticEntity>();
                childEntities.Add(entity1);
                childEntities.Add(entity2);
            }
            return childEntities;
        }

        /// <summary>
        /// Applies the <see cref="MutationOperator"/>, if one is set, to the <paramref name="entities"/>.
        /// </summary>
        /// <param name="entities">List of <see cref="GeneticEntity"/> objects to be potentially mutated.</param>
        /// <returns>List of <see cref="GeneticEntity"/> objects after the mutation was applied.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entities"/> is null.</exception>
        protected IList<GeneticEntity> ApplyMutation(IList<GeneticEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            List<GeneticEntity> mutants = new List<GeneticEntity>();
            foreach (GeneticEntity entity in entities)
            {
                MutationOperator mutationOperator = this.operators.MutationOperator;
                GeneticEntity newEntity = entity;
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
                  StringUtil.GetFormattedString(FwkResources.ErrorMsg_MissingOperatorType,
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

            for (int i = 0; i < this.config.Statistics.Count; i++)
            {
                this.ValidateRequiredComponents(this.config.Statistics[i].ComponentType);
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
        /// Validates that the <see cref="GeneticAlgorithm"/> is configured to use all the types 
        /// required by <paramref name="type"/> via the <see cref="RequiredComponentAttribute"/>.
        /// </summary>
        /// <param name="type">Type whose dependencies are to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="type"/> has a required type defined via
        /// the <see cref="RequiredComponentAttribute"/> that the <see cref="GeneticAlgorithm"/> is not
        /// configured to use.
        /// </exception>
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
                        configurableTypeCommonName = FwkResources.CrossoverCommonName;
                        configuredType = this.config.CrossoverOperator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredElitismStrategyAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.ElitismStrategy.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.ElitismCommonName;
                        configuredType = this.config.ElitismStrategy.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredFitnessEvaluatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.FitnessEvaluator.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.FitnessEvaluatorCommonName;
                        configuredType = this.config.FitnessEvaluator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredFitnessScalingStrategyAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.FitnessScalingStrategy.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.FitnessScalingCommonName;
                        configuredType = this.config.FitnessScalingStrategy.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredGeneticAlgorithmAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.GetType()))
                    {
                        configurableTypeCommonName = FwkResources.GeneticAlgorithmCommonName;
                        configuredType = this.GetType().FullName;
                    }
                }
                else if (attribs[i] is RequiredEntityAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.Entity.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.EntityCommonName;
                        configuredType = this.config.Entity.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredMutationOperatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.MutationOperator.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.MutationCommonName;
                        configuredType = this.config.MutationOperator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredPopulationAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.Population.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.PopulationCommonName;
                        configuredType = this.config.Population.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredSelectionOperatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.SelectionOperator.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.SelectionCommonName;
                        configuredType = this.config.SelectionOperator.ComponentType.FullName;
                    }
                }
                else if (attribs[i] is RequiredStatisticAttribute)
                {
                    bool foundRequiredType = false;
                    for (int statIndex = 0; statIndex < this.config.Statistics.Count; statIndex++)
                    {
                        if (!attribs[i].RequiredType.IsAssignableFrom(this.config.Statistics[statIndex].ComponentType))
                        {
                            foundRequiredType = true;
                            break;
                        }
                    }

                    if (!foundRequiredType)
                    {
                        Type[] statTypes = new Type[this.config.Statistics.Count];
                        for (int statIndex = 0; statIndex < statTypes.Length; statIndex++)
                        {
                            statTypes[statIndex] = this.config.Statistics[statIndex].ComponentType;
                        }

                        configuredType = statTypes
                            .Select(statType => statType.FullName)
                            .Aggregate((type1, type2) => type1 + ", " + type2);

                        configurableTypeCommonName = FwkResources.StatisticCommonName;
                    }
                }
                else if (attribs[i] is RequiredTerminatorAttribute)
                {
                    if (!attribs[i].RequiredType.IsAssignableFrom(this.config.Terminator.ComponentType))
                    {
                        configurableTypeCommonName = FwkResources.TerminatorCommonName;
                        configuredType = this.config.Terminator.ComponentType.FullName;
                    }
                }

                if (configurableTypeCommonName != null)
                {
                    throw new InvalidOperationException(
                        StringUtil.GetFormattedString(FwkResources.ErrorMsg_NoRequiredConfigurableType,
                          type.FullName, configurableTypeCommonName.ToLower(CultureInfo.CurrentCulture), attribs[i].RequiredType.FullName, configuredType));
                }
            }
        }

        /// <summary>
        /// Validates that the <see cref="GeneticAlgorithm"/> contains the configuration required by the <paramref name="component"/>.
        /// </summary>
        /// <param name="component">Component object whose configuration is to be resolved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="component"/> does not contain a configuration object.</exception>
        /// <exception cref="ArgumentException"><paramref name="component"/> contains a configuration object that is not associated with that component.</exception>
        /// <exception cref="ValidationException">The configuration for <paramref name="component"/> is in an invalid state.</exception>
        protected internal void ValidateComponentConfiguration(GeneticComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            ComponentConfiguration componentConfig = component.Configuration;
            if (componentConfig == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  FwkResources.ErrorMsg_NoCorrespondingComponentConfiguration, component.GetType().FullName), nameof(component));
            }
            else if (componentConfig.ComponentType != component.GetType())
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  FwkResources.ErrorMsg_ComponentConfigurationTypeMismatch,
                  componentConfig.GetType().FullName, component.GetType().FullName), nameof(component));
            }

            ComponentHelper.Validate(componentConfig, this.externalValidationMapping);
        }

        /// <summary>
        /// Throws an exception if the algorithm is not initialized.
        /// </summary>
        private void CheckAlgorithmIsInitialized()
        {
            if (!this.isInitialized)
            {
                throw new InvalidOperationException(
                  StringUtil.GetFormattedString(FwkResources.ErrorMsg_AlgorithmNotInitialized, "Initialize"));
            }
        }

        /// <summary>
        /// Raises the <see cref="GeneticAlgorithm.FitnessEvaluated"/> event.
        /// </summary>
        /// <returns>True if the user has canceled continued execution of the genetic algorithm; otherwise, false.</returns>
        private bool RaiseFitnessEvaluatedEvent()
        {
            foreach (Plugin plugin in this.Plugins)
            {
                plugin.OnFitnessEvaluated(this.environment, this.currentGeneration);
            }

            EnvironmentFitnessEvaluatedEventArgs e = new EnvironmentFitnessEvaluatedEventArgs(this.environment, this.currentGeneration);
            this.OnFitnessEvaluated(e);
            return e.Cancel;
        }

        /// <summary>
        /// Raises the <see cref="GeneticAlgorithm.FitnessEvaluated"/> event.
        /// </summary>
        /// <param name="e"><see cref="EnvironmentFitnessEvaluatedEventArgs"/> to be passed to the <see cref="GeneticAlgorithm.FitnessEvaluated"/> event.</param>
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
        /// Compiles the mapping of component configuration properties to <see cref="Validator"/> objects as described by external components.
        /// </summary>
        private void CompileExternalValidatorMapping()
        {
            this.externalValidationMapping = new Dictionary<PropertyInfo, List<Validator>>();
            CompileExternalValidatorMapping(this.config.CrossoverOperator, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.ElitismStrategy, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.Entity, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.FitnessEvaluator, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.FitnessScalingStrategy, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.GeneticAlgorithm, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.MutationOperator, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.Population, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.SelectionOperator, this.externalValidationMapping);
            CompileExternalValidatorMapping(this.config.Terminator, this.externalValidationMapping);

            foreach (StatisticConfiguration stat in this.config.Statistics)
            {
                CompileExternalValidatorMapping(stat, this.externalValidationMapping);
            }
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="Validator"/> objects as described by the specified component.
        /// </summary>
        /// <param name="componentConfiguration"><see cref="ComponentConfiguration"/> for the component to check whether it has defined validators for a configuration property.</param>
        /// <param name="mapping">Property to validator mapping.</param>
        private static void CompileExternalValidatorMapping(ComponentConfiguration componentConfiguration, Dictionary<PropertyInfo, List<Validator>> mapping)
        {
            if (componentConfiguration == null)
            {
                return;
            }

            IExternalConfigurationValidatorAttribute[] attribs = (IExternalConfigurationValidatorAttribute[])componentConfiguration.ComponentType.GetCustomAttributes(typeof(IExternalConfigurationValidatorAttribute), true);
            foreach (IExternalConfigurationValidatorAttribute attrib in attribs)
            {
                PropertyInfo prop = ExternalValidatorAttributeHelper.GetTargetPropertyInfo(attrib.TargetComponentConfigurationType, attrib.TargetProperty);
                List<Validator> validators;
                if (!mapping.TryGetValue(prop, out validators))
                {
                    validators = new List<Validator>();
                    mapping.Add(prop, validators);
                }
                validators.Add(attrib.Validator);
            }
        }

        /// <summary>
        /// Executes the genetic algorithm until the <see cref="Terminator.IsComplete()"/> method returns
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

            foreach (Plugin plugin in this.Plugins)
            {
                plugin.OnAlgorithmCompleted();
            }
        }

        /// <summary>
        /// Calculates the statistics for a generation.
        /// </summary>
        private void CalculateStats(GeneticEnvironment geneticEnvironment, int generationIndex)
        {
            foreach (Statistic statistic in this.statistics)
            {
                statistic.Calculate(geneticEnvironment, generationIndex);
            }
        }

        /// <summary>
        /// Returns an instance of the genetic algorithm structure type requested.
        /// </summary>
        /// <typeparam name="T">Type of structure.</typeparam>
        /// <returns>An instance of the genetic algorithm structure type requested.</returns>
        internal T CreateStructureInstance<T>()
        {
            Type requestedType = typeof(T);
            try
            {
                if (requestedType == typeof(Population))
                    return (T)Activator.CreateInstance(this.config.Population.ComponentType, this);
                else if (requestedType == typeof(GeneticEntity))
                {
                    object entity = (GeneticEntity)Activator.CreateInstance(this.config.Entity.ComponentType, this);
                    ((GeneticEntity)entity).Initialize();
                    return (T)entity;
                }
                else
                {
                    Debug.Fail("Requested type is not a valid structure type.");
                    return default(T);
                }
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
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
                Population population = this.environment.Populations[i];

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

    /// <summary>
    /// Represents the configuration of <see cref="GeneticAlgorithm"/>.
    /// </summary>
    [Component(typeof(GeneticAlgorithm))]
    public abstract class GeneticAlgorithmConfiguration : ComponentConfiguration
    {
        private const int DefaultEnvironmentSize = 1;
        private const bool DefaultStatisticsEnabled = true;

        private int environmentSize = GeneticAlgorithmConfiguration.DefaultEnvironmentSize;
        private bool statisticsEnabled = GeneticAlgorithmConfiguration.DefaultStatisticsEnabled;

        /// <summary>
        /// Gets or sets the number of <see cref="Population"/> objects that are contained by the <see cref="GeneticEnvironment"/>.
        /// </summary>
        /// <value>
        /// The number of populations that are contained by the <see cref="GeneticEnvironment"/>.
        /// This value is defaulted to 1 and must be greater or equal to 1.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int EnvironmentSize
        {
            get { return this.environmentSize; }
            set { this.SetProperty(ref this.environmentSize, value); }
        }

        /// <summary>
        /// Gets or sets whether statistics should be calculated during genetic algorithm execution.
        /// </summary>
        /// <value>True if statistics should be calculated; otherwise, false.</value>
        public bool StatisticsEnabled
        {
            get { return this.statisticsEnabled; }
            set { this.SetProperty(ref this.statisticsEnabled, value); }
        }
    }
}
