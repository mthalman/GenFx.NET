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
        protected override Task CreateNextGenerationAsync(Population population)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RaiseGenerationCreatedEvent()
        {
            typeof(GeneticAlgorithm).GetMethod("OnGenerationCreated", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
        }
    }
    
    class MockGeneticAlgorithm2 : GeneticAlgorithm
    {
        protected override Task CreateNextGenerationAsync(Population population)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
