using GenFx.ComponentLibrary.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Base implementation of a list-based entity.
    /// </summary>
    public abstract class ListEntityBase<TEntity, TConfiguration> : GeneticEntity<TEntity, TConfiguration>
        where TEntity : ListEntityBase<TEntity, TConfiguration>
        where TConfiguration : ListEntityBaseConfiguration<TConfiguration, TEntity>
    {
        private string representation;

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
        /// Gets or sets the length of the list.
        /// </summary>
        public abstract int Length
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public object this[int index]
        {
            get { return this.GetListElement(index); }
            set { this.SetListElement(index, value); }
        }

        /// <summary>
        /// Returns the list string as a <see cref="String"/>.
        /// </summary>
        public override string Representation
        {
            get { return this.representation; }
        }

        /// <summary>
        /// Returns the list element at the specified position.
        /// </summary>
        /// <param name="index">Position of the element to return.</param>
        /// <returns>The list element.</returns>
        protected abstract object GetListElement(int index);

        /// <summary>
        /// Sets the list element at the specified position.
        /// </summary>
        /// <param name="index">Position of the element to set.</param>
        /// <param name="value">Value to set the element to.</param>
        protected abstract void SetListElement(int index, object value);

        /// <summary>
        /// Stores a cached string representation of this object.
        /// </summary>
        protected void UpdateStringRepresentation()
        {
            this.representation = this.CalculateStringRepresentation();
        }

        /// <summary>
        /// Calculates the string representation of this object.
        /// </summary>
        /// <returns>The string representation.</returns>
        protected virtual string CalculateStringRepresentation()
        {
            StringBuilder builder = new StringBuilder(this.Length);
            for (int i = 0; i < this.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(", ");
                }

                builder.Append(this[i]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Copies the state from this object to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="ListEntityBase{TEntity, TConfiguration}"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity<TEntity, TConfiguration> entity)
        {
            base.CopyTo(entity);

            ListEntityBase<TEntity, TConfiguration> listEntity = (ListEntityBase<TEntity, TConfiguration>)entity;
            listEntity.representation = this.representation;
        }

        /// <summary>
        /// Returns an array of the entity's gene values.
        /// </summary>
        /// <returns>An array of the entity's gene values.</returns>
        public IList<object> ToList()
        {
            List<object> list = new List<object>(this.Length);
            for (int i = 0; i < this.Length; i++)
            {
                list.Add(this[i]);
            }

            return list;
        }

        /// <summary>
        /// Restores the entity's state.
        /// </summary>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.representation = (string)state[nameof(this.representation)];
        }

        /// <summary>
        /// Saves the entity's state.
        /// </summary>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.representation)] = this.representation;
        }
    }
}
