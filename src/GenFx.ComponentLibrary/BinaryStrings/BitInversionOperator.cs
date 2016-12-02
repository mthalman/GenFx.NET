using System;
using System.ComponentModel;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// Provides the <see cref="BinaryStringEntity"/> with bit inversion operator support.
    /// </summary>
    /// <remarks>
    /// Bit inversion operates upon a binary string, causing two random bit positions to become swapped.
    /// </remarks>
    [RequiredEntity(typeof(BinaryStringEntity))]
    public class BitInversionOperator : InversionOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitInversionOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="BitInversionOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public BitInversionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="BitInversionOperator"/>.
    /// </summary>
    [Component(typeof(BitInversionOperator))]
    public class BitInversionOperatorConfiguration : InversionOperatorConfiguration
    {
    }
}
