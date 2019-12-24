using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the lowest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    [DataContract]
    [RequiredMetric(typeof(MinimumFitness))]
    public class BestMinimumFitness : Metric
    {
        [DataMember]
        private readonly Dictionary<int, double?> bestMinValues = new Dictionary<int, double?>();

        /// <summary>
        /// Calculates to determine the lowest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
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

            if (!this.bestMinValues.TryGetValue(population.Index, out double? bestMin))
            {
                bestMin = null;
                this.bestMinValues.Add(population.Index, bestMin);
            }

            MinimumFitness minFitness = this.Algorithm.Metrics.OfType<MinimumFitness>().First();
            double populationMin = (double)minFitness.GetResults(population.Index).Last().ResultValue;

            if (!bestMin.HasValue || bestMin > populationMin)
            {
                bestMin = populationMin;
                this.bestMinValues[population.Index] = bestMin;
            }

            return bestMin.Value;
        }
    }
}
