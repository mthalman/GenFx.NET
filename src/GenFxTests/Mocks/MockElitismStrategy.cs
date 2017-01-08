using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockElitismStrategy : ElitismStrategyBase<MockElitismStrategy, MockElitismStrategyFactoryConfig>
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

    class MockElitismStrategyFactoryConfig : ElitismStrategyFactoryConfigBase<MockElitismStrategyFactoryConfig, MockElitismStrategy>
    {
    }

    class MockElitismStrategy2 : ElitismStrategyBase<MockElitismStrategy2, MockElitismStrategy2FactoryConfig>
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

    class MockElitismStrategy2FactoryConfig : ElitismStrategyFactoryConfigBase<MockElitismStrategy2FactoryConfig, MockElitismStrategy2>
    {
    }
}
