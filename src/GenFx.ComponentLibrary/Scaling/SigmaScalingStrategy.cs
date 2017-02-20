using GenFx.Validation;
using System;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by incorporating the population's fitness variance to 
    /// derive a preprocessed fitness prior to scaling.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The sigma scaling algorithm is based on the one defined by Goldberg (1989).
    /// </para>
    /// </remarks>
    [DataContract]
    public class SigmaScalingStrategy : FitnessScalingStrategy
    {
        private const int DefaultMultiplier = 2;

        [DataMember]
        private int multiplier = DefaultMultiplier;

        /// <summary>
        /// Gets or sets the multiplier of the standard deviation.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int Multiplier
        {
            get { return this.multiplier; }
            set { this.SetProperty(ref this.multiplier, value); }
        }

        /// <summary>
        /// Sets the <see cref="GeneticEntity.ScaledFitnessValue"/> property of each entity
        /// in the <paramref name="population"/> according to the sigma scaling algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override void UpdateScaledFitnessValues(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            foreach (GeneticEntity geneticEntity in population.Entities)
            {
                double scaledFitness = this.GetSigmaScaleValue(geneticEntity, population.RawMean.Value, population.RawStandardDeviation.Value);
                geneticEntity.ScaledFitnessValue = scaledFitness;
            }
        }

        /// <summary>
        /// Returns the sigma scaled fitness value of <paramref name="geneticEntity"/>.
        /// </summary>
        private double GetSigmaScaleValue(GeneticEntity geneticEntity, double mean, double standardDeviation)
        {
            // Goldberg, 1989
            double val = geneticEntity.RawFitnessValue - (mean - this.Multiplier * standardDeviation);
            if (val < 0)
            {
                return 0;
            }
            else
            {
                return val;
            }
        }
    }
}
