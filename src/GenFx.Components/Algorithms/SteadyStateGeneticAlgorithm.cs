using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GenFx.Components.Algorithms
{
    /// <summary>
    /// A type of genetic algorithm that replaces the weakest members of a <see cref="Population"/>
    /// with the offspring of the previous generation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Usage of an <see cref="ElitismStrategy"/> type with this algorithm will result in elitism being ignored
    /// since all high-fitness <see cref="GeneticEntity"/> objects will be moved to the next generation anyways.
    /// </para>
    /// </remarks>
    [DataContract]
    public class SteadyStateGeneticAlgorithm : GeneticAlgorithm
    {
        [DataMember]
        private PopulationReplacementValue replacementValue = new PopulationReplacementValue(10, ReplacementValueKind.Percentage);
        
        /// <summary>
        /// Gets or sets the value indicating how many members of the the <see cref="Population"/> are to 
        /// be replaced with the offspring of the previous generation.
        /// </summary>
        /// <value>
        /// A value representing a fixed amount of <see cref="GeneticEntity"/> objects to be replaced
        /// or the percentage that is to be replaced.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [CustomPropertyValidator(typeof(PopulationReplacementValueValidator))]
        public PopulationReplacementValue PopulationReplacementValue
        {
            get { return this.replacementValue; }
            set { this.SetProperty(ref this.replacementValue, value); }
        }

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

            this.AssertIsInitialized();

            int populationCount = population.Entities.Count;
            int replacementCount;
            if (this.PopulationReplacementValue.Kind == ReplacementValueKind.Percentage)
            {
                replacementCount = Convert.ToInt32(
                    Math.Round(
                        populationCount * ((double)this.PopulationReplacementValue.Value / 100)
                    ));
            }
            else
            {
                replacementCount = this.PopulationReplacementValue.Value;
            }

            // Add a select number of potentially modified Entities to the new generation.
            IList<GeneticEntity> parents = this.ApplySelection(replacementCount, population);
            IList<GeneticEntity> offspring = this.ApplyCrossover(population, parents);
            offspring = this.ApplyMutation(offspring);
            population.Entities.AddRange(offspring.Take(replacementCount));
            
            // Remove the weakest Entities from the population.
            ObservableCollection<GeneticEntity> workingGeneticEntities = new ObservableCollection<GeneticEntity>(population.Entities);
            GeneticEntity[] sortedEntities = workingGeneticEntities.GetEntitiesSortedByFitness(
                this.SelectionOperator!.SelectionBasedOnFitnessType,
                this.FitnessEvaluator!.EvaluationMode).ToArray();

            population.Entities.Clear();
            for (int i = sortedEntities.Length - 1; i >= sortedEntities.Length - populationCount; i--)
            {
                population.Entities.Add(sortedEntities[i]);
            }

            return Task.FromResult(true);
        }
    }
}
