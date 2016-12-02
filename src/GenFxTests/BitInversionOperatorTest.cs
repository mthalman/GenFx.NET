using GenFx;
using GenFx.ComponentLibrary.BinaryStrings;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.BinaryStrings.BitInversionOperator and is intended
    ///to contain all GenFx.ComponentLibrary.BinaryStrings.BitInversionOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class BitInversionOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomHelper.Instance = new RandomHelper();
        }

        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void BitInversionOperator_Mutate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            FixedLengthBinaryStringEntityConfiguration entityConfig = new FixedLengthBinaryStringEntityConfiguration();
            entityConfig.Length = 4;
            algorithm.ConfigurationSet.Entity = entityConfig;
            BitInversionOperatorConfiguration opConfig = new BitInversionOperatorConfiguration();
            opConfig.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = opConfig;
            BitInversionOperator op = new BitInversionOperator(algorithm);
            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("age", 10);
            entity.Initialize();
            entity[0] = 1;
            entity[1] = 1;
            entity[2] = 0;
            entity[3] = 1;

            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomHelper.Instance = randomUtil;
            randomUtil.RandomValue = 1;

            GeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("1011", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }

        private class FakeRandomUtil : IRandomHelper
        {
            public int RandomValue;

            public int GetRandomValue(int maxValue)
            {
                return this.RandomValue++;
            }

            public double GetRandomRatio()
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
