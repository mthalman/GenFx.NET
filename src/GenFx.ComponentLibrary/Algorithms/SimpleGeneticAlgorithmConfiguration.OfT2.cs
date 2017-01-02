namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the configuration of <see cref="SimpleGeneticAlgorithm{TAlgorithm, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TAlgorithm">Type of the associated algorithm class.</typeparam>
    public abstract class SimpleGeneticAlgorithmConfiguration<TConfiguration, TAlgorithm> : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
        where TConfiguration : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
        
    {
    }
}
