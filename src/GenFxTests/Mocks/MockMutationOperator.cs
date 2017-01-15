using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockMutationOperator : MutationOperatorBase
    {
        internal int DoMutateCallCount;
        
        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            this.DoMutateCallCount++;
            return false;
        }
    }

    class MockMutationOperator2 : MutationOperatorBase
    {
        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            throw new Exception();
        }
    }
}
