using GenFx;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockGeneticAlgorithm : GeneticAlgorithm
    {
        protected override Task CreateNextGenerationAsync(Population population)
        {
            return Task.CompletedTask;
        }

        public void RaiseGenerationCreatedEvent()
        {
            typeof(GeneticAlgorithm).GetMethod("OnGenerationCreated", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
        }

        public void TestApplyCrossover(Population population, IList<GeneticEntity> parents)
        {
            this.ApplyCrossover(population, parents);
        }

        public void TestApplyElitism(Population population)
        {
            this.ApplyElitism(population);
        }

        public void TestApplyMutation(IList<GeneticEntity> entities)
        {
            this.ApplyMutation(entities);
        }
    }

    [DataContract]
    public class MockGeneticAlgorithm2 : GeneticAlgorithm
    {
        protected override Task CreateNextGenerationAsync(Population population)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
