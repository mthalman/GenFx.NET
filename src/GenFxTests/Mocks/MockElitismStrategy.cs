using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockElitismStrategy : ElitismStrategy
    {
        internal int GetElitistGeneticEntitiesCallCount;

        protected override IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            this.GetElitistGeneticEntitiesCallCount++;
            return base.GetEliteGeneticEntitiesCore(population);
        }
    }

    class MockElitismStrategy2 : ElitismStrategy
    {
        protected override IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            throw new Exception();
        }
    }
}
