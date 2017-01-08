using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessScalingStrategyBase{TConfiguration, TScaling}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TScaling">Type of the associated fitness scaling strategy class.</typeparam>
    public abstract class FitnessScalingStrategyFactoryConfigBase<TConfiguration, TScaling> : ConfigurationForComponentWithAlgorithm<TConfiguration, TScaling>, IFitnessScalingStrategyFactoryConfig
        where TConfiguration : FitnessScalingStrategyFactoryConfigBase<TConfiguration, TScaling> 
        where TScaling : FitnessScalingStrategyBase<TScaling, TConfiguration>
    {
    }
}
