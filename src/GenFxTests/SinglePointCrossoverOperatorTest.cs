using GenFx;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.ComponentLibrary.Lists;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                CrossoverOperator = new SinglePointCrossoverOperatorConfiguration
                {
                    CrossoverRate = 1
                },
                Entity = new FixedLengthBinaryStringEntityConfiguration
                {
                    Length = 4
                }
            });

            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator(algorithm);
            FixedLengthBinaryStringEntity entity1 = new FixedLengthBinaryStringEntity(algorithm);
            entity1.Initialize();
            entity1[0] = true;
            entity1[1] = false;
            entity1[2] = false;
            entity1[3] = true;

            FixedLengthBinaryStringEntity entity2 = new FixedLengthBinaryStringEntity(algorithm);
            entity2.Initialize();
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

            public double GetRandomPercentRatio()
            {
                return new RandomNumberService().GetRandomPercentRatio();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

    }


}
