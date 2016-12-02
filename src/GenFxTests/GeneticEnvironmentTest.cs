using GenFx;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.GeneticEnvironment and is intended
    /// to contain all GenFx.GeneticEnvironment Unit Tests
    ///</summary>
    [TestClass()]
    public class GeneticEnvironmentTest
    {
        /// <summary>
        /// Tests that EvaluateFitness works correctly.
        /// </summary>
        [TestMethod()]
        public async Task GeneticEnvironment_EvaluateFitness_Async()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            MockSelectionOperatorConfiguration config = new MockSelectionOperatorConfiguration();
            config.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = config;
            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);
            algorithm.Operators.SelectionOperator = new MockSelectionOperator(algorithm);

            GeneticEnvironment environment = new GeneticEnvironment(algorithm);

            Population population1 = GetPopulation(algorithm);
            Population population2 = GetPopulation(algorithm);
            environment.Populations.Add(population1);
            environment.Populations.Add(population2);

            await environment.EvaluateFitnessAsync();
            VerifyFitnessEvaluation(population1);
            VerifyFitnessEvaluation(population2);
        }

        /// <summary>
        /// Tests that Generate works correctly.
        /// </summary>
        [TestMethod()]
        public async Task GeneticEnvironment_Initialize_Async()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            int environmentSize = 2;
            int populationSize = 5;

            MockGeneticAlgorithmConfiguration algConfig = new MockGeneticAlgorithmConfiguration();
            algConfig.EnvironmentSize = environmentSize;
            algorithm.ConfigurationSet.GeneticAlgorithm = algConfig;

            MockPopulationConfiguration popConfig = new MockPopulationConfiguration();
            popConfig.PopulationSize = populationSize;
            algorithm.ConfigurationSet.Population = popConfig;

            GeneticEnvironment environment = new GeneticEnvironment(algorithm);

            await environment.InitializeAsync();

            Assert.AreEqual(environmentSize, environment.Populations.Count, "Incorrect number of populations created.");

            for (int i = 0; i < environmentSize; i++)
            {
                Assert.AreEqual(i, environment.Populations[i].Index, "Population {0}: Index not set correctly.", i);
                Assert.IsInstanceOfType(environment.Populations[i], typeof(MockPopulation), "Population {0}: Incorrect population type created.", i);
                Assert.AreEqual(populationSize, environment.Populations[i].Entities.Count, "Population {0}: Incorrect number of genetic entities created.", i);
            }
        }

        private static Population GetPopulation(GeneticAlgorithm algorithm)
        {
            Population population = new Population(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            entity1.Identifier = "5";
            MockEntity entity2 = new MockEntity(algorithm);
            entity2.Identifier = "2";
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            return population;
        }

        private static void VerifyFitnessEvaluation(Population population)
        {
            Assert.AreEqual("5", ((MockEntity)population.Entities[0]).Identifier, "Entity was not sorted correctly.");
            Assert.AreEqual("2", ((MockEntity)population.Entities[1]).Identifier, "Entity was not sorted correctly.");

            Assert.AreEqual((double)5, population.Entities[0].RawFitnessValue, "Fitness value was not evaluated correctly.");
            Assert.AreEqual((double)2, population.Entities[1].RawFitnessValue, "Fitness value was not evaluated correctly.");
        }
    }
}
