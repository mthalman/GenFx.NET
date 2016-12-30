namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the configuration of <see cref="SimpleGeneticAlgorithm{TAlgorithm, TConfiguration}"/>.
    /// </summary>
    public abstract class SimpleGeneticAlgorithmConfiguration<TConfiguration, TAlgorithm> : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
        where TConfiguration : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
        
    {
    }
}
