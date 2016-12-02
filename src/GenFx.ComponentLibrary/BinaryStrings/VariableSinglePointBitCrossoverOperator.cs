using System;
using System.ComponentModel;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// Provides the <see cref="VariableLengthBinaryStringEntity"/> with variable single-point bit crossover support.
    /// </summary>
    /// <remarks>
    /// Variable single-point bit crossover chooses a bit position -- potentially different -- within both of the 
    /// <see cref="VariableLengthBinaryStringEntity"/> objects and swaps the bits on either side of those
    /// points.  For example, if
    /// two <see cref="VariableLengthBinaryStringEntity"/> objects represented by 00110101 and 100011 were to
    /// be crossed over at position 2 in the first entity and position 4 in the second entity, the resulting offspring
    /// would be 0011 and 1000110101.
    /// </remarks>
    [RequiredEntity(typeof(VariableLengthBinaryStringEntity))]
    public class VariableSinglePointBitCrossoverOperator : VariableSinglePointCrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableSinglePointBitCrossoverOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="VariableSinglePointBitCrossoverOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public VariableSinglePointBitCrossoverOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="VariableSinglePointBitCrossoverOperator"/>.
    /// </summary>
    [Component(typeof(VariableSinglePointBitCrossoverOperator))]
    public class VariableSinglePointBitCrossoverOperatorConfiguration : VariableSinglePointCrossoverOperatorConfiguration
    {
    }
}
