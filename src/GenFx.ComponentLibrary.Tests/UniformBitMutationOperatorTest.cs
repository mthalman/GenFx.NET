using GenFx.ComponentLibrary.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="UniformBitMutationOperator"/> class.
    ///</summary>
    [TestClass]
    public class UniformBitMutationOperatorTest
    {
        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void UniformBitMutationOperator_Mutate()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new BinaryStringEntity
                {
                    MinimumStartingLength = 4,
                    MaximumStartingLength = 4
                },
                MutationOperator = new UniformBitMutationOperator
                {
                    MutationRate = 1
                }
            };
            UniformBitMutationOperator op = new UniformBitMutationOperator { MutationRate = 1 };
            op.Initialize(algorithm);
            BinaryStringEntity entity = new BinaryStringEntity { MinimumStartingLength = 4, MaximumStartingLength = 4 };
            entity.Age = 10;
            entity.Initialize(algorithm);
            entity[0] = true;
            entity[1] = true;
            entity[2] = false;
            entity[3] = true;
            GeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("0010", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity to <see cref="UniformBitMutationOperator.GenerateMutation"/>.
        /// </summary>
        [TestMethod]
        public void UniformBitMutationOperator_GenerateMutation_NullEntity()
        {
            UniformBitMutationOperator op = new UniformBitMutationOperator();
            PrivateObject accessor = new PrivateObject(op);
            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateMutation", (GeneticEntity)null));
        }
    }
}
