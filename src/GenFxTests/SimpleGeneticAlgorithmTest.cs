using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
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
            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new SimpleGeneticAlgorithmFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                ElitismStrategy = new MockElitismStrategyFactoryConfig
                {
                    ElitistRatio = .1
                },
                SelectionOperator = new MockSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                CrossoverOperator = new MockCrossoverOperatorFactoryConfig
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperatorFactoryConfig
                {
                    MutationRate = 1
                }
            });

            algorithm.Operators.ElitismStrategy = new MockElitismStrategy(algorithm);
            algorithm.Operators.SelectionOperator = new MockSelectionOperator(algorithm);
            algorithm.Operators.CrossoverOperator = new MockCrossoverOperator(algorithm);
            algorithm.Operators.MutationOperator = new MockMutationOperator(algorithm);
            PrivateObject accessor = new PrivateObject(algorithm);

            SimplePopulation population = GetPopulation(algorithm);

            int prevPopCount = population.Entities.Count;
            await (Task)accessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(1, ((MockElitismStrategy)algorithm.Operators.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism not called correctly.");
            Assert.AreEqual(4, ((MockSelectionOperator)algorithm.Operators.SelectionOperator).DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(2, ((MockCrossoverOperator)algorithm.Operators.CrossoverOperator).DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(4, ((MockMutationOperator)algorithm.Operators.MutationOperator).DoMutateCallCount, "Mutation not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        private static SimplePopulation GetPopulation(IGeneticAlgorithm algorithm)
        {
            SimplePopulation population = new SimplePopulation(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            return population;
        }
    }


}
