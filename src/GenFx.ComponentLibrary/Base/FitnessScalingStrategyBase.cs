using GenFx.ComponentModel;
using GenFx.Properties;
using System;

namespace GenFx.ComponentLibrary.Base
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
    public abstract class FitnessScalingStrategyBase<TScaling, TConfiguration> : GeneticComponentWithAlgorithm<TScaling, TConfiguration>, IFitnessScalingStrategy
        where TScaling : FitnessScalingStrategyBase<TScaling, TConfiguration>
        where TConfiguration : FitnessScalingStrategyConfigurationBase<TConfiguration, TScaling>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FitnessScalingStrategyBase(IGeneticAlgorithm algorithm)
            : base(algorithm, GetConfiguration(algorithm, c => c.FitnessScalingStrategy))
        {
        }

        /// <summary>
        /// Updates the <see cref="IGeneticEntity.ScaledFitnessValue"/>
        /// of the <see cref="IGeneticEntity"/> objects in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> does not contain any entities.</exception>
        public void Scale(IPopulation population)
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
        /// When overriden in a derived class, updates the <see cref="IGeneticEntity.ScaledFitnessValue"/>
        /// of the <see cref="IGeneticEntity"/> objects in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects.</param>
        protected abstract void UpdateScaledFitnessValues(IPopulation population);
    }
}
