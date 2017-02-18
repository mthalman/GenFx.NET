using GenFx.Validation;
using System;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by adjusting the fitness of a <see cref="GeneticEntity"/> so that those <see cref="GeneticEntity"/>
    /// objects which are common have a degraded fitness while rare <see cref="GeneticEntity"/> objects are given a boost in
    /// fitness.
    /// </summary>
    [DataContract]
    public abstract class FitnessSharingScalingStrategy : FitnessScalingStrategy
    {
        private const double DefaultScalingCurvature = 1;
        private const double DefaultScalingDistanceCutoff = 1;

        [DataMember]
        private double[] fitnessDistances;

        [DataMember]
        private double scalingDistanceCutoff = DefaultScalingDistanceCutoff;

        [DataMember]
        private double scalingCurvature = DefaultScalingCurvature;

        /// <summary>
        /// Gets or sets the power to which the distance between two entities will be raised when calculating
        /// the scaled fitness value.
        /// </summary>
        /// <remarks>A value of 1 the curvature is a straight line.</remarks>
        [ConfigurationProperty]
        public double ScalingCurvature
        {
            get { return this.scalingCurvature; }
            set { this.SetProperty(ref this.scalingCurvature, value); }
        }

        /// <summary>
        /// Gets or sets the cutoff value of the distance between two genetic entities that will result in the curvature being applied to those genetic entities.
        /// </summary>
        /// <remarks>
        /// This value represents the point at which two entities' raw fitness values are considered close enough
        /// where their scaled fitness values should be penalized.
        /// </remarks>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [DoubleValidator(MinValue = 0, IsMinValueInclusive = false)]
        public double ScalingDistanceCutoff
        {
            get { return this.scalingDistanceCutoff; }
            set { this.SetProperty(ref this.scalingDistanceCutoff, value); }
        }

        /// <summary>
        /// Sets the <see cref="GeneticEntity.ScaledFitnessValue"/> property of each entity
        /// in <paramref name="population"/> according to the fitness sharing scaling algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override void UpdateScaledFitnessValues(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int entityCount = population.Entities.Count;
            this.fitnessDistances = new double[(int)Math.Pow(entityCount, 2)];

            // Collect the fitness distances between genetic entities
            for (int i = 0; i < entityCount; i++)
            {
                this.fitnessDistances[(i * entityCount) + 1] = 0;
                for (int j = 0; j < entityCount; j++)
                {
                    this.fitnessDistances[(i * entityCount) + j] =
                        this.fitnessDistances[(j * entityCount) + i] =
                        this.EvaluateFitnessDistance(population.Entities[i], population.Entities[j]);
                }
            }

            for (int i = 0; i < entityCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < entityCount; j++)
                {
                    if (this.fitnessDistances[(i * entityCount) + j] < this.ScalingDistanceCutoff)
                    {
                        sum += 1 - Math.Pow(this.fitnessDistances[(i * entityCount) + j] / this.ScalingDistanceCutoff,
                          this.ScalingCurvature);
                    }
                }
                population.Entities[i].ScaledFitnessValue = population.Entities[i].ScaledFitnessValue / sum;
            }
        }

        /// <summary>
        /// Returns the fitness distance between <paramref name="entity1"/> and <paramref name="entity2"/>.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be evaluated against <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be evaluated against <paramref name="entity1"/>.</param>
        /// <returns>Fitness distance between <paramref name="entity1"/> and <paramref name="entity2"/>.</returns>
        /// <remarks>A distance of 0 means that the two <see cref="GeneticEntity"/> objects are identical.</remarks>
        public abstract double EvaluateFitnessDistance(GeneticEntity entity1, GeneticEntity entity2);
    }
}
