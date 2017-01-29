using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace GenFxTests.Mocks
{
    [DataContract]
    public class MockStatistic : Statistic
    {
        internal bool StatisticEvaluated;
        
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
