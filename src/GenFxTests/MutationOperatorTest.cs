using GenFx;
using GenFx.ComponentModel;
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
            GeneticAlgorithm algorithm = GetAlgorithm(mutationRate);
            MockMutationOperator op = new MockMutationOperator(algorithm);
            Assert.AreEqual(mutationRate, op.MutationRate, "MutationRate not initialized correctly.");
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
            AssertEx.Throws<ArgumentException>(() => new MockMutationOperator(new MockGeneticAlgorithm()));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_InvalidSetting1()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockMutationOperatorConfiguration config = new MockMutationOperatorConfiguration();
            AssertEx.Throws<ValidationException>(() => config.MutationRate = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Ctor_InvalidSetting2()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockMutationOperatorConfiguration config = new MockMutationOperatorConfiguration();
            AssertEx.Throws<ValidationException>(() => config.MutationRate = -1);
        }

        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Mutate()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(.03);
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            MockMutationOperator op = new MockMutationOperator(algorithm);
            GeneticEntity entity = new MockEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("age", 10);
            GeneticEntity mutant = op.Mutate(entity);

            Assert.AreNotSame(entity, mutant, "Entities should not be same instance.");
            Assert.AreEqual(entity.Age, mutant.Age, "Age should be reset.");
            Assert.AreEqual(1, op.DoMutateCallCount, "Mutation not called correctly.");

            algorithm.ConfigurationSet.MutationOperator = new FakeMutationOperatorConfiguration();
            FakeMutationOperator fakeOp = new FakeMutationOperator(algorithm);
            mutant = fakeOp.Mutate(entity);
            Assert.AreNotSame(entity, mutant, "Entities should not be same instance.");
            Assert.AreEqual(0, mutant.Age, "Age should be reset.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod]
        public void MutationOperator_Mutate_NullEntity()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(.03);
            MockMutationOperator op = new MockMutationOperator(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => op.Mutate(null));
        }

        private static GeneticAlgorithm GetAlgorithm(double mutationRate)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockMutationOperatorConfiguration config = new MockMutationOperatorConfiguration();
            config.MutationRate = mutationRate;
            algorithm.ConfigurationSet.MutationOperator = config;
            return algorithm;
        }

        private class FakeMutationOperator : MutationOperator
        {
            public FakeMutationOperator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override bool GenerateMutation(GeneticEntity entity)
            {
                return true;
            }
        }

        [Component(typeof(FakeMutationOperator))]
        private class FakeMutationOperatorConfiguration : MutationOperatorConfiguration
        {
        }
    }
}
