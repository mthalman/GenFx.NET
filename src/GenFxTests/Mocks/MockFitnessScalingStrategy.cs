using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace GenFxTests.Mocks
{
    [DataContract]
    class MockFitnessScalingStrategy : FitnessScalingStrategy
    {
        internal int OnScaleCallCount;

        protected override void UpdateScaledFitnessValues(Population population)
        {
            this.OnScaleCallCount++;
        }
    }

    [DataContract]
    class MockFitnessScalingStrategy2 : FitnessScalingStrategy
    {
        protected override void UpdateScaledFitnessValues(Population population)
        {
        }
    }
}
