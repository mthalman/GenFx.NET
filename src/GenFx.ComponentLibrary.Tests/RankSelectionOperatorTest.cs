using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon.Helpers;
using System.Reflection;

namespace GenFx.ComponentLibrary.Tests
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
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [TestMethod]
        public void RankSelectionOperator_Select()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            RankSelectionOperator op = new RankSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            MockEntity entity3 = new MockEntity();
            entity3.Initialize(algorithm);
            MockEntity entity4 = new MockEntity();
            entity4.Initialize(algorithm);
            entity1.ScaledFitnessValue = 0;
            entity2.ScaledFitnessValue = 50;
            entity3.ScaledFitnessValue = 23;
            entity4.ScaledFitnessValue = 25;
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            population.Entities.Add(entity4);

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.Ratio = 0;
            IList<GeneticEntity> selectedEntities = op.SelectEntities(1, population).ToList();
            Assert.AreSame(entity1, selectedEntities[0], "Incorrect entity seleceted.");

            randomUtil.Ratio = .099999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(entity1, selectedEntities[0], "Incorrect entity seleceted.");

            randomUtil.Ratio = .1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(entity3, selectedEntities[0], "Incorrect entity seleceted.");

            randomUtil.Ratio = .299999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(entity3, selectedEntities[0], "Incorrect entity seleceted.");

            randomUtil.Ratio = .3;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(entity4, selectedEntities[0], "Incorrect entity seleceted.");

            randomUtil.Ratio = .599999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(entity4, selectedEntities[0], "Incorrect entity seleceted.");

            randomUtil.Ratio = .6;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(entity2, selectedEntities[0], "Incorrect entity seleceted.");

            randomUtil.Ratio = 1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(entity2, selectedEntities[0], "Incorrect entity seleceted.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="RankSelectionOperator.SelectEntitiesFromPopulation"/>.
        /// </summary>
        [TestMethod]
        public void RankSelectionOperator_SelectEntitiesFromPopulation_NullPopulation()
        {
            RankSelectionOperator op = new RankSelectionOperator();
            PrivateObject accessor = new PrivateObject(op);
            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("SelectEntitiesFromPopulation", BindingFlags.Instance | BindingFlags.NonPublic, (int)0, (Population)null));
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new SimplePopulation(),
                GeneticEntitySeed = new MockEntity(),
                SelectionOperator = new RankSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                FitnessEvaluator = new MockFitnessEvaluator()
            };

            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);

            return algorithm;
        }

        private class TestRandomUtil : IRandomNumberService
        {
            internal double Ratio;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetDouble()
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
