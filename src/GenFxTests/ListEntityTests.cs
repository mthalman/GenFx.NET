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

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void Serialization()
        {
            ListEntity<string> entity = new ListEntity<string>();
            entity.IsFixedSize = true;
            PrivateObject privObj = new PrivateObject(entity);

            List<string> genes = new List<string> { "a", "b" };
            privObj.SetField("genes", genes);

            ListEntity<string> result = (ListEntity<string>)SerializationHelper.TestSerialization(entity, new Type[0]);

            Assert.AreEqual(entity.IsFixedSize, result.IsFixedSize);

            PrivateObject resultPrivObj = new PrivateObject(result);
            List<string> resultGenes = (List<string>)resultPrivObj.GetField("genes");
            Assert.AreEqual(genes[0], resultGenes[0]);
            Assert.AreEqual(genes[1], resultGenes[1]);
        }
    }
}
