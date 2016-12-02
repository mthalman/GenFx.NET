using GenFx;
using GenFx.ComponentLibrary.Lists;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Lists.UniformIntegerMutationOperator and is intended
    ///to contain all GenFx.ComponentLibrary.Lists.UniformIntegerMutationOperator Unit Tests
    ///</summary>
    [TestClass]
    public class UniformIntegerMutationOperatorTest
    {
        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void UniformIntegerMutationOperatorTest_Mutate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            FixedLengthIntegerListEntityConfiguration entityConfig = new FixedLengthIntegerListEntityConfiguration();
            entityConfig.Length = 4;
            entityConfig.MaxElementValue = 2;
            entityConfig.MinElementValue = 1;
            algorithm.ConfigurationSet.Entity = entityConfig;
            UniformIntegerMutationOperatorConfiguration opConfig = new UniformIntegerMutationOperatorConfiguration();
            opConfig.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = opConfig;
            UniformIntegerMutationOperator op = new UniformIntegerMutationOperator(algorithm);
            FixedLengthIntegerListEntity entity = new FixedLengthIntegerListEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("age", 10);
            entity.Initialize();
            entity[0] = 1;
            entity[1] = 1;
            entity[2] = 2;
            entity[3] = 1;
            GeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("2, 2, 1, 2", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }
    }
}
