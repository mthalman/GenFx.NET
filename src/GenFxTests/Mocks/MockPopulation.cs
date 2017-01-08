using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockPopulation : PopulationBase<MockPopulation, MockPopulationFactoryConfig>
    {
        public MockPopulation(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    class MockPopulationFactoryConfig : PopulationFactoryConfigBase<MockPopulationFactoryConfig, MockPopulation>
    {
    }

    class MockPopulation2 : PopulationBase<MockPopulation2, MockPopulation2FactoryConfig>
    {
        public MockPopulation2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    class MockPopulation2FactoryConfig : PopulationFactoryConfigBase<MockPopulation2FactoryConfig, MockPopulation2>
    {
    }
}
