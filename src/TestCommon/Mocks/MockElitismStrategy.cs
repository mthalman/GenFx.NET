using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockElitismStrategy : ElitismStrategy
    {
        public int GetElitistGeneticEntitiesCallCount;

        protected override IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            this.GetElitistGeneticEntitiesCallCount++;
            return base.GetEliteGeneticEntitiesCore(population);
        }
    }

    [DataContract]
    public class MockElitismStrategy2 : ElitismStrategy
    {
        protected override IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            throw new Exception();
        }
    }

    public class MockElitismStrategy3 : ElitismStrategy
    {
        public bool GetEliteGeneticEntitiesCoreCalled;

        protected override IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            GetEliteGeneticEntitiesCoreCalled = true;
            return base.GetEliteGeneticEntitiesCore(null);
        }
    }
}
