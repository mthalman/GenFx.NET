using System;
using System.Collections;
using System.Collections.Generic;
using TestCommon.Helpers;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ComparisonHelper"/> class.
    /// </summary>
    public class ComparisonHelperTest
    {
        /// <summary>
        /// Tests that the <see cref="ComparisonHelper.CompareLists(IList, IList)"/> method works correctly.
        /// </summary>
        [Fact]
        public void ComparisonHelper_CompareLists()
        {
            TestCompareLists(0, new List<object> { 4, 2, 5 }, new List<object> { 4, 2, 5 });
            TestCompareLists(-1, new List<object> { 4, 1, 5 }, new List<object> { 4, 2, 5 });
            TestCompareLists(1, new List<object> { 4, 2, 6 }, new List<object> { 4, 2, 5 });
            TestCompareLists(0, new List<object> { null, 2, 5 }, new List<object> { null, 2, 5 });
            TestCompareLists(-1, new List<object> { null, 2, 5 }, new List<object> { 0, 2, 5 });
            TestCompareLists(1, new List<object> { 0, 2, 5 }, new List<object> { null, 2, 5 });
            TestCompareLists(1, new List<object> { 4, 2, 5, 6 }, new List<object> { 4, 2, 5 });
            TestCompareLists(-1, new List<object> { 4, 2, 5 }, new List<object> { 4, 2, 5, 6 });
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a non-IComparable object to <see cref="ComparisonHelper.CompareLists(IList, IList)"/>.
        /// </summary>
        [Fact]
        public void ComparisonHelper_CompareLists_NonIComparable()
        {
            Assert.Throws<InvalidOperationException>(
                () => ComparisonHelper.CompareLists(
                    (IList)new List<object> { new ComparisonHelperTest() }, new List<object> { 2 }));

            Assert.Throws<InvalidOperationException>(
                () => ComparisonHelper.CompareLists(
                    (IList)new List<object> { 1 }, new List<object> { new ComparisonHelperTest() }));
        }

        /// <summary>
        /// Tests that the <see cref="ComparisonHelper.CompareLists{T}(IList{T}, IList{T})"/> method works correctly.
        /// </summary>
        [Fact]
        public void ComparisonHelper_CompareListsOfT()
        {
            TestCompareListsOfT(0, new List<string> { "4", "2", "5" }, new List<string> { "4", "2", "5" });
            TestCompareListsOfT(-1, new List<string> { "4", "1", "5" }, new List<string> { "4", "2", "5" });
            TestCompareListsOfT(1, new List<string> { "4", "2", "6" }, new List<string> { "4", "2", "5" });
            TestCompareListsOfT(0, new List<string> { null, "2", "5" }, new List<string> { null, "2", "5" });
            TestCompareListsOfT(-1, new List<string> { null, "2", "5" }, new List<string> { "0", "2", "5" });
            TestCompareListsOfT(1, new List<string> { "0", "2", "5" }, new List<string> { null, "2", "5" });
            TestCompareListsOfT(1, new List<string> { "4", "2", "5", "6" }, new List<string> { "4", "2", "5" });
            TestCompareListsOfT(-1, new List<string> { "4", "2", "5" }, new List<string> { "4", "2", "5", "6" });
        }

        /// <summary>
        /// Tests that the <see cref="ComparisonHelper.CompareObjects"/> method works correctly.
        /// </summary>
        [Fact]
        public void ComparisonHelper_CompareObjects()
        {
            Assert.Equal(0, ComparisonHelper.CompareObjects(null, null));
            Assert.Equal(-1, ComparisonHelper.CompareObjects(null, 1));
            Assert.Equal(1, ComparisonHelper.CompareObjects(1, null));
            Assert.Equal(0, ComparisonHelper.CompareObjects(1, 1));
            Assert.Equal(-1, ComparisonHelper.CompareObjects(1, 2));
            Assert.Equal(1, ComparisonHelper.CompareObjects(2, 1));
        }

        private static void TestCompareLists(int expectedValue, IList list1, IList list2)
        {
            int result = ComparisonHelper.CompareLists(list1, list2);
            Assert.Equal(expectedValue, result);
        }

        private static void TestCompareListsOfT<T>(int expectedValue, List<T> list1, List<T> list2)
            where T : IComparable
        {
            int result = ComparisonHelper.CompareLists<T>(list1, list2);
            Assert.Equal(expectedValue, result);
        }
    }
}
