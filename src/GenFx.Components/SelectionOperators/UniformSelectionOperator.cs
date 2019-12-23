using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx.Components.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby all <see cref="GeneticEntity"/> objects have an equal
    /// probability of being selected regardless of fitness.
    /// </summary>
    [DataContract]
    public class UniformSelectionOperator : SelectionOperator
    {
        /// <summary>
        /// Selects the specified number of <see cref="GeneticEntity"/> objects from <paramref name="population"/>.
        /// </summary>
        /// <param name="entityCount">Number of <see cref="GeneticEntity"/> objects to select from the population.</param>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects from which to select.
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            List<GeneticEntity> result = new List<GeneticEntity>();
            for (int i = 0; i < entityCount; i++)
            {
                int selectedEntityIndex = RandomNumberService.Instance.GetRandomValue(population.Entities.Count);
                result.Add(population.Entities[selectedEntityIndex]);
            }

            return result;
        }
    }
}
