using GenFx;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.SelectionOperators.RouletteWheelSampler.EntityPercentageRange and is intended
    /// to contain all GenFx.ComponentLibrary.SelectionOperators.RouletteWheelSampler.EntityPercentageRange Unit Tests
    /// </summary>
    [TestClass()]
    public class RouletteWheelSamplerTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the GetEntity method works correctly.
        /// </summary>
        [TestMethod]
        public void RouletteWheelSampler_GetEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Population = new MockPopulationConfiguration()
            });
            List<WheelSlice> slices = new List<WheelSlice>();
            MockEntity entity1 = new MockEntity(algorithm);
            MockEntity entity2 = new MockEntity(algorithm);
            MockEntity entity3 = new MockEntity(algorithm);
            MockEntity entity4 = new MockEntity(algorithm);

            slices.Add(new WheelSlice(entity1, 4));
            slices.Add(new WheelSlice(entity2, 2));
            slices.Add(new WheelSlice(entity3, 1));
            slices.Add(new WheelSlice(entity4, 3));

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.Ratio = 0;
            IGeneticEntity sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity1, sampledEntity, "Incorrect entity instance returned.");

            randomUtil.Ratio = .39999;
            sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity1, sampledEntity, "Incorrect entity instance returned.");

            randomUtil.Ratio = .4;
            sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity2, sampledEntity, "Incorrect entity instance returned.");

            randomUtil.Ratio = .59999;
            sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity2, sampledEntity, "Incorrect entity instance returned.");

            randomUtil.Ratio = .6;
            sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity3, sampledEntity, "Incorrect entity instance returned.");

            randomUtil.Ratio = .69999;
            sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity3, sampledEntity, "Incorrect entity instance returned.");

            randomUtil.Ratio = .7;
            sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity4, sampledEntity, "Incorrect entity instance returned.");

            randomUtil.Ratio = 1;
            sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreSame(entity4, sampledEntity, "Incorrect entity instance returned.");
        }

        /// <summary>
        /// Tests that the GetEntity method returns a random entity when no wheel slices sizes are set.
        /// </summary>
        [TestMethod]
        public void RouletteWheelSampler_GetEntity_NoSizes()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Population = new MockPopulationConfiguration()
            });
            List<WheelSlice> slices = new List<WheelSlice>();
            MockEntity entity1 = new MockEntity(algorithm);
            MockEntity entity2 = new MockEntity(algorithm);
            MockEntity entity3 = new MockEntity(algorithm);
            MockEntity entity4 = new MockEntity(algorithm);

            slices.Add(new WheelSlice(entity1, 0));
            slices.Add(new WheelSlice(entity2, 0));
            slices.Add(new WheelSlice(entity3, 0));
            slices.Add(new WheelSlice(entity4, 0));

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.RandomValue = 2;
            IGeneticEntity sampledEntity = RouletteWheelSampler.GetEntity(slices);
            Assert.AreEqual(randomUtil.MaxValuePassed, 4, "Incorrect max value passed.");
            Assert.AreSame(entity3, sampledEntity, "Incorrect entity returned.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null collection is passed.
        /// </summary>
        [TestMethod]
        public void RouletteWheelSampler_GetEntity_NullCollection()
        {
            AssertEx.Throws<ArgumentNullException>(() => RouletteWheelSampler.GetEntity(null));
        }

        private class TestRandomUtil : IRandomNumberService
        {
            internal double Ratio;
            internal int RandomValue;
            internal int MaxValuePassed;

            public int GetRandomValue(int maxValue)
            {
                this.MaxValuePassed = maxValue;
                return RandomValue;
            }

            public double GetRandomPercentRatio()
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
