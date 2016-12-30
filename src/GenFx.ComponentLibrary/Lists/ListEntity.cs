using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Default implementation of a list-based entity.
    /// </summary>
    /// <remarks>This class uses a <see cref="List{TItem}"/> data structure to represent the list.</remarks>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the entity's configuration class.</typeparam>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class ListEntity<TEntity, TConfiguration, TItem> : ListEntityBase<TEntity, TConfiguration, TItem>, IListEntity<TItem>
        where TEntity : ListEntity<TEntity, TConfiguration, TItem>
        where TConfiguration : ListEntityConfiguration<TConfiguration, TEntity, TItem>
    {
        private List<TItem> genes;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <param name="initialLength">Initial length of the list.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialLength"/> is less than zero.</exception>
        protected ListEntity(IGeneticAlgorithm algorithm, int initialLength)
            : base(algorithm)
        {
            this.genes = new List<TItem>(initialLength);

            for (int i = 0; i < initialLength; i++)
            {
                this.genes.Add(default(TItem));
            }
        }

        /// <summary>
        /// Gets or sets the length of the list.
        /// </summary>
        /// <remarks>
        /// By default, the length of this object cannot be changed
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
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public override TItem this[int index]
        {
            get { return this.genes[index]; }
            set
            {
                this.genes[index] = value;
                this.UpdateStringRepresentation();
            }
        }

        /// <summary>
        /// Copies the state from this object to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="ListEntity{TEntity, TConfiguration, TItem}"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(TEntity entity)
        {
            base.CopyTo(entity);

            TItem[] values = new TItem[this.genes.Count];
            this.genes.CopyTo(values);
            entity.genes = values.ToList();
            entity.UpdateStringRepresentation();
        }

        /// <summary>
        /// Restores the entity's state.
        /// </summary>
        public override void RestoreState(KeyValueMap state)
        {
            base.RestoreState(state);

            this.genes = (List<TItem>)state[nameof(this.genes)];
        }

        /// <summary>
        /// Saves the entity's state.
        /// </summary>
        public override void SetSaveState(KeyValueMap state)
        {
            base.SetSaveState(state);

            state[nameof(this.genes)] = this.genes;
        }
    }
}
