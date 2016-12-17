using System;
using System.Collections.Generic;

namespace GenFx
{
    /// <summary>
    /// Contains extension methods for <see cref="ICollection{T}"/>.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a set of items to the collection.
        /// </summary>
        /// <typeparam name="T">Type of the items contained in the collection.</typeparam>
        /// <param name="collection">The collection to add the items to.</param>
        /// <param name="items">The items to be added.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (T item in items)
            {
                collection.Add(item);
            }
        }
    }
}
