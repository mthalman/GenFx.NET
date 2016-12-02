using System;
using System.ComponentModel;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby all <see cref="GeneticEntity"/> objects have an equal
    /// probability of being selected regardless of fitness.
    /// </summary>
    public class UniformSelectionOperator : SelectionOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniformSelectionOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="UniformSelectionOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public UniformSelectionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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

            int selectedEntityIndex = RandomHelper.Instance.GetRandomValue(population.Entities.Count);
            return population.Entities[selectedEntityIndex];
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="UniformSelectionOperator"/>.
    /// </summary>
    [Component(typeof(UniformSelectionOperator))]
    public class UniformSelectionOperatorConfiguration : SelectionOperatorConfiguration
    {
    }
}
