using GenFx.ComponentLibrary.ComponentModel;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessScalingStrategyBase{TConfiguration, TScaling}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TScaling">Type of the associated fitness scaling strategy class.</typeparam>
    public abstract class FitnessScalingStrategyConfigurationBase<TConfiguration, TScaling> : ConfigurationForComponentWithAlgorithm<TConfiguration, TScaling>, IFitnessScalingStrategyConfiguration
        where TConfiguration : FitnessScalingStrategyConfigurationBase<TConfiguration, TScaling> 
        where TScaling : FitnessScalingStrategyBase<TScaling, TConfiguration>
    {
    }
}
