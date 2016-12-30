using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockPopulation : PopulationBase<MockPopulation, MockPopulationConfiguration>
    {
        public MockPopulation(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    class MockPopulationConfiguration : PopulationConfigurationBase<MockPopulationConfiguration, MockPopulation>
    {
    }

    class MockPopulation2 : PopulationBase<MockPopulation2, MockPopulation2Configuration>
    {
        public MockPopulation2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    class MockPopulation2Configuration : PopulationConfigurationBase<MockPopulation2Configuration, MockPopulation2>
    {
    }
}
