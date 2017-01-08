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
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor()
        {
            double mutationRate = .005;
            IGeneticAlgorithm algorithm = GetAlgorithm(mutationRate);
            MockMutationOperator op = new MockMutationOperator(algorithm);
            Assert.IsInstanceOfType(op.Configuration, typeof(MockMutationOperatorFactoryConfig));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new MockMutationOperator(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config is missing.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_MissingConfig()
        {
            AssertEx.Throws<ArgumentException>(() => new MockMutationOperator(new MockGeneticAlgorithm(new ComponentFactoryConfigSet())));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_InvalidSetting1()
        {
            MockMutationOperatorFactoryConfig config = new MockMutationOperatorFactoryConfig();
            AssertEx.Throws<ValidationException>(() => config.MutationRate = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_InvalidSetting2()
        {
            MockMutationOperatorFactoryConfig config = new MockMutationOperatorFactoryConfig();
            AssertEx.Throws<ValidationException>(() => config.MutationRate = -1);
        }

        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Mutate()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(.03);
            MockMutationOperator op = new MockMutationOperator(algorithm);
            IGeneticEntity entity = new MockEntity(algorithm);
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
            IGeneticAlgorithm algorithm = GetAlgorithm(.03);
            MockMutationOperator op = new MockMutationOperator(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => op.Mutate(null));
        }

        private static IGeneticAlgorithm GetAlgorithm(double mutationRate)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                MutationOperator = new MockMutationOperatorFactoryConfig
                {
                    MutationRate = mutationRate
                }
            });
            return algorithm;
        }

        private class FakeMutationOperator : MutationOperatorBase<FakeMutationOperator, FakeMutationOperatorFactoryConfig>
        {
            public FakeMutationOperator(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override bool GenerateMutation(IGeneticEntity entity)
            {
                return true;
            }
        }

        private class FakeMutationOperatorFactoryConfig : MutationOperatorFactoryConfigBase<FakeMutationOperatorFactoryConfig, FakeMutationOperator>
        {
        }
    }
}
