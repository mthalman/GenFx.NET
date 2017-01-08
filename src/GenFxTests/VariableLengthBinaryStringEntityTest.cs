using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.BinaryStrings.VariableLengthBinaryStringEntity and is intended
    ///to contain all GenFx.ComponentLibrary.BinaryStrings.VariableLengthBinaryStringEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class VariableLengthBinaryStringEntityTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Clone()
        {
            VariableLengthBinaryStringEntity entity = GetEntity();
            VariableLengthBinaryStringEntity clone = (VariableLengthBinaryStringEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Ctor()
        {
            int minSize = 3;
            int maxSize = 5;
            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.MinValue = minSize;
            randomUtil.MaxValue = maxSize;

            IGeneticAlgorithm algorithm = GetAlgorithm(minSize, maxSize);
            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity<VariableLengthBinaryStringEntity, VariableLengthBinaryStringEntityFactoryConfig>)));
            Assert.AreEqual(maxSize, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(maxSize, ((BitArray)accessor.GetField("genes")).Length, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config class is missing.
        /// </summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Ctor_MissingConfig()
        {
            AssertEx.Throws<ArgumentException>(() => new VariableLengthBinaryStringEntity(new MockGeneticAlgorithm(new ComponentFactoryConfigSet())));
        }

        /// <summary>
        /// Tests that an exception is thrown when min and max are not valid.
        /// </summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Ctor_MismatchedMinMax()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(10, 5);
            AssertEx.Throws<ValidationException>(() => new VariableLengthBinaryStringEntity(algorithm));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid binary length is used.
        /// </summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Ctor_InvalidLength1()
        {
            VariableLengthBinaryStringEntityFactoryConfig config = new VariableLengthBinaryStringEntityFactoryConfig();
            AssertEx.Throws<ValidationException>(() => config.MinimumStartingLength = 0);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid binary length is used.
        /// </summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Ctor_InvalidLength2()
        {
            VariableLengthBinaryStringEntityFactoryConfig config = new VariableLengthBinaryStringEntityFactoryConfig();
            config.MinimumStartingLength = 10;
            AssertEx.Throws<ValidationException>(() => config.MaximumStartingLength = 0);
        }

        /// <summary>
        /// Tests that the Length property works correctly.
        ///</summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Length()
        {
            int minLength = 10;
            int maxLength = 20;
            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.MinValue = minLength;
            randomUtil.MaxValue = maxLength;

            IGeneticAlgorithm algorithm = GetAlgorithm(minLength, maxLength);

            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            Assert.AreEqual(maxLength, entity.Length, "Length not set correctly.");

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity<VariableLengthBinaryStringEntity, VariableLengthBinaryStringEntityFactoryConfig>)));
            BitArray genes = (BitArray)accessor.GetField("genes");
            entity.Length = maxLength + 10;
            Assert.AreEqual(maxLength + 10, genes.Count, "Genes not expanded correctly.");
            Assert.AreEqual(genes.Count, entity.Representation.Length, "Representation not updated correctly.");

            entity.Length = maxLength - 5;
            Assert.AreEqual(maxLength - 5, genes.Count, "Genes not expanded correctly.");
            Assert.AreEqual(genes.Count, entity.Representation.Length, "Representation not updated correctly.");
        }

        /// <summary>
        /// Tests that the Insert property works correctly.
        ///</summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_Insert()
        {
            int minLength = 2;
            int maxLength = 4;
            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.MinValue = minLength;
            randomUtil.MaxValue = maxLength;
            IGeneticAlgorithm algorithm = GetAlgorithm(minLength, maxLength);

            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            entity.Initialize();

            int currentLength = entity.Length;
            string currentBinaryString = entity.Representation;

            bool newBitValue = true;
            entity.Insert(0, newBitValue);
            string newBitValueStr = newBitValue ? "1" : "0";
            Assert.AreEqual(newBitValueStr + currentBinaryString, entity.Representation, "Bit not inserted correctly.");

            currentBinaryString = entity.Representation;
            entity.Insert(entity.Length, newBitValue);
            Assert.AreEqual(currentBinaryString + newBitValueStr, entity.Representation, "Bit not inserted correctly.");

            currentBinaryString = entity.Representation;
            entity.Insert(1, newBitValue);
            Assert.AreEqual(currentBinaryString.Substring(0, 1) + newBitValueStr + currentBinaryString.Substring(1), entity.Representation, "Bit not inserted correctly.");
        }

        /// <summary>
        /// Tests that the RemoveAt property works correctly.
        ///</summary>
        [TestMethod()]
        public void VariableLengthBinaryStringEntity_RemoveAt()
        {
            int minLength = 10;
            int maxLength = 20;
            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;
            randomUtil.MinValue = minLength;
            randomUtil.MaxValue = maxLength;
            IGeneticAlgorithm algorithm = GetAlgorithm(minLength, maxLength);

            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            entity.Initialize();

            int currentLength = entity.Length;
            string currentBinaryString = entity.Representation;

            entity.RemoveAt(0);
            Assert.AreEqual(currentBinaryString.Substring(1), entity.Representation, "Bit not inserted correctly.");

            currentBinaryString = entity.Representation;
            entity.RemoveAt(entity.Length - 1);
            Assert.AreEqual(currentBinaryString.Substring(0, currentBinaryString.Length - 1), entity.Representation, "Bit not inserted correctly.");

            currentBinaryString = entity.Representation;
            entity.RemoveAt(1);
            Assert.AreEqual(currentBinaryString.Substring(0, 1) + currentBinaryString.Substring(2), entity.Representation, "Bit not inserted correctly.");
        }

        private static void CompareGeneticEntities(VariableLengthBinaryStringEntity expectedEntity, VariableLengthBinaryStringEntity actualEntity)
        {
            Assert.AreNotSame(expectedEntity, actualEntity, "Objects should not be the same instance.");
            Assert.AreEqual(expectedEntity.Representation, actualEntity.Representation, "Representation not cloned correctly.");
            Assert.AreEqual(expectedEntity[0], actualEntity[0], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[1], actualEntity[1], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[2], actualEntity[2], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[3], actualEntity[3], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity.Age, actualEntity.Age, "Age not set correctly.");
            Assert.AreEqual(expectedEntity.RawFitnessValue, actualEntity.RawFitnessValue, "Raw fitness not set correctly.");
            Assert.AreEqual(expectedEntity.ScaledFitnessValue, actualEntity.ScaledFitnessValue, "Scaled fitness not set correctly.");
        }

        private static VariableLengthBinaryStringEntity GetEntity()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(4, 5);

            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            entity[0] = true;
            entity[1] = true;
            entity[2] = false;
            entity[3] = true;

            entity.Age = 3;
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity<VariableLengthBinaryStringEntity, VariableLengthBinaryStringEntityFactoryConfig>)));
            accessor.SetField("rawFitnessValue", 1);
            entity.ScaledFitnessValue = 2;

            return entity;
        }

        private static IGeneticAlgorithm GetAlgorithm(int minLength, int maxLength)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new VariableLengthBinaryStringEntityFactoryConfig
                {
                    MinimumStartingLength = minLength,
                    MaximumStartingLength = maxLength
                }
            });
            return algorithm;
        }

        private class TestRandomUtil : IRandomNumberService
        {
            private bool switcher;
            internal int MinValue;
            internal int MaxValue;

            public int GetRandomValue(int maxValue)
            {
                switcher = !switcher;
                return (switcher) ? 1 : 0;
            }

            public double GetRandomPercentRatio()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                Assert.AreEqual(MinValue, minValue, "Minimum value passed in is not correct.");
                Assert.AreEqual(MaxValue, maxValue - 1, "Maximum value passed in is not correct.");

                Assert.IsTrue(MinValue < MaxValue, "Test expects min to be less than max.");
                return maxValue - 1;
            }
        }

    }


}
