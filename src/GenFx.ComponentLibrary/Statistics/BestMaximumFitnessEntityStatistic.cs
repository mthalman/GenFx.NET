using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the <see cref="IGeneticEntity"/> object with the highest
    /// <see cref="IGeneticEntity.ScaledFitnessValue"/> found for a <see cref="IPopulation"/> during the entire run of the genetic algorithm.
    /// </summary>
    public sealed class BestMaximumFitnessEntityStatistic : StatisticBase<BestMaximumFitnessEntityStatistic, BestMaximumFitnessEntityStatisticFactoryConfig>
    {
        private Dictionary<int, IGeneticEntity> bestEntities = new Dictionary<int, IGeneticEntity>();

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public BestMaximumFitnessEntityStatistic(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates to determine the <see cref="IGeneticEntity"/> object with the highest
        /// <see cref="IGeneticEntity.ScaledFitnessValue"/> found for a <see cref="IPopulation"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> from which to derive the statistic.</param>
        /// <returns>String representation of the <see cref="IGeneticEntity"/> with the highest <see cref="IGeneticEntity.ScaledFitnessValue"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int startIndex = 0;
            IGeneticEntity bestEntity;
            if (!this.bestEntities.TryGetValue(population.Index, out bestEntity))
            {
                if (population.Entities.Count > 0)
                {
                    bestEntity = population.Entities[0].Clone();
                    this.bestEntities.Add(population.Index, bestEntity);
                    startIndex = 1;
                }
            }

            for (int i = startIndex; i < population.Entities.Count; i++)
            {
                double currentFitness = population.Entities[i].ScaledFitnessValue;
                if (currentFitness > bestEntity.ScaledFitnessValue)
                {
                    bestEntity = population.Entities[i].Clone();
                    this.bestEntities[population.Index] = bestEntity;
                }
            }

            return bestEntity;
        }

        /// <summary>
        /// Sets the statistic's state.
        /// </summary>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            Dictionary<int, KeyValueMap> savedEntitiesByPopulation = new Dictionary<int, KeyValueMap>();
            foreach (KeyValuePair<int, IGeneticEntity> kvp in this.bestEntities)
            {
                KeyValueMap keyValueMap = new KeyValueMap();
                kvp.Value.SetSaveState(keyValueMap);
                savedEntitiesByPopulation.Add(kvp.Key, keyValueMap);
            }

            state[nameof(this.bestEntities)] = savedEntitiesByPopulation;
        }

        /// <summary>
        /// Restores the statistic's state.
        /// </summary>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            Dictionary<int, KeyValueMap> savedEntitiesByPopulation = (Dictionary<int, KeyValueMap>)state[nameof(this.bestEntities)];
            this.bestEntities = new Dictionary<int, IGeneticEntity>();
            foreach (KeyValuePair<int, KeyValueMap> kvp in savedEntitiesByPopulation)
            {
                IGeneticEntity entity = (IGeneticEntity)this.Algorithm.ConfigurationSet.Entity.CreateComponent(this.Algorithm);
                entity.RestoreState(kvp.Value);
                this.bestEntities.Add(kvp.Key, entity);
            }
        }
    }
}
