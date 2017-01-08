using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby all <see cref="IGeneticEntity"/> objects have an equal
    /// probability of being selected regardless of fitness.
    /// </summary>
    public sealed class UniformSelectionOperator : UniformSelectionOperator<UniformSelectionOperator, UniformSelectionOperatorFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public UniformSelectionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
