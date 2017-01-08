using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary
{
    /// <summary>
    /// Represents a customizable component within the framework that is associated with a <see cref="IGeneticAlgorithm"/>.
    /// </summary>
    /// <typeparam name="TComponent">Type of the deriving component class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class GeneticComponentWithAlgorithm<TComponent, TConfiguration> : GeneticComponent<TComponent, TConfiguration>
        where TComponent : GeneticComponentWithAlgorithm<TComponent, TConfiguration>
        where TConfiguration : ComponentFactoryConfig<TConfiguration, TComponent>
    {
        /// <summary>
        /// Gets the <see cref="IGeneticAlgorithm"/>.
        /// </summary>
        protected IGeneticAlgorithm Algorithm { get; private set; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm">The <see cref="IGeneticAlgorithm"/> this component is associated with.</param>
        /// <param name="configuration">The <typeparamref name="TConfiguration"/> object that provides the configuration for this component.</param>
        protected GeneticComponentWithAlgorithm(IGeneticAlgorithm algorithm, TConfiguration configuration)
            : base(configuration)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            this.Algorithm = algorithm;
            
            this.Algorithm.ConfigurationSet.Validate(this);
        }

        internal void SetAlgorithm(IGeneticAlgorithm algorithm)
        {
            this.Algorithm = algorithm;
        }

        internal static TConfiguration GetConfiguration(IGeneticAlgorithm algorithm, Func<ComponentFactoryConfigSet, IComponentFactoryConfig> getConfiguration)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException("algorithm");
            }

            IComponentFactoryConfig config = getConfiguration(algorithm.ConfigurationSet);

            if (!(config is TConfiguration))
            {
                throw new InvalidOperationException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_MissingComponentConfiguration,
                  typeof(TComponent).FullName));
            }

            return (TConfiguration)config;
        }
    }
}
