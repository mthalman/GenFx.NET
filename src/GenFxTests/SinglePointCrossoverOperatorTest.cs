using GenFx;
using GenFx.ComponentLibrary.BinaryStrings;
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
            RandomHelper.Instance = new RandomHelper();
        }

        /// <summary>
        /// Tests that the Crossover method works correctly.
        /// </summary>
        [TestMethod]
        public void SinglePointCrossoverOperator_Crossover()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            SinglePointCrossoverOperatorConfiguration opConfig = new SinglePointCrossoverOperatorConfiguration();
            opConfig.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = opConfig;

            FixedLengthBinaryStringEntityConfiguration entityConfig = new FixedLengthBinaryStringEntityConfiguration();
            entityConfig.Length = 4;
            algorithm.ConfigurationSet.Entity = entityConfig;

            SinglePointCrossoverOperator op = new SinglePointCrossoverOperator(algorithm);
            FixedLengthBinaryStringEntity entity1 = new FixedLengthBinaryStringEntity(algorithm);
            entity1.Initialize();
            entity1[0] = 1;
            entity1[1] = 0;
            entity1[2] = 0;
            entity1[3] = 1;

            FixedLengthBinaryStringEntity entity2 = new FixedLengthBinaryStringEntity(algorithm);
            entity2.Initialize();
            entity2[0] = 1;
            entity2[1] = 1;
            entity2[2] = 0;
            entity2[3] = 0;

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomHelper.Instance = randomUtil;

            randomUtil.RandomVal = 1;
            IList<GeneticEntity> result = op.Crossover(entity1, entity2);

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

        private class TestRandomUtil : IRandomHelper
        {
            internal int RandomVal;

            public int GetRandomValue(int maxValue)
            {
                return RandomVal;
            }

            public double GetRandomRatio()
            {
                return new RandomHelper().GetRandomRatio();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

    }


}
