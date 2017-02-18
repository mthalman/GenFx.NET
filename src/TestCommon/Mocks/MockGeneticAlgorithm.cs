using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GenFx;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockGeneticAlgorithm : GeneticAlgorithm
    {
        protected override Task CreateNextGenerationAsync(Population population)
        {
            throw new Exception("The method or operation is not implemented.");
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
