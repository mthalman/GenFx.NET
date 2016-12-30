using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new FixedLengthBinaryStringEntityConfiguration
                {
                    Length = 4
                },
                MutationOperator = new UniformBitMutationOperatorConfiguration
                {
                    MutationRate = 1
                }
            });
            UniformBitMutationOperator op = new UniformBitMutationOperator(algorithm);
            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity(algorithm);
            entity.Age = 10;
            entity.Initialize();
            entity[0] = true;
            entity[1] = true;
            entity[2] = false;
            entity[3] = true;
            IGeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("0010", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }
    }
}
