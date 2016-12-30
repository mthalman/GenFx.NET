using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockCrossoverOperator : CrossoverOperatorBase<MockCrossoverOperator, MockCrossoverOperatorConfiguration>
    {
        internal int DoCrossoverCallCount;

        public MockCrossoverOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<IGeneticEntity> GenerateCrossover(IGeneticEntity entity1, IGeneticEntity entity2)
        {
            this.DoCrossoverCallCount++;
            List<IGeneticEntity> geneticEntities = new List<IGeneticEntity>();
            geneticEntities.Add(entity1);
            geneticEntities.Add(entity2);
            return geneticEntities;
        }
    }

    class MockCrossoverOperatorConfiguration : CrossoverOperatorConfigurationBase<MockCrossoverOperatorConfiguration, MockCrossoverOperator>
    {
    }

    class MockCrossoverOperator2 : CrossoverOperatorBase<MockCrossoverOperator2, MockCrossoverOperator2Configuration>
    {
        public MockCrossoverOperator2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override IList<IGeneticEntity> GenerateCrossover(IGeneticEntity entity1, IGeneticEntity entity2)
        {
            throw new Exception();
        }
    }

    class MockCrossoverOperator2Configuration : CrossoverOperatorConfigurationBase<MockCrossoverOperator2Configuration, MockCrossoverOperator2>
    {
    }
}
