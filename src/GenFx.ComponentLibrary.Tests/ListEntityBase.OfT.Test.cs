using GenFx.ComponentLibrary.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ListEntityBase{TItem}"/> class.
    /// </summary>
    [TestClass]
    public class ListEntityBaseOfTTest
    {
        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.Contains"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_Contains()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(1, 5));

            Assert.IsTrue(entity.Contains(1));
            Assert.IsTrue(entity.Contains(5));
            Assert.IsFalse(entity.Contains(0));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.IndexOf"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_IndexOf()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(2, 3));

            Assert.AreEqual(-1, entity.IndexOf(1));
            Assert.AreEqual(0, entity.IndexOf(2));
            Assert.AreEqual(2, entity.IndexOf(4));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.Insert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_Insert()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.Insert(0, 10);
            entity.Insert(1, 20);
            entity.Insert(0, 5);

            Assert.AreEqual(5, entity.InnerList[0]);
            Assert.AreEqual(10, entity.InnerList[1]);
            Assert.AreEqual(20, entity.InnerList[2]);

            AssertEx.Throws<ArgumentOutOfRangeException>(() => entity.Insert(5, 1));
            AssertEx.Throws<ArgumentOutOfRangeException>(() => entity.Insert(-1, 1));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.Remove"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_Remove()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());
            entity.InnerList.Add(3);

            entity.Remove(3);

            Assert.AreEqual(3, entity.InnerList.Count);
            Assert.AreEqual(4, entity.InnerList[0]);
            Assert.AreEqual(5, entity.InnerList[1]);
            Assert.AreEqual(3, entity.InnerList[2]);

            entity.Remove(0);
            entity.Remove("test");
            Assert.AreEqual(3, entity.InnerList.Count);

            entity.Remove(3);
            entity.Remove(4);
            entity.Remove(5);
            Assert.AreEqual(0, entity.InnerList.Count);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.SetValue"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_SetValue()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());

            entity.SetValue(2, 1);
            Assert.AreEqual(1, entity.InnerList[2]);

            AssertEx.Throws<ArgumentOutOfRangeException>(() => entity.SetValue(3, 1));
            AssertEx.Throws<ArgumentNullException>(() => entity.SetValue(0, null));
            AssertEx.Throws<ArgumentException>(() => entity.SetValue(0, "test"));
        }

        /// <summary>
        /// Tests that the <see cref="ICollection{T}.Add"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_ICollection_Add()
        {
            TestEntity<int> entity = new TestEntity<int>();
            ((ICollection<int>)entity).Add(10);
            CollectionAssert.AreEqual(new int[] { 10 }, entity.InnerList);
            ((ICollection<int>)entity).Add(20);
            CollectionAssert.AreEqual(new int[] { 10, 20 }, entity.InnerList);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection{T}.CopyTo"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_ICollection_CopyTo()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());

            int[] array = new int[entity.InnerList.Count];
            ((ICollection<int>)entity).CopyTo(array, 0);

            CollectionAssert.AreEqual(new int[] { 3, 4, 5 }, array);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection{T}.Count"/> property works correctly.
        /// </summary>
        [TestMethod]
        public void ListEntityBaseOfT_ICollection_Count()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());

            Assert.AreEqual(3, ((ICollection<int>)entity).Count);
        }

        private class TestEntity<T> : ListEntityBase<T>
        {
            public List<T> InnerList = new List<T>();

            public override T this[int index]
            {
                get { return this.InnerList[index]; }
                set { this.InnerList[index] = value; }
            }

            public override bool IsFixedSize
            {
                get;
                set;
            }

            public override int Length
            {
                get { return this.InnerList.Count; }
                set
                {
                    if (value < this.InnerList.Count)
                    {
                        this.InnerList.RemoveRange(value, this.InnerList.Count - value);
                    }
                    else if (value > this.InnerList.Count)
                    {
                        this.InnerList.AddRange(Enumerable.Repeat<T>(default(T), value - this.InnerList.Count));
                    }

                }
            }

            public override bool RequiresUniqueElementValues
            {
                get;
                set;
            }
        }
    }
}
