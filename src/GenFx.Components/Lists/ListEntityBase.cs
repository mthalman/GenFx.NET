using GenFx.Validation;
using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace GenFx.Components.Lists
{
    /// <summary>
    /// Base implementation of a list-based entity.
    /// </summary>
    /// <remarks>
    /// This class does not define the list structure.  It only defines the API.  Classes which derive from
    /// <see cref="ListEntityBase"/> are responsible for the definition of the actual data structure
    /// representing the list.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [DataContract]
    [CustomComponentValidator(typeof(ListStartingLengthValidator))]
    public abstract class ListEntityBase : GeneticEntity, IList
    {
        private const int DefaultMaxLength = 20;
        private const int DefaultMinLength = 5;

        [DataMember]
        private string representation;

        [DataMember]
        private int minimumStartingLength = DefaultMinLength;

        [DataMember]
        private int maximumStartingLength = DefaultMaxLength;

        /// <summary>
        /// Gets or sets the maximum starting length a new entity can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int MaximumStartingLength
        {
            get { return this.maximumStartingLength; }
            set { this.SetProperty(ref this.maximumStartingLength, value); }
        }

        /// <summary>
        /// Gets or sets the minimum starting length a new entity can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int MinimumStartingLength
        {
            get { return this.minimumStartingLength; }
            set { this.SetProperty(ref this.minimumStartingLength, value); }
        }

        /// <summary>
        /// When overriden by a derived class, gets or sets a value indicating whether the list is a fixed size.
        /// </summary>
        public abstract bool IsFixedSize { get; set; }

        /// <summary>
        /// When override by a derived class, gets or sets a value indicating whether each of the element values should be unique for the entity.
        /// </summary>
        public abstract bool RequiresUniqueElementValues { get; set; }

        /// <summary>
        /// Returns the list string as a <see cref="String"/>.
        /// </summary>
        public override string Representation { get { return this.representation; } }

        /// <summary>
        /// Gets or sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get or set.</param>
        object IList.this[int index]
        {
            get { return this.GetValue(index); }
            set { this.SetValue(index, value); }
        }

        /// <summary>
        /// Returns the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to get.</param>
        /// <returns>The list element at the specified index.</returns>
        public abstract object GetValue(int index);

        /// <summary>
        /// Sets the list element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the list element to set.</param>
        /// <param name="value">The value to set at the index.</param>
        public abstract void SetValue(int index, object value);
        
        /// <summary>
        /// When overriden by a derived class, gets or sets the length of the list.
        /// </summary>
        public abstract int Length { get; set; }

        int ICollection.Count { get { return this.Length; } }

        /// <summary>
        /// Gets a value indicating whether the list is read-only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        bool ICollection.IsSynchronized { get { return false; } }

        object ICollection.SyncRoot { get { return null; } }

        /// <summary>
        /// Returns the initial length to use for the list.
        /// </summary>
        /// <returns>The initial length to use for the list.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected int GetInitialLength()
        {
            if (this.MinimumStartingLength == this.MaximumStartingLength)
            {
                return this.MinimumStartingLength;
            }

            return RandomNumberService.Instance.GetRandomValue(this.MinimumStartingLength, this.MaximumStartingLength + 1);
        }

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

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            this.Length = 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether the specified value is contained in the list.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>True if the value is contained in the list; otherwise, false.</returns>
        public bool Contains(object value)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if (Object.Equals(this.GetValue(i), value))
                {
                    return true;
                }
            }

            return false;
        }
        
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index >= this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            for (int i = index; i < this.Length; i++)
            {
                array.SetValue(this.GetValue(i), i - index);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates the items in the list.
        /// </summary>
        /// <returns>An enumerator that iterates the items in the list.</returns>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this.GetValue(i);
            }
        }

        /// <summary>
        /// Determines the index of a specified value in the list.
        /// </summary>
        /// <param name="value">The value to locate in the list.</param>
        /// <returns>The index of the item if it exists; otherwise, returns -1.</returns>
        public int IndexOf(object value)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if (Object.Equals(this.GetValue(i), value))
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// Inserts the value at the specified index.
        /// </summary>
        /// <param name="index">Index within the list where the value should be inserted.</param>
        /// <param name="value">The value to be inserted.</param>
        public void Insert(int index, object value)
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
                    this.SetValue(i + 1, this.GetValue(i));
                }

                ((IList)this)[index] = value;
            }
        }
        
        /// <summary>
        /// Removes the specified value from the list.
        /// </summary>
        /// <param name="value">Value to be removed.</param>
        public void Remove(object value)
        {
            int index = ((IList)this).IndexOf(value);
            if (index < 0)
            {
                return;
            }

            ((IList)this).RemoveAt(index);
        }
        
        /// <summary>
        /// Removes the value at the specified index within the list.
        /// </summary>
        /// <param name="index">Index position of the value to be removed.</param>
        public void RemoveAt(int index)
        {
            if (index >= this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            // Move all the items one position down and remove the last item by reducing the length
            for (int i = index; i < this.Length - 1; i++)
            {
                this.SetValue(i, this.GetValue(i + 1));
            }

            this.Length--;
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

            ListEntityBase listEntityBase = other as ListEntityBase;
            if (listEntityBase == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ObjectIsWrongType, typeof(ListEntityBase)), nameof(other));
            }

            return ComparisonHelper.CompareLists(this, listEntityBase);
        }

        /// <summary>
        /// Stores a cached string representation of the <see cref="ListEntityBase{TItem}"/>.
        /// </summary>
        protected void UpdateStringRepresentation()
        {
            this.representation = this.CalculateStringRepresentation();
        }

        /// <summary>
        /// Calculates the string representation of the <see cref="ListEntityBase{TItem}"/>.
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

                builder.Append(this.GetValue(i));
            }

            return builder.ToString();
        }
    }
}
