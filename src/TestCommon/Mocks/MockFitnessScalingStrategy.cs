using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockFitnessScalingStrategy : FitnessScalingStrategy
    {
        public int OnScaleCallCount;

        protected override void UpdateScaledFitnessValues(Population population)
        {
            this.OnScaleCallCount++;
        }
    }

    [DataContract]
    public class MockFitnessScalingStrategy2 : FitnessScalingStrategy
    {
        protected override void UpdateScaledFitnessValues(Population population)
        {
        }
    }
}
