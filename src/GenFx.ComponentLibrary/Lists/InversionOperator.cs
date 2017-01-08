using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides a <see cref="IListEntityBase"/> with inversion operator support.
    /// </summary>
    /// <remarks>
    /// Inversion operates upon a list, causing the values of two list positions to become swapped.
    /// </remarks>
    public sealed class InversionOperator : InversionOperator<InversionOperator, InversionOperatorFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public InversionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
