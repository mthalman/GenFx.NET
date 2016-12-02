using GenFx;
using GenFx.ComponentLibrary.BinaryStrings;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.BinaryStrings.UniformBitMutationOperator and is intended
    ///to contain all GenFx.ComponentLibrary.BinaryStrings.UniformBitMutationOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class UniformBitMutationOperatorTest
    {
        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void UniformBitMutationOperator_Mutate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            FixedLengthBinaryStringEntityConfiguration entityConfig = new FixedLengthBinaryStringEntityConfiguration();
            entityConfig.Length = 4;
            algorithm.ConfigurationSet.Entity = entityConfig;
            UniformBitMutationOperatorConfiguration opConfig = new UniformBitMutationOperatorConfiguration();
            opConfig.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = opConfig;
            UniformBitMutationOperator op = new UniformBitMutationOperator(algorithm);
            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("age", 10);
            entity.Initialize();
            entity[0] = 1;
            entity[1] = 1;
            entity[2] = 0;
            entity[3] = 1;
            GeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("0010", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }
    }
}
