using System;
using System.Collections.Generic;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="CollectionExtensions"/> class.
    /// </summary>
    public class CollectionExtensionsTest
    {
        /// <summary>
        /// Tests that an exception is thrown when passing a null source collection.
        /// </summary>
        [Fact]
        public void CollectionExtensions_AddRange_NullSourceCollection()
        {
            Assert.Throws<ArgumentNullException>(() => CollectionExtensions.AddRange<int>(null, new int[0]));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null items collection.
        /// </summary>
        [Fact]
        public void CollectionExtensions_AddRange_NullItemsCollection()
        {
            Assert.Throws<ArgumentNullException>(() => CollectionExtensions.AddRange<int>(new List<int>(), null));
        }

        /// <summary>
        /// Tests that no items are added when an empty items collection is provided.
        /// </summary>
        [Fact]
        public void CollectionExtensions_AddRange_EmptyItemsCollection()
        {
            List<int> list = new List<int>
            {
                1, 2
            };

            CollectionExtensions.AddRange(list, new int[0]);

            Assert.Equal(2, list.Count);
        }

        /// <summary>
        /// Tests that items are added to the source collection.
        /// </summary>
        [Fact]
        public void CollectionExtensions_AddRange_ItemsAdded()
        {
            List<int> list = new List<int>
            {
                1, 2
            };

            CollectionExtensions.AddRange(list, new int[] { 3, 4 });

            Assert.Equal(4, list.Count);
            Assert.Equal(3, list[2]);
            Assert.Equal(4, list[3]);
        }
    }
}
