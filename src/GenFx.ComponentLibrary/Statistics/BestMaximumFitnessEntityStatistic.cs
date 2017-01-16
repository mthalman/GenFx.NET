using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the <see cref="GeneticEntity"/> object with the highest
    /// <see cref="GeneticEntity.ScaledFitnessValue"/> found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    public class BestMaximumFitnessEntityStatistic : Statistic
    {
        private Dictionary<int, GeneticEntity> bestEntities = new Dictionary<int, GeneticEntity>();
        
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

            int startIndex = 0;
            GeneticEntity bestEntity;
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
            foreach (KeyValuePair<int, GeneticEntity> kvp in this.bestEntities)
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
            this.bestEntities = new Dictionary<int, GeneticEntity>();
            foreach (KeyValuePair<int, KeyValueMap> kvp in savedEntitiesByPopulation)
            {
                GeneticEntity entity = (GeneticEntity)this.Algorithm.GeneticEntitySeed.CreateNew();
                entity.RestoreState(kvp.Value);
                this.bestEntities.Add(kvp.Key, entity);
            }
        }
    }
}
