using GenFx.ComponentLibrary.ComponentModel;
using System;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm selection operator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Selection in a genetic algorithm involves choosing a entity from a population to be acted
    /// upon by other operators, such as crossover and mutation, and move to the next generation.  The
    /// general strategy is for a entity to have a higher probability of being selected if it has a higher
    /// fitness value.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.SelectionOperator"/> 
    /// property.
    /// </para>
    /// </remarks>
    /// <typeparam name="TSelection">Type of the deriving selection operator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class SelectionOperatorBase<TSelection, TConfiguration> : GeneticComponentWithAlgorithm<TSelection, TConfiguration>, ISelectionOperator
        where TSelection : SelectionOperatorBase<TSelection, TConfiguration>
        where TConfiguration : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected SelectionOperatorBase(IGeneticAlgorithm algorithm)
            : base(algorithm, GetConfiguration(algorithm, c => c.SelectionOperator))
        {
        }

        /// <summary>
        /// Selects a <see cref="IGeneticEntity"/> from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="IGeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> does not contain any entities.</exception>
        public IGeneticEntity SelectEntity(IPopulation population)
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
        /// When overriden in a derived class, selects a <see cref="IGeneticEntity"/> from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="IGeneticEntity"/> object that was selected.</returns>
        protected abstract IGeneticEntity SelectEntityFromPopulation(IPopulation population);
    }
}
