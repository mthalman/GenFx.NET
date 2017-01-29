using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Default implementation of a list-based entity.
    /// </summary>
    /// <remarks>This class uses a <see cref="List{TItem}"/> data structure to represent the list.</remarks>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [DataContract]
    public class ListEntity<TItem> : ListEntityBase<TItem>
    {
        [DataMember]
        private bool isFixedSize;

        [DataMember]
        private List<TItem> genes;

        /// <summary>
        /// Gets or sets a value indicating whether the list is a fixed size.
        /// </summary>
        [ConfigurationProperty]
        public override bool IsFixedSize
        {
            get { return this.isFixedSize; }
            set { this.SetProperty(ref this.isFixedSize, value); }
        }

        /// <summary>
        /// Gets or sets the length of the integer list.
        /// </summary>
        /// <remarks>
        /// The length of this entity can be changed from its initial value.  The list will be truncated
        /// if the value is less than the current length. The list will be expanded with zeroes if the
        /// value is greater than the current length.
        /// </remarks>
        public override int Length
        {
            get
            {
                this.EnsureEntityIsInitialized();
                return this.genes.Count;
            }
            set
            {
                if (value != this.Length)
                {
                    if (this.IsFixedSize)
                    {
                        throw new ArgumentException(Resources.ErrorMsg_ListEntityLengthCannotBeChanged, nameof(value));
                    }

                    if (value > this.Length)
                    {
                        for (int i = 0; i <= value - this.Length; i++)
                        {
                            this.genes.Add(default(TItem));
                        }
                    }
                    else
                    {
                        int currentLength = this.Length;
                        for (int i = 0; i < currentLength - value; i++)
                        {
                            this.genes.RemoveAt(this.Length - 1);
                        }
                    }

                    this.UpdateStringRepresentation();
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
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(GeneticAlgorithm algorithm)
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
        
        private void EnsureEntityIsInitialized()
        {
            if (this.genes == null)
            {
                throw new InvalidOperationException(Resources.ErrorMsg_EntityNotInitialized);
            }
        }
    }
}
