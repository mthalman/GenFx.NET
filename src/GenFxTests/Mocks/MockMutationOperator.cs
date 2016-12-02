using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockMutationOperator : MutationOperator
    {
        internal int DoMutateCallCount;

        public MockMutationOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override bool GenerateMutation(GeneticEntity entity)
        {
            this.DoMutateCallCount++;
            return false;
        }
    }

    [Component(typeof(MockMutationOperator))]
    class MockMutationOperatorConfiguration : MutationOperatorConfiguration
    {
    }

    class MockMutationOperator2 : MutationOperator
    {
        public MockMutationOperator2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override bool GenerateMutation(GeneticEntity entity)
        {
            throw new Exception();
        }
    }

    [Component(typeof(MockMutationOperator2))]
    class MockMutationOperator2Configuration : MutationOperatorConfiguration
    {
    }
}
