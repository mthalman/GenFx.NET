using GenFx.ComponentLibrary.Base;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Entity made up of a fixed-length list of integers.
    /// </summary>
    public sealed class FixedLengthIntegerListEntity : FixedLengthIntegerListEntity<FixedLengthIntegerListEntity, FixedLengthIntegerListEntityConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public FixedLengthIntegerListEntity(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Returns a clone of this object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public override GeneticEntity<FixedLengthIntegerListEntity, FixedLengthIntegerListEntityConfiguration> Clone()
        {
            FixedLengthIntegerListEntity entity = new FixedLengthIntegerListEntity(this.Algorithm);
            this.CopyTo(entity);
            return entity;
        }
    }
}
