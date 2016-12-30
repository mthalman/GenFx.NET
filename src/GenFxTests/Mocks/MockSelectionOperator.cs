using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockSelectionOperator : SelectionOperatorBase<MockSelectionOperator, MockSelectionOperatorConfiguration>
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

    class MockSelectionOperatorConfiguration : SelectionOperatorConfigurationBase<MockSelectionOperatorConfiguration, MockSelectionOperator>
    {
    }

    class MockSelectionOperator2 : SelectionOperatorBase<MockSelectionOperator2, MockSelectionOperator2Configuration>
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

    class MockSelectionOperator2Configuration : SelectionOperatorConfigurationBase<MockSelectionOperator2Configuration, MockSelectionOperator2>
    {
    }
}
