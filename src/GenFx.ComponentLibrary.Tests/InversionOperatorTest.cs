using GenFx.ComponentLibrary.Lists;
using System;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="InversionOperator"/> class.
    ///</summary>
    public class InversionOperatorTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [Fact]
        public void InversionOperator_Mutate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new BinaryStringEntity
                {
                    MinimumStartingLength = 4,
                    MaximumStartingLength = 4
                },
                MutationOperator = new InversionOperator
                {
                    MutationRate = 1
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);

            InversionOperator op = (InversionOperator)algorithm.MutationOperator;
            op.Initialize(algorithm);
            BinaryStringEntity entity = (BinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity.Age = 10;
            entity.Initialize(algorithm);
            entity[0] = true;
            entity[1] = true;
            entity[2] = false;
            entity[3] = true;

            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.RandomValue = 1;

            GeneticEntity mutant = op.Mutate(entity);

            Assert.Equal("1011", mutant.Representation);
            Assert.Equal(0, mutant.Age);
        }

        /// <summary>
        /// Tests that the <see cref="InversionOperator.GenerateMutation"/> method works correctly
        /// when no mutation occurs.
        /// </summary>
        [Fact]
        public void InversionOperator_GenerateMutation_NoMutation()
        {
            InversionOperator op = new InversionOperator
            {
                MutationRate = 0
            };

            PrivateObject accessor = new PrivateObject(op);
            bool result = (bool)accessor.Invoke("GenerateMutation", new BinaryStringEntity());
            Assert.False(result);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed to <see cref="InversionOperator.GenerateMutation"/>.
        /// </summary>
        [Fact]
        public void InversionOperator_GenerateMutation_NullEntity()
        {
            InversionOperator op = new InversionOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateMutation", (GeneticEntity)null));
        }

        private class FakeRandomUtil : IRandomNumberService
        {
            public int RandomValue;

            public int GetRandomValue(int maxValue)
            {
                return this.RandomValue++;
            }

            public double GetDouble()
            {
                return 1;
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
