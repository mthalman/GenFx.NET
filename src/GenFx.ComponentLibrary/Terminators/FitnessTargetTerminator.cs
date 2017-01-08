using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Linq;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once a generation
    /// contains a <see cref="IGeneticEntity"/> whose <see cref="IGeneticEntity.ScaledFitnessValue"/> property value
    /// matches the <see cref="FitnessTargetTerminatorFactoryConfig.FitnessTarget"/> property value.
    /// </summary>
    public sealed class FitnessTargetTerminator : TerminatorBase<FitnessTargetTerminator, FitnessTargetTerminatorFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessTargetTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="FitnessTargetTerminator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public FitnessTargetTerminator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates whether a <see cref="IGeneticEntity"/> exists whose <see cref="IGeneticEntity.ScaledFitnessValue"/> property value
        /// matches the <see cref="FitnessTargetTerminatorFactoryConfig.FitnessTarget"/> property value.
        /// </summary>
        /// <returns>True if the genetic algorithm is to stop executing; otherwise, false.</returns>
        public override bool IsComplete()
        {
            return this.Algorithm.Environment.Populations.SelectMany(p => p.Entities).Any(e => this.GetFitnessValue(e) == this.Configuration.FitnessTarget);
        }

        /// <summary>
        /// Returns the fitness value to base termination on.
        /// </summary>
        /// <param name="entity">The <see cref="IGeneticEntity"/> whose appropriate fitness value should be returned.</param>
        /// <returns>The fitness value to base termination on.</returns>
        private double GetFitnessValue(IGeneticEntity entity)
        {
            if (this.Configuration.FitnessValueType == FitnessType.Raw)
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
