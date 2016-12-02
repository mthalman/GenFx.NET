using GenFx;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.SelectionOperators.UniformSelectionOperator and is intended
    ///to contain all GenFx.ComponentLibrary.SelectionOperators.UniformSelectionOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class UniformSelectionOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomHelper.Instance = new RandomHelper();
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [TestMethod]
        public void UniformSelectionOperator_Select()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            UniformSelectionOperatorConfiguration config = new UniformSelectionOperatorConfiguration();
            config.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = config;
            UniformSelectionOperator op = new UniformSelectionOperator(algorithm);
            Population population = new Population(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomHelper.Instance = randomUtil;

            randomUtil.Value = 3;
            GeneticEntity selectedEntity = op.Select(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");

            randomUtil.Value = 2;
            selectedEntity = op.Select(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");

            randomUtil.Value = 1;
            selectedEntity = op.Select(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");

            randomUtil.Value = 0;
            selectedEntity = op.Select(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");
        }

        private class TestRandomUtil : IRandomHelper
        {
            internal int Value;

            public int GetRandomValue(int maxValue)
            {
                return Value;
            }

            public double GetRandomRatio()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }


}
