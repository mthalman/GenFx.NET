using GenFx.Validation;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the standard deviation of the values of the 
    /// <see cref="GeneticEntity.ScaledFitnessValue"/> property in a <see cref="Population"/>.
    /// </summary>
    [DataContract]
    [RequiredMetric(typeof(MeanFitness))]
    public class FitnessStandardDeviation : Metric
    {
        /// <summary>
        /// Calculates the standard deviation of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// property in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the metric.</param>
        /// <returns>Standard deviation of the values of the <see cref="GeneticEntity.ScaledFitnessValue"/> property.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object? GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            this.AssertIsInitialized();

            // If there's no fitness scaling, just use the raw max value.
            if (this.Algorithm!.FitnessScalingStrategy == null)
            {
                return population.RawStandardDeviation;
            }

            MeanFitness meanFitness = this.Algorithm.Metrics.OfType<MeanFitness>().First();
            return MathHelper.GetStandardDeviation(
                population.Entities,
                (double)meanFitness.GetResults(population.Index).Last().ResultValue,
                FitnessType.Scaled);
        }
    }
}
