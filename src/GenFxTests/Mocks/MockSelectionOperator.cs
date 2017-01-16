using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockSelectionOperator : SelectionOperator
    {
        internal int DoSelectCallCount;
        
        protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
        {
            this.DoSelectCallCount++;
            return population.Entities.Take(entityCount);
        }
    }
    
    class MockSelectionOperator2 : SelectionOperator
    {
        protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
        {
            throw new Exception();
        }
    }
}
