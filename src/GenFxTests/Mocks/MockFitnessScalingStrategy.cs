using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockFitnessScalingStrategy : FitnessScalingStrategy
    {
        internal int OnScaleCallCount;

        public MockFitnessScalingStrategy(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void UpdateScaledFitnessValues(Population population)
        {
            this.OnScaleCallCount++;
        }
    }

    [Component(typeof(MockFitnessScalingStrategy))]
    class MockFitnessScalingStrategyConfiguration : FitnessScalingStrategyConfiguration
    {
    }

    class MockFitnessScalingStrategy2 : FitnessScalingStrategy
    {
        public MockFitnessScalingStrategy2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void UpdateScaledFitnessValues(Population population)
        {
        }
    }

    [Component(typeof(MockFitnessScalingStrategy2))]
    class MockFitnessScalingStrategy2Configuration : FitnessScalingStrategyConfiguration
    {
    }
}
