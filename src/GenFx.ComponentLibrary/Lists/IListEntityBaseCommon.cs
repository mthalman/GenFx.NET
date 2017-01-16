using GenFx.Contracts;
using System.Collections;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents a list-based entity.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface IListEntityBaseCommon : IGeneticEntity
    {
        /// <summary>
        /// Gets or sets the length of the list.
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// Gets or sets the maximum starting length a new entity can have.
        /// </summary>
        int MaximumStartingLength { get; set; }

        /// <summary>
        /// Gets or sets the minimum starting length a new entity can have.
        /// </summary>
        int MinimumStartingLength { get; set; }
    }
}
