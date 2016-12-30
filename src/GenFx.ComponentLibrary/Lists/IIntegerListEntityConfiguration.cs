namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="IIntegerListEntity"/>.
    /// </summary>
    public interface IIntegerListEntityConfiguration : IListEntityConfiguration<int>
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
