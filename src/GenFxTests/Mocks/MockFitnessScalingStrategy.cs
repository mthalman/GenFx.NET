using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockFitnessScalingStrategy : FitnessScalingStrategy
    {
        internal int OnScaleCallCount;

        protected override void UpdateScaledFitnessValues(Population population)
        {
            this.OnScaleCallCount++;
        }
    }

    class MockFitnessScalingStrategy2 : FitnessScalingStrategy
    {
        protected override void UpdateScaledFitnessValues(Population population)
        {
        }
    }
}
