using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockElitismStrategy : ElitismStrategyBase
    {
        internal int GetElitistGeneticEntitiesCallCount;

        protected override IList<IGeneticEntity> GetEliteGeneticEntitiesCore(IPopulation population)
        {
            this.GetElitistGeneticEntitiesCallCount++;
            return base.GetEliteGeneticEntitiesCore(population);
        }
    }

    class MockElitismStrategy2 : ElitismStrategyBase
    {
        protected override IList<IGeneticEntity> GetEliteGeneticEntitiesCore(IPopulation population)
        {
            throw new Exception();
        }
    }
}
