using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="CollectionExtensions"/> class.
    /// </summary>
    [TestClass]
    public class CollectionExtensionsTest
    {
        /// <summary>
        /// Tests that an exception is thrown when passing a null source collection.
        /// </summary>
        [TestMethod]
        public void CollectionExtensions_AddRange_NullSourceCollection()
        {
            AssertEx.Throws<ArgumentNullException>(() => CollectionExtensions.AddRange<int>(null, new int[0]));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null items collection.
        /// </summary>
        [TestMethod]
        public void CollectionExtensions_AddRange_NullItemsCollection()
        {
            AssertEx.Throws<ArgumentNullException>(() => CollectionExtensions.AddRange<int>(new List<int>(), null));
        }

        /// <summary>
        /// Tests that no items are added when an empty items collection is provided.
        /// </summary>
        [TestMethod]
        public void CollectionExtensions_AddRange_EmptyItemsCollection()
        {
            List<int> list = new List<int>
            {
                1, 2
            };

            CollectionExtensions.AddRange(list, new int[0]);

            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// Tests that items are added to the source collection.
        /// </summary>
        [TestMethod]
        public void CollectionExtensions_AddRange_ItemsAdded()
        {
            List<int> list = new List<int>
            {
                1, 2
            };

            CollectionExtensions.AddRange(list, new int[] { 3, 4 });

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(3, list[2]);
            Assert.AreEqual(4, list[3]);
        }
    }
}
