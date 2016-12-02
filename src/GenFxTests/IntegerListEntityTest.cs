using GenFx;
using GenFx.ComponentLibrary.Lists;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Lists.IntegerListEntity and is intended
    ///to contain all GenFx.ComponentLibrary.Lists.IntegerListEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class IntegerListEntityTest
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
        public void IntegerListEntity_UpdateStringRepresentation()
        {
            IntegerListEntity entity = GetEntity();
            PrivateObject accessor = new PrivateObject(entity);
            accessor.Invoke("UpdateStringRepresentation");

            Assert.AreEqual("5, 3, 10, 127", entity.Representation, "Representation was not calculated correctly.");
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Clone()
        {
            IntegerListEntity entity = GetEntity();
            IntegerListEntity clone = (IntegerListEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_CopyTo()
        {
            IntegerListEntity entity = GetEntity();
            IntegerListEntity entity2 = new TestIntegerListEntity(GetAlgorithm(), 4);

            entity.CopyTo(entity2);

            CompareGeneticEntities(entity, entity2);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BinaryStringEntity_CopyTo_NullEntity()
        {
            IntegerListEntity entity = GetEntity();

            entity.CopyTo(null);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Ctor()
        {
            int size = 3;
            GeneticAlgorithm algorithm = GetAlgorithm();
            IntegerListEntity entity = new TestIntegerListEntity(algorithm, size);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<int>)));
            Assert.AreEqual(size, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(size, ((List<int>)accessor.GetField("genes")).Count, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Initialize()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            IntegerListEntity entity = new TestIntegerListEntity(algorithm, 4);
            RandomHelper.Instance = new TestRandomUtil();
            entity.Initialize();
            Assert.AreEqual("0, 1, 2, 3", entity.Representation, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Indexer()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            IntegerListEntity entity = new TestIntegerListEntity(algorithm, 3);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<int>)));
            List<int> genes = (List<int>)accessor.GetField("genes");

            entity[0] = 1;
            Assert.AreEqual(1, genes[0], "Genes not set correctly.");
            Assert.AreEqual(0, genes[1], "Genes not set correctly.");
            Assert.AreEqual(0, genes[2], "Genes not set correctly.");
            Assert.AreEqual(1, entity[0], "Indexer returned incorrect value.");
            entity[1] = 2;
            Assert.AreEqual(1, genes[0], "Genes not set correctly.");
            Assert.AreEqual(2, genes[1], "Genes not set correctly.");
            Assert.AreEqual(0, genes[2], "Genes not set correctly.");
            Assert.AreEqual(2, entity[1], "Indexer returned incorrect value.");
            entity[2] = 3;
            Assert.AreEqual(1, genes[0], "Genes not set correctly.");
            Assert.AreEqual(2, genes[1], "Genes not set correctly.");
            Assert.AreEqual(3, genes[2], "Genes not set correctly.");
            Assert.AreEqual(3, entity[2], "Indexer returned incorrect value.");
        }

        /// <summary>
        /// Tests that the Length property works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Length()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            int length = 50;
            IntegerListEntity entity = new TestIntegerListEntity(algorithm, length);
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");

            entity.Length = length;
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when the Length is set to a different value.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BinaryStringEntity_Length_SetToDifferentValue()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            int length = 50;
            IntegerListEntity entity = new TestIntegerListEntity(algorithm, length);
            entity.Length = 51;
        }

        private static void CompareGeneticEntities(IntegerListEntity expectedEntity, IntegerListEntity actualEntity)
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

        private static IntegerListEntity GetEntity()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

            algorithm.ConfigurationSet.Entity = new TestIntegerListEntityConfiguration();
            IntegerListEntity entity = new TestIntegerListEntity(algorithm, 4);
            entity[0] = 5;
            entity[1] = 3;
            entity[2] = 10;
            entity[3] = 127;

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            entity.Age = 3;
            accessor.SetField("rawFitnessValue", 1);
            entity.ScaledFitnessValue = 2;

            return entity;
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestIntegerListEntityConfiguration();
            return algorithm;
        }

        private class TestIntegerListEntity : IntegerListEntity
        {
            public TestIntegerListEntity(GeneticAlgorithm algorithm, int initialStringLength)
                : base(algorithm, initialStringLength)
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

        private class TestRandomUtil : IRandomHelper
        {
            private int increment;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetRandomRatio()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                return this.increment++;
            }
        }
    }
}
