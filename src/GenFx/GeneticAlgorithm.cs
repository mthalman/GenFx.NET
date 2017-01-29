using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a type of genetic algorithm.
    /// </summary>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    [DataContract]
    public abstract class GeneticAlgorithm : GeneticComponent
    {
        private const int DefaultEnvironmentSize = 1;

        [DataMember]
        private int currentGeneration;

        [DataMember]
        private GeneticEnvironment environment;

        [DataMember]
        private List<Statistic> statistics = new List<Statistic>();

        [DataMember]
        private List<Plugin> plugins = new List<Plugin>();

        [DataMember]
        private bool isInitialized;

        [DataMember]
        private FitnessEvaluator fitnessEvaluator;

        [DataMember]
        private Terminator terminator;

        [DataMember]
        private FitnessScalingStrategy fitnessScalingStrategy;

        [DataMember]
        private SelectionOperator selectionOperator;

        [DataMember]
        private MutationOperator mutationOperator;

        [DataMember]
        private CrossoverOperator crossoverOperator;

        [DataMember]
        private ElitismStrategy elitismStrategy;

        [DataMember]
        private GeneticEntity geneticEntitySeed;

        [DataMember]
        private Population populationSeed;

        [DataMember]
        private int minimumEnvironmentSize = DefaultEnvironmentSize;

        // Mapping of component properties to Validator objects as described by external components
        private Dictionary<PropertyInfo, List<PropertyValidator>> externalValidationMapping;

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
        /// Gets or sets the minimum number of <see cref="Population"/> objects that are contained by the <see cref="GeneticEnvironment"/>.
        /// </summary>
        /// <value>
        /// The number of populations that are contained by the <see cref="GeneticEnvironment"/>.
        /// This value is defaulted to 1 and must be greater or equal to 1.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int MinimumEnvironmentSize
        {
            get { return this.minimumEnvironmentSize; }
            set { this.SetProperty(ref this.minimumEnvironmentSize, value); }
        }

        /// <summary>
        /// Gets the <see cref="FitnessEvaluator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        [RequiredValidator]
        public FitnessEvaluator FitnessEvaluator
        {
            get { return this.fitnessEvaluator; }
            set { this.SetProperty(ref this.fitnessEvaluator, value); }
        }

        /// <summary>
        /// Gets the <see cref="GenFx.Terminator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public Terminator Terminator
        {
            get { return this.terminator; }
            set { this.SetProperty(ref this.terminator, value); }
        }

        /// <summary>
        /// Gets the <see cref="GenFx.FitnessScalingStrategy"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public FitnessScalingStrategy FitnessScalingStrategy
        {
            get { return this.fitnessScalingStrategy; }
            set { this.SetProperty(ref this.fitnessScalingStrategy, value); }
        }

        /// <summary>
        /// Gets the <see cref="GenFx.SelectionOperator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        [RequiredValidator]
        public SelectionOperator SelectionOperator
        {
            get { return this.selectionOperator; }
            set { this.SetProperty(ref this.selectionOperator, value); }
        }

        /// <summary>
        /// Gets the <see cref="GenFx.MutationOperator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public MutationOperator MutationOperator
        {
            get { return this.mutationOperator; }
            set { this.SetProperty(ref this.mutationOperator, value); }
        }

        /// <summary>
        /// Gets the <see cref="GenFx.CrossoverOperator"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public CrossoverOperator CrossoverOperator
        {
            get { return this.crossoverOperator; }
            set { this.SetProperty(ref this.crossoverOperator, value); }
        }

        /// <summary>
        /// Gets the <see cref="GenFx.ElitismStrategy"/> to be used by the algorithm.
        /// </summary>
        [ConfigurationProperty]
        public ElitismStrategy ElitismStrategy
        {
            get { return this.elitismStrategy; }
            set { this.SetProperty(ref this.elitismStrategy, value); }
        }

        /// <summary>
        /// Gets the <see cref="GeneticEntity"/> to be used by the algorithm.
        /// </summary>
        /// <remarks>
        /// This instance is only used for its configuration property values and to generate
        /// additional genetic entities.
        /// </remarks>
        [ConfigurationProperty]
        [RequiredValidator]
        public GeneticEntity GeneticEntitySeed
        {
            get { return this.geneticEntitySeed; }
            set { this.SetProperty(ref this.geneticEntitySeed, value); }
        }

        /// <summary>
        /// Gets the <see cref="Population"/> to be used by the algorithm.
        /// </summary>
        /// <remarks>
        /// This instance is only used for its configuration property values and to generate
        /// additional populations.
        /// </remarks>
        [ConfigurationProperty]
        [RequiredValidator]
        public Population PopulationSeed
        {
            get { return this.populationSeed; }
            set { this.SetProperty(ref this.populationSeed, value); }
        }

        /// <summary>
        /// Gets the collection of statistics to be calculated for the genetic algorithm.
        /// </summary>
        [ConfigurationProperty]
        public IList<Statistic> Statistics
        {
            get { return this.statistics; }
        }

        /// <summary>
        /// Gets the collection of plugins to be used by the genetic algorithm.
        /// </summary>
        [ConfigurationProperty]
        public IList<Plugin> Plugins
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

            if (this.terminator == null)
            {
                this.terminator = new DefaultTerminator();
            }

            this.CompileExternalValidatorMapping();
            this.Validate(this);
            
            foreach (GeneticComponentWithAlgorithm component in this.GetAllComponents())
            {
                component.Initialize(this);
                this.Validate(component);
            }

            this.environment.Populations.Clear();

            this.currentGeneration = 0;

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
        /// of entities.
        /// </summary>
        /// <param name="population">The current <see cref="Population"/> to be modified.</param>
        protected abstract Task CreateNextGenerationAsync(Population population);

        /// <summary>
        /// Helper method used to apply the <see cref="GenFx.SelectionOperator"/>.
        /// </summary>
        /// <param name="entityCount">Number of <see cref="GeneticEntity"/> objects to select from the population.</param>
        /// <param name="currentPopulation"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects from which to select.</param>
        /// <returns>The selected <see cref="GeneticEntity"/> objects.</returns>
        protected IList<GeneticEntity> ApplySelection(int entityCount, Population currentPopulation)
        {
            return this.SelectionOperator.SelectEntities(entityCount, currentPopulation);
        }

        /// <summary>
        /// Helper method used to apply the <see cref="GenFx.ElitismStrategy"/>, if one is set, to the <paramref name="currentPopulation"/>.
        /// </summary>
        /// <param name="currentPopulation"><see cref="Population"/> from which to select the
        /// elite entities.</param>
        /// <returns>The collection of entities, if any, that are elite.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="currentPopulation"/> is null.</exception>
        protected IList<GeneticEntity> ApplyElitism(Population currentPopulation)
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
                return new List<GeneticEntity>();
            }
        }

        ///// <summary>
        ///// Selects entities from the population and applies
        ///// the <see cref="GenFx.CrossoverOperator"/> and <see cref="GenFx.MutationOperator"/>, if they exist, to the selected
        ///// entities.
        ///// </summary>
        ///// <param name="population"><see cref="Population"/> from which to select the entities.</param>
        ///// <returns>
        ///// List of entities that were selected from the <paramref name="population"/>
        ///// and potentially modified through crossover and mutation.
        ///// </returns>
        ///// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        //protected IList<GeneticEntity> SelectGeneticEntitiesAndApplyCrossoverAndMutation(Population population)
        //{
        //    if (population == null)
        //    {
        //        throw new ArgumentNullException(nameof(population));
        //    }

        //    IList<GeneticEntity> parents = this.SelectionOperator.SelectEntities(population.MinimumPopulationSize, population).ToList();
        //    IList<GeneticEntity> offspring = this.ApplyCrossover(population, parents);
        //    offspring = this.ApplyMutation(offspring);
        //    return offspring;
        //}

        /// <summary>
        /// Applies the <see cref="GenFx.CrossoverOperator"/>, if one is set, to the genetic entities.
        /// </summary>
        /// <param name="population">Current population.</param>
        /// <param name="parents">Parents to which to apply the crossover operation.</param>
        /// <returns>List of entities after the crossover was applied.</returns>
        protected IList<GeneticEntity> ApplyCrossover(Population population, IList<GeneticEntity> parents)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            if (parents == null)
            {
                throw new ArgumentNullException(nameof(parents));
            }

            IList<GeneticEntity> childEntities = new List<GeneticEntity>();

            if (this.CrossoverOperator != null)
            {
                Queue<GeneticEntity> parentQueue = new Queue<GeneticEntity>(parents);
                while (childEntities.Count < population.MinimumPopulationSize)
                {
                    // If there are not enough parents left to perform a crossover with, then
                    // just skip the crossover and add the parents to the child list.
                    if (this.CrossoverOperator.RequiredParentCount > parentQueue.Count)
                    {
                        childEntities.AddRange(parentQueue);
                        parentQueue.Clear();
                        break;
                    }
                    else
                    {
                        List<GeneticEntity> parentSubset = new List<GeneticEntity>();
                        for (int i = 0; i < this.CrossoverOperator.RequiredParentCount; i++)
                        {
                            parentSubset.Add(parentQueue.Dequeue());
                        }

                        childEntities.AddRange(this.CrossoverOperator.Crossover(parentSubset));
                    }
                }
            }
            else
            {
                childEntities = parents;
            }

            return childEntities;
        }

        /// <summary>
        /// Applies the <see cref="GenFx.MutationOperator"/>, if one is set, to the <paramref name="entities"/>.
        /// </summary>
        /// <param name="entities">List of entities to be potentially mutated.</param>
        /// <returns>List of entities after the mutation was applied.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entities"/> is null.</exception>
        protected IList<GeneticEntity> ApplyMutation(IList<GeneticEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (this.MutationOperator == null)
            {
                return entities;
            }

            List<GeneticEntity> mutants = new List<GeneticEntity>();
            foreach (GeneticEntity entity in entities)
            {
                GeneticEntity newEntity = this.MutationOperator.Mutate(entity);
                mutants.Add(newEntity);
            }

            return mutants;
        }

        /// <summary>
        /// Returns all <see cref="GeneticComponent"/> instances contained by this algorithm.
        /// </summary>
        /// <returns>All <see cref="GeneticComponent"/> instances contained by this algorithm.</returns>
        internal IEnumerable<GeneticComponent> GetAllComponents()
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

            foreach (Statistic stat in this.Statistics)
            {
                yield return stat;
            }

            foreach (Plugin pluginConfig in this.Plugins)
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
            foreach (Plugin plugin in this.Plugins)
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

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CompileExternalValidatorMapping();
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

        /// <summary>
        /// Validates the component.
        /// </summary>
        /// <param name="component">The <see cref="GeneticComponent"/> to validate.</param>
        private void Validate(GeneticComponent component)
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
                List<PropertyValidator> externalValidators;
                if (externalValidationMapping.TryGetValue(propertyInfo, out externalValidators))
                {
                    object propValue = propertyInfo.GetValue(this, null);
                    foreach (PropertyValidator validator in externalValidators)
                    {
                        validator.EnsureIsValid(component.GetType().Name + Type.Delimiter + propertyInfo.Name, propValue, component);
                    }
                }
            }
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="PropertyValidator"/> objects as described by external components.
        /// </summary>
        private void CompileExternalValidatorMapping()
        {
            this.externalValidationMapping = new Dictionary<PropertyInfo, List<PropertyValidator>>();

            foreach (GeneticComponent component in this.GetAllComponents())
            {
                this.CompileExternalValidatorMapping(component);
            }
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="PropertyValidator"/> objects as described by the specified component.
        /// </summary>
        /// <param name="component">The component to check whether it has defined validators for a configuration property.</param>
        private void CompileExternalValidatorMapping(GeneticComponent component)
        {
            if (component == null)
            {
                return;
            }

            IExternalConfigurationPropertyValidatorAttribute[] attribs = (IExternalConfigurationPropertyValidatorAttribute[])component.GetType().GetCustomAttributes(typeof(IExternalConfigurationPropertyValidatorAttribute), true);
            foreach (IExternalConfigurationPropertyValidatorAttribute attrib in attribs)
            {
                PropertyInfo prop = ExternalValidatorAttributeHelper.GetTargetPropertyInfo(attrib.TargetComponentType, attrib.TargetProperty);
                List<PropertyValidator> validators;
                if (!this.externalValidationMapping.TryGetValue(prop, out validators))
                {
                    validators = new List<PropertyValidator>();
                    this.externalValidationMapping.Add(prop, validators);
                }
                validators.Add(attrib.Validator);
            }
        }
    }
}
