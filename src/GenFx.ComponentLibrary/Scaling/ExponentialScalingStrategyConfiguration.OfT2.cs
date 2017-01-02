using GenFx.ComponentLibrary.Base;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Represents the configuration of <see cref="ExponentialScalingStrategy{TScaling, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TScaling">Type of the associated fitness scaling strategy class.</typeparam>
    public abstract class ExponentialScalingStrategyConfiguration<TConfiguration, TScaling> : FitnessScalingStrategyConfigurationBase<TConfiguration, TScaling>
        where TConfiguration : ExponentialScalingStrategyConfiguration<TConfiguration, TScaling> 
        where TScaling : ExponentialScalingStrategy<TScaling, TConfiguration>
    {
        private const double DefaultScalingPower = 1.005;

        private double scalingPower = DefaultScalingPower;

        /// <summary>
        /// Gets or sets the power which raw fitness values are to be scaled by.
        /// </summary>
        /// <exception cref="ValidationException">Value is valid.</exception>
        [DoubleValidator(MinValue = 0, IsMinValueInclusive = false)]
        public double ScalingPower
        {
            get { return this.scalingPower; }
            set { this.SetProperty(ref this.scalingPower, value); }
        }
    }
}
