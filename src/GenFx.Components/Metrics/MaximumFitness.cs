using System;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// in a <see cref="Population"/>.
    /// </summary>
    [DataContract]
    public class MaximumFitness : Metric
    {
        /// <summary>
        /// Calculates to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the metric.</param>
        /// <returns>Largest value of <see cref="GeneticEntity.ScaledFitnessValue"/> found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            // If there's no fitness scaling, just use the raw max value.
            if (this.Algorithm.FitnessScalingStrategy == null)
            {
                return population.RawMax;
            }

            return population.Entities.Max(e => e.ScaledFitnessValue);
        }
    }
}
