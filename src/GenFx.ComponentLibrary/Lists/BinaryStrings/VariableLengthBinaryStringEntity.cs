using System;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Entity made up of a variable-length string of bits.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    public sealed class VariableLengthBinaryStringEntity : VariableLengthBinaryStringEntity<VariableLengthBinaryStringEntity, VariableLengthBinaryStringEntityConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public VariableLengthBinaryStringEntity(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
