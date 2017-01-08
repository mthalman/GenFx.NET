namespace GenFx.Contracts
{
    /// <summary>
    /// Represents the configuration of <see cref="IGeneticAlgorithm"/>.
    /// </summary>
    public interface IGeneticAlgorithmFactoryConfig : IComponentFactoryConfig
    {
        /// <summary>
        /// Gets or sets the number of <see cref="IPopulation"/> objects that are contained by the <see cref="GeneticEnvironment"/>.
        /// </summary>
        /// <value>
        /// The number of populations that are contained by the <see cref="GeneticEnvironment"/>.
        /// </value>
        int EnvironmentSize { get; set; }
    }
}
