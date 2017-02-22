using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Base implementation of a list-based entity.
    /// </summary>
    /// <remarks>
    /// This class does not define the list structure.  It only defines the API.  Classes which derive from
    /// <see cref="ListEntityBase{TItem}"/> are responsible for the definition of the actual data structure
    /// representing the list.
    /// </remarks>
    /// <typeparam name="TItem">Type of the values contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [DataContract]
    public abstract class ListEntityBase<TItem> : ListEntityBase, IList<TItem>
        where TItem : IComparable
    {
        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        public abstract TItem this[int index]
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get.</param>
        /// <returns>The list element at the specified index.</returns>
        public sealed override object GetValue(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to set.</param>
        /// <param name="value">The value to set at the index.</param>
        public sealed override void SetValue(int index, object value)
        {
            if (value == null && default(TItem) != null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value != null && !(value is TItem))
            {
                throw new ArgumentException(nameof(value),
                    StringUtil.GetFormattedString(Resources.ErrorMsg_ListEntityBase_InvalidItemType, value.GetType(), typeof(TItem)));
            }
            this[index] = (TItem)value;
        }
        
        int ICollection<TItem>.Count { get { return this.Length; } }
        
        void ICollection<TItem>.Add(TItem value)
        {
            this.Add(value);
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
        
        void ICollection<TItem>.CopyTo(TItem[] array, int arrayIndex)
        {
            ((ICollection)this).CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Returns an enumerator that iterates the items in the list.
        /// </summary>
        /// <returns>An enumerator that iterates the items in the list.</returns>
        public new IEnumerator<TItem> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this[i];
            }
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
        
        /// <summary>
        /// Inserts an item to the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, TItem item)
        {
            ((IList)this).Insert(index, item);
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
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings:
        ///  * Less than zero: This object is less than <paramref name="other"/>.
        ///  * Zero: This object is equal to <paramref name="other"/>.
        ///  * Greater than zero: This object is greater than <paramref name="other"/>.
        ///  </returns>
        public override int CompareTo(GeneticEntity other)
        {
            if (other == null)
            {
                return 1;
            }

            ListEntityBase<TItem> listEntityBase = other as ListEntityBase<TItem>;
            if (listEntityBase == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ObjectIsWrongType, typeof(ListEntityBase<TItem>)), nameof(other));
            }

            return ComparisonHelper.CompareLists<TItem>(this, listEntityBase);
        }
    }
}
