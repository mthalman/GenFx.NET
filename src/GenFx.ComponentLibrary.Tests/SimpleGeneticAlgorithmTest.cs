using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="SimpleGeneticAlgorithm"/> class.
    /// </summary>
    [TestClass()]
    public class SimpleGeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that the <see cref="SimpleGeneticAlgorithm.CreateNextGenerationAsync"/> method works correctly.
        /// </summary>
        [TestMethod]
        public async Task SimpleGeneticAlgorithm_CreateNextGeneration_Async()
        {
            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                ElitismStrategy = new MockElitismStrategy
                {
                    ElitistRatio = .1
                },
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };

            algorithm.ElitismStrategy = new MockElitismStrategy { ElitistRatio = .1 };
            algorithm.ElitismStrategy.Initialize(algorithm);
            algorithm.SelectionOperator = new MockSelectionOperator { SelectionBasedOnFitnessType = FitnessType.Scaled };
            algorithm.SelectionOperator.Initialize(algorithm);
            algorithm.CrossoverOperator = new MockCrossoverOperator { CrossoverRate = 1 };
            algorithm.CrossoverOperator.Initialize(algorithm);
            algorithm.MutationOperator = new MockMutationOperator { MutationRate = 1 };
            algorithm.MutationOperator.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(algorithm);

            SimplePopulation population = GetPopulation(algorithm, 10);

            List<GeneticEntity> originalEntities = new List<GeneticEntity>(population.Entities);

            int prevPopCount = population.Entities.Count;
            await (Task)accessor.Invoke("CreateNextGenerationAsync", population);

            // Find the number of entities in the new population that were in the original population
            int actualElitistEntitiesCount = population.Entities.Count(e => originalEntities.Contains(e));

            Assert.AreEqual(1, actualElitistEntitiesCount);
            Assert.AreEqual(1, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism not called correctly.");
            Assert.AreEqual(1, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(4, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(9, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }
        
        /// <summary>
        /// Tests that the an exception is thrown when passing a null population to <see cref="SimpleGeneticAlgorithm.CreateNextGenerationAsync"/>.
        /// </summary>
        [TestMethod]
        public async Task SimpleGeneticAlgorithm_CreateNextGeneration_NullPopulation()
        {
            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm();
            PrivateObject accessor = new PrivateObject(algorithm);
            await AssertEx.ThrowsAsync<ArgumentNullException>(() => (Task)accessor.Invoke("CreateNextGenerationAsync", (Population)null));
        }

        private static SimplePopulation GetPopulation(GeneticAlgorithm algorithm, int populationSize)
        {
            SimplePopulation population = new SimplePopulation { MinimumPopulationSize = populationSize };
            population.Initialize(algorithm);

            for (int i = 0; i < population.MinimumPopulationSize; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            
            return population;
        }
    }


}
