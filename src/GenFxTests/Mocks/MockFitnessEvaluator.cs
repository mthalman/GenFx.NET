using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Threading.Tasks;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockFitnessEvaluator : FitnessEvaluatorBase
    {
        internal int DoEvaluateFitnessCallCount;
        
        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            this.DoEvaluateFitnessCallCount++;
            MockEntity mockEntity = (MockEntity)entity;
            return Task.FromResult(Double.Parse(mockEntity.Identifier));
        }
    }
    
    class MockFitnessEvaluator2 : FitnessEvaluatorBase
    {
        public override Task<double> EvaluateFitnessAsync(IGeneticEntity entity)
        {
            throw new Exception();
        }
    }
}
