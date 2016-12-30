using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// A type of genetic algorithm that replaces the weakest members of a <see cref="IPopulation"/>
    /// with the offspring of the previous generation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Usage of an <see cref="IElitismStrategy"/> type with this algorithm will result in elitism being ignored
    /// since all high-fitness <see cref="IGeneticEntity"/> objects will be moved to the next generation anyways.
    /// </para>
    /// </remarks>
    public abstract class SteadyStateGeneticAlgorithm<TAlgorithm, TConfiguration> : GeneticAlgorithm<TAlgorithm, TConfiguration>
        where TAlgorithm : SteadyStateGeneticAlgorithm<TAlgorithm, TConfiguration>
        where TConfiguration : SteadyStateGeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configurationSet">Contains the component configuration for the algorithm.</param>
        protected SteadyStateGeneticAlgorithm(ComponentConfigurationSet configurationSet)
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

            int populationCount = population.Entities.Count;
            PopulationReplacementValue replacementValue = this.Configuration.PopulationReplacementValue;
            int replacementCount;
            if (replacementValue.Kind == ReplacementValueKind.Percentage)
            {
                replacementCount = Convert.ToInt32(
                    Math.Round(
                        populationCount * ((double)replacementValue.Value / 100)
                    ));
            }
            else
            {
                replacementCount = replacementValue.Value;
            }

            // Add a select number of potentially modified Entities to the new generation.
            for (int i = 0; i < replacementCount; i++)
            {
                IList<IGeneticEntity> childEntities = this.SelectGeneticEntitiesAndApplyCrossoverAndMutation(population);

                for (int entityIndex = 0; entityIndex < childEntities.Count; entityIndex++)
                {
                    population.Entities.Add(childEntities[entityIndex]);
                }
            }

            // Remove the weakest Entities from the population.
            ObservableCollection<IGeneticEntity> workingGeneticEntities = new ObservableCollection<IGeneticEntity>(population.Entities);
            IGeneticEntity[] sortedEntities = workingGeneticEntities.GetEntitiesSortedByFitness(
                this.ConfigurationSet.SelectionOperator.SelectionBasedOnFitnessType,
                this.ConfigurationSet.FitnessEvaluator.EvaluationMode).ToArray();

            population.Entities.Clear();
            for (int i = sortedEntities.Length - 1; i >= sortedEntities.Length - populationCount; i--)
            {
                population.Entities.Add(sortedEntities[i]);
            }

            return Task.FromResult(true);
        }
    }
}
