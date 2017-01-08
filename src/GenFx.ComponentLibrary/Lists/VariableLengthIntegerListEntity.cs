using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Entity made up of a variable-length list of integers.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    public sealed class VariableLengthIntegerListEntity : VariableLengthIntegerListEntity<VariableLengthIntegerListEntity, VariableLengthIntegerListEntityFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public VariableLengthIntegerListEntity(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
