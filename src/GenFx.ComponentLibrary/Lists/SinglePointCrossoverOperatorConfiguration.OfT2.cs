using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="SinglePointCrossoverOperator{TCrossover, TConfiguration}"/>.
    /// </summary>
    public abstract class SinglePointCrossoverOperatorConfiguration<TConfiguration, TCrossover> : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>
        where TConfiguration : SinglePointCrossoverOperatorConfiguration<TConfiguration, TCrossover> 
        where TCrossover : SinglePointCrossoverOperator<TCrossover, TConfiguration>
    {
    }
}
