using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace GenFxTests.Mocks
{
    [DataContract]
    class MockElitismStrategy : ElitismStrategy
    {
        internal int GetElitistGeneticEntitiesCallCount;

        protected override IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            this.GetElitistGeneticEntitiesCallCount++;
            return base.GetEliteGeneticEntitiesCore(population);
        }
    }

    [DataContract]
    class MockElitismStrategy2 : ElitismStrategy
    {
        protected override IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            throw new Exception();
        }
    }
}
