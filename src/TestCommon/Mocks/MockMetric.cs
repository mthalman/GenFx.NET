using GenFx;
using System;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockMetric : Metric
    {
        public bool MetricEvaluated;
        
        public override object GetResultValue(Population population)
        {
            this.MetricEvaluated = true;
            return "foo";
        }
    }

    [DataContract]
    public class MockMetric2 : Metric
    {
        public override object GetResultValue(Population population)
        {
            throw new Exception();
        }
    }
}
