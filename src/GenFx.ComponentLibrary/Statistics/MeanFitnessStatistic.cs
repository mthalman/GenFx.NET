using System;
using System.ComponentModel;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the mean of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// property in a <see cref="Population"/>.
    /// </summary>
    public class MeanFitnessStatistic : Statistic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeanFitnessStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="MeanFitnessStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public MeanFitnessStatistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates the mean of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// property in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the statistic.</param>
        /// <returns>Mean of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> property.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            return population.ScaledMean;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="MeanFitnessStatistic"/>.
    /// </summary>
    [Component(typeof(MeanFitnessStatistic))]
    public class MeanFitnessStatisticConfiguration : StatisticConfiguration
    {
    }
}
