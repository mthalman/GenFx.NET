using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Operates upon a <see cref="IListEntityBase"/> by shifting a random segment of the list to the left or right by one position.
    /// </summary>
    public sealed class ListShiftMutationOperator : ListShiftMutationOperator<ListShiftMutationOperator, ListShiftMutationOperatorConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public ListShiftMutationOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
