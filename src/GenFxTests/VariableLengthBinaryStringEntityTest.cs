using GenFx;
using GenFx.ComponentLibrary.BinaryStrings;
using GenFx.ComponentModel;
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
            RandomHelper.Instance = new RandomHelper();
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
            RandomHelper.Instance = randomUtil;
            randomUtil.MinValue = minSize;
            randomUtil.MaxValue = maxSize;

            GeneticAlgorithm algorithm = GetAlgorithm(minSize, maxSize);
            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
            Assert.AreEqual(maxSize, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(maxSize, ((BitArray)accessor.GetField("genes")).Length, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config class is missing.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void VariableLengthBinaryStringEntity_Ctor_MissingConfig()
        {
            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(new MockGeneticAlgorithm());
        }

        /// <summary>
        /// Tests that an exception is thrown when min and max are not valid.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void VariableLengthBinaryStringEntity_Ctor_MismatchedMinMax()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(10, 5);
            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid binary length is used.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void VariableLengthBinaryStringEntity_Ctor_InvalidLength1()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(0, 10);
            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid binary length is used.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void VariableLengthBinaryStringEntity_Ctor_InvalidLength2()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(10, 0);
            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
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
            RandomHelper.Instance = randomUtil;
            randomUtil.MinValue = minLength;
            randomUtil.MaxValue = maxLength;

            GeneticAlgorithm algorithm = GetAlgorithm(minLength, maxLength);

            BinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            Assert.AreEqual(maxLength, entity.Length, "Length not set correctly.");

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
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
            int minLength = 10;
            int maxLength = 20;
            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomHelper.Instance = randomUtil;
            randomUtil.MinValue = minLength;
            randomUtil.MaxValue = maxLength;
            GeneticAlgorithm algorithm = GetAlgorithm(minLength, maxLength);

            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            entity.Initialize();

            int currentLength = entity.Length;
            string currentBinaryString = entity.Representation;

            int newBitValue = 1;
            entity.Insert(0, newBitValue);
            Assert.AreEqual(newBitValue.ToString() + currentBinaryString, entity.Representation, "Bit not inserted correctly.");

            currentBinaryString = entity.Representation;
            entity.Insert(entity.Length, newBitValue);
            Assert.AreEqual(currentBinaryString + newBitValue.ToString(), entity.Representation, "Bit not inserted correctly.");

            currentBinaryString = entity.Representation;
            entity.Insert(1, newBitValue);
            Assert.AreEqual(currentBinaryString.Substring(0, 1) + newBitValue.ToString() + currentBinaryString.Substring(1), entity.Representation, "Bit not inserted correctly.");
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
            RandomHelper.Instance = randomUtil;
            randomUtil.MinValue = minLength;
            randomUtil.MaxValue = maxLength;
            GeneticAlgorithm algorithm = GetAlgorithm(minLength, maxLength);

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
            Assert.AreSame(expectedEntity.Algorithm, actualEntity.Algorithm, "Algorithms should be the same instance.");
            Assert.AreEqual(expectedEntity.Age, actualEntity.Age, "Age not set correctly.");
            Assert.AreEqual(expectedEntity.RawFitnessValue, actualEntity.RawFitnessValue, "Raw fitness not set correctly.");
            Assert.AreEqual(expectedEntity.ScaledFitnessValue, actualEntity.ScaledFitnessValue, "Scaled fitness not set correctly.");
        }

        private static VariableLengthBinaryStringEntity GetEntity()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(4, 5);

            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(algorithm);
            entity[0] = 1;
            entity[1] = 1;
            entity[2] = 0;
            entity[3] = 1;

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("age", 3);
            accessor.SetField("rawFitnessValue", 1);
            entity.ScaledFitnessValue = 2;

            return entity;
        }

        private static GeneticAlgorithm GetAlgorithm(int minLength, int maxLength)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            VariableLengthBinaryStringEntityConfiguration config = new VariableLengthBinaryStringEntityConfiguration();
            config.MinimumStartingLength = minLength;
            config.MaximumStartingLength = maxLength;
            algorithm.ConfigurationSet.Entity = config;
            return algorithm;
        }

        private class TestRandomUtil : IRandomHelper
        {
            private bool switcher;
            internal int MinValue;
            internal int MaxValue;

            public int GetRandomValue(int maxValue)
            {
                switcher = !switcher;
                return (switcher) ? 1 : 0;
            }

            public double GetRandomRatio()
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
