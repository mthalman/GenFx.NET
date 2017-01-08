using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

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
    /// <typeparam name="TScaling">Type of the deriving fitness scaling strategy class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class SigmaScalingStrategy<TScaling, TConfiguration> : FitnessScalingStrategyBase<TScaling, TConfiguration>
        where TScaling : SigmaScalingStrategy<TScaling, TConfiguration>
        where TConfiguration : SigmaScalingStrategyFactoryConfig<TConfiguration, TScaling>
    {

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected SigmaScalingStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Sets the <see cref="IGeneticEntity.ScaledFitnessValue"/> property of each entity
        /// in the <paramref name="population"/> according to the sigma scaling algorithm.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            foreach (IGeneticEntity geneticEntity in population.Entities)
            {
                double scaledFitness = this.GetSigmaScaleValue(geneticEntity, population.RawMean, population.RawStandardDeviation);
                geneticEntity.ScaledFitnessValue = scaledFitness;
            }
        }

        /// <summary>
        /// Returns the sigma scaled fitness value of <paramref name="geneticEntity"/>.
        /// </summary>
        private double GetSigmaScaleValue(IGeneticEntity geneticEntity, double mean, double standardDeviation)
        {
            // Goldberg, 1989
            double val = geneticEntity.RawFitnessValue - (mean - this.Configuration.Multiplier * standardDeviation);
            if (val < 0)
                return 0;
            else
                return val;
        }
    }
}
