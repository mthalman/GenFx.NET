using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="VariableSinglePointCrossoverOperator{TCrossover, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TCrossover">Type of the associated crossover operator class.</typeparam>
    public abstract class VariableSinglePointCrossoverOperatorFactoryConfig<TConfiguration, TCrossover> : CrossoverOperatorFactoryConfigBase<TConfiguration, TCrossover>
        where TCrossover : VariableSinglePointCrossoverOperator<TCrossover, TConfiguration>
        where TConfiguration : VariableSinglePointCrossoverOperatorFactoryConfig<TConfiguration, TCrossover>
    {
    }
}
