using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockCrossoverOperator : CrossoverOperator
    {
        internal int DoCrossoverCallCount;

        public MockCrossoverOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            this.DoCrossoverCallCount++;
            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            geneticEntities.Add(entity1);
            geneticEntities.Add(entity2);
            return geneticEntities;
        }
    }

    [Component(typeof(MockCrossoverOperator))]
    class MockCrossoverOperatorConfiguration : CrossoverOperatorConfiguration
    {
    }

    class MockCrossoverOperator2 : CrossoverOperator
    {
        public MockCrossoverOperator2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            throw new Exception();
        }
    }

    [Component(typeof(MockCrossoverOperator2))]
    class MockCrossoverOperator2Configuration : CrossoverOperatorConfiguration
    {
    }
}
