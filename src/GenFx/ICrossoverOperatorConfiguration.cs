using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="ICrossoverOperator"/>.
    /// </summary>
    public interface ICrossoverOperatorConfiguration : IConfigurationForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the probability that two <see cref="IGeneticEntity"/> objects will crossover after being selected.
        /// </summary>
        double CrossoverRate { get; }
    }
}
