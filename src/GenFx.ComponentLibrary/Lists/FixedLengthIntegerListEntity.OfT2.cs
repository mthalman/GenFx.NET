using GenFx.ComponentLibrary.Base;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Entity made up of a fixed-length list of integers.
    /// </summary>
    public abstract class FixedLengthIntegerListEntity<TEntity, TConfiguration> : IntegerListEntity<TEntity, TConfiguration>
        where TEntity : FixedLengthIntegerListEntity<TEntity, TConfiguration>
        where TConfiguration : FixedLengthIntegerListEntityConfiguration<TConfiguration, TEntity>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FixedLengthIntegerListEntity(IGeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Returns the initial length to use for this object.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <returns>Initial length to use for this object.</returns>
        private static int GetLength(IGeneticAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            return ((FixedLengthIntegerListEntityConfiguration<TConfiguration, TEntity>)algorithm.ConfigurationSet.Entity).Length;
        }

        /// <summary>
        /// Returns a clone of this object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public override GeneticEntity<TEntity, TConfiguration> Clone()
        {
            FixedLengthIntegerListEntity<TEntity, TConfiguration> binaryEntity = new FixedLengthIntegerListEntity<TEntity, TConfiguration>(this.Algorithm);
            this.CopyTo(binaryEntity);
            return binaryEntity;
        }
    }
}
