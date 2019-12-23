using GenFx.Components.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ListEntityBase{TItem}"/> class.
    /// </summary>
    public class ListEntityBaseOfTTest
    {
        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.Contains"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_Contains()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(1, 5));

            Assert.Contains(1, entity);
            Assert.Contains(5, entity);
            Assert.DoesNotContain(0, entity);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.IndexOf"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_IndexOf()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(2, 3));

            Assert.Equal(-1, entity.IndexOf(1));
            Assert.Equal(0, entity.IndexOf(2));
            Assert.Equal(2, entity.IndexOf(4));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.Insert"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_Insert()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.Insert(0, 10);
            entity.Insert(1, 20);
            entity.Insert(0, 5);

            Assert.Equal(5, entity.InnerList[0]);
            Assert.Equal(10, entity.InnerList[1]);
            Assert.Equal(20, entity.InnerList[2]);

            Assert.Throws<ArgumentOutOfRangeException>(() => entity.Insert(5, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => entity.Insert(-1, 1));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.Remove"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_Remove()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());
            entity.InnerList.Add(3);

            entity.Remove(3);

            Assert.Equal(3, entity.InnerList.Count);
            Assert.Equal(4, entity.InnerList[0]);
            Assert.Equal(5, entity.InnerList[1]);
            Assert.Equal(3, entity.InnerList[2]);

            entity.Remove(0);
            entity.Remove("test");
            Assert.Equal(3, entity.InnerList.Count);

            entity.Remove(3);
            entity.Remove(4);
            entity.Remove(5);
            Assert.Empty(entity.InnerList);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.SetValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_SetValue()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());

            entity.SetValue(2, 1);
            Assert.Equal(1, entity.InnerList[2]);

            Assert.Throws<ArgumentOutOfRangeException>(() => entity.SetValue(3, 1));
            Assert.Throws<ArgumentNullException>(() => entity.SetValue(0, null));
            Assert.Throws<ArgumentException>(() => entity.SetValue(0, "test"));
        }

        /// <summary>
        /// Tests that the <see cref="ICollection{T}.Add"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_ICollection_Add()
        {
            TestEntity<int> entity = new TestEntity<int>();
            ((ICollection<int>)entity).Add(10);
            Assert.Equal(new int[] { 10 }, entity.InnerList);
            ((ICollection<int>)entity).Add(20);
            Assert.Equal(new int[] { 10, 20 }, entity.InnerList);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection{T}.CopyTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_ICollection_CopyTo()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());

            int[] array = new int[entity.InnerList.Count];
            ((ICollection<int>)entity).CopyTo(array, 0);

            Assert.Equal(new int[] { 3, 4, 5 }, array);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection{T}.Count"/> property works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_ICollection_Count()
        {
            TestEntity<int> entity = new TestEntity<int>();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<int>());

            Assert.Equal(3, ((ICollection<int>)entity).Count);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_CompareTo_Equal()
        {
            TestEntity<string> entity1 = new TestEntity<string>
            {
                "a",
                null,
                "b",
                null
            };

            TestEntity<string> entity2 = new TestEntity<string>
            {
                "a",
                null,
                "b",
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(0, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_CompareTo_LessThan()
        {
            TestEntity<string> entity1 = new TestEntity<string>
            {
                "a",
                null,
                "a",
                null
            };

            TestEntity<string> entity2 = new TestEntity<string>
            {
                "a",
                null,
                "b",
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_CompareTo_GreaterThan()
        {
            TestEntity<string> entity1 = new TestEntity<string>
            {
                "a",
                null,
                "c",
                null
            };

            TestEntity<string> entity2 = new TestEntity<string>
            {
                "a",
                null,
                "b",
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_CompareTo_ShorterLength()
        {
            TestEntity<string> entity1 = new TestEntity<string>
            {
                "a",
                null,
                "b"
            };

            TestEntity<string> entity2 = new TestEntity<string>
            {
                "a",
                null,
                "b",
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_CompareTo_LongerLength()
        {
            TestEntity<string> entity1 = new TestEntity<string>
            {
                "a",
                null,
                "b",
                null,
                "c"
            };

            TestEntity<string> entity2 = new TestEntity<string>
            {
                "a",
                null,
                "b",
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase{TItem}.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_CompareTo_Null()
        {
            TestEntity<string> entity1 = new TestEntity<string>();
            int result = entity1.CompareTo((GeneticEntity)null);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid entity to <see cref="ListEntityBase{TItem}.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBaseOfT_CompareTo_InvalidEntity()
        {
            TestEntity<string> entity1 = new TestEntity<string>();
            Assert.Throws<ArgumentException>(() => entity1.CompareTo(new MockEntity()));
        }

        private class TestEntity<T> : ListEntityBase<T>
            where T : IComparable
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
                        this.InnerList.AddRange(Enumerable.Repeat<T>(default, value - this.InnerList.Count));
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
