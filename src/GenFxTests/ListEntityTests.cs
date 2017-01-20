using GenFx.ComponentLibrary.Lists;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ListEntity{T}"/> class.
    /// </summary>
    [TestClass]
    public class ListEntityTests
    {
        /// <summary>
        /// Tests that the length can be expanded to contain more items.
        /// </summary>
        [TestMethod]
        public void ListEntity_SetLengthToExpand()
        {
            ListEntity<int> entity = new ListEntity<int>
            {
                MinimumStartingLength = 2,
                MaximumStartingLength = 2,
            };

            entity.Initialize(new MockGeneticAlgorithm());

            Assert.AreEqual(2, entity.Length);

            entity.Length = 4;
            Assert.AreEqual(4, entity.Length);

            Assert.AreEqual(0, entity[2]);
            Assert.AreEqual(0, entity[3]);
        }

        /// <summary>
        /// Tests that the length can be contracted to decrease the number of items.
        /// </summary>
        [TestMethod]
        public void ListEntity_SetLengthToContract()
        {
            ListEntity<int> entity = new ListEntity<int>
            {
                MinimumStartingLength = 4,
                MaximumStartingLength = 4,
            };

            entity.Initialize(new MockGeneticAlgorithm());
            Assert.AreEqual(4, entity.Length);

            entity[0] = 999;
            Assert.AreEqual(999, entity[0]);

            entity.Length = 1;
            Assert.AreEqual(1, entity.Length);

            Assert.AreEqual(999, entity[0]);
        }

        /// <summary>
        /// Tests that an exception is thrown if the length is changed when the list is a fixed size.
        /// </summary>
        [TestMethod]
        public void ListEntity_ThrowsWhenLengthChangedOnFixedSizeList()
        {
            ListEntity<int> entity = new ListEntity<int>
            {
                MinimumStartingLength = 2,
                MaximumStartingLength = 2,
                IsFixedSize = true
            };

            entity.Initialize(new MockGeneticAlgorithm());

            Assert.AreEqual(2, entity.Length);

            AssertEx.Throws<ArgumentException>(() => entity.Length = 4);
        }
    }
}
