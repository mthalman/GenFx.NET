using GenFx;
using GenFx.ComponentModel;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ElitismStrategy and is intended
    ///to contain all GenFx.ElitismStrategy Unit Tests
    ///</summary>
    [TestClass()]
    public class ElitismStrategyTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_Ctor()
        {
            double elitistRatio = .1;
            MockGeneticAlgorithm algorithm = GetGeneticAlgorithm(elitistRatio);
            ElitismStrategy strategy = new ElitismStrategy(algorithm);
            Assert.AreEqual(elitistRatio, strategy.ElitistRatio, "ElitistRatio was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ElitismStrategy_Ctor_NullAlgorithm()
        {
            ElitismStrategy strategy = new ElitismStrategy(null);
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ElitismStrategy_Ctor_MissingSettings()
        {
            ElitismStrategy strategy = new ElitismStrategy(new MockGeneticAlgorithm());
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ElitismStrategy_Ctor_InvalidSetting1()
        {
            ElitismStrategyConfiguration config = new ElitismStrategyConfiguration();
            config.ElitistRatio = 2;
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ElitismStrategy_Ctor_InvalidSetting2()
        {
            ElitismStrategyConfiguration config = new ElitismStrategyConfiguration();
            config.ElitistRatio = -1;
        }

        /// <summary>
        /// Tests that ApplyElitism works correctly.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_GetElitistGeneticEntities()
        {
            double elitismRatio = .1;
            int totalGeneticEntities = 100;
            GeneticAlgorithm algorithm = GetGeneticAlgorithm(elitismRatio);
            Population population = new Population(algorithm);
            for (int i = 0; i < totalGeneticEntities; i++)
            {
                population.Entities.Add(new MockEntity(algorithm));
            }
            algorithm.Environment.Populations.Add(population);
            ElitismStrategy strategy = new ElitismStrategy(algorithm);

            MockSelectionOperatorConfiguration selectionConfig = new MockSelectionOperatorConfiguration();
            algorithm.ConfigurationSet.SelectionOperator = selectionConfig;

            MockFitnessEvaluatorConfiguration fitnessConfig = new MockFitnessEvaluatorConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = fitnessConfig;

            IList<GeneticEntity> geneticEntities = strategy.GetElitistGeneticEntities(population);

            Assert.AreEqual(Convert.ToInt32(Math.Round(elitismRatio * totalGeneticEntities)), geneticEntities.Count, "Incorrect number of elitist genetic entities.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ElitismStrategy_GetElitistGeneticEntities_NullPopulation()
        {
            ElitismStrategy strategy = new ElitismStrategy(GetGeneticAlgorithm(.1));
            strategy.GetElitistGeneticEntities(null);
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ElitismStrategy_GetElitistGeneticEntities_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetGeneticAlgorithm(.1);
            ElitismStrategy strategy = new ElitismStrategy(algorithm);
            strategy.GetElitistGeneticEntities(new Population(algorithm));
        }

        private static MockGeneticAlgorithm GetGeneticAlgorithm(double elitismRatio)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            ElitismStrategyConfiguration config = new ElitismStrategyConfiguration();
            config.ElitistRatio = elitismRatio;
            algorithm.ConfigurationSet.ElitismStrategy = config;
            return algorithm;
        }

    }


}
