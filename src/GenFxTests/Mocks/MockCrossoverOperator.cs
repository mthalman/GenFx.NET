using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockCrossoverOperator : CrossoverOperator
    {
        internal int DoCrossoverCallCount;

        public MockCrossoverOperator() : base(2)
        {
        }

        protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
        {
            this.DoCrossoverCallCount++;
            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            geneticEntities.Add(parents[0]);
            geneticEntities.Add(parents[1]);
            return geneticEntities;
        }
    }
    
    class MockCrossoverOperator2 : CrossoverOperator
    {
        public MockCrossoverOperator2() : base(2)
        {
        }

        protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
        {
            throw new Exception();
        }
    }
}
