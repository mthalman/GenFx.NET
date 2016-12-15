using GenFx;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.FitnessScalingStrategy and is intended
    /// to contain all GenFx.FitnessScalingStrategy Unit Tests
    /// </summary>
    [TestClass()]
    public class FitnessScalingStrategyTest
    {

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new MockFitnessScalingStrategy(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting class is missing.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FakeFitnessScalingStrategy(new MockGeneticAlgorithm()));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Scale()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2(algorithm);
            Population population = new Population(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            strategy.Scale(population);

            Assert.IsTrue(strategy.OnScaleCalled, "ScaleCore was not called.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Scale_NullPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => strategy.Scale(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Scale_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2(algorithm);
            Population population = new Population(algorithm);
            AssertEx.Throws<ArgumentException>(() => strategy.Scale(population));
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.FitnessScalingStrategy = new FakeFitnessScalingStrategy2Configuration();
            return algorithm;
        }

        private class FakeFitnessScalingStrategy : FitnessScalingStrategy
        {
            public FakeFitnessScalingStrategy(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override void UpdateScaledFitnessValues(Population population)
            {
            }
        }

        [Component(typeof(FakeFitnessScalingStrategy))]
        private class FakeFitnessScalingStrategyConfiguration : FitnessScalingStrategyConfiguration
        {
        }

        private class FakeFitnessScalingStrategy2 : FitnessScalingStrategy
        {
            public bool OnScaleCalled;

            public FakeFitnessScalingStrategy2(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override void UpdateScaledFitnessValues(Population population)
            {
                this.OnScaleCalled = true;
            }
        }

        [Component(typeof(FakeFitnessScalingStrategy2))]
        private class FakeFitnessScalingStrategy2Configuration : FitnessScalingStrategyConfiguration
        {
        }
    }
}
