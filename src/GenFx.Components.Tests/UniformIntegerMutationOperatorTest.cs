using GenFx.Components.Lists;
using System;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="UniformIntegerMutationOperator"/> class.
    ///</summary>
    public class UniformIntegerMutationOperatorTest
    {
        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [Fact]
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

            Assert.Equal("2, 2, 1, 2", mutant.Representation);
            Assert.Equal(0, mutant.Age);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity to <see cref="UniformIntegerMutationOperator.GenerateMutation"/>.
        /// </summary>
        [Fact]
        public void UniformIntegerMutationOperator_GenerateMutation_NullEntity()
        {
            UniformIntegerMutationOperator op = new UniformIntegerMutationOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateMutation", (GeneticEntity)null));
        }
    }
}
