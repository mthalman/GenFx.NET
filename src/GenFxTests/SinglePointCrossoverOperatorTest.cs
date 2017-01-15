using GenFx;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.ComponentLibrary.Lists;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using GenFx.Contracts;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Lists.SinglePointCrossoverOperator and is intended
    ///to contain all GenFx.ComponentLibrary.Lists.SinglePointCrossoverOperator Unit Tests
    ///</summary>
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
        public void SinglePointCrossoverOperator_Crossover()
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
                GeneticEntitySeed = new FixedLengthBinaryStringEntity
                {
                    FixedLength = 4
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);

            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator { CrossoverRate = 1 };
            op.Initialize(algorithm);
            FixedLengthBinaryStringEntity entity1 = (FixedLengthBinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity1[0] = true;
            entity1[1] = false;
            entity1[2] = false;
            entity1[3] = true;

            FixedLengthBinaryStringEntity entity2 = (FixedLengthBinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity2.Initialize(algorithm);
            entity2[0] = true;
            entity2[1] = true;
            entity2[2] = false;
            entity2[3] = false;

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomVal = 1;
            IList<IGeneticEntity> result = op.Crossover(entity1, entity2);

            FixedLengthBinaryStringEntity resultEntity1 = (FixedLengthBinaryStringEntity)result[0];
            FixedLengthBinaryStringEntity resultEntity2 = (FixedLengthBinaryStringEntity)result[1];

            Assert.AreEqual("1100", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("1001", resultEntity2.Representation, "Crossover not correct.");

            randomUtil.RandomVal = 3;
            result = op.Crossover(entity1, entity2);

            resultEntity1 = (FixedLengthBinaryStringEntity)result[0];
            resultEntity2 = (FixedLengthBinaryStringEntity)result[1];

            Assert.AreEqual("1000", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("1101", resultEntity2.Representation, "Crossover not correct.");
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
