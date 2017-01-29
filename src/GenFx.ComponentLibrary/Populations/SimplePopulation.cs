using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Populations
{
    /// <summary>
    /// Represents a collection of <see cref="GeneticEntity"/> objects which interact locally with each other.  A population is 
    /// the unit from which the <see cref="SelectionOperator"/> selects its genetic entities.
    /// </summary>
    /// <remarks>
    /// Populations can be isolated or interactive with one another through migration depending on
    /// which <see cref="GeneticAlgorithm"/> is used.
    /// </remarks>
    [DataContract]
    public class SimplePopulation : Population
    {
    }
}
