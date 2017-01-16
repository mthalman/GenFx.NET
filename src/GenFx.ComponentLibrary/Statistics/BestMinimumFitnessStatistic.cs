using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the lowest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    public class BestMinimumFitnessStatistic : Statistic
    {
        private Dictionary<int, double?> bestMinValues = new Dictionary<int, double?>();

        /// <summary>
        /// Calculates to determine the lowest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the statistic.</param>
        /// <returns>Smallest value of <see cref="GeneticEntity.ScaledFitnessValue"/> found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            double? bestMin;
            if (!this.bestMinValues.TryGetValue(population.Index, out bestMin))
            {
                bestMin = null;
                this.bestMinValues.Add(population.Index, bestMin);
            }

            if (!bestMin.HasValue || bestMin > population.ScaledMin)
            {
                bestMin = population.ScaledMin;
                this.bestMinValues[population.Index] = bestMin;
            }

            return bestMin.Value;
        }

        /// <summary>
        /// Sets the statistic's state.
        /// </summary>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.bestMinValues)] = this.bestMinValues;
        }

        /// <summary>
        /// Restores the statistic's state.
        /// </summary>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.bestMinValues = (Dictionary<int, double?>)state[nameof(this.bestMinValues)];
        }
    }
}
