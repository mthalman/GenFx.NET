using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by incorporating the population's fitness variance to 
    /// derive a preprocessed fitness prior to scaling.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The sigma scaling algorithm is based on the one defined by Goldberg (1989).
    /// </para>
    /// </remarks>
    public sealed class SigmaScalingStrategy : SigmaScalingStrategy<SigmaScalingStrategy, SigmaScalingStrategyFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public SigmaScalingStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
