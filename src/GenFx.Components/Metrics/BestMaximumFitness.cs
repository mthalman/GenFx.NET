using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    [DataContract]
    [RequiredMetric(typeof(MaximumFitness))]
    public class BestMaximumFitness : Metric
    {
        [DataMember]
        private Dictionary<int, double> bestMaxValues = new Dictionary<int, double>();
        
        /// <summary>
        /// Calculates to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
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

            double bestMax;
            if (!this.bestMaxValues.TryGetValue(population.Index, out bestMax))
            {
                bestMax = Double.MinValue;
                this.bestMaxValues.Add(population.Index, bestMax);
            }

            MaximumFitness maxFitness = this.Algorithm.Metrics.OfType<MaximumFitness>().First();
            double populationMax = (double)maxFitness.GetResults(population.Index).Last().ResultValue;

            if (bestMax < populationMax)
            {
                bestMax = populationMax;
                this.bestMaxValues[population.Index] = bestMax;
            }

            return bestMax;
        }
    }
}
