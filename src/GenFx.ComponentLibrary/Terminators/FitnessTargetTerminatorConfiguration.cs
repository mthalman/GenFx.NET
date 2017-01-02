using GenFx.ComponentLibrary.Base;
using GenFx.Validation;
using System.ComponentModel;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessTargetTerminator"/>.
    /// </summary>
    public sealed class FitnessTargetTerminatorConfiguration : TerminatorConfigurationBase<FitnessTargetTerminatorConfiguration, FitnessTargetTerminator>
    {
        private const FitnessType DefaultFitnessValueType = FitnessType.Scaled;

        private FitnessType fitnessValueType = FitnessTargetTerminatorConfiguration.DefaultFitnessValueType;
        private double fitnessTarget;

        /// <summary>
        /// Gets or sets the fitness value which a <see cref="IGeneticEntity"/> must have in order for the algorithm
        /// to stop.
        /// </summary>
        public double FitnessTarget
        {
            get { return this.fitnessTarget; }
            set { this.SetProperty(ref this.fitnessTarget, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="FitnessType"/> to base termination on.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [FitnessTypeValidator]
        public FitnessType FitnessValueType
        {
            get { return this.fitnessValueType; }
            set { this.SetProperty(ref this.fitnessValueType, value); }
        }
    }
}
