namespace GenFx.Contracts
{
    /// <summary>
    /// Represents the configuration of <see cref="IMutationOperator"/>.
    /// </summary>
    public interface IMutationOperatorFactoryConfig : IFactoryConfigForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the probability that a data segment within a <see cref="IGeneticEntity"/> will become mutated.
        /// </summary>
        double MutationRate { get; set; }
    }
}
