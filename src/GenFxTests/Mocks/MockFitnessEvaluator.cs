using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Threading.Tasks;

namespace GenFxTests.Mocks
{
    class MockFitnessEvaluator : FitnessEvaluator
    {
        internal int DoEvaluateFitnessCallCount;

        public MockFitnessEvaluator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
        {
            this.DoEvaluateFitnessCallCount++;
            MockEntity mockEntity = (MockEntity)entity;
            return Task.FromResult(Double.Parse(mockEntity.Identifier));
        }
    }

    [Component(typeof(MockFitnessEvaluator))]
    class MockFitnessEvaluatorConfiguration : FitnessEvaluatorConfiguration
    {
    }

    class MockFitnessEvaluator2 : FitnessEvaluator
    {
        public MockFitnessEvaluator2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
        {
            throw new Exception();
        }
    }

    [Component(typeof(MockFitnessEvaluator2))]
    class MockFitnessEvaluator2Configuration : FitnessEvaluatorConfiguration
    {
    }
}
