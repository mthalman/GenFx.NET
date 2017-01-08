using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockMutationOperator : MutationOperatorBase<MockMutationOperator, MockMutationOperatorFactoryConfig>
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

    class MockMutationOperatorFactoryConfig : MutationOperatorFactoryConfigBase<MockMutationOperatorFactoryConfig, MockMutationOperator>
    {
    }

    class MockMutationOperator2 : MutationOperatorBase<MockMutationOperator2, MockMutationOperator2FactoryConfig>
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

    class MockMutationOperator2FactoryConfig : MutationOperatorFactoryConfigBase<MockMutationOperator2FactoryConfig, MockMutationOperator2>
    {
    }
}
