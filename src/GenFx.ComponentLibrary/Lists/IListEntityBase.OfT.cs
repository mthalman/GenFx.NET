using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents a list-based entity.
    /// </summary>
    /// <typeparam name="T">Type of the items contained in the list.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface IListEntityBase<T> : IListEntityBaseCommon, IList<T>
    {
    }
}
