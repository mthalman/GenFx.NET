using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GenFx;
using System.Threading.Tasks;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockGeneticAlgorithm : GeneticAlgorithm<MockGeneticAlgorithm, MockGeneticAlgorithmFactoryConfig>
    {
        public MockGeneticAlgorithm(ComponentFactoryConfigSet config)
            : base(config)
        {
        }

        protected override Task CreateNextGenerationAsync(IPopulation population)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RaiseGenerationCreatedEvent()
        {
            typeof(GeneticAlgorithm<MockGeneticAlgorithm, MockGeneticAlgorithmFactoryConfig>).GetMethod("OnGenerationCreated", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
        }
    }

    class MockGeneticAlgorithmFactoryConfig : GeneticAlgorithmFactoryConfig<MockGeneticAlgorithmFactoryConfig, MockGeneticAlgorithm>
    {
    }

    class MockGeneticAlgorithm2 : GeneticAlgorithm<MockGeneticAlgorithm2, MockGeneticAlgorithm2FactoryConfig>
    {
        public MockGeneticAlgorithm2(ComponentFactoryConfigSet config)
            : base(config)
        {
        }

        protected override Task CreateNextGenerationAsync(IPopulation population)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class MockGeneticAlgorithm2FactoryConfig : GeneticAlgorithmFactoryConfig<MockGeneticAlgorithm2FactoryConfig, MockGeneticAlgorithm2>
    {
    }
}
