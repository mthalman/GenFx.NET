using GenFx.Validation;
using System;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm selection operator.
    /// </summary>
    /// <remarks>
    /// Selection in a genetic algorithm involves choosing a entity from a population to be acted
    /// upon by other operators, such as crossover and mutation, and move to the next generation.  The
    /// general strategy is for a entity to have a higher probability of being selected if it has a higher
    /// fitness value.
    /// </remarks>
    public abstract class SelectionOperator : GeneticComponentWithAlgorithm
    {
        private const FitnessType DefaultSelectionBasedOnFitnessType = FitnessType.Scaled;

        private FitnessType selectionBasedOnFitnessType = DefaultSelectionBasedOnFitnessType;

        /// <summary>
        /// Gets or sets the <see cref="FitnessType"/> to base selection of <see cref="GeneticEntity"/> objects on.
        /// </summary>
        /// <exception cref="ValidationException">Value is undefined.</exception>
        [ConfigurationProperty]
        [FitnessTypeValidator]
        public FitnessType SelectionBasedOnFitnessType
        {
            get { return this.selectionBasedOnFitnessType; }
            set { this.SetProperty(ref this.selectionBasedOnFitnessType, value); }
        }

        /// <summary>
        /// Selects a <see cref="GeneticEntity"/> from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> does not contain any entities.</exception>
        public GeneticEntity SelectEntity(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            if (population.Entities.Count == 0)
            {
                throw new ArgumentException(
                  StringUtil.GetFormattedString(Resources.ErrorMsg_EntityListEmpty), nameof(population));
            }

            return this.SelectEntityFromPopulation(population);
        }

        /// <summary>
        /// When overriden in a derived class, selects a <see cref="GeneticEntity"/> from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        protected abstract GeneticEntity SelectEntityFromPopulation(Population population);
    }
}
