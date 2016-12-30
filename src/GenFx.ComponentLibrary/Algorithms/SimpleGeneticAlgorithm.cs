namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the most basic type of genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <b>SimpleGeneticAlgorithm</b> can operate multiple <see cref="IPopulation"/> objects but
    /// they run isolated from one another.
    /// </remarks>
    public sealed class SimpleGeneticAlgorithm : SimpleGeneticAlgorithm<SimpleGeneticAlgorithm, SimpleGeneticAlgorithmConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configurationSet">Contains the component configuration for the algorithm.</param>
        public SimpleGeneticAlgorithm(ComponentConfigurationSet configurationSet)
            : base(configurationSet)
        {
        }
    }
}
