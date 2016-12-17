using GenFx.ComponentModel;
using System;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents a customizable component within the framework that is associated with a <see cref="IGeneticAlgorithm"/>.
    /// </summary>
    public abstract class GeneticComponentWithAlgorithm<TComponent, TConfiguration> : GeneticComponent<TComponent, TConfiguration>
        where TComponent : GeneticComponent<TComponent, TConfiguration>
        where TConfiguration : ComponentConfiguration<TConfiguration, TComponent>
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
    }
}
