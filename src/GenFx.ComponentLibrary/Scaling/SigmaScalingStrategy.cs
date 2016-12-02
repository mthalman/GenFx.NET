using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;

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
    public class SigmaScalingStrategy : FitnessScalingStrategy
    {
        /// <summary>
        /// Gets the multiplier of the standard deviation.
        /// </summary>
        public int Multiplier
        {
            get { return ((SigmaScalingStrategyConfiguration)this.Algorithm.ConfigurationSet.FitnessScalingStrategy).Multiplier; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SigmaScalingStrategy"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="SigmaScalingStrategy"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public SigmaScalingStrategy(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Sets the <see cref="GeneticEntity.ScaledFitnessValue"/> property of each of the <see cref="GeneticEntity"/>
        /// objects in <paramref name="population"/> according to the sigma scaling algorithm.
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
                double scaledFitness = this.GetSigmaScaleValue(geneticEntity, population.RawMean, population.RawStandardDeviation);
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
                return 0;
            else
                return val;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="SigmaScalingStrategy"/>.
    /// </summary>
    [Component(typeof(SigmaScalingStrategy))]
    public class SigmaScalingStrategyConfiguration : FitnessScalingStrategyConfiguration
    {
        private const int DefaultMultiplier = 2;

        private int multiplier = SigmaScalingStrategyConfiguration.DefaultMultiplier;

        /// <summary>
        /// Gets or sets the multiplier of the standard deviation.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int Multiplier
        {
            get { return this.multiplier; }
            set { this.SetProperty(ref this.multiplier, value); }
        }
    }
}
