using GenFx;
using System;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockStatistic : Statistic
    {
        public bool StatisticEvaluated;
        
        public override object GetResultValue(Population population)
        {
            this.StatisticEvaluated = true;
            return "foo";
        }
    }

    [DataContract]
    public class MockStatistic2 : Statistic
    {
        public override object GetResultValue(Population population)
        {
            throw new Exception();
        }
    }
}
