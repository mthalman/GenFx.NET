using GenFx;
using GenFx.ComponentLibrary.Lists;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Lists.IntegerInversionOperator and is intended
    ///to contain all GenFx.ComponentLibrary.Lists.IntegerInversionOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class IntegerInversionOperatorTest
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
        public void IntegerInversionOperator_Mutate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            TestIntegerListEntityConfiguration entityConfig = new TestIntegerListEntityConfiguration();
            entityConfig.MinElementValue = 0;
            entityConfig.MaxElementValue = 10;
            algorithm.ConfigurationSet.Entity = entityConfig;
            IntegerInversionOperatorConfiguration opConfig = new IntegerInversionOperatorConfiguration();
            opConfig.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = opConfig;
            IntegerInversionOperator op = new IntegerInversionOperator(algorithm);
            TestIntegerListEntity entity = new TestIntegerListEntity(algorithm, 4);
            entity.Age = 10;
            entity.Initialize();
            entity[0] = 3;
            entity[1] = 4;
            entity[2] = 1;
            entity[3] = 6;

            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomHelper.Instance = randomUtil;
            randomUtil.RandomValue = 1;

            GeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("3, 1, 4, 6", mutant.Representation, "Mutation not called correctly.");
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

        private class TestIntegerListEntity : IntegerListEntity
        {
            public TestIntegerListEntity(GeneticAlgorithm algorithm, int initialLength)
                : base(algorithm, initialLength)
            {

            }

            public override GeneticEntity Clone()
            {
                TestIntegerListEntity entity = new TestIntegerListEntity(this.Algorithm, this.Length);
                this.CopyTo(entity);
                return entity;
            }
        }

        [Component(typeof(TestIntegerListEntity))]
        private class TestIntegerListEntityConfiguration : IntegerListEntityConfiguration
        {
        }
    }
}
