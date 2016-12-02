using GenFx;
using GenFx.ComponentLibrary.BinaryStrings;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.BinaryStrings.FixedLengthBinaryStringEntity and is intended
    ///to contain all GenFx.ComponentLibrary.BinaryStrings.FixedLengthBinaryStringEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class FixedLengthBinaryStringEntityTest
    {
        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [TestMethod()]
        public void FixedLengthBinaryStringEntity_Clone()
        {
            FixedLengthBinaryStringEntity entity = GetEntity();
            FixedLengthBinaryStringEntity clone = (FixedLengthBinaryStringEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void FixedLengthBinaryStringEntity_Ctor()
        {
            int size = 3;
            GeneticAlgorithm algorithm = GetAlgorithm(size);
            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
            Assert.AreEqual(size, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(size, ((BitArray)accessor.GetField("genes")).Length, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when required settings are missing.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FixedLengthBinaryStringEntity_Ctor_MissingSetting()
        {
            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity(new MockGeneticAlgorithm());
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid binary length is used.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void FixedLengthBinaryStringEntity_Ctor_InvalidLength()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(0);
            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity(algorithm);
        }

        private static void CompareGeneticEntities(FixedLengthBinaryStringEntity expectedEntity, FixedLengthBinaryStringEntity actualEntity)
        {
            PrivateObject accessor = new PrivateObject(expectedEntity, new PrivateType(typeof(GeneticEntity)));
            PrivateObject actualEntityAccessor = new PrivateObject(actualEntity, new PrivateType(typeof(GeneticEntity)));
            Assert.AreNotSame(expectedEntity, actualEntity, "Objects should not be the same instance.");
            Assert.AreEqual(expectedEntity.Representation, actualEntity.Representation, "Representation not cloned correctly.");
            Assert.AreEqual(expectedEntity[0], actualEntity[0], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[1], actualEntity[1], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[2], actualEntity[2], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[3], actualEntity[3], "Binary value not set correctly.");
            Assert.AreSame(expectedEntity.Algorithm, actualEntity.Algorithm, "Algorithms should be the same instance.");
            Assert.AreEqual(expectedEntity.Age, actualEntity.Age, "Age not set correctly.");
            Assert.AreEqual(accessor.GetField("rawFitnessValue"), actualEntityAccessor.GetField("rawFitnessValue"), "Raw fitness not set correctly.");
            Assert.AreEqual(expectedEntity.ScaledFitnessValue, actualEntity.ScaledFitnessValue, "Scaled fitness not set correctly.");
        }

        private static FixedLengthBinaryStringEntity GetEntity()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(4);

            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity(algorithm);
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

        private static GeneticAlgorithm GetAlgorithm(int entityLength)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            FixedLengthBinaryStringEntityConfiguration config = new FixedLengthBinaryStringEntityConfiguration();
            config.Length = entityLength;
            algorithm.ConfigurationSet.Entity = config;
            return algorithm;
        }

    }


}
