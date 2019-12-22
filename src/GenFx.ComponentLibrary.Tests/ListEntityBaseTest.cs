using GenFx.ComponentLibrary.Lists;
using GenFx.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ListEntityBase"/> class.
    /// </summary>
    public class ListEntityBaseTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.Add"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_Add()
        {
            TestEntity entity = new TestEntity();
            int index = entity.Add(2);
            Assert.Equal(0, index);
            Assert.Single(entity.InnerList);
            Assert.Equal(2, entity.InnerList[0]);

            index = entity.Add("test");
            Assert.Equal(1, index);
            Assert.Equal(2, entity.InnerList.Count);
            Assert.Equal(2, entity.InnerList[0]);
            Assert.Equal("test", entity.InnerList[1]);
        }

        /// <summary>
        /// Tests that an exception is thrown when the <see cref="ListEntityBase.Add"/> method is called for a fixed size list.
        /// </summary>
        [Fact]
        public void ListEntityBase_Add_FixedSize()
        {
            TestEntity entity = new TestEntity { IsFixedSize = true };
            Assert.Throws<NotSupportedException>(() => entity.Add(2));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.Clear"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_Clear()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(1, 5).Cast<object>());
            entity.Clear();
            Assert.Empty(entity.InnerList);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.Contains"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_Contains()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(1, 5).Cast<object>());
            Assert.True(entity.Contains(1));
            Assert.True(entity.Contains(3));
            Assert.True(entity.Contains(5));
            Assert.False(entity.Contains(6));
            Assert.False(entity.Contains("1"));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.GetInitialLength"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_GetInitialLength_DifferentStartingLengths()
        {
            TestEntity entity = new TestEntity
            {
                MinimumStartingLength = 2,
                MaximumStartingLength = 5
            };

            FakeRandomUtil random = new FakeRandomUtil();
            RandomNumberService.Instance = random;
            random.RandomValue = 4;

            int result = entity.GetInitialLength();
            Assert.Equal(4, result);

            Assert.Equal(entity.MinimumStartingLength, random.MinValue);
            Assert.Equal(entity.MaximumStartingLength + 1, random.MaxValue);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.GetInitialLength"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_GetInitialLength_SameStartingLengths()
        {
            TestEntity entity = new TestEntity
            {
                MinimumStartingLength = 5,
                MaximumStartingLength = 5
            };

            int result = entity.GetInitialLength();
            Assert.Equal(5, result);
        }

        /// <summary>
        /// Tests that no validation exception is thrown when the entity has valid starting lengths.
        /// </summary>
        [Fact]
        public void ListEntityBase_ValidStartingLengths()
        {
            TestEntity entity = new TestEntity
            {
                MinimumStartingLength = 1,
                MaximumStartingLength = 2
            };
            entity.Validate();
        }

        /// <summary>
        /// Tests that a validation exception is thrown when the entity has invalid starting lengths.
        /// </summary>
        [Fact]
        public void ListEntityBase_InvalidStartingLengths()
        {
            TestEntity entity = new TestEntity
            {
                MinimumStartingLength = 2,
                MaximumStartingLength = 1
            };
            Assert.Throws<ValidationException>(() => entity.Validate());
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.IndexOf"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_IndexOf()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<object>());
            Assert.Equal(0, entity.IndexOf(3));
            Assert.Equal(2, entity.IndexOf(5));
            Assert.Equal(-1, entity.IndexOf(2));
            Assert.Equal(-1, entity.IndexOf("3"));

            entity.InnerList.Add(3);
            Assert.Equal(0, entity.IndexOf(3));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.Insert"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_Insert()
        {
            TestEntity entity = new TestEntity();
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
        /// Tests that the <see cref="ListEntityBase.Remove"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_Remove()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<object>());
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
        /// Tests that the <see cref="ListEntityBase.RemoveAt"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_RemoveAt()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<object>());

            entity.RemoveAt(1);

            Assert.Equal(2, entity.InnerList.Count);
            Assert.Equal(3, entity.InnerList[0]);
            Assert.Equal(5, entity.InnerList[1]);

            Assert.Throws<ArgumentOutOfRangeException>(() => entity.RemoveAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => entity.RemoveAt(2));

            entity.RemoveAt(0);
            entity.RemoveAt(0);
            Assert.Empty(entity.InnerList);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection.CopyTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CopyTo()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<object>());

            int[] destination = new int[entity.InnerList.Count];
            ((ICollection)entity).CopyTo(destination, 0);
            Assert.Equal(new int[] { 3, 4, 5 }, destination);

            destination = new int[entity.InnerList.Count - 1];
            ((ICollection)entity).CopyTo(destination, 1);
            Assert.Equal(new int[] { 4, 5 }, destination);

            Assert.Throws<ArgumentNullException>(() => ((ICollection)entity).CopyTo(null, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((ICollection)entity).CopyTo(destination, 3));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((ICollection)entity).CopyTo(destination, -1));
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.GetEnumerator"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_GetEnumerator()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(3, 3).Cast<object>());

            List<object> list = new List<object>(entity.Cast<object>());
            Assert.Equal(new object[] { 3, 4, 5 }, list);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection.Count"/> property works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_ICollection_Count()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Range(1, 3).Cast<object>());
            Assert.Equal(3, ((ICollection)entity).Count);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.IsReadOnly"/> property works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_IsReadOnly()
        {
            TestEntity entity = new TestEntity();
            Assert.False(entity.IsReadOnly);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection.IsSynchronized"/> property works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_IsSynchronized()
        {
            TestEntity entity = new TestEntity();
            Assert.False(((ICollection)entity).IsSynchronized);
        }

        /// <summary>
        /// Tests that the <see cref="ICollection.SyncRoot"/> property works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_SyncRoot()
        {
            TestEntity entity = new TestEntity();
            Assert.Null(((ICollection)entity).SyncRoot);
        }

        /// <summary>
        /// Tests that the <see cref="IList.this"/> property works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_IList_Indexer()
        {
            TestEntity entity = new TestEntity();
            entity.InnerList.AddRange(Enumerable.Repeat<object>(null, 3));

            IList list = (IList)entity;
            list[0] = 1;
            Assert.Equal(new object[] { 1, null, null }, entity.InnerList);

            list[0] = 2;
            Assert.Equal(new object[] { 2, null, null }, entity.InnerList);

            list[1] = 2;
            Assert.Equal(new object[] { 2, 2, null }, entity.InnerList);

            list[2] = 3;
            Assert.Equal(new object[] { 2, 2, 3 }, entity.InnerList);

            Assert.Equal(2, list[0]);
            Assert.Equal(2, list[1]);
            Assert.Equal(3, list[2]);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CompareTo_Equal()
        {
            TestEntity entity1 = new TestEntity
            {
                1,
                null,
                2,
                null
            };

            TestEntity entity2 = new TestEntity
            {
                1,
                null,
                2,
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(0, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CompareTo_LessThan()
        {
            TestEntity entity1 = new TestEntity
            {
                1,
                null,
                1,
                null
            };

            TestEntity entity2 = new TestEntity
            {
                1,
                null,
                2,
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CompareTo_GreaterThan()
        {
            TestEntity entity1 = new TestEntity
            {
                1,
                null,
                3,
                null
            };

            TestEntity entity2 = new TestEntity
            {
                1,
                null,
                2,
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CompareTo_ShorterLength()
        {
            TestEntity entity1 = new TestEntity
            {
                1,
                null,
                2
            };

            TestEntity entity2 = new TestEntity
            {
                1,
                null,
                2,
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CompareTo_LongerLength()
        {
            TestEntity entity1 = new TestEntity
            {
                1,
                null,
                2,
                null,
                3
            };

            TestEntity entity2 = new TestEntity
            {
                1,
                null,
                2,
                null
            };

            int result = entity1.CompareTo(entity2);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that the <see cref="ListEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CompareTo_Null()
        {
            TestEntity entity1 = new TestEntity();
            int result = entity1.CompareTo((GeneticEntity)null);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid entity to <see cref="ListEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListEntityBase_CompareTo_InvalidEntity()
        {
            TestEntity entity1 = new TestEntity();
            Assert.Throws<ArgumentException>(() => entity1.CompareTo(new MockEntity()));
        }

        private class TestEntity : ListEntityBase
        {
            public List<object> InnerList = new List<object>();

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
                        this.InnerList.AddRange(Enumerable.Repeat<object>(null, value - this.InnerList.Count));
                    }
                    
                }
            }

            public override bool RequiresUniqueElementValues
            {
                get;
                set;
            }

            public override object GetValue(int index)
            {
                return this.InnerList[index];
            }

            public override void SetValue(int index, object value)
            {
                this.InnerList[index] = value;
            }

            public new int GetInitialLength()
            {
                return base.GetInitialLength();
            }
        }

        private class FakeRandomUtil : IRandomNumberService
        {
            public int RandomValue;
            public int MinValue;
            public int MaxValue;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetDouble()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                this.MinValue = minValue;
                this.MaxValue = maxValue;
                return this.RandomValue;
            }
        }
    }
}
