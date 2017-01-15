namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents an entity made up of a list of integers.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface IIntegerListEntity : IListEntity<int>
    {
        /// <summary>
        /// Gets or sets the minimum value an integer in the list can have.
        /// </summary>
        int MinElementValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value an integer in the list can have.
        /// </summary>
        int MaxElementValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether each of the element values should be unique for the entity.
        /// </summary>
        bool UseUniqueElementValues { get; set; }
    }
}
