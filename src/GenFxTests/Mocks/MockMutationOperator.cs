using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace GenFxTests.Mocks
{
    [DataContract]
    class MockMutationOperator : MutationOperator
    {
        internal int DoMutateCallCount;
        
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            this.DoMutateCallCount++;
            return false;
        }
    }

    [DataContract]
    class MockMutationOperator2 : MutationOperator
    {
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            throw new Exception();
        }
    }
}
