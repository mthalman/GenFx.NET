using GenFx.ComponentModel;
using System.Collections.Generic;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="ICrossoverOperator"/>.
    /// </summary>
    public interface ICrossoverOperatorConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// Gets the probability that two <see cref="IGeneticEntity"/> objects will crossover after being selected.
        /// </summary>
        double CrossoverRate { get; }
    }
}
