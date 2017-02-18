using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockFitnessEvaluator : FitnessEvaluator
    {
        public int DoEvaluateFitnessCallCount;
        
        public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
        {
            this.DoEvaluateFitnessCallCount++;
            MockEntity mockEntity = (MockEntity)entity;
            return Task.FromResult(Double.Parse(mockEntity.Identifier));
        }
    }

    [DataContract]
    public class MockFitnessEvaluator2 : FitnessEvaluator
    {
        public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
        {
            return Task.FromResult<double>(0);
        }
    }
}
