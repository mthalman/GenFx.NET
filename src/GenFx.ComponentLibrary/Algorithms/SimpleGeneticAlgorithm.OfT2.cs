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
    public abstract class SimpleGeneticAlgorithm<TAlgorithm, TConfiguration> : GeneticAlgorithm<TAlgorithm, TConfiguration>
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
        where TConfiguration : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configurationSet">Contains the component configuration for the algorithm.</param>
        protected SimpleGeneticAlgorithm(ComponentConfigurationSet configurationSet)
            : base(configurationSet)
        {
        }

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

        /// <summary>
        /// Returns whether the child <see cref="IPopulation"/> has reached the limit of <see cref="IGeneticEntity"/> objects.
        /// </summary>
        /// <param name="nextGeneration"><see cref="IPopulation"/> to test.</param>
        /// <param name="previousGeneration"><see cref="IPopulation"/> that is the previous generation.</param>
        /// <returns>true if the child population is full; otherwise, false.</returns>
        private static bool IsChildPopulationFull(ObservableCollection<IGeneticEntity> nextGeneration, ObservableCollection<IGeneticEntity> previousGeneration)
        {
            return (nextGeneration.Count >= previousGeneration.Count);
        }
    }
}
