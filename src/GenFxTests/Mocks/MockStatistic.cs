using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    public class MockStatistic : StatisticBase<MockStatistic, MockStatisticConfiguration>
    {
        internal bool StatisticEvaluated;

        public MockStatistic(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override object GetResultValue(IPopulation population)
        {
            this.StatisticEvaluated = true;
            return "foo";
        }
    }

    public class MockStatisticConfiguration : StatisticConfigurationBase<MockStatisticConfiguration, MockStatistic>
    {
    }

    public class MockStatistic2 : StatisticBase<MockStatistic2, MockStatistic2Configuration>
    {
        public MockStatistic2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override object GetResultValue(IPopulation population)
        {
            throw new Exception();
        }
    }

    public class MockStatistic2Configuration : StatisticConfigurationBase<MockStatistic2Configuration, MockStatistic2>
    {
    }
}
