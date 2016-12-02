using System;
using System.Collections.Generic;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once a generation
    /// contains a <see cref="GeneticEntity"/> whose <see cref="GeneticEntity.ScaledFitnessValue"/> property value
    /// matches the <see cref="FitnessTargetTerminator.FitnessTarget"/> property value.
    /// </summary>
    public class FitnessTargetTerminator : Terminator
    {
        /// <summary>
        /// Gets the fitness value which a <see cref="GeneticEntity"/> must have in order for the algorithm
        /// to stop.
        /// </summary>
        public double FitnessTarget
        {
            get { return ((FitnessTargetTerminatorConfiguration)this.Algorithm.ConfigurationSet.Terminator).FitnessTarget; }
        }

        /// <summary>
        /// Gets the <see cref="FitnessType"/> to base termination on.
        /// </summary>
        public FitnessType FitnessValueType
        {
            get { return ((FitnessTargetTerminatorConfiguration)this.Algorithm.ConfigurationSet.Terminator).FitnessValueType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessTargetTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="FitnessTargetTerminator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public FitnessTargetTerminator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates whether a <see cref="GeneticEntity"/> exists whose <see cref="GeneticEntity.ScaledFitnessValue"/> property value
        /// matches the <see cref="FitnessTargetTerminator.FitnessTarget"/> property value.
        /// </summary>
        /// <returns>True if the genetic algorithm is to stop executing; otherwise, false.</returns>
        public override bool IsComplete()
        {
            IList<Population> populations = this.Algorithm.Environment.Populations;
            for (int populationIndex = 0; populationIndex < populations.Count; populationIndex++)
            {
                for (int entityIndex = 0; entityIndex < populations[populationIndex].Entities.Count; entityIndex++)
                {
                    if (this.GetFitnessValue(populations[populationIndex].Entities[entityIndex]) == this.FitnessTarget)
                    {
                        return true;
                    }
                }
            }

            return false;
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

    /// <summary>
    /// Represents the configuration of <see cref="FitnessTargetTerminator"/>.
    /// </summary>
    [Component(typeof(FitnessTargetTerminator))]
    public class FitnessTargetTerminatorConfiguration : TerminatorConfiguration
    {
        private const FitnessType DefaultFitnessValueType = FitnessType.Scaled;

        private FitnessType fitnessValueType = FitnessTargetTerminatorConfiguration.DefaultFitnessValueType;
        private double fitnessTarget;

        /// <summary>
        /// Gets or sets the fitness value which a <see cref="GeneticEntity"/> must have in order for the algorithm
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
