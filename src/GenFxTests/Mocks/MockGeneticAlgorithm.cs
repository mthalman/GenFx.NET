using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GenFx;
using System.Threading.Tasks;

namespace GenFxTests.Mocks
{
    class MockGeneticAlgorithm : GeneticAlgorithm
    {
        public MockGeneticAlgorithm()
        {
        }

        protected override Task CreateNextGenerationAsync(Population population)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RaiseFitnessEvaluatedEvent()
        {
            typeof(GeneticAlgorithm).GetMethod("RaiseFitnessEvaluatedEvent", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
        }
    }

    [Component(typeof(MockGeneticAlgorithm))]
    class MockGeneticAlgorithmConfiguration : GeneticAlgorithmConfiguration
    {
    }

    class MockGeneticAlgorithm2 : GeneticAlgorithm
    {
        public MockGeneticAlgorithm2()
        {
        }

        protected override Task CreateNextGenerationAsync(Population population)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    [Component(typeof(MockGeneticAlgorithm2))]
    class MockGeneticAlgorithm2Configuration : GeneticAlgorithmConfiguration
    {
    }
}
