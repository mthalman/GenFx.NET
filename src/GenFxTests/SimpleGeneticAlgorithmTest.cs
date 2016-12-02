using GenFx;
using GenFx.ComponentLibrary.Algorithms;
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
            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            MockElitismStrategyConfiguration eliteConfig = new MockElitismStrategyConfiguration();
            eliteConfig.ElitistRatio = .1;
            algorithm.ConfigurationSet.ElitismStrategy = eliteConfig;

            MockSelectionOperatorConfiguration selConfig = new MockSelectionOperatorConfiguration();
            selConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = selConfig;

            MockCrossoverOperatorConfiguration crossConfig = new MockCrossoverOperatorConfiguration();
            crossConfig.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = crossConfig;

            MockMutationOperatorConfiguration mutConfig = new MockMutationOperatorConfiguration();
            mutConfig.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = mutConfig;

            algorithm.Operators.ElitismStrategy = new MockElitismStrategy(algorithm);
            algorithm.Operators.SelectionOperator = new MockSelectionOperator(algorithm);
            algorithm.Operators.CrossoverOperator = new MockCrossoverOperator(algorithm);
            algorithm.Operators.MutationOperator = new MockMutationOperator(algorithm);
            PrivateObject accessor = new PrivateObject(algorithm);

            Population population = GetPopulation(algorithm);

            int prevPopCount = population.Entities.Count;
            await (Task)accessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(1, ((MockElitismStrategy)algorithm.Operators.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism not called correctly.");
            Assert.AreEqual(4, ((MockSelectionOperator)algorithm.Operators.SelectionOperator).DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(2, ((MockCrossoverOperator)algorithm.Operators.CrossoverOperator).DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(4, ((MockMutationOperator)algorithm.Operators.MutationOperator).DoMutateCallCount, "Mutation not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        private static Population GetPopulation(GeneticAlgorithm algorithm)
        {
            Population population = new Population(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            return population;
        }
    }


}
