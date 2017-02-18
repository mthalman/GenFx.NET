using GenFx.ComponentLibrary.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="UniformIntegerMutationOperator"/> class.
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new IntegerListEntity
                {
                    MinimumStartingLength = 4,
                    MaximumStartingLength = 4,
                    MaxElementValue = 2,
                    MinElementValue = 1
                },
                MutationOperator = new UniformIntegerMutationOperator
                {
                    MutationRate = 1
                }
            };
            UniformIntegerMutationOperator op = new UniformIntegerMutationOperator { MutationRate = 1 };
            op.Initialize(algorithm);
            IntegerListEntity entity = new IntegerListEntity { MinimumStartingLength = 4, MaximumStartingLength = 4, MaxElementValue = 2, MinElementValue = 1 };
            entity.Age = 10;
            entity.Initialize(algorithm);
            entity[0] = 1;
            entity[1] = 1;
            entity[2] = 2;
            entity[3] = 1;
            GeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("2, 2, 1, 2", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity to <see cref="UniformIntegerMutationOperator.GenerateMutation"/>.
        /// </summary>
        [TestMethod]
        public void UniformIntegerMutationOperator_GenerateMutation_NullEntity()
        {
            UniformIntegerMutationOperator op = new UniformIntegerMutationOperator();
            PrivateObject accessor = new PrivateObject(op);
            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateMutation", (GeneticEntity)null));
        }
    }
}
