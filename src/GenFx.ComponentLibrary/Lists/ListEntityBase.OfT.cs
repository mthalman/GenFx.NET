using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Base implementation of a list-based entity.
    /// </summary>
    /// <typeparam name="T">Type of the values contained in the list.</typeparam>
    public abstract class ListEntityBase<T> : ListEntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListEntityBase{T}"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="ListEntityBase{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected ListEntityBase(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public new abstract T this[int index]
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
            this[index] = (T)value;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="ListEntityBase{T}"/>.
    /// </summary>
    public abstract class ListEntityBaseConfiguration<T> : ListEntityBaseConfiguration
    {
    }
}
