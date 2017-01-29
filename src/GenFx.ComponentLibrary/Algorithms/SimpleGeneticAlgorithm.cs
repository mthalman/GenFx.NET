using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the most basic type of genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <b>SimpleGeneticAlgorithm</b> can operate multiple <see cref="Population"/> objects but
    /// they run isolated from one another.
    /// </remarks>
    [DataContract]
    public class SimpleGeneticAlgorithm : GeneticAlgorithm
    {
        /// <summary>
        /// Modifies <paramref name="population"/> to become the next generation of <see cref="GeneticEntity"/> objects.
        /// </summary>
        /// <param name="population">The current <see cref="Population"/> to be modified.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override Task CreateNextGenerationAsync(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            IList<GeneticEntity> eliteGeneticEntities = this.ApplyElitism(population);

            ObservableCollection<GeneticEntity> nextGeneration = new ObservableCollection<GeneticEntity>();

            foreach (GeneticEntity entity in eliteGeneticEntities)
            {
                nextGeneration.Add(entity);
                population.Entities.Remove(entity);
            }

            IList<GeneticEntity> parents = this.ApplySelection(population.MinimumPopulationSize - nextGeneration.Count, population);
            IList<GeneticEntity> offspring = this.ApplyCrossover(population, parents);
            offspring = this.ApplyMutation(offspring);
            nextGeneration.AddRange(offspring);

            population.Entities.Clear();
            population.Entities.AddRange(nextGeneration);

            return Task.FromResult(true);
        }
    }
}
