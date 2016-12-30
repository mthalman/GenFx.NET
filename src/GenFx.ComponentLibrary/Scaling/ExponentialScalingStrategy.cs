using System;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by raising the fitness of a <see cref="IGeneticEntity"/> to the power of the
    /// value of the <see cref="ExponentialScalingStrategyConfiguration{TConfiguration, TScaling}.ScalingPower"/> property.
    /// </summary>
    public sealed class ExponentialScalingStrategy : ExponentialScalingStrategy<ExponentialScalingStrategy, ExponentialScalingStrategyConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public ExponentialScalingStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
