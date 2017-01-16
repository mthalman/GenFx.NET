using System;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// in a <see cref="Population"/>.
    /// </summary>
    public class MaximumFitnessStatistic : Statistic
    {
        /// <summary>
        /// Calculates to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// in the <paramref name="population"/>.
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

            return population.ScaledMax;
        }
    }
}
