namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents a list-based entity.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface IListEntity<T> : IListEntityBase
    {
        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">Index of the item to get.</param>
        /// <returns>Item at the specified index.</returns>
        new T this[int index] { get; set; }
    }
}
