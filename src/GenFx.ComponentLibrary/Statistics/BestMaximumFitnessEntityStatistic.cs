using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the <see cref="GeneticEntity"/> object with the highest
    /// <see cref="GeneticEntity.ScaledFitnessValue"/> found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    public class BestMaximumFitnessEntityStatistic : Statistic
    {
        private Dictionary<int, BestEntity> bestEntities = new Dictionary<int, BestEntity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BestMaximumFitnessEntityStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="BestMaximumFitnessEntityStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public BestMaximumFitnessEntityStatistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates to determine the <see cref="GeneticEntity"/> object with the highest
        /// <see cref="GeneticEntity.ScaledFitnessValue"/> found for a <see cref="Population"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the statistic.</param>
        /// <returns>String representation of the <see cref="GeneticEntity"/> with the highest <see cref="GeneticEntity.ScaledFitnessValue"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            BestEntity bestEntity;
            if (!this.bestEntities.TryGetValue(population.Index, out bestEntity))
            {
                bestEntity = new BestEntity();
                this.bestEntities.Add(population.Index, bestEntity);
            }

            for (int i = 0; i < population.Entities.Count; i++)
            {
                double currentFitness = population.Entities[i].ScaledFitnessValue;
                if (i == 0 || currentFitness > bestEntity.Fitness)
                {
                    bestEntity.Fitness = currentFitness;
                    bestEntity.EntityRepresentation = population.Entities[i].Representation;
                }
            }

            return bestEntity.EntityRepresentation;
        }

        /// <summary>
        /// Sets the statistic's state.
        /// </summary>
        protected override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.bestEntities)] = this.bestEntities;
        }

        /// <summary>
        /// Restores the statistic's state.
        /// </summary>
        protected override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.bestEntities = (Dictionary<int, BestEntity>)state[nameof(this.bestEntities)];
        }
    }

    /// <summary>
    /// Represents the statistic data of the best <see cref="GeneticEntity"/> in the <see cref="Population"/>.
    /// </summary>
    public class BestEntity
    {
        /// <summary>
        /// Gets or sets the fitness value of the best entity.
        /// </summary>
        public double Fitness { get; set; }

        /// <summary>
        /// Gets or sets the representation of the entity.
        /// </summary>
        public string EntityRepresentation { get; set; }
    }

    /// <summary>
    /// Represents the configuration of <see cref="BestMaximumFitnessEntityStatistic"/>.
    /// </summary>
    [Component(typeof(BestMaximumFitnessEntityStatistic))]
    public class BestMaximumFitnessEntityStatisticConfiguration : StatisticConfiguration
    {
    }
}
