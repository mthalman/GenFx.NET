using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
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
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the UpdateStringRepresentation method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_UpdateStringRepresentation()
        {
            TestBinaryStringEntity entity = GetEntity();
            PrivateObject accessor = new PrivateObject(entity);
            accessor.Invoke("UpdateStringRepresentation");

            Assert.AreEqual("1010", entity.Representation, "Representation was not calculated correctly.");
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Clone()
        {
            TestBinaryStringEntity entity = GetEntity();
            TestBinaryStringEntity clone = (TestBinaryStringEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_CopyTo()
        {
            TestBinaryStringEntity entity = GetEntity();
            TestBinaryStringEntity entity2 = new TestBinaryStringEntity(GetAlgorithm(4));

            entity.CopyTo(entity2);

            CompareGeneticEntities(entity, entity2);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_CopyTo_NullEntity()
        {
            TestBinaryStringEntity entity = GetEntity();

            AssertEx.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Ctor()
        {
            int size = 3;
            IGeneticAlgorithm algorithm = GetAlgorithm(size);
            TestBinaryStringEntity entity = new TestBinaryStringEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity<TestBinaryStringEntity, TestBinaryStringEntityConfiguration>)));
            Assert.AreEqual(size, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(size, ((BitArray)accessor.GetField("genes")).Length, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Initialize()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(4);
            TestBinaryStringEntity entity = new TestBinaryStringEntity(algorithm);
            RandomNumberService.Instance = new TestRandomUtil();
            entity.Initialize();
            Assert.AreEqual("1010", entity.Representation, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Indexer()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(3);
            TestBinaryStringEntity entity = new TestBinaryStringEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity<TestBinaryStringEntity, TestBinaryStringEntityConfiguration>)));
            entity[0] = true;

            BitArray genes = (BitArray)accessor.GetField("genes");
            Assert.IsTrue(genes[0], "Genes not set correctly.");
            Assert.IsFalse(genes[1], "Genes not set correctly.");
            Assert.IsFalse(genes[2], "Genes not set correctly.");
            Assert.IsTrue(entity[0], "Indexer returned incorrect value.");
            entity[1] = true;
            Assert.IsTrue(genes[0], "Genes not set correctly.");
            Assert.IsTrue(genes[1], "Genes not set correctly.");
            Assert.IsFalse(genes[2], "Genes not set correctly.");
            Assert.IsTrue(entity[1], "Indexer returned incorrect value.");
            entity[2] = true;
            Assert.IsTrue(genes[0], "Genes not set correctly.");
            Assert.IsTrue(genes[1], "Genes not set correctly.");
            Assert.IsTrue(genes[2], "Genes not set correctly.");
            Assert.IsTrue(entity[2], "Indexer returned incorrect value.");
        }

        /// <summary>
        /// Tests that the Length property works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Length()
        {
            int length = 50;
            IGeneticAlgorithm algorithm = GetAlgorithm(length);
            
            TestBinaryStringEntity entity = new TestBinaryStringEntity(algorithm);
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
            int length = 50;
            IGeneticAlgorithm algorithm = GetAlgorithm(length);
            
            TestBinaryStringEntity entity = new TestBinaryStringEntity(algorithm);
            AssertEx.Throws<ArgumentException>(() => entity.Length = 51);
        }

        private static void CompareGeneticEntities(TestBinaryStringEntity expectedEntity, TestBinaryStringEntity actualEntity)
        {
            PrivateObject accessor = new PrivateObject(expectedEntity, new PrivateType(typeof(GeneticEntity<TestBinaryStringEntity, TestBinaryStringEntityConfiguration>)));
            PrivateObject actualEntityAccessor = new PrivateObject(actualEntity, new PrivateType(typeof(GeneticEntity<TestBinaryStringEntity, TestBinaryStringEntityConfiguration>)));
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

        private static TestBinaryStringEntity GetEntity()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(4);

            TestBinaryStringEntity entity = new TestBinaryStringEntity(algorithm);
            entity[0] = true;
            entity[1] = false;
            entity[2] = true;
            entity[3] = false;

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity<TestBinaryStringEntity, TestBinaryStringEntityConfiguration>)));
            accessor.SetProperty("Age", 3);
            accessor.SetField("rawFitnessValue", 1);
            entity.ScaledFitnessValue = 2;

            return entity;
        }

        private static IGeneticAlgorithm GetAlgorithm(int entityLength)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Population = new MockPopulationConfiguration(),
                Entity = new TestBinaryStringEntityConfiguration
                {
                    Length = entityLength
                }
            });
            return algorithm;
        }

        private class TestBinaryStringEntity : BinaryStringEntity<TestBinaryStringEntity, TestBinaryStringEntityConfiguration>
        {
            public TestBinaryStringEntity(IGeneticAlgorithm algorithm)
                : base(algorithm, ((TestBinaryStringEntityConfiguration)algorithm.ConfigurationSet.Entity).Length)
            {

            }

            public override bool IsFixedSize
            {
                get { return true; }
            }
        }

        private class TestBinaryStringEntityConfiguration : BinaryStringEntityConfiguration<TestBinaryStringEntityConfiguration, TestBinaryStringEntity>
        {

            public int Length { get; set; }
        }

        private class TestRandomUtil : IRandomNumberService
        {
            private bool switcher;

            public int GetRandomValue(int maxValue)
            {
                this.switcher = !this.switcher;
                return (this.switcher) ? 1 : 0;
            }

            public double GetRandomPercentRatio()
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
