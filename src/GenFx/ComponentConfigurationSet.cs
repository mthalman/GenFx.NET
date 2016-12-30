using GenFx.ComponentModel;
using GenFx.Properties;
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
    public sealed class ComponentConfigurationSet
    {
        private ISelectionOperatorConfiguration selectionOperatorConfiguration;
        private IGeneticEntityConfiguration entityConfiguration;
        private IPopulationConfiguration populationConfiguration;
        private IFitnessEvaluatorConfiguration fitnessEvaluatorConfiguration;
        private HashSet<IStatisticConfiguration> statisticConfigurations = new HashSet<IStatisticConfiguration>(new ConfigurationComparer<IStatisticConfiguration>());
        private HashSet<IPluginConfiguration> pluginConfigurations = new HashSet<IPluginConfiguration>(new ConfigurationComparer<IPluginConfiguration>());
        private IGeneticAlgorithmConfiguration geneticAlgorithmConfiguration;
        private IElitismStrategyConfiguration elitismStrategyConfiguration;
        private IFitnessScalingStrategyConfiguration fitnessScalingStrategyConfiguration;
        private ICrossoverOperatorConfiguration crossoverOperatorConfiguration;
        private IMutationOperatorConfiguration mutationOperatorConfiguration;
        private ITerminatorConfiguration terminatorConfiguration = new DefaultTerminatorConfiguration();
        private bool isFrozen;

        // Mapping of component configuration properties to Validator objects as described by external components.
        private Dictionary<PropertyInfo, List<Validator>> externalValidationMapping;

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
        public ICollection<IStatisticConfiguration> Statistics
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
        public ICollection<IPluginConfiguration> Plugins
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
        public IGeneticEntityConfiguration Entity
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
        public IPopulationConfiguration Population
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
        public IElitismStrategyConfiguration ElitismStrategy
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
        public IFitnessScalingStrategyConfiguration FitnessScalingStrategy
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
        public ICrossoverOperatorConfiguration CrossoverOperator
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
        public IMutationOperatorConfiguration MutationOperator
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
        public ISelectionOperatorConfiguration SelectionOperator
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
        public ITerminatorConfiguration Terminator
        {
            get { return this.terminatorConfiguration; }
            set
            {
                this.EnsureNotFrozen();
                if (value == null)
                {
                    value = new DefaultTerminatorConfiguration();
                }

                this.terminatorConfiguration = value;
            }
        }

        /// <summary>
        /// Gets or sets the configuration of <see cref="GeneticAlgorithm"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public IGeneticAlgorithmConfiguration GeneticAlgorithm
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
            state[nameof(this.CrossoverOperator)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.CrossoverOperator);
            state[nameof(this.ElitismStrategy)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.ElitismStrategy);
            state[nameof(this.Entity)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.entityConfiguration);
            state[nameof(this.FitnessEvaluator)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.fitnessEvaluatorConfiguration);
            state[nameof(this.FitnessScalingStrategy)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.FitnessScalingStrategy);
            state[nameof(this.GeneticAlgorithm)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.geneticAlgorithmConfiguration);
            state[nameof(this.MutationOperator)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.MutationOperator);
            state[nameof(this.Population)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.populationConfiguration);
            state[nameof(this.SelectionOperator)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.selectionOperatorConfiguration);
            state[nameof(this.Terminator)] = ComponentConfigurationHelper.SaveComponentConfiguration(this.Terminator);

            state[nameof(this.Plugins)] = new KeyValueMapCollection(this.pluginConfigurations.Select(c => ComponentConfigurationHelper.SaveComponentConfiguration(c)));
            state[nameof(this.Statistics)] = new KeyValueMapCollection(this.statisticConfigurations.Select(c => ComponentConfigurationHelper.SaveComponentConfiguration(c)));

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

            this.CrossoverOperator = (ICrossoverOperatorConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.CrossoverOperator)]);
            this.ElitismStrategy = (IElitismStrategyConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.ElitismStrategy)]);
            this.entityConfiguration = (IGeneticEntityConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration((KeyValueMap)state[nameof(this.Entity)]);
            this.fitnessEvaluatorConfiguration = (IFitnessEvaluatorConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.FitnessEvaluator)]);
            this.FitnessScalingStrategy = (IFitnessScalingStrategyConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.FitnessScalingStrategy)]);
            this.geneticAlgorithmConfiguration = (IGeneticAlgorithmConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.GeneticAlgorithm)]);
            this.MutationOperator = (IMutationOperatorConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.MutationOperator)]);
            this.populationConfiguration = (IPopulationConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.Population)]);
            this.selectionOperatorConfiguration = (ISelectionOperatorConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.SelectionOperator)]);
            this.Terminator = (ITerminatorConfiguration)ComponentConfigurationHelper.RestoreComponentConfiguration(
                (KeyValueMap)state[nameof(this.Terminator)]);

            this.pluginConfigurations = new HashSet<IPluginConfiguration>();
            this.pluginConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Plugins)]).Select(d => ComponentConfigurationHelper.RestoreComponentConfiguration(d)).Cast<IPluginConfiguration>());

            this.statisticConfigurations = new HashSet<IStatisticConfiguration>();
            this.statisticConfigurations.AddRange(((KeyValueMapCollection)state[nameof(this.Statistics)]).Select(d => ComponentConfigurationHelper.RestoreComponentConfiguration(d)).Cast<IStatisticConfiguration>());

            this.isFrozen = (bool)state[nameof(this.isFrozen)];
        }

        internal void Freeze()
        {
            this.isFrozen = true;
        }

        private void EnsureNotFrozen()
        {
            if (this.isFrozen)
            {
                throw new InvalidOperationException(FwkResources.ErrorMsg_ComponentConfigurationSetIsFrozen);
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

            IComponentConfiguration componentConfig = component.Configuration;
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

            componentConfig.Validate();

            IEnumerable<PropertyInfo> properties = componentConfig.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(p => p.DeclaringType != typeof(ComponentConfiguration));
            foreach (PropertyInfo propertyInfo in properties)
            {
                // Check that the property is valid using the validators described by external components.
                List<Validator> externalValidators;
                if (externalValidationMapping.TryGetValue(propertyInfo, out externalValidators))
                {
                    object propValue = propertyInfo.GetValue(this, null);
                    foreach (Validator validator in externalValidators)
                    {
                        ComponentHelper.CheckValidation(validator, componentConfig.GetType().Name + Type.Delimiter + propertyInfo.Name, propValue, componentConfig);
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

            foreach (IStatisticConfiguration stat in this.Statistics)
            {
                this.CompileExternalValidatorMapping(stat);
            }

            foreach (IStatisticConfiguration plugin in this.Plugins)
            {
                this.CompileExternalValidatorMapping(plugin);
            }
        }

        internal ComponentConfigurationSet Clone()
        {
            ComponentConfigurationSet clone = new ComponentConfigurationSet();
            clone.crossoverOperatorConfiguration = this.crossoverOperatorConfiguration;
            clone.elitismStrategyConfiguration = this.elitismStrategyConfiguration;
            clone.entityConfiguration = this.entityConfiguration;
            clone.externalValidationMapping = this.externalValidationMapping;
            clone.fitnessEvaluatorConfiguration = this.fitnessEvaluatorConfiguration;
            clone.fitnessScalingStrategyConfiguration = this.fitnessScalingStrategyConfiguration;
            clone.geneticAlgorithmConfiguration = this.geneticAlgorithmConfiguration;
            clone.isFrozen = this.isFrozen;
            clone.mutationOperatorConfiguration = this.mutationOperatorConfiguration;
            clone.pluginConfigurations = new HashSet<IPluginConfiguration>(this.pluginConfigurations);
            clone.populationConfiguration = this.populationConfiguration;
            clone.selectionOperatorConfiguration = this.selectionOperatorConfiguration;
            clone.statisticConfigurations = new HashSet<IStatisticConfiguration>(this.statisticConfigurations);
            clone.terminatorConfiguration = this.terminatorConfiguration;

            return clone;
        }

        /// <summary>
        /// Compiles the mapping of component configuration properties to <see cref="Validator"/> objects as described by the specified component.
        /// </summary>
        /// <param name="componentConfiguration"><see cref="IComponentConfiguration"/> for the component to check whether it has defined validators for a configuration property.</param>
        private void CompileExternalValidatorMapping(IComponentConfiguration componentConfiguration)
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
            where T : IComponentConfiguration
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
