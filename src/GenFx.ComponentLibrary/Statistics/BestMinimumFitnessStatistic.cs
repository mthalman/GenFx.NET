using GenFx.ComponentLibrary.Base;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the lowest <see cref="IGeneticEntity.ScaledFitnessValue"/> 
    /// found for a <see cref="IPopulation"/> during the entire run of the genetic algorithm.
    /// </summary>
    public sealed class BestMinimumFitnessStatistic : StatisticBase<BestMinimumFitnessStatistic, BestMinimumFitnessStatisticConfiguration>
    {
        private Dictionary<int, double?> bestMinValues = new Dictionary<int, double?>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BestMinimumFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="BestMinimumFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public BestMinimumFitnessStatistic(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates to determine the lowest <see cref="IGeneticEntity.ScaledFitnessValue"/> 
        /// found for a <see cref="IPopulation"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> from which to derive the statistic.</param>
        /// <returns>Smallest value of <see cref="IGeneticEntity.ScaledFitnessValue"/> found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(IPopulation population)
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
