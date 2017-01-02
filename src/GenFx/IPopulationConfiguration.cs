using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="IPopulation"/>.
    /// </summary>
    public interface IPopulationConfiguration : IConfigurationForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the number of <see cref="IGeneticEntity"/> objects that are contained by a population.
        /// </summary>
        int PopulationSize { get; }
    }
}
