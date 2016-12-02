using GenFx.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of all components of a genetic algorithm.  This class cannot be inherited.
    /// </summary>
    /// <remarks>The configuration values must be appropriately set before executing the genetic algorithm.</remarks>
    public sealed class ComponentConfigurationSet
    {
        private SelectionOperatorConfiguration selectionOperatorConfiguration;
        private GeneticEntityConfiguration entityConfiguration;
        private PopulationConfiguration populationConfiguration;
        private FitnessEvaluatorConfiguration fitnessEvaluatorConfiguration;
        private ComponentConfigurationCollection<StatisticConfiguration> statisticConfigurations = new ComponentConfigurationCollection<StatisticConfiguration>();
        private ComponentConfigurationCollection<PluginConfiguration> pluginConfigurations = new ComponentConfigurationCollection<PluginConfiguration>();
        private GeneticAlgorithmConfiguration geneticAlgorithmConfiguration;

        /// <summary>
        /// Gets or sets the configuration of the <see cref="FitnessEvaluator"/> class that is
        /// used during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public FitnessEvaluatorConfiguration FitnessEvaluator
        {
            get { return this.fitnessEvaluatorConfiguration; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.fitnessEvaluatorConfiguration = value;
            }
        }

        /// <summary>
        /// Gets the configuration for all the <see cref="Statistic"/> classes that
        /// are used to calculate statistics during execution of the genetic algorithm.
        /// </summary>
        public ComponentConfigurationCollection<StatisticConfiguration> Statistics
        {
            get { return this.statisticConfigurations; }
        }

        /// <summary>
        /// Gets the configuration for all the <see cref="Plugin"/> classes that
        /// are used extend functionality.
        /// </summary>
        public ComponentConfigurationCollection<PluginConfiguration> Plugins
        {
            get { return this.pluginConfigurations; }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="GeneticEntity"/> class that is used 
        /// during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public GeneticEntityConfiguration Entity
        {
            get { return this.entityConfiguration; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.entityConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="Population"/> class that is used
        /// during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public PopulationConfiguration Population
        {
            get { return this.populationConfiguration; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.populationConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="ElitismStrategy"/> class that is
        /// used during execution of the genetic algorithm.
        /// </summary>
        public ElitismStrategyConfiguration ElitismStrategy { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="FitnessScalingStrategy"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public FitnessScalingStrategyConfiguration FitnessScalingStrategy { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="CrossoverOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public CrossoverOperatorConfiguration CrossoverOperator { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="MutationOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public MutationOperatorConfiguration MutationOperator { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="SelectionOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public SelectionOperatorConfiguration SelectionOperator
        {
            get { return this.selectionOperatorConfiguration; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.selectionOperatorConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="Terminator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        /// <value>
        /// If the value is null, it indicates that the genetic algorithm should run continously.
        /// </value>
        public TerminatorConfiguration Terminator { get; set; }

        /// <summary>
        /// Gets or sets the configuration of <see cref="GeneticAlgorithm"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public GeneticAlgorithmConfiguration GeneticAlgorithm
        {
            get { return this.geneticAlgorithmConfiguration; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.geneticAlgorithmConfiguration = value;
            }
        }

        /// <summary>
        /// Saves the state of the configuration set.
        /// </summary>
        internal KeyValueMap SaveState()
        {
            KeyValueMap state = new KeyValueMap();
            state[nameof(this.CrossoverOperator)] = SaveComponentConfiguration(this.CrossoverOperator);
            state[nameof(this.ElitismStrategy)] = SaveComponentConfiguration(this.ElitismStrategy);
            state[nameof(this.Entity)] = SaveComponentConfiguration(this.entityConfiguration);
            state[nameof(this.FitnessEvaluator)] = SaveComponentConfiguration(this.fitnessEvaluatorConfiguration);
            state[nameof(this.FitnessScalingStrategy)] = SaveComponentConfiguration(this.FitnessScalingStrategy);
            state[nameof(this.GeneticAlgorithm)] = SaveComponentConfiguration(this.geneticAlgorithmConfiguration);
            state[nameof(this.MutationOperator)] = SaveComponentConfiguration(this.MutationOperator);
            state[nameof(this.Population)] = SaveComponentConfiguration(this.populationConfiguration);
            state[nameof(this.SelectionOperator)] = SaveComponentConfiguration(this.selectionOperatorConfiguration);
            state[nameof(this.Terminator)] = SaveComponentConfiguration(this.Terminator);

            state[nameof(this.Plugins)] = new KeyValueMapCollection(this.pluginConfigurations.Select(c => SaveComponentConfiguration(c)));
            state[nameof(this.Statistics)] = new KeyValueMapCollection(this.statisticConfigurations.Select(c => SaveComponentConfiguration(c)));
            return state;
        }

        /// <summary>
        /// Restores the state of the configuration set.
        /// </summary>
        /// <param name="state">State from which to restore.</param>
        internal void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            this.CrossoverOperator = (CrossoverOperatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.CrossoverOperator)]);
            this.ElitismStrategy = (ElitismStrategyConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.ElitismStrategy)]);
            this.entityConfiguration = (GeneticEntityConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.Entity)]);
            this.fitnessEvaluatorConfiguration = (FitnessEvaluatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.FitnessEvaluator)]);
            this.FitnessScalingStrategy = (FitnessScalingStrategyConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.FitnessScalingStrategy)]);
            this.geneticAlgorithmConfiguration = (GeneticAlgorithmConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.GeneticAlgorithm)]);
            this.MutationOperator = (MutationOperatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.MutationOperator)]);
            this.populationConfiguration = (PopulationConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.Population)]);
            this.selectionOperatorConfiguration = (SelectionOperatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.SelectionOperator)]);
            this.Terminator = (TerminatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.Terminator)]);

            this.pluginConfigurations = new ComponentConfigurationCollection<PluginConfiguration>();
            this.pluginConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Plugins)]).Select(d => RestoreComponentConfiguration(d)).Cast<PluginConfiguration>());

            this.statisticConfigurations = new ComponentConfigurationCollection<StatisticConfiguration>();
            this.statisticConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Statistics)]).Select(d => RestoreComponentConfiguration(d)).Cast<StatisticConfiguration>());
        }

        private static KeyValueMap SaveComponentConfiguration(ComponentConfiguration configuration)
        {
            if (configuration == null)
            {
                return null;
            }

            KeyValueMap state = new KeyValueMap();
            state["$type"] = configuration.GetType().AssemblyQualifiedName;

            IEnumerable<PropertyInfo> properties = configuration.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite);
            foreach (PropertyInfo property in properties)
            {
                object val = property.GetValue(configuration);
                if (val is Enum)
                {
                    val = val.ToString();
                }

                state[property.Name] = val;
            }

            return state;
        }

        private static ComponentConfiguration RestoreComponentConfiguration(Dictionary<string, object> state)
        {
            if (state == null)
            {
                return null;
            }

            ComponentConfiguration config = (ComponentConfiguration)Activator.CreateInstance(Type.GetType((string)state["$type"]));

            IEnumerable<PropertyInfo> properties = config.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite);
            foreach (PropertyInfo property in properties)
            {
                object val = state[property.Name];

                if (property.PropertyType.IsEnum)
                {
                    val = Enum.Parse(property.PropertyType, (string)val);
                }

                property.SetValue(config, val);
            }

            return config;
        }
    }
}
