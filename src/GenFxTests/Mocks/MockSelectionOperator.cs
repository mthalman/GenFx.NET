using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockSelectionOperator : SelectionOperatorBase
    {
        internal int DoSelectCallCount;
        
        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
        {
            this.DoSelectCallCount++;
            return population.Entities[0];
        }
    }
    
    class MockSelectionOperator2 : SelectionOperatorBase
    {
        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
        {
            throw new Exception();
        }
    }
}
