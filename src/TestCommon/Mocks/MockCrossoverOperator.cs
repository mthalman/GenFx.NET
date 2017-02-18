using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockCrossoverOperator : CrossoverOperator
    {
        public int DoCrossoverCallCount;

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

    [DataContract]
    public class MockCrossoverOperator2 : CrossoverOperator
    {
        public MockCrossoverOperator2() : base(2)
        {
        }

        protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
        {
            throw new Exception();
        }
    }

    public class MockCrossoverOperator3 : CrossoverOperator
    {
        public bool GenerateCrossoverCalled;

        public MockCrossoverOperator3() : base(2)
        {
        }

        protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
        {
            GenerateCrossoverCalled = true;
            return null;
        }
    }
}
