using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Threading.Tasks;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockFitnessEvaluator : FitnessEvaluatorBase<MockFitnessEvaluator, MockFitnessEvaluatorConfiguration>
    {
        internal int DoEvaluateFitnessCallCount;

        public MockFitnessEvaluator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            this.DoEvaluateFitnessCallCount++;
            MockEntity mockEntity = (MockEntity)entity;
            return Task.FromResult(Double.Parse(mockEntity.Identifier));
        }
    }

    class MockFitnessEvaluatorConfiguration : FitnessEvaluatorConfigurationBase<MockFitnessEvaluatorConfiguration, MockFitnessEvaluator>
    {
    }

    class MockFitnessEvaluator2 : FitnessEvaluatorBase<MockFitnessEvaluator2, MockFitnessEvaluator2Configuration>
    {
        public MockFitnessEvaluator2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            throw new Exception();
        }
    }

    class MockFitnessEvaluator2Configuration : FitnessEvaluatorConfigurationBase<MockFitnessEvaluator2Configuration, MockFitnessEvaluator2>
    {
    }
}
