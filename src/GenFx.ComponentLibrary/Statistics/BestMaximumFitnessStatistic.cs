using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    [DataContract]
    public class BestMaximumFitnessStatistic : Statistic
    {
        [DataMember]
        private Dictionary<int, double> bestMaxValues = new Dictionary<int, double>();
        
        /// <summary>
        /// Calculates to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the statistic.</param>
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

            if (bestMax < population.ScaledMax)
            {
                bestMax = population.ScaledMax;
                this.bestMaxValues[population.Index] = bestMax;
            }

            return bestMax;
        }
    }
}
