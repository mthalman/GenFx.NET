using System;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby all <see cref="GeneticEntity"/> objects have an equal
    /// probability of being selected regardless of fitness.
    /// </summary>
    public class UniformSelectionOperator : SelectionOperator
    {
        /// <summary>
        /// Selects a <see cref="GeneticEntity"/> from <paramref name="population"/> with all <see cref="GeneticEntity"/>
        /// objects having an equal probability of being selected.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override GeneticEntity SelectEntityFromPopulation(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int selectedEntityIndex = RandomNumberService.Instance.GetRandomValue(population.Entities.Count);
            return population.Entities[selectedEntityIndex];
        }
    }
}
