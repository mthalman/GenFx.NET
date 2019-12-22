using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// This is a test class for GenFx.FitnessScalingStrategy and is intended
    /// to contain all GenFx.FitnessScalingStrategy Unit Tests
    /// </summary>
    public class FitnessScalingStrategyTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void FitnessScalingStrategy_Ctor_NullAlgorithm()
        {
            MockFitnessScalingStrategy strategy = new MockFitnessScalingStrategy();
            
            Assert.Throws<ArgumentNullException>(() => strategy.Initialize(null));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [Fact]
        public void FitnessScalingStrategy_Scale()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2();
            strategy.Initialize(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            population.Entities.Add(entity);
            strategy.Scale(population);

            Assert.True(strategy.OnScaleCalled, "ScaleCore was not called.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [Fact]
        public void FitnessScalingStrategy_Scale_NullPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2();
            strategy.Initialize(algorithm);
            Assert.Throws<ArgumentNullException>(() => strategy.Scale(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [Fact]
        public void FitnessScalingStrategy_Scale_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FakeFitnessScalingStrategy2 strategy = new FakeFitnessScalingStrategy2();
            strategy.Initialize(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            Assert.Throws<ArgumentException>(() => strategy.Scale(population));
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new FakeFitnessScalingStrategy2()
            };
            return algorithm;
        }

        private class FakeFitnessScalingStrategy : FitnessScalingStrategy
        {
            protected override void UpdateScaledFitnessValues(Population population)
            {
            }
        }
        
        private class FakeFitnessScalingStrategy2 : FitnessScalingStrategy
        {
            public bool OnScaleCalled;
            
            protected override void UpdateScaledFitnessValues(Population population)
            {
                this.OnScaleCalled = true;
            }
        }
    }
}
