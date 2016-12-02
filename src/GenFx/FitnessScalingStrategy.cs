using System;
using GenFx.ComponentModel;
using GenFx.Properties;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for the fitness scaling strategy of a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Fitness scaling is used to prevent the problem of premature convergence where too much emphasis
    /// is placed on the highly fit genetic entities in early generations causing loss of diversity.  Fitness
    /// scaling is a method of mapping raw fitness values to scaled fitness values so as to more easily
    /// control the diversity of a population.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.FitnessScalingStrategy"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class FitnessScalingStrategy : GeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.FitnessScalingStrategy; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessScalingStrategy"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="FitnessScalingStrategy"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FitnessScalingStrategy(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Updates the <see cref="GeneticEntity.ScaledFitnessValue"/>
        /// of the <see cref="GeneticEntity"/> objects in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> does not contain any entities.</exception>
        public void Scale(Population population)
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

            this.UpdateScaledFitnessValues(population);
        }

        /// <summary>
        /// When overriden in a derived class, updates the <see cref="GeneticEntity.ScaledFitnessValue"/>
        /// of the <see cref="GeneticEntity"/> objects in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects.</param>
        protected abstract void UpdateScaledFitnessValues(Population population);
    }

    /// <summary>
    /// Represents the configuration of <see cref="FitnessScalingStrategy"/>.
    /// </summary>
    [Component(typeof(FitnessScalingStrategy))]
    public abstract class FitnessScalingStrategyConfiguration : ComponentConfiguration
    {
    }
}
