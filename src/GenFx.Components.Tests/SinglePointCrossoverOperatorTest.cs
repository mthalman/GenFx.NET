using GenFx.Components.Lists;
using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="SinglePointCrossoverOperator"/> class.
    /// </summary>
    public class SinglePointCrossoverOperatorTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Crossover method works correctly.
        /// </summary>
        [Fact]
        public void SinglePointCrossoverOperator_GenerateCrossover_SameLength()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                CrossoverOperator = new SinglePointCrossoverOperator
                {
                    CrossoverRate = 1
                },
                GeneticEntitySeed = new BinaryStringEntity
                {
                    MinimumStartingLength = 4,
                    MaximumStartingLength = 4
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);

            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator { CrossoverRate = 1 };
            op.Initialize(algorithm);
            BinaryStringEntity entity1 = (BinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity1[0] = true;
            entity1[1] = false;
            entity1[2] = false;
            entity1[3] = true;

            BinaryStringEntity entity2 = (BinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity2.Initialize(algorithm);
            entity2[0] = true;
            entity2[1] = true;
            entity2[2] = false;
            entity2[3] = false;

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomVal = 1;
            IList<GeneticEntity> result = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();

            BinaryStringEntity resultEntity1 = (BinaryStringEntity)result[0];
            BinaryStringEntity resultEntity2 = (BinaryStringEntity)result[1];

            Assert.Equal("1100", resultEntity1.Representation);
            Assert.Equal("1001", resultEntity2.Representation);

            randomUtil.RandomVal = 3;
            result = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();

            resultEntity1 = (BinaryStringEntity)result[0];
            resultEntity2 = (BinaryStringEntity)result[1];

            Assert.Equal("1000", resultEntity1.Representation);
            Assert.Equal("1101", resultEntity2.Representation);
        }

        /// <summary>
        /// Tests that the <see cref="SinglePointCrossoverOperator.GenerateCrossover"/> method works correctly.
        /// </summary>
        [Fact]
        public void SinglePointCrossoverOperator_GenerateCrossover_DifferentLength()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                CrossoverOperator = new SinglePointCrossoverOperator
                {
                    CrossoverRate = 1
                },
                GeneticEntitySeed = new BinaryStringEntity
                {
                    MinimumStartingLength = 4,
                    MaximumStartingLength = 4
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);

            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator { CrossoverRate = 1 };
            op.Initialize(algorithm);
            BinaryStringEntity entity1 = (BinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity1.Length = 5;
            entity1[0] = true;
            entity1[1] = false;
            entity1[2] = false;
            entity1[3] = true;
            entity1[4] = true;

            BinaryStringEntity entity2 = (BinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity2.Initialize(algorithm);
            entity2[0] = true;
            entity2[1] = true;
            entity2[2] = false;
            entity2[3] = false;

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomVal = 1;
            IList<GeneticEntity> result = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();

            BinaryStringEntity resultEntity1 = (BinaryStringEntity)result[0];
            BinaryStringEntity resultEntity2 = (BinaryStringEntity)result[1];

            Assert.Equal("1100", resultEntity1.Representation);
            Assert.Equal("10011", resultEntity2.Representation);

            randomUtil.RandomVal = 3;
            result = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();

            resultEntity1 = (BinaryStringEntity)result[0];
            resultEntity2 = (BinaryStringEntity)result[1];

            Assert.Equal("1000", resultEntity1.Representation);
            Assert.Equal("11011", resultEntity2.Representation);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing null parents to <see cref="SinglePointCrossoverOperator.GenerateCrossover"/>.
        /// </summary>
        [Fact]
        public void SinglePointCrossoverOperator_GenerateCrossover_NullParents()
        {
            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateCrossover", (IList<GeneticEntity>)null));
        }

        /// <summary>
        /// Tests that no validation exception is thrown if the operator is used with an
        /// algorithm that is configured with a <see cref="ListEntityBase"/>.
        /// </summary>
        [Fact]
        public void SinglePointCrossoverOperator_Validation_WithListEntityBase()
        {
            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator();
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
        [Fact]
        public void SinglePointCrossoverOperator_Validation_WithoutListEntityBase()
        {
            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity()
            };
            op.Initialize(algorithm);

            Assert.Throws<ValidationException>(() => op.Validate());
        }

        /// <summary>
        /// Tests that a validation exception is thrown if the operator is used with an
        /// algorithm that is configured with a <see cref="ListEntityBase"/> that requires unique element values.
        /// </summary>
        [Fact]
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
                CrossoverOperator = new SinglePointCrossoverOperator()
            };

            await Assert.ThrowsAsync<ValidationException>(() => algorithm.InitializeAsync());
        }

        private class TestRandomUtil : IRandomNumberService
        {
            internal int RandomVal;

            public int GetRandomValue(int maxValue)
            {
                return RandomVal;
            }

            public double GetDouble()
            {
                return new RandomNumberService().GetDouble();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
