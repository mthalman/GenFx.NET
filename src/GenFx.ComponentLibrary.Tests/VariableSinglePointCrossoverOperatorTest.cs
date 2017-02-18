using GenFx.ComponentLibrary.Lists;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="VariableSinglePointCrossoverOperator"/> class.
    /// </summary>
    [TestClass]
    public class VariableSinglePointCrossoverOperatorTest
    {
        [TestCleanup]
        public void TestCleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the <see cref="VariableSinglePointCrossoverOperator.GenerateCrossover"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void VariableSinglePointCrossoverOperator_GenerateCrossover()
        {
            VariableSinglePointCrossoverOperator op = new VariableSinglePointCrossoverOperator();
            PrivateObject accessor = new PrivateObject(op);

            RandomNumberService.Instance = new FakeRandomNumberService(
                new Queue<int>(new int[] { 5, 3 }),
                new Queue<int>(new int[] { 3, 2 }));

            List<GeneticEntity> entities = new List<GeneticEntity>
            {
                new TestEntity<int>(1, 2, 3, 4, 5),
                new TestEntity<int>(4, 1, 7)
            };

            IEnumerable<TestEntity<int>> result = 
                ((IEnumerable<GeneticEntity>)accessor.Invoke("GenerateCrossover", entities))
                .Cast<TestEntity<int>>();

            Assert.AreEqual(2, result.Count());
            CollectionAssert.AreEqual(
                new int[] { 1, 2, 3, 7 }, result.ElementAt(0).InnerList);
            CollectionAssert.AreEqual(
                new int[] { 4, 1, 4, 5 }, result.ElementAt(1).InnerList);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing null parents to <see cref="VariableSinglePointCrossoverOperator.GenerateCrossover"/>.
        /// </summary>
        [TestMethod]
        public void VariableSinglePointCrossoverOperator_GenerateCrossover_NullParents()
        {
            VariableSinglePointCrossoverOperator op = new VariableSinglePointCrossoverOperator();
            PrivateObject accessor = new PrivateObject(op);

            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateCrossover", (IList<GeneticEntity>)null));
        }

        /// <summary>
        /// Tests that no validation exception is thrown if the operator is used with an
        /// algorithm that is configured with a <see cref="ListEntityBase"/>.
        /// </summary>
        [TestMethod]
        public void VariableSinglePointCrossoverOperator_Validation_WithListEntityBase()
        {
            VariableSinglePointCrossoverOperator op = new VariableSinglePointCrossoverOperator();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new IntegerListEntity()
            };
            op.Initialize(algorithm);

            op.Validate();
        }

        /// <summary>
        /// Tests that a validation exception is thrown if the operator is used with an
        /// algorithm that is not configured with a <see cref="ListEntityBase"/>.
        /// </summary>
        [TestMethod]
        public void VariableSinglePointCrossoverOperator_Validation_WithoutListEntityBase()
        {
            VariableSinglePointCrossoverOperator op = new VariableSinglePointCrossoverOperator();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity()
            };
            op.Initialize(algorithm);

            AssertEx.Throws<ValidationException>(() => op.Validate());
        }

        /// <summary>
        /// Tests that a validation exception is thrown if the operator is used with an
        /// algorithm that is configured with a <see cref="ListEntityBase"/> that requires unique element values.
        /// </summary>
        [TestMethod]
        public async Task SinglePointCrossoverOperator_Validation_RequiresUniqueElementValues()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new IntegerListEntity
                {
                    MinElementValue = 0,
                    MaxElementValue = 10,
                    MinimumStartingLength = 5,
                    MaximumStartingLength = 5,
                    RequiresUniqueElementValues = true
                },
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator2(),
                SelectionOperator = new MockSelectionOperator(),
                CrossoverOperator = new VariableSinglePointCrossoverOperator()
            };

            await AssertEx.ThrowsAsync<ValidationException>(() => algorithm.InitializeAsync());
        }

        /// <summary>
        /// Tests that a validation exception is thrown if the operator is used with an
        /// algorithm that is configured with a <see cref="ListEntityBase"/> that is a fixed size.
        /// </summary>
        [TestMethod]
        public async Task SinglePointCrossoverOperator_Validation_IsFixedSize()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new IntegerListEntity
                {
                    MinElementValue = 0,
                    MaxElementValue = 10,
                    MinimumStartingLength = 5,
                    MaximumStartingLength = 5,
                    IsFixedSize = true
                },
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator2(),
                SelectionOperator = new MockSelectionOperator(),
                CrossoverOperator = new VariableSinglePointCrossoverOperator()
            };

            await AssertEx.ThrowsAsync<ValidationException>(() => algorithm.InitializeAsync());
        }

        private class FakeRandomNumberService : IRandomNumberService
        {
            private Queue<int> randomValues;
            private Queue<int> expectedMaxValues;

            public FakeRandomNumberService(Queue<int> expectedMaxValues, Queue<int> randomValues)
            {
                this.expectedMaxValues = expectedMaxValues;
                this.randomValues = randomValues;
            }

            public double GetDouble()
            {
                throw new NotImplementedException();
            }

            public int GetRandomValue(int maxValue)
            {
                Assert.AreEqual(this.expectedMaxValues.Dequeue(), maxValue);
                return this.randomValues.Dequeue();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new NotImplementedException();
            }
        }

        private class TestEntity<T> : ListEntityBase<T>
        {
            public List<T> InnerList = new List<T>();

            public TestEntity(params T[] values)
            {
                this.InnerList.AddRange(values);
            }

            public override T this[int index]
            {
                get { return this.InnerList[index]; }
                set { this.InnerList[index] = value; }
            }

            public override bool IsFixedSize
            {
                get;
                set;
            }

            public override int Length
            {
                get { return this.InnerList.Count; }
                set
                {
                    if (value < this.InnerList.Count)
                    {
                        this.InnerList.RemoveRange(value, this.InnerList.Count - value);
                    }
                    else if (value > this.InnerList.Count)
                    {
                        this.InnerList.AddRange(Enumerable.Repeat<T>(default(T), value - this.InnerList.Count));
                    }

                }
            }

            public override bool RequiresUniqueElementValues
            {
                get;
                set;
            }
        }
    }
}
