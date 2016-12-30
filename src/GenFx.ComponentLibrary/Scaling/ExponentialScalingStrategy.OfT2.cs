using GenFx.ComponentLibrary.Base;
using System;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by raising the fitness of a <see cref="IGeneticEntity"/> to the power of the
    /// value of the <see cref="ExponentialScalingStrategyConfiguration{TConfiguration, TScaling}.ScalingPower"/> property.
    /// </summary>
    public abstract class ExponentialScalingStrategy<TScaling, TConfiguration> : FitnessScalingStrategyBase<TScaling, TConfiguration>
        where TScaling : ExponentialScalingStrategy<TScaling, TConfiguration>
        where TConfiguration : ExponentialScalingStrategyConfiguration<TConfiguration, TScaling>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected ExponentialScalingStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Sets the <see cref="IGeneticEntity.ScaledFitnessValue"/> property of each entity
        /// in the <paramref name="population"/> by raising it to the power of <see cref="ExponentialScalingStrategyConfiguration{TConfiguration, TScaling}.ScalingPower"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            for (int i = 0; i < population.Entities.Count; i++)
            {
                IGeneticEntity entity = population.Entities[i];
                entity.ScaledFitnessValue = Math.Pow(entity.RawFitnessValue, this.Configuration.ScalingPower);
            }
        }
    }
}
