using System.Collections;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents a list-based entity.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface IListEntityBase : IGeneticEntity, IList
    {
        /// <summary>
        /// Gets or sets the length of the list.
        /// </summary>
        int Length { get; set; }
    }
}
