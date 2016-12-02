using System;
using System.ComponentModel;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the <see cref="IntegerListEntity"/> with bit inversion operator support.
    /// </summary>
    /// <remarks>
    /// Inversion operates upon an integer list, causing two random integer positions to become swapped.
    /// </remarks>
    [RequiredEntity(typeof(IntegerListEntity))]
    public class IntegerInversionOperator : InversionOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerInversionOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="IntegerInversionOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public IntegerInversionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="IntegerInversionOperator"/>.
    /// </summary>
    [Component(typeof(IntegerInversionOperator))]
    public class IntegerInversionOperatorConfiguration : InversionOperatorConfiguration
    {
    }
}
