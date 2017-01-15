using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby all <see cref="IGeneticEntity"/> objects have an equal
    /// probability of being selected regardless of fitness.
    /// </summary>
    public class UniformSelectionOperator : SelectionOperatorBase
    {
        /// <summary>
        /// Selects a <see cref="IGeneticEntity"/> from <paramref name="population"/> with all <see cref="IGeneticEntity"/>
        /// objects having an equal probability of being selected.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="IGeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
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
