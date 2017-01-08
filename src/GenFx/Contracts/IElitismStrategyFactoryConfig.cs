namespace GenFx.Contracts
{
    /// <summary>
    /// Represents the configuration of <see cref="IElitismStrategy"/>.
    /// </summary>
    public interface IElitismStrategyFactoryConfig : IFactoryConfigForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the ratio of <see cref="IGeneticEntity"/> objects that will be selected as elite and move on 
        /// to the next generation unchanged.
        /// </summary>
        double ElitistRatio { get; }
    }
}
