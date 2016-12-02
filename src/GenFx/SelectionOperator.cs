using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Properties;
using GenFx.Validation;

namespace GenFx
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
    public abstract class SelectionOperator : GeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the <see cref="FitnessType"/> to base selection of <see cref="GeneticEntity"/> objects on.
        /// </summary>
        public FitnessType SelectionBasedOnFitnessType
        {
            get { return this.Algorithm.ConfigurationSet.SelectionOperator.SelectionBasedOnFitnessType; }
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.SelectionOperator; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="SelectionOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected SelectionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Selects a <see cref="GeneticEntity"/> from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> does not contain any entities.</exception>
        public GeneticEntity Select(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            if (population.Entities.Count == 0)
            {
                throw new ArgumentException(
                  StringUtil.GetFormattedString(FwkResources.ErrorMsg_EntityListEmpty), nameof(population));
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

    /// <summary>
    /// Represents the configuration of <see cref="SelectionOperator"/>.
    /// </summary>
    [Component(typeof(SelectionOperator))]
    public abstract class SelectionOperatorConfiguration : ComponentConfiguration
    {
        private const FitnessType DefaultSelectionBasedOnFitnessType = FitnessType.Scaled;

        private FitnessType selectionBasedOnFitnessType = SelectionOperatorConfiguration.DefaultSelectionBasedOnFitnessType;

        /// <summary>
        /// Gets or sets the <see cref="FitnessType"/> to base selection of <see cref="GeneticEntity"/> objects on.
        /// </summary>
        /// <exception cref="ValidationException">Value is undefined.</exception>
        [FitnessTypeValidator]
        public FitnessType SelectionBasedOnFitnessType
        {
            get { return this.selectionBasedOnFitnessType; }
            set { this.SetProperty(ref this.selectionBasedOnFitnessType, value); }
        }
    }
}
