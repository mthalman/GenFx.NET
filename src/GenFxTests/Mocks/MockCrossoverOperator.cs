using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockCrossoverOperator : CrossoverOperator
    {
        internal int DoCrossoverCallCount;
        
        protected override IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            this.DoCrossoverCallCount++;
            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            geneticEntities.Add(entity1);
            geneticEntities.Add(entity2);
            return geneticEntities;
        }
    }
    
    class MockCrossoverOperator2 : CrossoverOperator
    {
        protected override IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            throw new Exception();
        }
    }
}
