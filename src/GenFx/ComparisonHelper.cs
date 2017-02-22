using System;
using System.Collections;
using System.Collections.Generic;

namespace GenFx
{
    /// <summary>
    /// Contains helper methods related to comparing objects.
    /// </summary>
    internal static class ComparisonHelper
    {
        /// <summary>
        /// Compares two lists.
        /// </summary>
        /// <typeparam name="T">Type of the items contained in the lists.</typeparam>
        /// <param name="list1">List to be compared.</param>
        /// <param name="list2">List to be compared.</param>
        /// <returns>
        /// A value that indicates the relative order of the lists being compared. The
        /// return value has the following meanings:
        ///  * Less than zero: <paramref name="list1"/> is less than <paramref name="list2"/>.
        ///  * Zero: <paramref name="list1"/> is equal to <paramref name="list2"/>.
        ///  * Greater than zero: <paramref name="list1"/> is greater than <paramref name="list2"/>.
        ///  </returns>
        public static int CompareLists<T>(IList<T> list1, IList<T> list2)
            where T : IComparable
        {
            for (int i = 0; i < Math.Min(list1.Count, list2.Count); i++)
            {
                T item1 = list1[i];
                T item2 = list2[i];

                int compareResult = ComparisonHelper.CompareObjects(item1, item2);
                if (compareResult != 0)
                {
                    return compareResult;
                }
            }

            if (list1.Count == list2.Count)
            {
                return 0;
            }

            // If we get here, then all list items have been equal up to the point where the two lists
            // have the same length.  Now, whichever list has more elements is considered to be greater
            // than the other.
            return list1.Count > list2.Count ? 1 : -1;
        }

        /// <summary>
        /// Compares two lists.
        /// </summary>
        /// <param name="list1">List to be compared.</param>
        /// <param name="list2">List to be compared.</param>
        /// <returns>
        /// A value that indicates the relative order of the lists being compared. The
        /// return value has the following meanings:
        ///  * Less than zero: <paramref name="list1"/> is less than <paramref name="list2"/>.
        ///  * Zero: <paramref name="list1"/> is equal to <paramref name="list2"/>.
        ///  * Greater than zero: <paramref name="list1"/> is greater than <paramref name="list2"/>.
        ///  </returns>
        public static int CompareLists(IList list1, IList list2)
        {
            for (int i = 0; i < Math.Min(list1.Count, list2.Count); i++)
            {
                object item1 = list1[i];
                object item2 = list2[i];

                if (item1 == null || item2 == null)
                {
                    if (item1 == null && item2 == null)
                    {
                        continue;
                    }

                    return item1 == null ? -1 : 1;
                }

                IComparable item1Comparable = item1 as IComparable;
                if (item1Comparable == null)
                {
                    throw new InvalidOperationException(StringUtil.GetFormattedString(
                        Resources.ErrorMsg_ListItemNotComparable, item1));
                }

                IComparable item2Comparable = item2 as IComparable;
                if (item2Comparable == null)
                {
                    throw new InvalidOperationException(StringUtil.GetFormattedString(
                        Resources.ErrorMsg_ListItemNotComparable, item2));
                }

                int compareResult = item1Comparable.CompareTo(item2Comparable);
                if (compareResult != 0)
                {
                    return compareResult;
                }
            }

            if (list1.Count == list2.Count)
            {
                return 0;
            }

            // If we get here, then all list items have been equal up to the point where the two lists
            // have the same length.  Now, whichever list has more elements is considered to be greater
            // than the other.
            return list1.Count > list2.Count ? 1 : -1;
        }

        /// <summary>
        /// Compares two <see cref="IComparable"/> objects.
        /// </summary>
        /// <param name="obj1">Object to be compared.</param>
        /// <param name="obj2">Object to be compared.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings:
        ///  * Less than zero: <paramref name="obj1"/> is less than <paramref name="obj2"/>.
        ///  * Zero: <paramref name="obj1"/> is equal to <paramref name="obj2"/>.
        ///  * Greater than zero: <paramref name="obj1"/> is greater than <paramref name="obj2"/>.
        ///  </returns>
        public static int CompareObjects(IComparable obj1, IComparable obj2)
        {
            if (Object.ReferenceEquals(obj1, null) || Object.ReferenceEquals(obj2, null))
            {
                if (Object.ReferenceEquals(obj1, null) && Object.ReferenceEquals(obj2, null))
                {
                    return 0;
                }

                return Object.ReferenceEquals(obj1, null) ? -1 : 1;
            }

            return obj1.CompareTo(obj2);
        }
    }
}
