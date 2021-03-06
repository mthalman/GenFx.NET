using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.Algorithms
{
    /// <summary>
    /// A type of genetic algorithm that maintains multiple isolated populations and then migrates
    /// a select number of genetic entities between the populations after each generation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The number of <see cref="GeneticEntity"/> objects that migrate each generation is determined by the 
    /// <see cref="MultiDemeGeneticAlgorithm.MigrantCount"/> property value.  Those <see cref="GeneticEntity"/>
    /// objects with the highest fitness value are the ones chosen to be migrated.
    /// </para>
    /// </remarks>
    [DataContract]
    public class MultiDemeGeneticAlgorithm : SimpleGeneticAlgorithm
    {
        private const int DefaultMigrantCount = 0;
        private const int DefaultMigrateEachGeneration = 1;

        [DataMember]
        private int migrantCount = DefaultMigrantCount;

        [DataMember]
        private int migrateEachGeneration = DefaultMigrateEachGeneration;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public MultiDemeGeneticAlgorithm()
        {
            this.GenerationCreated += this.MultiDemeGeneticAlgorithm_GenerationCreated;
        }

        /// <summary>
        /// Gets or sets the value indicating how many <see cref="GeneticEntity"/> objects are migrated between
        /// populations each generation.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 0)]
        public int MigrantCount
        {
            get { return this.migrantCount; }
            set { this.SetProperty(ref this.migrantCount, value); }
        }

        /// <summary>
        /// Gets or sets how many generations go by before each migration occurs.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int MigrateEachGeneration
        {
            get { return this.migrateEachGeneration; }
            set { this.SetProperty(ref this.migrateEachGeneration, value); }
        }

        /// <summary>
        /// Handles the event when a generation has been created.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The event args.</param>
        private void MultiDemeGeneticAlgorithm_GenerationCreated(object sender, EventArgs e)
        {
            // We should only migrate on every generation increment as defined by the settings.
            if (this.CurrentGeneration % this.MigrateEachGeneration == 0)
            {
                this.Migrate();
            }
        }

        /// <summary>
        /// Migrates the <see cref="GeneticEntity"/> objects with the best fitness between populations.
        /// </summary>
        public void Migrate()
        {
            this.OnMigrate();
        }

        /// <summary>
        /// Migrates the <see cref="GeneticEntity"/> objects with the best fitness between populations.
        /// </summary>
        protected virtual void OnMigrate()
        {
            this.AssertIsInitialized();

            IList<Population> populations = this.Environment!.Populations;

            // Build a list of migrant genetic entities from the first population
            List<GeneticEntity> migrantGeneticEntities = new List<GeneticEntity>(this.MigrantCount);
            for (int i = 0; i < this.MigrantCount; i++)
            {
                GeneticEntity[] sortedEntities = populations[0].Entities.GetEntitiesSortedByFitness(
                    this.SelectionOperator!.SelectionBasedOnFitnessType,
                    this.FitnessEvaluator!.EvaluationMode).ToArray();
                migrantGeneticEntities.Add(sortedEntities[sortedEntities.Length - i - 1]);
            }

            // Migrate genetic entities between populations
            for (int populationIndex = 1; populationIndex < populations.Count; populationIndex++)
            {
                Population population = populations[populationIndex];
                List<GeneticEntity> sortedEntities = population.Entities.GetEntitiesSortedByFitness(
                    this.SelectionOperator!.SelectionBasedOnFitnessType,
                    this.FitnessEvaluator!.EvaluationMode).ToList();

                for (int entityIndex = 0; entityIndex < this.MigrantCount; entityIndex++)
                {
                    // Add new migrant
                    population.Entities.Add(migrantGeneticEntities[entityIndex]);
                    sortedEntities.Add(migrantGeneticEntities[entityIndex]);

                    // Set entity to be replaced as a new migrant for next population.
                    GeneticEntity migrant = sortedEntities[sortedEntities.Count - entityIndex - 2];
                    migrantGeneticEntities[entityIndex] = migrant;

                    // Remove the replaced entity.
                    population.Entities.Remove(migrant);
                    sortedEntities.Remove(migrant);
                }
            }

            Population firstPopulation = populations[0];
            List<GeneticEntity> firstPopulationSortedEntities = firstPopulation.Entities.GetEntitiesSortedByFitness(
                this.SelectionOperator!.SelectionBasedOnFitnessType,
                this.FitnessEvaluator!.EvaluationMode).ToList();

            // Use the migrants from the last population and migrate them to the first population.
            for (int entityIndex = 0; entityIndex < this.MigrantCount; entityIndex++)
            {
                // Add new migrant
                firstPopulation.Entities.Add(migrantGeneticEntities[entityIndex]);
                firstPopulationSortedEntities.Add(migrantGeneticEntities[entityIndex]);

                GeneticEntity replacedEntity = firstPopulationSortedEntities[firstPopulationSortedEntities.Count - entityIndex - 2];

                // Remove the replaced entity.
                firstPopulation.Entities.Remove(replacedEntity);
                firstPopulationSortedEntities.Remove(replacedEntity);
            }
        }
    }
}
