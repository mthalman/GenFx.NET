using System;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the mean of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// property in a <see cref="Population"/>.
    /// </summary>
    [DataContract]
    public class MeanFitness : Metric
    {
        /// <summary>
        /// Calculates the mean of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// property in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the metric.</param>
        /// <returns>Mean of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> property.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            // If there's no fitness scaling, just use the raw mean value.
            if (this.Algorithm.FitnessScalingStrategy == null)
            {
                return population.RawMean;
            }

            double scaledSum = population.Entities.Sum(e => e.ScaledFitnessValue);
            return scaledSum / population.Entities.Count;
        }
    }
}
