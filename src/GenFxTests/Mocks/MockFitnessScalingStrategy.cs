using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockFitnessScalingStrategy : FitnessScalingStrategyBase<MockFitnessScalingStrategy, MockFitnessScalingStrategyConfiguration>
    {
        internal int OnScaleCallCount;

        public MockFitnessScalingStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
            this.OnScaleCallCount++;
        }
    }

    class MockFitnessScalingStrategyConfiguration : FitnessScalingStrategyConfigurationBase<MockFitnessScalingStrategyConfiguration, MockFitnessScalingStrategy>
    {
    }

    class MockFitnessScalingStrategy2 : FitnessScalingStrategyBase<MockFitnessScalingStrategy2, MockFitnessScalingStrategy2Configuration>
    {
        public MockFitnessScalingStrategy2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
        }
    }

    class MockFitnessScalingStrategy2Configuration : FitnessScalingStrategyConfigurationBase<MockFitnessScalingStrategy2Configuration, MockFitnessScalingStrategy2>
    {
    }
}
