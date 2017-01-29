using GenFx.Validation;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once a generation
    /// contains a <see cref="GeneticEntity"/> whose <see cref="GeneticEntity.ScaledFitnessValue"/> property value
    /// matches the <see cref="FitnessTargetTerminator.FitnessTarget"/> property value.
    /// </summary>
    [DataContract]
    public class FitnessTargetTerminator : Terminator
    {
        private const FitnessType DefaultFitnessValueType = FitnessType.Scaled;

        [DataMember]
        private FitnessType fitnessValueType = DefaultFitnessValueType;

        [DataMember]
        private double fitnessTarget;

        /// <summary>
        /// Gets or sets the fitness value which a <see cref="GeneticEntity"/> must have in order for the algorithm
        /// to stop.
        /// </summary>
        [ConfigurationProperty]
        public double FitnessTarget
        {
            get { return this.fitnessTarget; }
            set { this.SetProperty(ref this.fitnessTarget, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="FitnessType"/> to base termination on.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [FitnessTypeValidator]
        public FitnessType FitnessValueType
        {
            get { return this.fitnessValueType; }
            set { this.SetProperty(ref this.fitnessValueType, value); }
        }

        /// <summary>
        /// Calculates whether a <see cref="GeneticEntity"/> exists whose <see cref="GeneticEntity.ScaledFitnessValue"/> property value
        /// matches the <see cref="FitnessTargetTerminator.FitnessTarget"/> property value.
        /// </summary>
        /// <returns>True if the genetic algorithm is to stop executing; otherwise, false.</returns>
        public override bool IsComplete()
        {
            return this.Algorithm.Environment.Populations.SelectMany(p => p.Entities).Any(e => this.GetFitnessValue(e) == this.FitnessTarget);
        }

        /// <summary>
        /// Returns the fitness value to base termination on.
        /// </summary>
        /// <param name="entity">The <see cref="GeneticEntity"/> whose appropriate fitness value should be returned.</param>
        /// <returns>The fitness value to base termination on.</returns>
        private double GetFitnessValue(GeneticEntity entity)
        {
            if (this.FitnessValueType == FitnessType.Raw)
            {
                return entity.RawFitnessValue;
            }
            else
            {
                return entity.ScaledFitnessValue;
            }
        }
    }
}
