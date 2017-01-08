using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the standard deviation of the values of the 
    /// <see cref="IGeneticEntity.ScaledFitnessValue"/> property in a <see cref="IPopulation"/>.
    /// </summary>
    public sealed class StandardDeviationFitnessStatistic : StatisticBase<StandardDeviationFitnessStatistic, StandardDeviationFitnessStatisticFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardDeviationFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="StandardDeviationFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public StandardDeviationFitnessStatistic(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
