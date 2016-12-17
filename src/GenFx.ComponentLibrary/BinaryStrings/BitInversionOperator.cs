using System;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// Provides the <see cref="BinaryStringEntity"/> with bit inversion operator support.
    /// </summary>
    /// <remarks>
    /// Bit inversion operates upon a binary string, causing two random bit positions to become swapped.
    /// </remarks>
    public sealed class BitInversionOperator : BitInversionOperator<BitInversionOperator, BitInversionOperatorConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public BitInversionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
