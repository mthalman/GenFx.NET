using System;
using System.ComponentModel;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// Provides the <see cref="VariableLengthIntegerListEntity"/> with variable single-point bit crossover support.
    /// </summary>
    /// <remarks>
    /// Variable single-point bit crossover chooses a bit position -- potentially different -- within both of the 
    /// <see cref="VariableLengthIntegerListEntity"/> objects and swaps the bits on either side of those
    /// points.  For example, if
    /// two <see cref="VariableLengthIntegerListEntity"/> objects represented by 1,2,3,4 and 5,6,7,8,9 were to
    /// be crossed over at position 2 in the first entity and position 4 in the second entity, the resulting offspring
    /// would be 1,2,9 and 5,6,7,8,3,4.
    /// </remarks>
    [RequiredEntity(typeof(VariableLengthIntegerListEntity))]
    public class VariableSinglePointListElementCrossoverOperator : VariableSinglePointCrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableSinglePointListElementCrossoverOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="VariableSinglePointListElementCrossoverOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public VariableSinglePointListElementCrossoverOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="VariableSinglePointListElementCrossoverOperator"/>.
    /// </summary>
    [Component(typeof(VariableSinglePointListElementCrossoverOperator))]
    public class VariableSinglePointListElementCrossoverOperatorConfiguration : VariableSinglePointCrossoverOperatorConfiguration
    {
    }
}
