using GenFx;
using GenFx.ComponentLibrary.BinaryStrings;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.BinaryStrings.VariableSinglePointBitCrossoverOperator and is intended
    ///to contain all GenFx.ComponentLibrary.BinaryStrings.VariableSinglePointBitCrossoverOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class VariableVariableSinglePointBitCrossoverOperatorTest
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
        public void VariableSinglePointBitCrossoverOperator_Crossover()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            VariableSinglePointBitCrossoverOperatorConfiguration opConfig = new VariableSinglePointBitCrossoverOperatorConfiguration();
            opConfig.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = opConfig;

            VariableLengthBinaryStringEntityConfiguration entityConfig = new VariableLengthBinaryStringEntityConfiguration();
            entityConfig.MinimumStartingLength = 4;
            entityConfig.MaximumStartingLength = 6;
            algorithm.ConfigurationSet.Entity = entityConfig;

            VariableSinglePointBitCrossoverOperator op = new VariableSinglePointBitCrossoverOperator(algorithm);
            VariableLengthBinaryStringEntity entity1 = new VariableLengthBinaryStringEntity(algorithm);
            entity1.Initialize();
            entity1.Length = 4;
            entity1[0] = 1;
            entity1[1] = 0;
            entity1[2] = 0;
            entity1[3] = 1;

            VariableLengthBinaryStringEntity entity2 = new VariableLengthBinaryStringEntity(algorithm);
            entity2.Initialize();
            entity2.Length = 6;
            entity2[0] = 1;
            entity2[1] = 1;
            entity2[2] = 0;
            entity2[3] = 0;
            entity2[4] = 0;
            entity2[5] = 1;

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomHelper.Instance = randomUtil;

            randomUtil.RandomVal1 = 1;
            randomUtil.RandomVal2 = 5;
            IList<GeneticEntity> result = op.Crossover(entity1, entity2);

            VariableLengthBinaryStringEntity resultEntity1 = (VariableLengthBinaryStringEntity)result[0];
            VariableLengthBinaryStringEntity resultEntity2 = (VariableLengthBinaryStringEntity)result[1];

            Assert.AreEqual("11", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("11000001", resultEntity2.Representation, "Crossover not correct.");

            randomUtil = new TestRandomUtil();
            RandomHelper.Instance = randomUtil;
            randomUtil.RandomVal1 = 3;
            randomUtil.RandomVal2 = 2;
            result = op.Crossover(entity1, entity2);

            resultEntity1 = (VariableLengthBinaryStringEntity)result[0];
            resultEntity2 = (VariableLengthBinaryStringEntity)result[1];

            Assert.AreEqual("1000001", resultEntity1.Representation, "Crossover not correct.");
            Assert.AreEqual("111", resultEntity2.Representation, "Crossover not correct.");
        }

        private class TestRandomUtil : IRandomHelper
        {
            internal int RandomVal1;
            internal int RandomVal2;

            private bool calledOnce = false;

            public int GetRandomValue(int maxValue)
            {
                if (!calledOnce)
                {
                    calledOnce = true;
                    return RandomVal1;
                }
                return RandomVal2;
            }

            public double GetRandomRatio()
            {
                return new RandomHelper().GetRandomRatio();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                return 1;
            }
        }

    }


}
