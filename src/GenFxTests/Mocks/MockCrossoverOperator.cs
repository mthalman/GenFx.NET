using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockCrossoverOperator : CrossoverOperatorBase
    {
        internal int DoCrossoverCallCount;
        
        protected override IList<IGeneticEntity> GenerateCrossover(IGeneticEntity entity1, IGeneticEntity entity2)
        {
            this.DoCrossoverCallCount++;
            List<IGeneticEntity> geneticEntities = new List<IGeneticEntity>();
            geneticEntities.Add(entity1);
            geneticEntities.Add(entity2);
            return geneticEntities;
        }
    }
    
    class MockCrossoverOperator2 : CrossoverOperatorBase
    {
        protected override IList<IGeneticEntity> GenerateCrossover(IGeneticEntity entity1, IGeneticEntity entity2)
        {
            throw new Exception();
        }
    }
}
