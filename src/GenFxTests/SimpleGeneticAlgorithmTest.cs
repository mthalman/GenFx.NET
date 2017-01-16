using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Algorithms.SimpleGeneticAlgorithm and is intended
    ///to contain all GenFx.ComponentLibrary.Algorithms.SimpleGeneticAlgorithm Unit Tests
    ///</summary>
    [TestClass()]
    public class SimpleGeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
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

            SimplePopulation population = GetPopulation(algorithm);

            int prevPopCount = population.Entities.Count;
            await (Task)accessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(1, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism not called correctly.");
            Assert.AreEqual(4, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(2, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(4, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        private static SimplePopulation GetPopulation(GeneticAlgorithm algorithm)
        {
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);

            for (int i = 0; i < 3; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            
            return population;
        }
    }


}
