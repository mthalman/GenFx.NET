using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockFitnessScalingStrategy : FitnessScalingStrategyBase
    {
        internal int OnScaleCallCount;

        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
            this.OnScaleCallCount++;
        }
    }

    class MockFitnessScalingStrategy2 : FitnessScalingStrategyBase
    {
        protected override void UpdateScaledFitnessValues(IPopulation population)
        {
        }
    }
}
