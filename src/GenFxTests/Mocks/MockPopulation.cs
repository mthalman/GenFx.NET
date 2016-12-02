using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockPopulation : Population
    {
        public MockPopulation(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    [Component(typeof(MockPopulation))]
    class MockPopulationConfiguration : PopulationConfiguration
    {
    }

    class MockPopulation2 : Population
    {
        public MockPopulation2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    [Component(typeof(MockPopulation2))]
    class MockPopulation2Configuration : PopulationConfiguration
    {
    }
}
