using System;
using System.ComponentModel;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the standard deviation of the values of the 
    /// <see cref="GeneticEntity.ScaledFitnessValue"/> property in a <see cref="Population"/>.
    /// </summary>
    public class StandardDeviationFitnessStatistic : Statistic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardDeviationFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="StandardDeviationFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public StandardDeviationFitnessStatistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates the standard deviation of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// property in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the statistic.</param>
        /// <returns>Standard deviation of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> property.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            return population.ScaledStandardDeviation;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="StandardDeviationFitnessStatistic"/>.
    /// </summary>
    [Component(typeof(StandardDeviationFitnessStatistic))]
    public class StandardDeviationFitnessStatisticConfiguration : StatisticConfiguration
    {
    }
}
