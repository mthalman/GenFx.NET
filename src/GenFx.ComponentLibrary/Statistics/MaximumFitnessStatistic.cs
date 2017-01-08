using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="IGeneticEntity.ScaledFitnessValue"/> 
    /// in a <see cref="IPopulation"/>.
    /// </summary>
    public sealed class MaximumFitnessStatistic : StatisticBase<MaximumFitnessStatistic, MaximumFitnessStatisticFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaximumFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="MaximumFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public MaximumFitnessStatistic(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
