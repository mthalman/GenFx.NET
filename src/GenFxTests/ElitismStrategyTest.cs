using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
using GenFxTests.Helpers;
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
            Assert.IsInstanceOfType(strategy.Configuration, typeof(ElitismStrategyConfiguration));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new ElitismStrategy(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_Ctor_MissingSettings()
        {
            AssertEx.Throws<ArgumentException>(() => new ElitismStrategy(new MockGeneticAlgorithm(new ComponentFactoryConfigSet())));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [TestMethod]
        public void ElitismStrategy_Ctor_InvalidSetting1()
        {
            ElitismStrategyConfiguration config = new ElitismStrategyConfiguration();
            AssertEx.Throws<ValidationException>(() => config.ElitistRatio = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [TestMethod]
        public void ElitismStrategy_Ctor_InvalidSetting2()
        {
            ElitismStrategyConfiguration config = new ElitismStrategyConfiguration();
            AssertEx.Throws<ValidationException>(() => config.ElitistRatio = -1);
        }

        /// <summary>
        /// Tests that ApplyElitism works correctly.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_GetElitistGeneticEntities()
        {
            double elitismRatio = .1;
            int totalGeneticEntities = 100;
            IGeneticAlgorithm algorithm = GetGeneticAlgorithm(elitismRatio);
            SimplePopulation population = new SimplePopulation(algorithm);
            for (int i = 0; i < totalGeneticEntities; i++)
            {
                population.Entities.Add(new MockEntity(algorithm));
            }
            algorithm.Environment.Populations.Add(population);
            ElitismStrategy strategy = new ElitismStrategy(algorithm);

            IList<IGeneticEntity> geneticEntities = strategy.GetEliteEntities(population);

            Assert.AreEqual(Convert.ToInt32(Math.Round(elitismRatio * totalGeneticEntities)), geneticEntities.Count, "Incorrect number of elitist genetic entities.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_GetElitistGeneticEntities_NullPopulation()
        {
            ElitismStrategy strategy = new ElitismStrategy(GetGeneticAlgorithm(.1));
            AssertEx.Throws<ArgumentNullException>(() => strategy.GetEliteEntities(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_GetElitistGeneticEntities_EmptyPopulation()
        {
            IGeneticAlgorithm algorithm = GetGeneticAlgorithm(.1);
            ElitismStrategy strategy = new ElitismStrategy(algorithm);
            AssertEx.Throws<ArgumentException>(() => strategy.GetEliteEntities(new SimplePopulation(algorithm)));
        }

        private static MockGeneticAlgorithm GetGeneticAlgorithm(double elitismRatio)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                ElitismStrategy = new ElitismStrategyConfiguration
                {
                    ElitistRatio = elitismRatio
                }
            });
            return algorithm;
        }
    }
}
