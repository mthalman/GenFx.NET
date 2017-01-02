using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Entity made up of a fixed-length list of integers.
    /// </summary>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the entity's configuration class.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
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
        /// Gets a value indicating whether the list is a fixed size.
        /// </summary>
        public override bool IsFixedSize { get { return true; } }
        
    }
}
