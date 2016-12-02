using System;
using System.ComponentModel;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// in a <see cref="Population"/>.
    /// </summary>
    public class MaximumFitnessStatistic : Statistic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaximumFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="MaximumFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public MaximumFitnessStatistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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

    /// <summary>
    /// Represents the configuration of <see cref="MaximumFitnessStatistic"/>.
    /// </summary>
    [Component(typeof(MaximumFitnessStatistic))]
    public class MaximumFitnessStatisticConfiguration : StatisticConfiguration
    {
    }
}
