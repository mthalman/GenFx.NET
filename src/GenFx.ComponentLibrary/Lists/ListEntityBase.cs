using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Base implementation of a list-based entity.
    /// </summary>
    /// <remarks>
    /// This class does not define the list structure.  It only defines the API.  Classes which derive from
    /// <see cref="ListEntityBase{TEntity, TConfiguration, TItem}"/> are responsible for the definition of the actual data structure
    /// representing the list.
    /// </remarks>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the entity's configuration class.</typeparam>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class ListEntityBase<TEntity, TConfiguration, TItem> : GeneticEntity<TEntity, TConfiguration>, IListEntityBase, IListEntityBase<TItem>
        where TEntity : ListEntityBase<TEntity, TConfiguration, TItem>
        where TConfiguration : ListEntityBaseFactoryConfig<TConfiguration, TEntity, TItem>
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
        /// Returns the list string as a <see cref="String"/>.
        /// </summary>
        public override string Representation { get { return this.representation; } }

        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public abstract TItem this[int index]
        {
            get;
            set;
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set
            {
                if (!(value is TItem))
                {
                    throw new ArgumentException(StringUtil.GetFormattedString(Resources.ErrorMsg_ListEntityBase_InvalidItemType, value?.GetType(), typeof(TItem)), nameof(value));
                }

                this[index] = (TItem)value;
            }
        }

        /// <summary>
        /// When overriden by a derived class, gets or sets the length of the list.
        /// </summary>
        public abstract int Length { get; set; }

        int ICollection.Count { get { return this.Length; } }

        int ICollection<TItem>.Count { get { return this.Length; } }

        /// <summary>
        /// When overriden by a derived class, gets a value indicating whether the list is a fixed size.
        /// </summary>
        public abstract bool IsFixedSize { get; }

        /// <summary>
        /// Gets a value indicating whether the list is read-only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        bool ICollection.IsSynchronized { get { return false; } }

        object ICollection.SyncRoot { get { return null; } }

        /// <summary>
        /// Adds the value to the end of the list.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <returns>Index position where the value was added.</returns>
        public int Add(object value)
        {
            if (IsFixedSize)
            {
                throw new NotSupportedException(StringUtil.GetFormattedString(Resources.ErrorMsg_ListEntityBase_FixedSize));
            }

            int index = this.Length++;
            ((IList)this)[index] = value;
            return index;
        }

        void ICollection<TItem>.Add(TItem value)
        {
            this.Add(value);
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            this.Length = 0;
        }
        
        bool IList.Contains(object value)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if ((object)this[i] == value)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether the specified item is contained in the list.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the value exists; otherwise, false.</returns>
        public bool Contains(TItem item)
        {
            return ((IList)this).Contains(item);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            for (int i = index; i < this.Length; i++)
            {
                array.SetValue(this[i], i);
            }
        }

        void ICollection<TItem>.CopyTo(TItem[] array, int arrayIndex)
        {
            ((ICollection)this).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TItem>)this).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates the items in the list.
        /// </summary>
        /// <returns>An enumerator that iterates the items in the list.</returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this[i];
            }
        }

        int IList.IndexOf(object value)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if ((object)this[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Determines the index of a specified item in the list.
        /// </summary>
        /// <param name="item">The item to locate in the list.</param>
        /// <returns>The index of the item if it exists; otherwise, returns -1.</returns>
        public int IndexOf(TItem item)
        {
            return ((IList)this).IndexOf(item);
        }

        void IList.Insert(int index, object value)
        {
            if (index > this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index == this.Length)
            {
                ((IList)this).Add(value);
                return;
            }
            else
            {
                this.Length++;

                // Move the position all items at and past the index by one
                for (int i = this.Length - 2; i >= index; i--)
                {
                    this[i + 1] = this[i];
                }

                ((IList)this)[index] = value;
            }
        }

        /// <summary>
        /// Inserts an item to the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, TItem item)
        {
            ((IList)this).Insert(index, item);
        }

        void IList.Remove(object value)
        {
            ((IList)this).RemoveAt(((IList)this).IndexOf(value));
        }

        /// <summary>
        /// Removes the specified item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>true if the item was found and removed; otherwise, false.</returns>
        public bool Remove(TItem item)
        {
            IList list = (IList)this;
            int count = list.Count;
            list.Remove(item);
            return list.Count != count;
        }

        /// <summary>
        /// Removes the value at the specified index within the list.
        /// </summary>
        /// <param name="index">Index position of the value to be removed.</param>
        public void RemoveAt(int index)
        {
            // Move all the items one position down and remove the last item by reducing the length
            for (int i = index; i < this.Length - 1; i++)
            {
                this[i] = this[i + 1];
            }

            this.Length--;
        }

        /// <summary>
        /// Stores a cached string representation of the <see cref="IListEntityBase"/>.
        /// </summary>
        protected void UpdateStringRepresentation()
        {
            this.representation = this.CalculateStringRepresentation();
        }

        /// <summary>
        /// Calculates the string representation of the <see cref="IListEntityBase"/>.
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
