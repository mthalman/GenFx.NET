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
    ///This is a test class for GenFx.ComponentLibrary.SelectionOperators.RankSelectionOperator and is intended
    ///to contain all GenFx.ComponentLibrary.SelectionOperators.RankSelectionOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class RankSelectionOperatorTest
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
        public void RankSelectionOperator_Select()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            RankSelectionOperator op = new RankSelectionOperator(algorithm);
            Population population = new Population(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            MockEntity entity2 = new MockEntity(algorithm);
            MockEntity entity3 = new MockEntity(algorithm);
            MockEntity entity4 = new MockEntity(algorithm);
            entity1.ScaledFitnessValue = 0;
            entity2.ScaledFitnessValue = 50;
            entity3.ScaledFitnessValue = 23;
            entity4.ScaledFitnessValue = 25;
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            population.Entities.Add(entity4);

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomHelper.Instance = randomUtil;

            randomUtil.Ratio = 0;
            GeneticEntity selectedEntity = op.Select(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity seleceted.");

            randomUtil.Ratio = .099999;
            selectedEntity = op.Select(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity seleceted.");

            randomUtil.Ratio = .1;
            selectedEntity = op.Select(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity seleceted.");

            randomUtil.Ratio = .299999;
            selectedEntity = op.Select(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity seleceted.");

            randomUtil.Ratio = .3;
            selectedEntity = op.Select(population);
            Assert.AreSame(entity4, selectedEntity, "Incorrect entity seleceted.");

            randomUtil.Ratio = .599999;
            selectedEntity = op.Select(population);
            Assert.AreSame(entity4, selectedEntity, "Incorrect entity seleceted.");

            randomUtil.Ratio = .6;
            selectedEntity = op.Select(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity seleceted.");

            randomUtil.Ratio = 1;
            selectedEntity = op.Select(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity seleceted.");
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            RankSelectionOperatorConfiguration config = new RankSelectionOperatorConfiguration();
            config.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = config;
            algorithm.ConfigurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();

            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);

            return algorithm;
        }

        private class TestRandomUtil : IRandomHelper
        {
            internal double Ratio;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetRandomRatio()
            {
                return Ratio;
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }


}
