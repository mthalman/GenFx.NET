using GenFx;
using GenFx.ComponentLibrary.Lists;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon.Helpers;
using GenFx.Validation;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="SinglePointCrossoverOperator"/> class.
    /// </summary>
    [TestClass()]
    public class SinglePointCrossoverOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Crossover method works correctly.
        /// </summary>
        [TestMethod]
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

            Assert.AreEqual("1100", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("1001", resultEntity2.Representation, "Crossover not correct.");

            randomUtil.RandomVal = 3;
            result = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();

            resultEntity1 = (BinaryStringEntity)result[0];
            resultEntity2 = (BinaryStringEntity)result[1];

            Assert.AreEqual("1000", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("1101", resultEntity2.Representation, "Crossover not correct.");
        }

        /// <summary>
        /// Tests that the <see cref="SinglePointCrossoverOperator.GenerateCrossover"/> method works correctly.
        /// </summary>
        [TestMethod]
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

            Assert.AreEqual("1100", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("10011", resultEntity2.Representation, "Crossover not correct.");

            randomUtil.RandomVal = 3;
            result = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();

            resultEntity1 = (BinaryStringEntity)result[0];
            resultEntity2 = (BinaryStringEntity)result[1];

            Assert.AreEqual("1000", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("11011", resultEntity2.Representation, "Crossover not correct.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing null parents to <see cref="SinglePointCrossoverOperator.GenerateCrossover"/>.
        /// </summary>
        [TestMethod]
        public void SinglePointCrossoverOperator_GenerateCrossover_NullParents()
        {
            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator();
            PrivateObject accessor = new PrivateObject(op);
            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateCrossover", (IList<GeneticEntity>)null));
        }

        /// <summary>
        /// Tests that no validation exception is thrown if the operator is used with an
        /// algorithm that is configured with a <see cref="ListEntityBase"/>.
        /// </summary>
        [TestMethod]
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
        [TestMethod]
        public void SinglePointCrossoverOperator_Validation_WithoutListEntityBase()
        {
            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator();
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
                CrossoverOperator = new SinglePointCrossoverOperator()
            };

            await AssertEx.ThrowsAsync<ValidationException>(() => algorithm.InitializeAsync());
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
