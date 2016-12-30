using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessScalingStrategyBase{TConfiguration, TScaling}"/>.
    /// </summary>
    public abstract class FitnessScalingStrategyConfigurationBase<TConfiguration, TScaling> : ComponentConfiguration<TConfiguration, TScaling>, IFitnessScalingStrategyConfiguration
        where TConfiguration : FitnessScalingStrategyConfigurationBase<TConfiguration, TScaling> 
        where TScaling : FitnessScalingStrategyBase<TScaling, TConfiguration>
    {
    }
}
