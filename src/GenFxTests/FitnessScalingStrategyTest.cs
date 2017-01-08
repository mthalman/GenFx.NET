using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
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
            AssertEx.Throws<ArgumentException>(() => new FakeFitnessScalingStrategy(new MockGeneticAlgorithm(new ComponentFactoryConfigSet())));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Scale()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
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
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => strategy.Scale(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Scale_EmptyPopulation()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            AssertEx.Throws<ArgumentException>(() => strategy.Scale(population));
        }

        private static IGeneticAlgorithm GetAlgorithm()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                FitnessScalingStrategy = new FakeFitnessScalingStrategy2FactoryConfig()
            });
            return algorithm;
        }

        private class FakeFitnessScalingStrategy : FitnessScalingStrategyBase<FakeFitnessScalingStrategy, FakeFitnessScalingStrategyFactoryConfig>
        {
            public FakeFitnessScalingStrategy(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override void UpdateScaledFitnessValues(IPopulation population)
            {
            }
        }

        private class FakeFitnessScalingStrategyFactoryConfig : FitnessScalingStrategyFactoryConfigBase<FakeFitnessScalingStrategyFactoryConfig, FakeFitnessScalingStrategy>
        {
        }

        private class FakeFitnessScalingStrategy2 : FitnessScalingStrategyBase<FakeFitnessScalingStrategy2, FakeFitnessScalingStrategy2FactoryConfig>
        {
            public bool OnScaleCalled;

            public FakeFitnessScalingStrategy2(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override void UpdateScaledFitnessValues(IPopulation population)
            {
                this.OnScaleCalled = true;
            }
        }

        private class FakeFitnessScalingStrategy2FactoryConfig : FitnessScalingStrategyFactoryConfigBase<FakeFitnessScalingStrategy2FactoryConfig, FakeFitnessScalingStrategy2>
        {
        }
    }
}
