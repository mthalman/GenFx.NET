using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the standard deviation of the values of the 
    /// <see cref="IGeneticEntity.ScaledFitnessValue"/> property in a <see cref="IPopulation"/>.
    /// </summary>
    public class StandardDeviationFitnessStatistic : StatisticBase
    {
        /// <summary>
        /// Calculates the standard deviation of the values of the <see cref="IGeneticEntity.ScaledFitnessValue"/> 
        /// property in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> from which to derive the statistic.</param>
        /// <returns>Standard deviation of the values of the <see cref="IGeneticEntity.ScaledFitnessValue"/> property.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            return population.ScaledStandardDeviation;
        }
    }
}
