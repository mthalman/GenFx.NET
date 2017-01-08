using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockSelectionOperator : SelectionOperatorBase<MockSelectionOperator, MockSelectionOperatorFactoryConfig>
    {
        internal int DoSelectCallCount;

        public MockSelectionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
        {
            this.DoSelectCallCount++;
            return population.Entities[0];
        }
    }

    class MockSelectionOperatorFactoryConfig : SelectionOperatorFactoryConfigBase<MockSelectionOperatorFactoryConfig, MockSelectionOperator>
    {
    }

    class MockSelectionOperator2 : SelectionOperatorBase<MockSelectionOperator2, MockSelectionOperator2FactoryConfig>
    {
        public MockSelectionOperator2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
        {
            throw new Exception();
        }
    }

    class MockSelectionOperator2FactoryConfig : SelectionOperatorFactoryConfigBase<MockSelectionOperator2FactoryConfig, MockSelectionOperator2>
    {
    }
}
