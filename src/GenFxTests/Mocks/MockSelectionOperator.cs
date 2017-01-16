using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockSelectionOperator : SelectionOperator
    {
        internal int DoSelectCallCount;
        
        protected override GeneticEntity SelectEntityFromPopulation(Population population)
        {
            this.DoSelectCallCount++;
            return population.Entities[0];
        }
    }
    
    class MockSelectionOperator2 : SelectionOperator
    {
        protected override GeneticEntity SelectEntityFromPopulation(Population population)
        {
            throw new Exception();
        }
    }
}
