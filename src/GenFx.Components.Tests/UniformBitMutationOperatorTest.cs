using GenFx.Components.Lists;
using System;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="UniformBitMutationOperator"/> class.
    ///</summary>
    public class UniformBitMutationOperatorTest
    {
        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [Fact]
        public async Task UniformBitMutationOperator_Mutate()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator2(),
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
            await algorithm.InitializeAsync();
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

            Assert.Equal("0010", mutant.Representation);
            Assert.Equal(0, mutant.Age);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity to <see cref="UniformBitMutationOperator.GenerateMutation"/>.
        /// </summary>
        [Fact]
        public void UniformBitMutationOperator_GenerateMutation_NullEntity()
        {
            UniformBitMutationOperator op = new UniformBitMutationOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateMutation", (GeneticEntity)null));
        }
    }
}
