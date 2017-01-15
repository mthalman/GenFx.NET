using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="IGeneticEntity.ScaledFitnessValue"/> 
    /// in a <see cref="IPopulation"/>.
    /// </summary>
    public class MaximumFitnessStatistic : StatisticBase
    {
        /// <summary>
        /// Calculates to determine the highest <see cref="IGeneticEntity.ScaledFitnessValue"/> 
        /// in the <paramref name="population"/>.
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

            return population.ScaledMax;
        }
    }
}
