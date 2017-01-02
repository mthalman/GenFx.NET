using GenFx.ComponentLibrary.Base;
using System;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby all <see cref="IGeneticEntity"/> objects have an equal
    /// probability of being selected regardless of fitness.
    /// </summary>
    /// <typeparam name="TSelection">Type of the deriving selection operator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class UniformSelectionOperator<TSelection, TConfiguration> : SelectionOperatorBase<TSelection, TConfiguration>
        where TSelection : UniformSelectionOperator<TSelection, TConfiguration>
        where TConfiguration : UniformSelectionOperatorConfiguration<TConfiguration, TSelection>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected UniformSelectionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
