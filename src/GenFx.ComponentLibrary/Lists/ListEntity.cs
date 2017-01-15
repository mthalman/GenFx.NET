using GenFx.ComponentLibrary.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using GenFx.Contracts;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Default implementation of a list-based entity.
    /// </summary>
    /// <remarks>This class uses a <see cref="List{TItem}"/> data structure to represent the list.</remarks>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public abstract class ListEntity<TItem> : ListEntityBase<TItem>, IListEntity<TItem>
    {
        private List<TItem> genes;
        
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
            get
            {
                this.EnsureEntityIsInitialized();
                return this.genes.Count;
            }
            set
            {
                this.EnsureEntityIsInitialized();
                if (value != this.genes.Count)
                {
                    throw new ArgumentException(Resources.ErrorMsg_ListEntityLengthCannotBeChanged, nameof(value));
                }
            }
        }

        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public override TItem this[int index]
        {
            get
            {
                this.EnsureEntityIsInitialized();
                return this.genes[index];
            }
            set
            {
                this.EnsureEntityIsInitialized();
                this.genes[index] = value;
                this.UpdateStringRepresentation();
            }
        }

        /// <summary>
        /// Returns the initial length to use for the list.
        /// </summary>
        /// <returns>The initial length to use for the list.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected abstract int GetInitialLength();

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(IGeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

            int initialCount = this.GetInitialLength();
            this.genes = new List<TItem>(initialCount);

            for (int i = 0; i < initialCount; i++)
            {
                this.genes.Add(default(TItem));
            }
        }

        /// <summary>
        /// Copies the state from this object to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            this.EnsureEntityIsInitialized();
            base.CopyTo(entity);

            ListEntity<TItem> listEntity = (ListEntity<TItem>)entity;
            TItem[] values = new TItem[this.genes.Count];
            this.genes.CopyTo(values);
            listEntity.genes = values.ToList();
            listEntity.UpdateStringRepresentation();
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

        private void EnsureEntityIsInitialized()
        {
            if (this.genes == null)
            {
                throw new InvalidOperationException(Resources.ErrorMsg_EntityNotInitialized);
            }
        }
    }
}
