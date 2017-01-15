using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides the abstract base class for the fitness scaling strategy of a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// Fitness scaling is used to prevent the problem of premature convergence where too much emphasis
    /// is placed on the highly fit genetic entities in early generations causing loss of diversity.  Fitness
    /// scaling is a method of mapping raw fitness values to scaled fitness values so as to more easily
    /// control the diversity of a population.
    /// </remarks>
    public abstract class FitnessScalingStrategyBase : GeneticComponentWithAlgorithm, IFitnessScalingStrategy
    {
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
                  StringUtil.GetFormattedString(Resources.ErrorMsg_EntityListEmpty), nameof(population));
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
