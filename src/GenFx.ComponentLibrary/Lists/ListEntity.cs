using System;
using System.Collections.Generic;
using System.Linq;
using GenFx.ComponentLibrary.Properties;
using System.Text;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Default implementation of a list-based entity.
    /// </summary>
    /// <typeparam name="T">Type of the values contained in the list.</typeparam>
    public abstract class ListEntity<T> : ListEntityBase<T>
    {
        private List<T> genes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListEntity{T}"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="ListEntity{T}"/>.</param>
        /// <param name="initialLength">Initial length of the list.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialLength"/> is less than zero.</exception>
        protected ListEntity(GeneticAlgorithm algorithm, int initialLength)
            : base(algorithm)
        {
            this.genes = new List<T>(initialLength);

            for (int i = 0; i < initialLength; i++)
            {
                this.genes.Add(default(T));
            }
        }

        /// <summary>
        /// Gets or sets the length of the list.
        /// </summary>
        /// <remarks>
        /// By default, the length of a <see cref="ListEntity{T}"/> cannot be changed
        /// from its initial value unless the derived class overrides this behavior.
        /// </remarks>
        /// <exception cref="ArgumentException">Value is different from the current length.</exception>
        public override int Length
        {
            get { return this.genes.Count; }
            set
            {
                if (value != this.genes.Count)
                {
                    throw new ArgumentException(LibResources.ErrorMsg_ListEntityLengthCannotBeChanged, nameof(value));
                }
            }
        }

        /// <summary>
        /// Gets the list containing the values.
        /// </summary>
        protected IList<T> Genes
        {
            get { return this.genes; }
        }

        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public override T this[int index]
        {
            get { return this.genes[index]; }
            set
            {
                this.genes[index] = value;
                this.UpdateStringRepresentation();
            }
        }

        /// <summary>
        /// Copies the state from this <see cref="ListEntity{T}"/> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="ListEntity{T}"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            base.CopyTo(entity);

            ListEntity<T> listEntity = (ListEntity<T>)entity;
            T[] values = new T[this.genes.Count];
            this.genes.CopyTo(values);
            listEntity.genes = values.ToList();
        }

        /// <summary>
        /// Restores the entity's state.
        /// </summary>
        protected override void RestoreState(KeyValueMap state)
        {
            base.RestoreState(state);

            this.genes = (List<T>)state[nameof(this.genes)];
        }

        /// <summary>
        /// Saves the entity's state.
        /// </summary>
        protected override void SetSaveState(KeyValueMap state)
        {
            base.SetSaveState(state);

            state[nameof(this.genes)] = this.genes;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="ListEntity{T}"/>.
    /// </summary>
    public abstract class ListEntityConfiguration<T> : ListEntityBaseConfiguration<T>
    {
    }
}
