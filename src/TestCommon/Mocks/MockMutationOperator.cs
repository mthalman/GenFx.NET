using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockMutationOperator : MutationOperator
    {
        public int DoMutateCallCount;
        
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            this.DoMutateCallCount++;
            return false;
        }
    }

    [DataContract]
    public class MockMutationOperator2 : MutationOperator
    {
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            throw new Exception();
        }
    }
}
