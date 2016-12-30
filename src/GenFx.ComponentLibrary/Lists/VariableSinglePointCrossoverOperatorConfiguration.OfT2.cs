using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="VariableSinglePointCrossoverOperator{TCrossover, TConfiguration}"/>.
    /// </summary>
    public abstract class VariableSinglePointCrossoverOperatorConfiguration<TConfiguration, TCrossover> : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>
        where TCrossover : VariableSinglePointCrossoverOperator<TCrossover, TConfiguration>
        where TConfiguration : VariableSinglePointCrossoverOperatorConfiguration<TConfiguration, TCrossover>
    {
    }
}
