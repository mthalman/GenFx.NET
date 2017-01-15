using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    public class MockStatistic : StatisticBase
    {
        internal bool StatisticEvaluated;
        
        public override object GetResultValue(IPopulation population)
        {
            this.StatisticEvaluated = true;
            return "foo";
        }
    }

    public class MockStatistic2 : StatisticBase
    {
        public override object GetResultValue(IPopulation population)
        {
            throw new Exception();
        }
    }
}
