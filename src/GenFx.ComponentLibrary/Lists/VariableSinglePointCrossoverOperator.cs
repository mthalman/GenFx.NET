using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the variable length types of <see cref="IListEntityBase"/> with variable single-point crossover support.
    /// </summary>
    /// <remarks>
    /// Variable single-point crossover chooses an element position -- potentially different -- within both of the 
    /// <see cref="IListEntityBase"/> objects and swaps the elements on either side of those
    /// points.  For example, if
    /// two <see cref="IListEntityBase"/> objects represented by 00110101 and 100011 were to
    /// be crossed over at position 2 in the first entity and position 4 in the second entity, the resulting offspring
    /// would be 0011 and 1000110101.
    /// </remarks>
    public sealed class VariableSinglePointCrossoverOperator : VariableSinglePointCrossoverOperator<VariableSinglePointCrossoverOperator, VariableSinglePointCrossoverOperatorConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public VariableSinglePointCrossoverOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
