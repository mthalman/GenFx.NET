using GenFx.Components.Lists;
using System;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ListShiftMutationOperator"/> class.
    /// </summary>
    public class ListShiftMutationOperatorTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_MiddleShiftRight()
        {
            this.TestMutation(1, 3, new int[] { 0, 3, 1, 2, 4 });
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_LeftEdgeShiftRight()
        {
            this.TestMutation(0, 2, new int[] { 2, 0, 1, 3, 4 });
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_RightEdgeShiftRight()
        {
            this.TestMutation(2, 4, new int[] { 0, 1, 4, 2, 3 });
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_MiddleShiftLeft()
        {
            this.TestMutation(3, 1, new int[] { 0, 2, 3, 1, 4 });
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_LeftEdgeShiftLeft()
        {
            this.TestMutation(2, 0, new int[] { 1, 2, 0, 3, 4 });
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_RightEdgeShiftLeft()
        {
            this.TestMutation(4, 2, new int[] { 0, 1, 3, 4, 2 });
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_BothEdgesShiftLeft()
        {
            this.TestMutation(4, 0, new int[] { 1, 2, 3, 4, 0 });
        }

        /// <summary>
        /// Tests that the <see cref="ListShiftMutationOperator.GenerateMutation"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_BothEdgesShiftRight()
        {
            this.TestMutation(0, 4, new int[] { 4, 0, 1, 2, 3 });
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity to <see cref="ListShiftMutationOperator.GenerateMutation"/>.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_NullEntity()
        {
            ListShiftMutationOperator op = new ListShiftMutationOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateMutation", (GeneticEntity)null));
        }

        /// <summary>
        /// Tests the result of <see cref="ListShiftMutationOperator.GenerateMutation"/> when no mutation occurs.
        /// </summary>
        [Fact]
        public void ListShiftMutationOperator_GenerateMutation_NoMutation()
        {
            ListShiftMutationOperator op = new ListShiftMutationOperator
            {
                MutationRate = 0
            };
            PrivateObject accessor = new PrivateObject(op);
            bool result = (bool)accessor.Invoke("GenerateMutation", new IntegerListEntity());
            Assert.False(result);
        }

        private void TestMutation(int firstRandomValue, int secondRandomValue, int[] expectedValues)
        {
            ListShiftMutationOperator op = new ListShiftMutationOperator
            {
                MutationRate = 1
            };

            RandomNumberService.Instance = new FakeRandomNumberService(firstRandomValue, secondRandomValue);

            IntegerListEntity entity = new IntegerListEntity
            {
                MinimumStartingLength = 5,
                MaximumStartingLength = 5,
            };
            entity.Initialize(new MockGeneticAlgorithm());
            for (int i = 0; i < 5; i++)
            {
                entity[i] = i;
            }

            IntegerListEntity result = (IntegerListEntity)op.Mutate(entity);
            Assert.Equal(expectedValues, result);
        }

        private class FakeRandomNumberService : IRandomNumberService
        {
            private int getRandomValueCount = 0;
            private int firstRandomValue;
            private int secondRandomValue;

            public FakeRandomNumberService(int firstRandomValue, int secondRandomValue)
            {
                this.firstRandomValue = firstRandomValue;
                this.secondRandomValue = secondRandomValue;
            }

            public double GetDouble()
            {
                return 0;
            }

            public int GetRandomValue(int maxValue)
            {
                this.getRandomValueCount++;
                Assert.NotEqual(3, this.getRandomValueCount);
                if (this.getRandomValueCount == 1)
                {
                    return this.firstRandomValue;
                }
                else
                {
                    return this.secondRandomValue;
                }
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                return 0;
            }
        }
    }
}
