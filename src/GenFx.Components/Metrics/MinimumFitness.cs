using System;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the lowest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// in a <see cref="Population"/>.
    /// </summary>
    [DataContract]
    public class MinimumFitness : Metric
    {
        /// <summary>
        /// Calculates to determine the lowest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the metric.</param>
        /// <returns>Smallest value of <see cref="GeneticEntity.ScaledFitnessValue"/> found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            // If there's no fitness scaling, just use the raw min value.
            if (this.Algorithm.FitnessScalingStrategy == null)
            {
                return population.RawMin;
            }

            return population.Entities.Min(e => e.ScaledFitnessValue);
        }
    }
}
