using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by raising the fitness of a <see cref="GeneticEntity"/> to the power of the
    /// value of the <see cref="ExponentialScalingStrategy.ScalingPower"/> property.
    /// </summary>
    public class ExponentialScalingStrategy : FitnessScalingStrategy
    {
        /// <summary>
        /// Gets the power which raw fitness value are to be scaled by.
        /// </summary>
        public double ScalingPower
        {
            get { return ((ExponentialScalingStrategyConfiguration)this.Algorithm.ConfigurationSet.FitnessScalingStrategy).ScalingPower; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialScalingStrategy"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="ExponentialScalingStrategy"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public ExponentialScalingStrategy(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Sets the <see cref="GeneticEntity.ScaledFitnessValue"/> property of each of the <see cref="GeneticEntity"/>
        /// objects in <paramref name="population"/> by raising it to the power of <see cref="ExponentialScalingStrategy.ScalingPower"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override void UpdateScaledFitnessValues(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            for (int i = 0; i < population.Entities.Count; i++)
            {
                GeneticEntity entity = population.Entities[i];
                entity.ScaledFitnessValue = Math.Pow(entity.RawFitnessValue, this.ScalingPower);
            }
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="ExponentialScalingStrategy"/>.
    /// </summary>
    [Component(typeof(ExponentialScalingStrategy))]
    public class ExponentialScalingStrategyConfiguration : FitnessScalingStrategyConfiguration
    {
        private const double DefaultScalingPower = 1.005;

        private double scalingPower = ExponentialScalingStrategyConfiguration.DefaultScalingPower;

        /// <summary>
        /// Gets or sets the power which raw fitness values are to be scaled by.
        /// </summary>
        /// <exception cref="ValidationException">Value is valid.</exception>
        [DoubleValidator(MinValue = 0, IsMinValueInclusive = false)]
        public double ScalingPower
        {
            get { return this.scalingPower; }
            set { this.SetProperty(ref this.scalingPower, value); }
        }
    }
}
