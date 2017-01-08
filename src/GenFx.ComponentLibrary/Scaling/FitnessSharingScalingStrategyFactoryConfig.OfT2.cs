using GenFx.ComponentLibrary.Base;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessSharingScalingStrategy{TScaling, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TScaling">Type of the associated fitness scaling strategy class.</typeparam>
    public abstract class FitnessSharingScalingStrategyFactoryConfig<TConfiguration, TScaling> : FitnessScalingStrategyFactoryConfigBase<TConfiguration, TScaling>
        where TConfiguration : FitnessSharingScalingStrategyFactoryConfig<TConfiguration, TScaling> 
        where TScaling : FitnessSharingScalingStrategy<TScaling, TConfiguration>
    {        
        private const double DefaultScalingCurvature = 1;
        private const double DefaultScalingDistanceCutoff = 1;

        private double scalingDistanceCutoff = DefaultScalingDistanceCutoff;
        private double scalingCurvature = DefaultScalingCurvature;

        /// <summary>
        /// Gets or sets the power to which the distance between two entities will be raised when calculating
        /// the scaled fitness value.
        /// </summary>
        /// <remarks>A value of 1 the curvature is a straight line.</remarks>
        public double ScalingCurvature
        {
            get { return this.scalingCurvature; }
            set { this.SetProperty(ref this.scalingCurvature, value); }
        }

        /// <summary>
        /// Gets or sets the cutoff value of the distance between two genetic entities that will result in the curvature being applied to those genetic entities.
        /// </summary>
        /// <remarks>
        /// This value represents the point at which two entities' raw fitness values are considered close enough
        /// where their scaled fitness values should be penalized.
        /// </remarks>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DoubleValidator(MinValue = 0, IsMinValueInclusive = false)]
        public double ScalingDistanceCutoff
        {
            get { return this.scalingDistanceCutoff; }
            set { this.SetProperty(ref this.scalingDistanceCutoff, value); }
        }
    }
}
