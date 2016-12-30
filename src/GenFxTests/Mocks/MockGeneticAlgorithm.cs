using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GenFx;
using System.Threading.Tasks;

namespace GenFxTests.Mocks
{
    class MockGeneticAlgorithm : GeneticAlgorithm<MockGeneticAlgorithm, MockGeneticAlgorithmConfiguration>
    {
        public MockGeneticAlgorithm(ComponentConfigurationSet config)
            : base(config)
        {
        }

        protected override Task CreateNextGenerationAsync(IPopulation population)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RaiseGenerationCreatedEvent()
        {
            typeof(GeneticAlgorithm<MockGeneticAlgorithm, MockGeneticAlgorithmConfiguration>).GetMethod("OnGenerationCreated", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
        }
    }

    class MockGeneticAlgorithmConfiguration : GeneticAlgorithmConfiguration<MockGeneticAlgorithmConfiguration, MockGeneticAlgorithm>
    {
    }

    class MockGeneticAlgorithm2 : GeneticAlgorithm<MockGeneticAlgorithm2, MockGeneticAlgorithm2Configuration>
    {
        public MockGeneticAlgorithm2(ComponentConfigurationSet config)
            : base(config)
        {
        }

        protected override Task CreateNextGenerationAsync(IPopulation population)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class MockGeneticAlgorithm2Configuration : GeneticAlgorithmConfiguration<MockGeneticAlgorithm2Configuration, MockGeneticAlgorithm2>
    {
    }
}
