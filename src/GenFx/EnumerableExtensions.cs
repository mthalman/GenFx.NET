using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GenFx
{
    internal static class EnumerableExtensions
    {
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> enumerable)
        {
            return new ReadOnlyCollection<T>(enumerable.ToList());
        }
    }
}
