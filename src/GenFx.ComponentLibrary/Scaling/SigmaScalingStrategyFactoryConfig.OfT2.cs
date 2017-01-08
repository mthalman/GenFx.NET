using GenFx.ComponentLibrary.Base;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Represents the configuration of <see cref="SigmaScalingStrategy{TScaling, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TScaling">Type of the associated fitness scaling strategy class.</typeparam>
    public abstract class SigmaScalingStrategyFactoryConfig<TConfiguration, TScaling> : FitnessScalingStrategyFactoryConfigBase<TConfiguration, TScaling>
        where TConfiguration : SigmaScalingStrategyFactoryConfig<TConfiguration, TScaling>
        where TScaling : SigmaScalingStrategy<TScaling, TConfiguration>
    {
        private const int DefaultMultiplier = 2;

        private int multiplier = DefaultMultiplier;

        /// <summary>
        /// Gets or sets the multiplier of the standard deviation.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int Multiplier
        {
            get { return this.multiplier; }
            set { this.SetProperty(ref this.multiplier, value); }
        }
    }
}
