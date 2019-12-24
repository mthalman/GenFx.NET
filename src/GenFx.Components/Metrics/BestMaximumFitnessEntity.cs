using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx.Components.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the <see cref="GeneticEntity"/> object with the highest
    /// <see cref="GeneticEntity.ScaledFitnessValue"/> found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    [DataContract]
    public class BestMaximumFitnessEntity : Metric
    {
        [DataMember]
        private readonly Dictionary<int, GeneticEntity> bestEntities = new Dictionary<int, GeneticEntity>();
        
        /// <summary>
        /// Calculates to determine the <see cref="GeneticEntity"/> object with the highest
        /// <see cref="GeneticEntity.ScaledFitnessValue"/> found for a <see cref="Population"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the metric.</param>
        /// <returns>String representation of the <see cref="GeneticEntity"/> with the highest <see cref="GeneticEntity.ScaledFitnessValue"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int startIndex = 0;
            if (!this.bestEntities.TryGetValue(population.Index, out GeneticEntity bestEntity))
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
    }
}
