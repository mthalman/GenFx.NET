using GenFx.ComponentLibrary.SelectionOperators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RouletteWheelSampler"/> class.
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation()
            };
            List<WheelSlice> slices = new List<WheelSlice>();
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            MockEntity entity3 = new MockEntity();
            entity3.Initialize(algorithm);
            MockEntity entity4 = new MockEntity();
            entity4.Initialize(algorithm);

            slices.Add(new WheelSlice(entity1, 4));
            slices.Add(new WheelSlice(entity2, 2));
            slices.Add(new WheelSlice(entity3, 1));
            slices.Add(new WheelSlice(entity4, 3));

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.Ratio = 0;
            GeneticEntity sampledEntity = RouletteWheelSampler.GetEntity(slices);
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation()
            };
            List<WheelSlice> slices = new List<WheelSlice>();

            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            MockEntity entity3 = new MockEntity();
            entity3.Initialize(algorithm);
            MockEntity entity4 = new MockEntity();
            entity4.Initialize(algorithm);

            slices.Add(new WheelSlice(entity1, 0));
            slices.Add(new WheelSlice(entity2, 0));
            slices.Add(new WheelSlice(entity3, 0));
            slices.Add(new WheelSlice(entity4, 0));

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.RandomValue = 2;
            GeneticEntity sampledEntity = RouletteWheelSampler.GetEntity(slices);
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

        /// <summary>
        /// Tests that an exception is thrown when an empty collection is passed.
        /// </summary>
        [TestMethod]
        public void RouletteWheelSampler_GetEntity_EmptyCollection()
        {
            AssertEx.Throws<ArgumentException>(() => RouletteWheelSampler.GetEntity(new List<WheelSlice>()));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null wheel slice is passed.
        /// </summary>
        [TestMethod]
        public void RouletteWheelSampler_GetEntity_NullWheelSlice()
        {
            AssertEx.Throws<ArgumentException>(() => RouletteWheelSampler.GetEntity(new WheelSlice[] { null }.ToList()));
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
