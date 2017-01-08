using GenFx.Contracts;
using GenFx.Validation;
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
    public sealed class ComponentFactoryConfigSet
    {
        private ISelectionOperatorFactoryConfig selectionOperatorConfiguration;
        private IGeneticEntityFactoryConfig entityConfiguration;
        private IPopulationFactoryConfig populationConfiguration;
        private IFitnessEvaluatorFactoryConfig fitnessEvaluatorConfiguration;
        private HashSet<IStatisticFactoryConfig> statisticConfigurations = new HashSet<IStatisticFactoryConfig>(new ConfigurationComparer<IStatisticFactoryConfig>());
        private HashSet<IPluginFactoryConfig> pluginConfigurations = new HashSet<IPluginFactoryConfig>(new ConfigurationComparer<IPluginFactoryConfig>());
        private IGeneticAlgorithmFactoryConfig geneticAlgorithmConfiguration;
        private IElitismStrategyFactoryConfig elitismStrategyConfiguration;
        private IFitnessScalingStrategyFactoryConfig fitnessScalingStrategyConfiguration;
        private ICrossoverOperatorFactoryConfig crossoverOperatorConfiguration;
        private IMutationOperatorFactoryConfig mutationOperatorConfiguration;
        private ITerminatorFactoryConfig terminatorConfiguration = new DefaultTerminatorFactoryConfig();
        private bool isFrozen;

        // Mapping of component configuration properties to Validator objects as described by external components.
        private Dictionary<PropertyInfo, List<Validator>> externalValidationMapping;

        /// <summary>
        /// Gets or sets the configuration of the <see cref="FitnessEvaluator"/> class that is
        /// used during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public IFitnessEvaluatorFactoryConfig FitnessEvaluator
        {
            get { return this.fitnessEvaluatorConfiguration; }
            set
            {
                this.EnsureNotFrozen();
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
        public ICollection<IStatisticFactoryConfig> Statistics
        {
            get
            {
                if (this.isFrozen)
                {
                    return this.statisticConfigurations.AsReadOnly();
                }
                else
                {
                    return this.statisticConfigurations;
                }
            }
        }

        /// <summary>
        /// Gets the configuration for all the <see cref="IPlugin"/> classes that
        /// are used extend functionality.
        /// </summary>
        public ICollection<IPluginFactoryConfig> Plugins
        {
            get
            {
                if (this.isFrozen)
                {
                    return this.pluginConfigurations.AsReadOnly();
                }
                else
                {
                    return this.pluginConfigurations;
                }
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="IGeneticEntity"/> class that is used 
        /// during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public IGeneticEntityFactoryConfig Entity
        {
            get { return this.entityConfiguration; }
            set
            {
                this.EnsureNotFrozen();
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
        public IPopulationFactoryConfig Population
        {
            get { return this.populationConfiguration; }
            set
            {
                this.EnsureNotFrozen();
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
        public IElitismStrategyFactoryConfig ElitismStrategy
        {
            get { return this.elitismStrategyConfiguration; }
            set
            {
                this.EnsureNotFrozen();
                this.elitismStrategyConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="FitnessScalingStrategy"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public IFitnessScalingStrategyFactoryConfig FitnessScalingStrategy
        {
            get { return this.fitnessScalingStrategyConfiguration; }
            set
            {
                this.EnsureNotFrozen();
                this.fitnessScalingStrategyConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="CrossoverOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public ICrossoverOperatorFactoryConfig CrossoverOperator
        {
            get { return this.crossoverOperatorConfiguration; }
            set
            {
                this.EnsureNotFrozen();
                this.crossoverOperatorConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="MutationOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        public IMutationOperatorFactoryConfig MutationOperator
        {
            get { return this.mutationOperatorConfiguration; }
            set
            {
                this.EnsureNotFrozen();
                this.mutationOperatorConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of the <see cref="SelectionOperator"/> class that
        /// is used during execution of the genetic algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public ISelectionOperatorFactoryConfig SelectionOperator
        {
            get { return this.selectionOperatorConfiguration; }
            set
            {
                this.EnsureNotFrozen();
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
        public ITerminatorFactoryConfig Terminator
        {
            get { return this.terminatorConfiguration; }
            set
            {
                this.EnsureNotFrozen();
                if (value == null)
                {
                    value = new DefaultTerminatorFactoryConfig();
                }

                this.terminatorConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of <see cref="GeneticAlgorithm"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public IGeneticAlgorithmFactoryConfig GeneticAlgorithm
        {
            get { return this.geneticAlgorithmConfiguration; }
            set
            {
                this.EnsureNotFrozen();
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
            state[nameof(this.CrossoverOperator)] = this.CrossoverOperator.SaveState();
            state[nameof(this.ElitismStrategy)] = this.ElitismStrategy.SaveState();
            state[nameof(this.Entity)] = this.entityConfiguration.SaveState();
            state[nameof(this.FitnessEvaluator)] = this.fitnessEvaluatorConfiguration.SaveState();
            state[nameof(this.FitnessScalingStrategy)] = this.FitnessScalingStrategy.SaveState();
            state[nameof(this.GeneticAlgorithm)] = this.geneticAlgorithmConfiguration.SaveState();
            state[nameof(this.MutationOperator)] = this.MutationOperator.SaveState();
            state[nameof(this.Population)] = this.populationConfiguration.SaveState();
            state[nameof(this.SelectionOperator)] = this.selectionOperatorConfiguration.SaveState();
            state[nameof(this.Terminator)] = this.Terminator.SaveState();

            state[nameof(this.Plugins)] = new KeyValueMapCollection(this.pluginConfigurations.Select(c => c.SaveState()));
            state[nameof(this.Statistics)] = new KeyValueMapCollection(this.statisticConfigurations.Select(c => c.SaveState()));

            state[nameof(this.isFrozen)] = this.isFrozen;

            return state;
        }

        /// <summary>
        /// Restores the state of the configuration set.
        /// </summary>
        /// <param name="state">State from which to restore.</param>
        internal void RestoreState(KeyValueMap state)
        {
            this.EnsureNotFrozen();
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            this.CrossoverOperator = (ICrossoverOperatorFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.CrossoverOperator)]);
            this.ElitismStrategy = (IElitismStrategyFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.ElitismStrategy)]);
            this.entityConfiguration = (IGeneticEntityFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration((KeyValueMap)state[nameof(this.Entity)]);
            this.fitnessEvaluatorConfiguration = (IFitnessEvaluatorFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.FitnessEvaluator)]);
            this.FitnessScalingStrategy = (IFitnessScalingStrategyFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.FitnessScalingStrategy)]);
            this.geneticAlgorithmConfiguration = (IGeneticAlgorithmFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.GeneticAlgorithm)]);
            this.MutationOperator = (IMutationOperatorFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.MutationOperator)]);
            this.populationConfiguration = (IPopulationFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.Population)]);
            this.selectionOperatorConfiguration = (ISelectionOperatorFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.SelectionOperator)]);
            this.Terminator = (ITerminatorFactoryConfig)ComponentFactoryConfigExtensions.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.Terminator)]);

            this.pluginConfigurations = new HashSet<IPluginFactoryConfig>();
            this.pluginConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Plugins)]).Select(d => ComponentFactoryConfigExtensions.RestoreComponentConfiguration(d)).Cast<IPluginFactoryConfig>());

            this.statisticConfigurations = new HashSet<IStatisticFactoryConfig>();
            this.statisticConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Statistics)]).Select(d => ComponentFactoryConfigExtensions.RestoreComponentConfiguration(d)).Cast<IStatisticFactoryConfig>());

            this.isFrozen = (bool)state[nameof(this.isFrozen)];
        }

        internal void Freeze()
        {
            this.isFrozen = true;
        }

        /// <summary>
        /// Throws an exception if the object is currently frozen.
        /// </summary>
        private void EnsureNotFrozen()
        {
            if (this.isFrozen)
            {
                throw new InvalidOperationException(Resources.ErrorMsg_ComponentConfigurationSetIsFrozen);
            }
        }

        /// <summary>
        /// Validates the component.
        /// </summary>
        /// <param name="component">The <see cref="IGeneticComponent"/> to validate.</param>
        public void Validate(IGeneticComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            IComponentFactoryConfig componentConfig = component.Configuration;
            if (componentConfig == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_NoCorrespondingComponentConfiguration, component.GetType().FullName), nameof(component));
            }
            else if (componentConfig.ComponentType != component.GetType())
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_ComponentConfigurationTypeMismatch,
                  componentConfig.GetType().FullName, component.GetType().FullName), nameof(component));
            }

            componentConfig.Validate();

            IEnumerable<PropertyInfo> properties = componentConfig.GetType()
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
                        validator.EnsureIsValid(componentConfig.GetType().Name + Type.Delimiter + propertyInfo.Name, propValue, componentConfig);
                    }
                }
            }
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="Validator"/> objects as described by external components.
        /// </summary>
        internal void CompileExternalValidatorMapping()
        {
            this.externalValidationMapping = new Dictionary<PropertyInfo, List<Validator>>();
            this.CompileExternalValidatorMapping(this.CrossoverOperator);
            this.CompileExternalValidatorMapping(this.ElitismStrategy);
            this.CompileExternalValidatorMapping(this.Entity);
            this.CompileExternalValidatorMapping(this.FitnessEvaluator);
            this.CompileExternalValidatorMapping(this.FitnessScalingStrategy);
            this.CompileExternalValidatorMapping(this.GeneticAlgorithm);
            this.CompileExternalValidatorMapping(this.MutationOperator);
            this.CompileExternalValidatorMapping(this.Population);
            this.CompileExternalValidatorMapping(this.SelectionOperator);
            this.CompileExternalValidatorMapping(this.Terminator);

            foreach (IStatisticFactoryConfig stat in this.Statistics)
            {
                this.CompileExternalValidatorMapping(stat);
            }

            foreach (IStatisticFactoryConfig plugin in this.Plugins)
            {
                this.CompileExternalValidatorMapping(plugin);
            }
        }

        internal ComponentFactoryConfigSet Clone()
        {
            ComponentFactoryConfigSet clone = new ComponentFactoryConfigSet();
            clone.crossoverOperatorConfiguration = this.crossoverOperatorConfiguration;
            clone.elitismStrategyConfiguration = this.elitismStrategyConfiguration;
            clone.entityConfiguration = this.entityConfiguration;
            clone.externalValidationMapping = this.externalValidationMapping;
            clone.fitnessEvaluatorConfiguration = this.fitnessEvaluatorConfiguration;
            clone.fitnessScalingStrategyConfiguration = this.fitnessScalingStrategyConfiguration;
            clone.geneticAlgorithmConfiguration = this.geneticAlgorithmConfiguration;
            clone.isFrozen = this.isFrozen;
            clone.mutationOperatorConfiguration = this.mutationOperatorConfiguration;
            clone.pluginConfigurations = new HashSet<IPluginFactoryConfig>(this.pluginConfigurations);
            clone.populationConfiguration = this.populationConfiguration;
            clone.selectionOperatorConfiguration = this.selectionOperatorConfiguration;
            clone.statisticConfigurations = new HashSet<IStatisticFactoryConfig>(this.statisticConfigurations);
            clone.terminatorConfiguration = this.terminatorConfiguration;

            return clone;
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="Validator"/> objects as described by the specified component.
        /// </summary>
        /// <param name="componentConfiguration"><see cref="IComponentFactoryConfig"/> for the component to check whether it has defined validators for a configuration property.</param>
        private void CompileExternalValidatorMapping(IComponentFactoryConfig componentConfiguration)
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
                if (!this.externalValidationMapping.TryGetValue(prop, out validators))
                {
                    validators = new List<Validator>();
                    this.externalValidationMapping.Add(prop, validators);
                }
                validators.Add(attrib.Validator);
            }
        }

        private class ConfigurationComparer<T> : IEqualityComparer<T>
            where T : IComponentFactoryConfig
        {
            public bool Equals(T x, T y)
            {
                return x.ComponentType == y.ComponentType;
            }

            public int GetHashCode(T obj)
            {
                return obj.ComponentType.GetHashCode();
            }
        }
    }
}
