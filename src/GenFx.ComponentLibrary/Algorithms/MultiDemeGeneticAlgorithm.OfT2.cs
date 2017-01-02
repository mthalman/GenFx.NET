using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// A type of genetic algorithm that maintains multiple isolated populations and then migrates
    /// a select number of genetic entities between the populations after each generation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The number of <see cref="IGeneticEntity"/> objects that migrate each generation is determined by the 
    /// <see cref="MultiDemeGeneticAlgorithmConfiguration{TConfiguration, TAlgorithm}.MigrantCount"/> property value.  Those <see cref="IGeneticEntity"/>
    /// objects with the highest fitness value are the ones chosen to be migrated.
    /// </para>
    /// </remarks>
    /// <typeparam name="TAlgorithm">Type of the deriving algorithm class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    public abstract class MultiDemeGeneticAlgorithm<TAlgorithm, TConfiguration> : SimpleGeneticAlgorithm<TAlgorithm, TConfiguration>
        where TAlgorithm : MultiDemeGeneticAlgorithm<TAlgorithm, TConfiguration>
        where TConfiguration : MultiDemeGeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configurationSet">Contains the component configuration for the algorithm.</param>
        protected MultiDemeGeneticAlgorithm(ComponentConfigurationSet configurationSet)
            : base(configurationSet)
        {
            this.GenerationCreated += this.MultiDemeGeneticAlgorithm_GenerationCreated;
        }

        /// <summary>
        /// Handles the event when a generation has been created.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The event args.</param>
        private void MultiDemeGeneticAlgorithm_GenerationCreated(object sender, EventArgs e)
        {
            // We should only migrate on every generation increment as defined by the settings.
            if (this.CurrentGeneration % this.Configuration.MigrateEachGeneration == 0)
            {
                this.Migrate();
            }
        }

        /// <summary>
        /// Migrates the <see cref="IGeneticEntity"/> objects with the best fitness between populations.
        /// </summary>
        public void Migrate()
        {
            this.OnMigrate();
        }

        /// <summary>
        /// Migrates the <see cref="IGeneticEntity"/> objects with the best fitness between populations.
        /// </summary>
        protected virtual void OnMigrate()
        {
            IList<IPopulation> populations = this.Environment.Populations;

            // Build a list of migrant genetic entities from the first population
            List<IGeneticEntity> migrantGeneticEntities = new List<IGeneticEntity>(this.Configuration.MigrantCount);
            for (int i = 0; i < this.Configuration.MigrantCount; i++)
            {
                IGeneticEntity[] sortedEntities = populations[0].Entities.GetEntitiesSortedByFitness(
                    this.ConfigurationSet.SelectionOperator.SelectionBasedOnFitnessType,
                    this.ConfigurationSet.FitnessEvaluator.EvaluationMode).ToArray();
                migrantGeneticEntities.Add(sortedEntities[sortedEntities.Length - i - 1]);
            }

            // Migrate genetic entities between populations
            for (int populationIndex = 1; populationIndex < populations.Count; populationIndex++)
            {
                IPopulation population = populations[populationIndex];
                List<IGeneticEntity> sortedEntities = population.Entities.GetEntitiesSortedByFitness(
                    this.ConfigurationSet.SelectionOperator.SelectionBasedOnFitnessType,
                    this.ConfigurationSet.FitnessEvaluator.EvaluationMode).ToList();

                for (int entityIndex = 0; entityIndex < this.Configuration.MigrantCount; entityIndex++)
                {
                    // Add new migrant
                    population.Entities.Add(migrantGeneticEntities[entityIndex]);
                    sortedEntities.Add(migrantGeneticEntities[entityIndex]);

                    // Set entity to be replaced as a new migrant for next population.
                    IGeneticEntity migrant = sortedEntities[sortedEntities.Count - entityIndex - 2];
                    migrantGeneticEntities[entityIndex] = migrant;

                    // Remove the replaced entity.
                    population.Entities.Remove(migrant);
                    sortedEntities.Remove(migrant);
                }
            }

            IPopulation firstPopulation = populations[0];
            List<IGeneticEntity> firstPopulationSortedEntities = firstPopulation.Entities.GetEntitiesSortedByFitness(
                this.ConfigurationSet.SelectionOperator.SelectionBasedOnFitnessType,
                this.ConfigurationSet.FitnessEvaluator.EvaluationMode).ToList();

            // Use the migrants from the last population and migrate them to the first population.
            for (int entityIndex = 0; entityIndex < this.Configuration.MigrantCount; entityIndex++)
            {
                // Add new migrant
                firstPopulation.Entities.Add(migrantGeneticEntities[entityIndex]);
                firstPopulationSortedEntities.Add(migrantGeneticEntities[entityIndex]);

                IGeneticEntity replacedEntity = firstPopulationSortedEntities[firstPopulationSortedEntities.Count - entityIndex - 2];

                // Remove the replaced entity.
                firstPopulation.Entities.Remove(replacedEntity);
                firstPopulationSortedEntities.Remove(replacedEntity);
            }
        }
    }
}
