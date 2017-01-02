using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="IElitismStrategy"/>.
    /// </summary>
    public interface IElitismStrategyConfiguration : IConfigurationForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the ratio of <see cref="IGeneticEntity"/> objects that will be selected as elite and move on 
        /// to the next generation unchanged.
        /// </summary>
        double ElitistRatio { get; }
    }
}
