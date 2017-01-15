using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Populations
{
    /// <summary>
    /// Represents a collection of <see cref="IGeneticEntity"/> objects which interact locally with each other.  A population is 
    /// the unit from which the <see cref="ISelectionOperator"/> selects its genetic entities.
    /// </summary>
    /// <remarks>
    /// Populations can be isolated or interactive with one another through migration depending on
    /// which <see cref="IGeneticAlgorithm"/> is used.
    /// </remarks>
    public class SimplePopulation : PopulationBase
    {
    }
}
