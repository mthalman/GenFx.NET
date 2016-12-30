using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockElitismStrategy : ElitismStrategyBase<MockElitismStrategy, MockElitismStrategyConfiguration>
    {
        internal int GetElitistGeneticEntitiesCallCount;
        public MockElitismStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<IGeneticEntity> GetEliteGeneticEntitiesCore(IPopulation population)
        {
            this.GetElitistGeneticEntitiesCallCount++;
            return base.GetEliteGeneticEntitiesCore(population);
        }
    }

    class MockElitismStrategyConfiguration : ElitismStrategyConfigurationBase<MockElitismStrategyConfiguration, MockElitismStrategy>
    {
    }

    class MockElitismStrategy2 : ElitismStrategyBase<MockElitismStrategy2, MockElitismStrategy2Configuration>
    {
        public MockElitismStrategy2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<IGeneticEntity> GetEliteGeneticEntitiesCore(IPopulation population)
        {
            throw new Exception();
        }
    }

    class MockElitismStrategy2Configuration : ElitismStrategyConfigurationBase<MockElitismStrategy2Configuration, MockElitismStrategy2>
    {
    }
}
