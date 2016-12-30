using GenFx.ComponentLibrary.Base;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="IGeneticEntity.ScaledFitnessValue"/> 
    /// found for a <see cref="IPopulation"/> during the entire run of the genetic algorithm.
    /// </summary>
    public sealed class BestMaximumFitnessStatistic : StatisticBase<BestMaximumFitnessStatistic, BestMaximumFitnessStatisticConfiguration>
    {
        private Dictionary<int, double> bestMaxValues = new Dictionary<int, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BestMaximumFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="BestMaximumFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public BestMaximumFitnessStatistic(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates to determine the highest <see cref="IGeneticEntity.ScaledFitnessValue"/> 
        /// found for a <see cref="IPopulation"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> from which to derive the statistic.</param>
        /// <returns>Largest value of <see cref="IGeneticEntity.ScaledFitnessValue"/> found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(IPopulation population)
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

            state[nameof(this.bestMaxValues)] = this.bestMaxValues;
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

            this.bestMaxValues = (Dictionary<int, double>)state[nameof(this.bestMaxValues)];
        }
    }
}
