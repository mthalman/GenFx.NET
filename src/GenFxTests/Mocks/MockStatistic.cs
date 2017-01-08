using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    public class MockStatistic : StatisticBase<MockStatistic, MockStatisticFactoryConfig>
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

    public class MockStatisticFactoryConfig : StatisticFactoryConfigBase<MockStatisticFactoryConfig, MockStatistic>
    {
    }

    public class MockStatistic2 : StatisticBase<MockStatistic2, MockStatistic2FactoryConfig>
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

    public class MockStatistic2FactoryConfig : StatisticFactoryConfigBase<MockStatistic2FactoryConfig, MockStatistic2>
    {
    }
}
