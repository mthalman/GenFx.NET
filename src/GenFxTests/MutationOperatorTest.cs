using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.MutationOperator and is intended
    /// to contain all GenFx.MutationOperator Unit Tests
    /// </summary>
    [TestClass()]
    public class MutationOperatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_NullAlgorithm()
        {
            MockMutationOperator op = new MockMutationOperator();
            AssertEx.Throws<ArgumentNullException>(() => op.Initialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_InvalidSetting1()
        {
            MockMutationOperator config = new MockMutationOperator();
            AssertEx.Throws<ValidationException>(() => config.MutationRate = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_InvalidSetting2()
        {
            MockMutationOperator config = new MockMutationOperator();
            AssertEx.Throws<ValidationException>(() => config.MutationRate = -1);
        }

        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Mutate()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(.03);
            MockMutationOperator op = new MockMutationOperator();
            op.Initialize(algorithm);
            IGeneticEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            entity.Age = 10;
            IGeneticEntity mutant = op.Mutate(entity);

            Assert.AreNotSame(entity, mutant, "Entities should not be same instance.");
            Assert.AreEqual(entity.Age, mutant.Age, "Age should be reset.");
            Assert.AreEqual(1, op.DoMutateCallCount, "Mutation not called correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Mutate_NullEntity()
        {
            MockMutationOperator op = new MockMutationOperator();
            AssertEx.Throws<ArgumentNullException>(() => op.Mutate(null));
        }

        private static IGeneticAlgorithm GetAlgorithm(double mutationRate)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = mutationRate
                }
            };
            return algorithm;
        }

        private class FakeMutationOperator : MutationOperatorBase
        {
            protected override bool GenerateMutation(IGeneticEntity entity)
            {
                return true;
            }
        }
    }
}
