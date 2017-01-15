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
            MockFitnessScalingStrategy strategy = new MockFitnessScalingStrategy();
            
            AssertEx.Throws<ArgumentNullException>(() => strategy.Initialize(null));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Scale()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2();
            strategy.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            population.Entities.Add(entity);
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
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2();
            strategy.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => strategy.Scale(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod]
        public void FitnessScalingStrategy_Scale_EmptyPopulation()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2();
            strategy.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            AssertEx.Throws<ArgumentException>(() => strategy.Scale(population));
        }

        private static IGeneticAlgorithm GetAlgorithm()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessScalingStrategy = new FakeFitnessScalingStrategy2()
            };
            return algorithm;
        }

        private class FakeFitnessScalingStrategy : FitnessScalingStrategyBase
        {
            protected override void UpdateScaledFitnessValues(IPopulation population)
            {
            }
        }
        
        private class FakeFitnessScalingStrategy2 : FitnessScalingStrategyBase
        {
            public bool OnScaleCalled;
            
            protected override void UpdateScaledFitnessValues(IPopulation population)
            {
                this.OnScaleCalled = true;
            }
        }
    }
}
