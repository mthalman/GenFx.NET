namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the configuration of <see cref="SimpleGeneticAlgorithm{TAlgorithm, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TAlgorithm">Type of the associated algorithm class.</typeparam>
    public abstract class SimpleGeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm> : GeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm>
        where TConfiguration : GeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm>
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
    {
    }
}
