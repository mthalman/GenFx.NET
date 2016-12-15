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
        private ISelectionOperatorConfiguration selectionOperatorConfiguration;
        private IGeneticEntityConfiguration entityConfiguration;
        private IPopulationConfiguration populationConfiguration;
        private IFitnessEvaluatorConfiguration fitnessEvaluatorConfiguration;
        private ComponentConfigurationCollection<IStatisticConfiguration> statisticConfigurations = new ComponentConfigurationCollection<IStatisticConfiguration>();
        private ComponentConfigurationCollection<IPluginConfiguration> pluginConfigurations = new ComponentConfigurationCollection<IPluginConfiguration>();
        private IGeneticAlgorithmConfiguration geneticAlgorithmConfiguration;

        /// <summary>
        /// Gets or sets the configuration of the <see cref="FitnessEvaluator"/> class that is
        /// used during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public IFitnessEvaluatorConfiguration FitnessEvaluator
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
        /// Gets the configuration for all the <see cref="IStatistic"/> classes that
        /// are used to calculate statistics during execution of the genetic algorithm.
        /// </summary>
        public ComponentConfigurationCollection<IStatisticConfiguration> Statistics
        {
            get { return this.statisticConfigurations; }
        }

        /// <summary>
        /// Gets the configuration for all the <see cref="IPlugin"/> classes that
        /// are used extend functionality.
        /// </summary>
        public ComponentConfigurationCollection<IPluginConfiguration> Plugins
        {
            get { return this.pluginConfigurations; }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="IGeneticEntity"/> class that is used 
        /// during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public IGeneticEntityConfiguration Entity
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
        public IPopulationConfiguration Population
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
        public IElitismStrategyConfiguration ElitismStrategy { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="FitnessScalingStrategy"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public IFitnessScalingStrategyConfiguration FitnessScalingStrategy { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="CrossoverOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public ICrossoverOperatorConfiguration CrossoverOperator { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="MutationOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public IMutationOperatorConfiguration MutationOperator { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="SelectionOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public ISelectionOperatorConfiguration SelectionOperator
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
        public ITerminatorConfiguration Terminator { get; set; }

        /// <summary>
        /// Gets or sets the configuration of <see cref="GeneticAlgorithm"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public IGeneticAlgorithmConfiguration GeneticAlgorithm
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

            this.CrossoverOperator = (ICrossoverOperatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.CrossoverOperator)]);
            this.ElitismStrategy = (IElitismStrategyConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.ElitismStrategy)]);
            this.entityConfiguration = (IGeneticEntityConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.Entity)]);
            this.fitnessEvaluatorConfiguration = (IFitnessEvaluatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.FitnessEvaluator)]);
            this.FitnessScalingStrategy = (IFitnessScalingStrategyConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.FitnessScalingStrategy)]);
            this.geneticAlgorithmConfiguration = (IGeneticAlgorithmConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.GeneticAlgorithm)]);
            this.MutationOperator = (IMutationOperatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.MutationOperator)]);
            this.populationConfiguration = (IPopulationConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.Population)]);
            this.selectionOperatorConfiguration = (ISelectionOperatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.SelectionOperator)]);
            this.Terminator = (ITerminatorConfiguration)RestoreComponentConfiguration((Dictionary<string, object>)state[nameof(this.Terminator)]);

            this.pluginConfigurations = new ComponentConfigurationCollection<IPluginConfiguration>();
            this.pluginConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Plugins)]).Select(d => RestoreComponentConfiguration(d)).Cast<IPluginConfiguration>());

            this.statisticConfigurations = new ComponentConfigurationCollection<IStatisticConfiguration>();
            this.statisticConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Statistics)]).Select(d => RestoreComponentConfiguration(d)).Cast<IStatisticConfiguration>());
        }

        private static KeyValueMap SaveComponentConfiguration(IComponentConfiguration configuration)
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
