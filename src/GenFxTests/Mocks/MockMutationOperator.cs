using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockMutationOperator : MutationOperator
    {
        internal int DoMutateCallCount;
        
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            this.DoMutateCallCount++;
            return false;
        }
    }

    class MockMutationOperator2 : MutationOperator
    {
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            throw new Exception();
        }
    }
}
