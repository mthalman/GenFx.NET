using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the configuration of <see cref="SingleNodeTreeCrossoverOperator{TCrossover, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TCrossover">Type of the associated crossover operator class.</typeparam>
    public abstract class SingleNodeTreeCrossoverOperatorConfiguration<TConfiguration, TCrossover> : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>
        where TConfiguration : SingleNodeTreeCrossoverOperatorConfiguration<TConfiguration, TCrossover> 
        where TCrossover : SingleNodeTreeCrossoverOperator<TCrossover, TConfiguration>
    {
    }
}
