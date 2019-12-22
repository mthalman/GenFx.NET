using GenFx.Validation;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// This is a test class for GenFx.MutationOperator and is intended
    /// to contain all GenFx.MutationOperator Unit Tests
    /// </summary>
    public class MutationOperatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void MutationOperator_Ctor_NullAlgorithm()
        {
            MockMutationOperator op = new MockMutationOperator();
            Assert.Throws<ArgumentNullException>(() => op.Initialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [Fact]
        public void MutationOperator_Ctor_InvalidSetting1()
        {
            MockMutationOperator config = new MockMutationOperator();
            Assert.Throws<ValidationException>(() => config.MutationRate = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the MutationRate setting.
        /// </summary>
        [Fact]
        public void MutationOperator_Ctor_InvalidSetting2()
        {
            MockMutationOperator config = new MockMutationOperator();
            Assert.Throws<ValidationException>(() => config.MutationRate = -1);
        }

        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [Fact]
        public void MutationOperator_Mutate()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(.03);
            MockMutationOperator op = new MockMutationOperator();
            op.Initialize(algorithm);
            GeneticEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            entity.Age = 10;
            GeneticEntity mutant = op.Mutate(entity);

            Assert.NotSame(entity, mutant);
            Assert.Equal(entity.Age, mutant.Age);
            Assert.Equal(1, op.DoMutateCallCount);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [Fact]
        public void MutationOperator_Mutate_NullEntity()
        {
            MockMutationOperator op = new MockMutationOperator();
            Assert.Throws<ArgumentNullException>(() => op.Mutate(null));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void MutationOperator_Serialization()
        {
            MockMutationOperator op = new MockMutationOperator
            {
                MutationRate = .8
            };

            MockMutationOperator result = (MockMutationOperator)SerializationHelper.TestSerialization(op, new Type[0]);

            Assert.Equal(op.MutationRate, result.MutationRate);
        }

        private static GeneticAlgorithm GetAlgorithm(double mutationRate)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
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

        private class FakeMutationOperator : MutationOperator
        {
            protected override bool GenerateMutation(GeneticEntity entity)
            {
                return true;
            }
        }
    }
}
