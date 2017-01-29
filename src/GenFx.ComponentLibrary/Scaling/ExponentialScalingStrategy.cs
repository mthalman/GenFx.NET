using GenFx.Validation;
using System;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by raising the fitness of a <see cref="GeneticEntity"/> to the power of the
    /// value of the <see cref="ScalingPower"/> property.
    /// </summary>
    [DataContract]
    public class ExponentialScalingStrategy : FitnessScalingStrategy
    {
        private const double DefaultScalingPower = 1.005;

        [DataMember]
        private double scalingPower = DefaultScalingPower;

        /// <summary>
        /// Gets or sets the power which raw fitness values are to be scaled by.
        /// </summary>
        /// <exception cref="ValidationException">Value is valid.</exception>
        [ConfigurationProperty]
        [DoubleValidator(MinValue = 0, IsMinValueInclusive = false)]
        public double ScalingPower
        {
            get { return this.scalingPower; }
            set { this.SetProperty(ref this.scalingPower, value); }
        }

        /// <summary>
        /// Sets the <see cref="GeneticEntity.ScaledFitnessValue"/> property of each entity
        /// in the <paramref name="population"/> by raising it to the power of <see cref="ScalingPower"/>.
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
}
