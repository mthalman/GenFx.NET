using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    public class MockStatistic : Statistic
    {
        internal bool StatisticEvaluated;
        
        public override object GetResultValue(Population population)
        {
            this.StatisticEvaluated = true;
            return "foo";
        }
    }

    public class MockStatistic2 : Statistic
    {
        public override object GetResultValue(Population population)
        {
            throw new Exception();
        }
    }
}
