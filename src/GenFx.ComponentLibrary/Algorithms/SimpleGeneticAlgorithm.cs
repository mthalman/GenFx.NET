using GenFx.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the most basic type of genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <b>SimpleGeneticAlgorithm</b> can operate multiple <see cref="IPopulation"/> objects but
    /// they run isolated from one another.
    /// </remarks>
    public class SimpleGeneticAlgorithm : GeneticAlgorithm
    {
        /// <summary>
        /// Modifies <paramref name="population"/> to become the next generation of <see cref="IGeneticEntity"/> objects.
        /// </summary>
        /// <param name="population">The current <see cref="IPopulation"/> to be modified.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override Task CreateNextGenerationAsync(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            IList<IGeneticEntity> eliteGeneticEntities = this.ApplyElitism(population);

            int populationCount = population.Entities.Count;

            ObservableCollection<IGeneticEntity> nextGeneration = new ObservableCollection<IGeneticEntity>();

            foreach (IGeneticEntity entity in eliteGeneticEntities)
            {
                nextGeneration.Add(entity);
                population.Entities.Remove(entity);
            }

            while (nextGeneration.Count != populationCount)
            {
                IList<IGeneticEntity> childGeneticEntities = this.SelectGeneticEntitiesAndApplyCrossoverAndMutation(population);

                foreach (IGeneticEntity entity in childGeneticEntities)
                {
                    if (nextGeneration.Count != populationCount)
                    {
                        nextGeneration.Add(entity);
                    }
                }
            }

            population.Entities.Clear();
            population.Entities.AddRange(nextGeneration);

            return Task.FromResult(true);
        }
    }
}
