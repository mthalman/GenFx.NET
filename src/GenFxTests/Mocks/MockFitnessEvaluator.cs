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
        
        public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
        {
            this.DoEvaluateFitnessCallCount++;
            MockEntity mockEntity = (MockEntity)entity;
            return Task.FromResult(Double.Parse(mockEntity.Identifier));
        }
    }
    
    class MockFitnessEvaluator2 : FitnessEvaluator
    {
        public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
        {
            throw new Exception();
        }
    }
}
