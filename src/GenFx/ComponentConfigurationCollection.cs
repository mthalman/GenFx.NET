using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenFx.ComponentModel;
using GenFx.Properties;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GenFx
{
    /// <summary>
    /// Represents a collection of <see cref="IComponentConfiguration"/> objects.
    /// </summary>
    /// <remarks>This collection can only contain unique types of <see cref="IComponentConfiguration"/>.</remarks>
    public class ComponentConfigurationCollection<T> : IList<T>
        where T : IComponentConfiguration
    {
        private List<T> configs = new List<T>();
        private Dictionary<Type, T> configsByType = new Dictionary<Type, T>();

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence 
        /// within the entire list.
        /// </summary>
        /// <param name="item">The object to locate in the list. The value can be null for reference types.</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire list, if found; otherwise, –1.</returns>
        public int IndexOf(T item)
        {
            return this.configs.IndexOf(item);
        }

        /// <summary>
        /// Inserts an element into the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        public void Insert(int index, T item)
        {
            this.AddConfig(item, "item");
            this.configs.Insert(index, item);
        }

        /// <summary>
        /// Removes the element at the specified index of the list.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            T item = this.configs[index];
            if (item != null)
            {
                this.configsByType.Remove(item.GetType());
                this.configs.Remove(item);
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index]
        {
            get { return this.configs[index]; }
            set
            {
                this.RemoveAt(index);
                this.AddConfig(value, "value");
                this.configs[index] = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> that corresponds to the specified type.
        /// </summary>
        /// <param name="type"><see cref="Type"/> of the <see cref="GeneticComponent"/> corresponding to the <see cref="ComponentConfiguration"/> to return.</param>
        /// <returns><see cref="ComponentConfiguration"/> that corresponds to the specified type. Returns null if there is no corresponding type.</returns>
        [SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers")]
        public T this[Type type]
        {
            get
            {
                T config;
                if (this.configsByType.TryGetValue(type, out config))
                {
                    return config;
                }
                else
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="item">The object to be added to the end of the list. The value can be null for reference types.</param>
        public void Add(T item)
        {
            this.AddConfig(item, "item");
            this.configs.Add(item);
        }

        /// <summary>
        /// Removes all elements from the list.
        /// </summary>
        public void Clear()
        {
            this.configsByType.Clear();
            this.configs.Clear();
        }

        /// <summary>
        /// Determines whether an element is in the list.
        /// </summary>
        /// <param name="item">The object to locate in the list. The value can be null for reference types.</param>
        /// <returns>true if item is found in the list; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return this.configs.Contains(item);
        }

        /// <summary>
        /// Copies the entire list to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from list. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.configs.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements actually contained in the list.
        /// </summary>
        public int Count
        {
            get { return this.configs.Count; }
        }

        /// <summary>
        /// Gets whether the list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the list.
        /// </summary>
        /// <param name="item">The object to remove from the list. The value can be null for reference types.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the list.</returns>
        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (this.configsByType.ContainsKey(item.GetType()))
            {
                this.configsByType.Remove(item.GetType());
            }
            return this.configs.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An enumerator for the listt.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.configs.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An enumerator for the listt.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.configs.GetEnumerator();
        }

        /// <summary>
        /// Verifies that the <see cref="ComponentConfiguration"/> object does not exist in the collection
        /// and adds it.
        /// </summary>
        /// <param name="config"><see cref="ComponentConfiguration"/> to add.</param>
        /// <param name="paramName">Name of <see cref="ComponentConfiguration"/> parameter in calling method.</param>
        private void AddConfig(T config, string paramName)
        {
            if (config == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (this.configsByType.ContainsKey(config.ComponentType))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  FwkResources.ErrorMsg_DuplicateConfiguration, paramName));
            }

            this.configsByType.Add(config.ComponentType, config);
        }
    }
}
