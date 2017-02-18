using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockSelectionOperator : SelectionOperator
    {
        public int DoSelectCallCount;
        
        protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
        {
            this.DoSelectCallCount++;
            return population.Entities.Take(entityCount);
        }
    }

    [DataContract]
    public class MockSelectionOperator2 : SelectionOperator
    {
        protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
        {
            return null;
        }
    }
}
