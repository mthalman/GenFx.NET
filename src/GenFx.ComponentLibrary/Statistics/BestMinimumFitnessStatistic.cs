using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        /// Initializes a new instance of the <see cref="BestMinimumFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="BestMinimumFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public BestMinimumFitnessStatistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
        protected override void SetSaveState(KeyValueMap state)
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
        protected override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.bestMinValues = (Dictionary<int, double?>)state[nameof(this.bestMinValues)];
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="BestMinimumFitnessStatistic"/>.
    /// </summary>
    [Component(typeof(BestMinimumFitnessStatistic))]
    public class BestMinimumFitnessStatisticConfiguration : StatisticConfiguration
    {
    }
}
