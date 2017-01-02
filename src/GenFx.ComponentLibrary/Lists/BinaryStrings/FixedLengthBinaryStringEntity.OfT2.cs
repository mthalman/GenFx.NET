using System;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Entity made up of a fixed-length string of bits.
    /// </summary>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the entity's configuration class.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public abstract class FixedLengthBinaryStringEntity<TEntity, TConfiguration> : BinaryStringEntity<TEntity, TConfiguration>
        where TEntity : FixedLengthBinaryStringEntity<TEntity, TConfiguration>
        where TConfiguration : FixedLengthBinaryStringEntityConfiguration<TConfiguration, TEntity>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FixedLengthBinaryStringEntity(IGeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Gets a value indicating whether the list is a fixed size.
        /// </summary>
        public override bool IsFixedSize { get { return true; } }

        /// <summary>
        /// Returns the initial length to use for the entity.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <returns>Initial length to use for the entity.</returns>
        private static int GetLength(IGeneticAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            FixedLengthBinaryStringEntityConfiguration<TConfiguration, TEntity> config = 
                (FixedLengthBinaryStringEntityConfiguration<TConfiguration, TEntity>)algorithm.ConfigurationSet.Entity;
            if (config != null)
            {
                return config.Length;
            }
            else
            {
                return 0;
            }
        }
    }
}
