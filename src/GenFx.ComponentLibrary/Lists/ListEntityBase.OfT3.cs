using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Base implementation of a list-based entity.
    /// </summary>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the entity's configuration class.</typeparam>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    public abstract class ListEntityBase<TEntity, TConfiguration, TItem> : ListEntityBase<TEntity, TConfiguration>
        where TEntity : ListEntityBase<TEntity, TConfiguration, TItem>
        where TConfiguration : ListEntityBaseConfiguration<TConfiguration, TEntity, TItem>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected ListEntityBase(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public new abstract TItem this[int index]
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the list element at the specified position.
        /// </summary>
        /// <param name="index">Position of the element to return.</param>
        /// <returns>The list element.</returns>
        protected override sealed object GetListElement(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Sets the list element at the specified position.
        /// </summary>
        /// <param name="index">Position of the element to set.</param>
        /// <param name="value">Value to set the element to.</param>
        protected override sealed void SetListElement(int index, object value)
        {
            this[index] = (TItem)value;
        }
    }
}
