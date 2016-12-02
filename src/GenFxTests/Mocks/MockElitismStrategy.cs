using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockElitismStrategy : ElitismStrategy
    {
        internal int GetElitistGeneticEntitiesCallCount;
        public MockElitismStrategy(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<GeneticEntity> GetElitistGeneticEntitiesCore(Population population)
        {
            this.GetElitistGeneticEntitiesCallCount++;
            return base.GetElitistGeneticEntitiesCore(population);
        }
    }

    [Component(typeof(MockElitismStrategy))]
    class MockElitismStrategyConfiguration : ElitismStrategyConfiguration
    {
    }

    class MockElitismStrategy2 : ElitismStrategy
    {
        public MockElitismStrategy2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<GeneticEntity> GetElitistGeneticEntitiesCore(Population population)
        {
            throw new Exception();
        }
    }

    [Component(typeof(MockElitismStrategy2))]
    class MockElitismStrategy2Configuration : ElitismStrategyConfiguration
    {
    }
}
