using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="SinglePointCrossoverOperator{TCrossover, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TCrossover">Type of the associated crossover operator class.</typeparam>
    public abstract class SinglePointCrossoverOperatorConfiguration<TConfiguration, TCrossover> : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>
        where TConfiguration : SinglePointCrossoverOperatorConfiguration<TConfiguration, TCrossover> 
        where TCrossover : SinglePointCrossoverOperator<TCrossover, TConfiguration>
    {
    }
}
