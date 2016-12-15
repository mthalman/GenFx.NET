using GenFx;
using GenFx.ComponentLibrary.BinaryStrings;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.BinaryStrings.BinaryStringEntity and is intended
    ///to contain all GenFx.ComponentLibrary.BinaryStrings.BinaryStringEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class BinaryStringEntityTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            RandomHelper.Instance = new RandomHelper();
        }

        /// <summary>
        /// Tests that the UpdateStringRepresentation method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_UpdateStringRepresentation()
        {
            BinaryStringEntity entity = GetEntity();
            PrivateObject accessor = new PrivateObject(entity);
            accessor.Invoke("UpdateStringRepresentation");

            Assert.AreEqual("1101", entity.Representation, "Representation was not calculated correctly.");
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Clone()
        {
            BinaryStringEntity entity = GetEntity();
            BinaryStringEntity clone = (BinaryStringEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_CopyTo()
        {
            BinaryStringEntity entity = GetEntity();
            BinaryStringEntity entity2 = new TestBinaryStringEntity(GetAlgorithm(), 4);

            entity.CopyTo(entity2);

            CompareGeneticEntities(entity, entity2);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_CopyTo_NullEntity()
        {
            BinaryStringEntity entity = GetEntity();

            AssertEx.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Ctor()
        {
            int size = 3;
            GeneticAlgorithm algorithm = GetAlgorithm();
            BinaryStringEntity entity = new TestBinaryStringEntity(algorithm, size);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
            Assert.AreEqual(size, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(size, ((BitArray)accessor.GetField("genes")).Length, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Initialize()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            BinaryStringEntity entity = new TestBinaryStringEntity(algorithm, 4);
            RandomHelper.Instance = new TestRandomUtil();
            entity.Initialize();
            Assert.AreEqual("1010", entity.Representation, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Indexer()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            BinaryStringEntity entity = new TestBinaryStringEntity(algorithm, 3);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
            entity[0] = 1;

            BitArray genes = (BitArray)accessor.GetField("genes");
            Assert.IsTrue(genes[0], "Genes not set correctly.");
            Assert.IsFalse(genes[1], "Genes not set correctly.");
            Assert.IsFalse(genes[2], "Genes not set correctly.");
            Assert.AreEqual(1, entity[0], "Indexer returned incorrect value.");
            entity[1] = 1;
            Assert.IsTrue(genes[0], "Genes not set correctly.");
            Assert.IsTrue(genes[1], "Genes not set correctly.");
            Assert.IsFalse(genes[2], "Genes not set correctly.");
            Assert.AreEqual(1, entity[1], "Indexer returned incorrect value.");
            entity[2] = 1;
            Assert.IsTrue(genes[0], "Genes not set correctly.");
            Assert.IsTrue(genes[1], "Genes not set correctly.");
            Assert.IsTrue(genes[2], "Genes not set correctly.");
            Assert.AreEqual(1, entity[2], "Indexer returned incorrect value.");
        }

        /// <summary>
        /// Tests that an exception is throw when an invalid binary value is used on the indexer.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Indexer_InvalidValue()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            BinaryStringEntity entity = new TestBinaryStringEntity(algorithm, 3);
            AssertEx.Throws<ArgumentOutOfRangeException>(() => entity[0] = 2);
        }

        /// <summary>
        /// Tests that the Length property works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Length()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            int length = 50;
            BinaryStringEntity entity = new TestBinaryStringEntity(algorithm, length);
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");

            entity.Length = length;
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when the Length is set to a different value.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Length_SetToDifferentValue()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            int length = 50;
            BinaryStringEntity entity = new TestBinaryStringEntity(algorithm, length);
            AssertEx.Throws<ArgumentException>(() => entity.Length = 51);
        }

        private static void CompareGeneticEntities(BinaryStringEntity expectedEntity, BinaryStringEntity actualEntity)
        {
            PrivateObject accessor = new PrivateObject(expectedEntity, new PrivateType(typeof(GeneticEntity)));
            PrivateObject actualEntityAccessor = new PrivateObject(actualEntity, new PrivateType(typeof(GeneticEntity)));
            Assert.AreNotSame(expectedEntity, actualEntity, "Objects should not be the same instance.");
            Assert.AreEqual(expectedEntity.Representation, actualEntity.Representation, "Representation not cloned correctly.");
            Assert.AreEqual(expectedEntity[0], actualEntity[0], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[1], actualEntity[1], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[2], actualEntity[2], "Binary value not set correctly.");
            Assert.AreEqual(expectedEntity[3], actualEntity[3], "Binary value not set correctly.");
            Assert.AreSame(accessor.GetProperty("Algorithm"), actualEntityAccessor.GetProperty("Algorithm"), "Algorithms should be the same instance.");
            Assert.AreEqual(expectedEntity.Age, actualEntity.Age, "Age not set correctly.");
            Assert.AreEqual(accessor.GetField("rawFitnessValue"), actualEntityAccessor.GetField("rawFitnessValue"), "Raw fitness not set correctly.");
            Assert.AreEqual(expectedEntity.ScaledFitnessValue, actualEntity.ScaledFitnessValue, "Scaled fitness not set correctly.");
        }

        private static BinaryStringEntity GetEntity()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

            algorithm.ConfigurationSet.Entity = new TestBinaryStringEntityConfiguration();
            BinaryStringEntity entity = new TestBinaryStringEntity(algorithm, 4);
            entity[0] = 1;
            entity[1] = 1;
            entity[2] = 0;
            entity[3] = 1;

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetProperty("Age", 3);
            accessor.SetField("rawFitnessValue", 1);
            entity.ScaledFitnessValue = 2;

            return entity;
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestBinaryStringEntityConfiguration();
            return algorithm;
        }

        private class TestBinaryStringEntity : BinaryStringEntity
        {
            public TestBinaryStringEntity(GeneticAlgorithm algorithm, int initialStringLength)
                : base(algorithm, initialStringLength)
            {

            }

            public override GeneticEntity Clone()
            {
                TestBinaryStringEntity entity = new TestBinaryStringEntity(this.Algorithm, this.Length);
                this.CopyTo(entity);
                return entity;
            }
        }

        [Component(typeof(TestBinaryStringEntity))]
        private class TestBinaryStringEntityConfiguration : BinaryStringEntityConfiguration
        {
        }

        private class TestRandomUtil : IRandomHelper
        {
            private bool switcher;

            public int GetRandomValue(int maxValue)
            {
                this.switcher = !this.switcher;
                return (this.switcher) ? 1 : 0;
            }

            public double GetRandomRatio()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

    }


}
