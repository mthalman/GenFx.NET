using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_Ctor_NullAlgorithm()
        {
            ElitismStrategy strategy = new ElitismStrategy();
            AssertEx.Throws<ArgumentNullException>(() => strategy.Initialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [TestMethod]
        public void ElitismStrategy_Ctor_InvalidSetting1()
        {
            ElitismStrategy strategy = new ElitismStrategy();
            AssertEx.Throws<ValidationException>(() => strategy.ElitistRatio = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [TestMethod]
        public void ElitismStrategy_Ctor_InvalidSetting2()
        {
            ElitismStrategy strategy = new ElitismStrategy();
            AssertEx.Throws<ValidationException>(() => strategy.ElitistRatio = -1);
        }

        /// <summary>
        /// Tests that ApplyElitism works correctly.
        /// </summary>
        [TestMethod()]
        public async Task ElitismStrategy_GetElitistGeneticEntities()
        {
            double elitismRatio = .1;
            int totalGeneticEntities = 100;
            IGeneticAlgorithm algorithm = GetGeneticAlgorithm(elitismRatio);
            await algorithm.InitializeAsync();
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            for (int i = 0; i < totalGeneticEntities; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            algorithm.Environment.Populations.Add(population);
            ElitismStrategy strategy = (ElitismStrategy)algorithm.ElitismStrategy;
            strategy.Initialize(algorithm);

            IList<IGeneticEntity> geneticEntities = strategy.GetEliteEntities(population);

            Assert.AreEqual(Convert.ToInt32(Math.Round(elitismRatio * totalGeneticEntities)), geneticEntities.Count, "Incorrect number of elitist genetic entities.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_GetElitistGeneticEntities_NullPopulation()
        {
            ElitismStrategy strategy = new ElitismStrategy();
            strategy.Initialize(GetGeneticAlgorithm(.1));
            AssertEx.Throws<ArgumentNullException>(() => strategy.GetEliteEntities(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod()]
        public void ElitismStrategy_GetElitistGeneticEntities_EmptyPopulation()
        {
            IGeneticAlgorithm algorithm = GetGeneticAlgorithm(.1);
            ElitismStrategy strategy = new ElitismStrategy();
            strategy.Initialize(algorithm);
            SimplePopulation pop = new SimplePopulation();
            pop.Initialize(algorithm);
            AssertEx.Throws<ArgumentException>(() => strategy.GetEliteEntities(pop));
        }

        private static MockGeneticAlgorithm GetGeneticAlgorithm(double elitismRatio)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                ElitismStrategy = new ElitismStrategy
                {
                    ElitistRatio = elitismRatio
                }
            };
            return algorithm;
        }
    }
}
