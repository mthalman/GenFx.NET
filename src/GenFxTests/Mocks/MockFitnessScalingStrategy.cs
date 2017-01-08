using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockFitnessScalingStrategy : FitnessScalingStrategyBase<MockFitnessScalingStrategy, MockFitnessScalingStrategyFactoryConfig>
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

    class MockFitnessScalingStrategyFactoryConfig : FitnessScalingStrategyFactoryConfigBase<MockFitnessScalingStrategyFactoryConfig, MockFitnessScalingStrategy>
    {
    }

    class MockFitnessScalingStrategy2 : FitnessScalingStrategyBase<MockFitnessScalingStrategy2, MockFitnessScalingStrategy2FactoryConfig>
    {
        public MockFitnessScalingStrategy2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
        }
    }

    class MockFitnessScalingStrategy2FactoryConfig : FitnessScalingStrategyFactoryConfigBase<MockFitnessScalingStrategy2FactoryConfig, MockFitnessScalingStrategy2>
    {
    }
}
