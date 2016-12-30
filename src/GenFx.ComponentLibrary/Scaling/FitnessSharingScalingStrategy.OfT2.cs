using GenFx.ComponentLibrary.Base;
using System;

namespace GenFx.ComponentLibrary.Scaling
{
    /// <summary>
    /// Provides fitness scaling by adjusting the fitness of a <see cref="IGeneticEntity"/> so that those <see cref="IGeneticEntity"/>
    /// objects which are common have a degraded fitness while rare <see cref="IGeneticEntity"/> objects are given a boost in
    /// fitness.
    /// </summary>
    public abstract class FitnessSharingScalingStrategy<TScaling, TConfiguration> : FitnessScalingStrategyBase<TScaling, TConfiguration>
        where TScaling : FitnessSharingScalingStrategy<TScaling, TConfiguration>
        where TConfiguration : FitnessSharingScalingStrategyConfiguration<TConfiguration, TScaling>
    {
        private double[] fitnessDistances;
        
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FitnessSharingScalingStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Sets the <see cref="IGeneticEntity.ScaledFitnessValue"/> property of each entity
        /// in <paramref name="population"/> according to the fitness sharing scaling algorithm.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int entityCount = population.Entities.Count;
            if (this.fitnessDistances == null || entityCount > this.fitnessDistances.Length)
            {
                this.fitnessDistances = new double[(int)Math.Pow(entityCount, 2)];
            }

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
                    if (this.fitnessDistances[(i * entityCount) + j] < this.Configuration.ScalingDistanceCutoff)
                    {
                        sum += 1 - Math.Pow(this.fitnessDistances[(i * entityCount) + j] / this.Configuration.ScalingDistanceCutoff,
                          this.Configuration.ScalingCurvature);
                    }
                }
                population.Entities[i].ScaledFitnessValue = population.Entities[i].ScaledFitnessValue / sum;
            }
        }

        /// <summary>
        /// Returns the fitness distance between <paramref name="entity1"/> and <paramref name="entity2"/>.
        /// </summary>
        /// <param name="entity1"><see cref="IGeneticEntity"/> to be evaluated against <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="IGeneticEntity"/> to be evaluated against <paramref name="entity1"/>.</param>
        /// <returns>Fitness distance between <paramref name="entity1"/> and <paramref name="entity2"/>.</returns>
        /// <remarks>A distance of 0 means that the two <see cref="IGeneticEntity"/> objects are identical.</remarks>
        public abstract double EvaluateFitnessDistance(IGeneticEntity entity1, IGeneticEntity entity2);
    }
}
