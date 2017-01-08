namespace GenFx.Contracts
{
    /// <summary>
    /// Represents the configuration of <see cref="ICrossoverOperator"/>.
    /// </summary>
    public interface ICrossoverOperatorFactoryConfig : IFactoryConfigForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the probability that two <see cref="IGeneticEntity"/> objects will crossover after being selected.
        /// </summary>
        double CrossoverRate { get; }
    }
}
