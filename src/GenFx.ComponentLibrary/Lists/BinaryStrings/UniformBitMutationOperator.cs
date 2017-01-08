using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Provides bit list entities with uniform bit mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform bit mutation operates upon a binary string, causing each bit of the string to
    /// mutate if it meets a certain probability.
    /// </remarks>
    public sealed class UniformBitMutationOperator : UniformBitMutationOperator<UniformBitMutationOperator, UniformBitMutationOperatorFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public UniformBitMutationOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
