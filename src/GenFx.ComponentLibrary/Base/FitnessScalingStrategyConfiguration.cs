using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessScalingStrategy{TConfiguration, TScaling}"/>.
    /// </summary>
    public abstract class FitnessScalingStrategyConfiguration<TConfiguration, TScaling> : ComponentConfiguration<TConfiguration, TScaling>, IFitnessScalingStrategyConfiguration
        where TConfiguration : FitnessScalingStrategyConfiguration<TConfiguration, TScaling> 
        where TScaling : FitnessScalingStrategy<TScaling, TConfiguration>
    {
    }
}
