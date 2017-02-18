using GenFx.ComponentLibrary.Lists;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Tests
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
            TestListEntity<int> entity = new TestListEntity<int>
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
            TestListEntity<int> entity = new TestListEntity<int>
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
            TestListEntity<int> entity = new TestListEntity<int>
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
        public void ListEntity_Serialization()
        {
            TestListEntity<string> entity = new TestListEntity<string>();
            entity.IsFixedSize = true;
            PrivateObject privObj = new PrivateObject(entity, new PrivateType(typeof(ListEntity<string>)));

            List<string> genes = new List<string> { "a", "b" };
            privObj.SetField("genes", genes);

            ListEntity<string> result = (ListEntity<string>)SerializationHelper.TestSerialization(entity, new Type[0]);

            Assert.AreEqual(entity.IsFixedSize, result.IsFixedSize);

            PrivateObject resultPrivObj = new PrivateObject(result, new PrivateType(typeof(ListEntity<string>)));
            List<string> resultGenes = (List<string>)resultPrivObj.GetField("genes");
            Assert.AreEqual(genes[0], resultGenes[0]);
            Assert.AreEqual(genes[1], resultGenes[1]);
        }

        /// <summary>
        /// Tests that an exception is thrown when attempting to access state on an uninitialized entity.
        /// </summary>
        [TestMethod]
        public void ListEntity_Uninitialized()
        {
            TestListEntity<int> entity = new TestListEntity<int>();
            AssertEx.Throws<InvalidOperationException>(() => { object x = entity.Length; });
            AssertEx.Throws<InvalidOperationException>(() => { object x = entity[0]; });
            AssertEx.Throws<InvalidOperationException>(() => { entity[0] = 1; });
            AssertEx.Throws<InvalidOperationException>(() =>
            {
                TestListEntity<int> entity2 = new TestListEntity<int>();
                entity.CopyTo(entity2);
            });
        }

        [DataContract]
        private class TestListEntity<T> : ListEntity<T>
        {
            public override bool RequiresUniqueElementValues
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
