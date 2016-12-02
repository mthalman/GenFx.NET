using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockSelectionOperator : SelectionOperator
    {
        internal int DoSelectCallCount;

        public MockSelectionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override GeneticEntity SelectEntityFromPopulation(Population population)
        {
            this.DoSelectCallCount++;
            return population.Entities[0];
        }
    }

    [Component(typeof(MockSelectionOperator))]
    class MockSelectionOperatorConfiguration : SelectionOperatorConfiguration
    {
    }

    class MockSelectionOperator2 : SelectionOperator
    {
        public MockSelectionOperator2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {

        }

        protected override GeneticEntity SelectEntityFromPopulation(Population population)
        {
            throw new Exception();
        }
    }

    [Component(typeof(MockSelectionOperator2))]
    class MockSelectionOperator2Configuration : SelectionOperatorConfiguration
    {
    }
}
