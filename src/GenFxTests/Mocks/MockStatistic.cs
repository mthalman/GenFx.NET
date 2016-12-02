using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    public class MockStatistic : Statistic
    {
        internal bool StatisticEvaluated;

        public MockStatistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override object GetResultValue(Population population)
        {
            this.StatisticEvaluated = true;
            return "foo";
        }
    }

    [Component(typeof(MockStatistic))]
    public class MockStatisticConfiguration : StatisticConfiguration
    {
    }

    public class MockStatistic2 : Statistic
    {
        public MockStatistic2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override object GetResultValue(Population population)
        {
            throw new Exception();
        }
    }

    [Component(typeof(MockStatistic2))]
    public class MockStatistic2Configuration : StatisticConfiguration
    {
    }
}
