using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockMutationOperator : MutationOperatorBase<MockMutationOperator, MockMutationOperatorConfiguration>
    {
        internal int DoMutateCallCount;

        public MockMutationOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            this.DoMutateCallCount++;
            return false;
        }
    }

    class MockMutationOperatorConfiguration : MutationOperatorConfigurationBase<MockMutationOperatorConfiguration, MockMutationOperator>
    {
    }

    class MockMutationOperator2 : MutationOperatorBase<MockMutationOperator2, MockMutationOperator2Configuration>
    {
        public MockMutationOperator2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            throw new Exception();
        }
    }

    class MockMutationOperator2Configuration : MutationOperatorConfigurationBase<MockMutationOperator2Configuration, MockMutationOperator2>
    {
    }
}
