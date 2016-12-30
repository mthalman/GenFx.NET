using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents the configuration of <see cref="SingleNodeTreeCrossoverOperator{TCrossover, TConfiguration}"/>.
    /// </summary>
    public abstract class SingleNodeTreeCrossoverOperatorConfiguration<TConfiguration, TCrossover> : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>
        where TConfiguration : SingleNodeTreeCrossoverOperatorConfiguration<TConfiguration, TCrossover> 
        where TCrossover : SingleNodeTreeCrossoverOperator<TCrossover, TConfiguration>
    {
    }
}
