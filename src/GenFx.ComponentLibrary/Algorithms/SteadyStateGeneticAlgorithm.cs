namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// A type of genetic algorithm that replaces the weakest members of a <see cref="IPopulation"/>
    /// with the offspring of the previous generation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Usage of an <see cref="IElitismStrategy"/> type with this algorithm will result in elitism being ignored
    /// since all high-fitness <see cref="IGeneticEntity"/> objects will be moved to the next generation anyways.
    /// </para>
    /// </remarks>
    public sealed class SteadyStateGeneticAlgorithm : SteadyStateGeneticAlgorithm<SteadyStateGeneticAlgorithm, SteadyStateGeneticAlgorithmConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configurationSet">Contains the component configuration for the algorithm.</param>
        public SteadyStateGeneticAlgorithm(ComponentConfigurationSet configurationSet)
            : base(configurationSet)
        {
        }
    }
}
