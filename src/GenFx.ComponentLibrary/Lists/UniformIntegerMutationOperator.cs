using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the <see cref="IIntegerListEntity"/> with uniform integer mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform integer mutation operates upon an integer list, causing each integer of the list to
    /// mutate if it meets a certain probability.
    /// </remarks>
    public sealed class UniformIntegerMutationOperator : UniformIntegerMutationOperator<UniformIntegerMutationOperator, UniformIntegerMutationOperatorConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public UniformIntegerMutationOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
