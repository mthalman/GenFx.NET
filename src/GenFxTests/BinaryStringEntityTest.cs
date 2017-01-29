using GenFx;
using GenFx.ComponentLibrary.Lists;
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
            TestBinaryStringEntity entity2 = new TestBinaryStringEntity();
            entity2.Initialize(entity.Algorithm);

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
            GeneticAlgorithm algorithm = GetAlgorithm(size);
            TestBinaryStringEntity entity = new TestBinaryStringEntity { MinimumStartingLength = size, MaximumStartingLength = size };
            entity.Initialize(algorithm);
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
            GeneticAlgorithm algorithm = GetAlgorithm(4);
            TestBinaryStringEntity entity = new TestBinaryStringEntity { MinimumStartingLength = 4, MaximumStartingLength = 4 };
            RandomNumberService.Instance = new TestRandomUtil();
            entity.Initialize(algorithm);
            Assert.AreEqual("1010", entity.Representation, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Indexer()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(3);
            TestBinaryStringEntity entity = new TestBinaryStringEntity { MinimumStartingLength = 3, MaximumStartingLength = 3 };
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
            
            BitArray genes = (BitArray)accessor.GetField("genes");
            for (int i = 0; i < entity.Length; i++)
            {
                genes[i] = false;
            }

            entity[0] = true;
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
            GeneticAlgorithm algorithm = GetAlgorithm(length);

            TestBinaryStringEntity entity = new TestBinaryStringEntity { MinimumStartingLength = length, MaximumStartingLength = length };
            entity.Initialize(algorithm);
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");

            entity.Length = length;
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when the Length is set to a different value when the string is fixed size.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Length_SetToDifferentValue()
        {
            int length = 50;
            GeneticAlgorithm algorithm = GetAlgorithm(length);

            TestBinaryStringEntity entity = new TestBinaryStringEntity
            {
                MinimumStartingLength = length,
                MaximumStartingLength = length,
                IsFixedSize = true
            };
            entity.Initialize(algorithm);
            AssertEx.Throws<ArgumentException>(() => entity.Length = 51);
        }

        /// <summary>
        /// Tests that the length can be expanded to contain more items.
        /// </summary>
        [TestMethod]
        public void BinaryStringEntity_SetLengthToExpand()
        {
            BinaryStringEntity entity = new BinaryStringEntity
            {
                MinimumStartingLength = 2,
                MaximumStartingLength = 2,
            };

            entity.Initialize(new MockGeneticAlgorithm());

            Assert.AreEqual(2, entity.Length);

            entity.Length = 4;
            Assert.AreEqual(4, entity.Length);

            Assert.AreEqual(false, entity[2]);
            Assert.AreEqual(false, entity[3]);
        }

        /// <summary>
        /// Tests that the length can be contracted to decrease the number of items.
        /// </summary>
        [TestMethod]
        public void BinaryStringEntity_SetLengthToContract()
        {
            BinaryStringEntity entity = new BinaryStringEntity
            {
                MinimumStartingLength = 4,
                MaximumStartingLength = 4,
            };

            entity.Initialize(new MockGeneticAlgorithm());
            Assert.AreEqual(4, entity.Length);

            entity[0] = true;
            Assert.AreEqual(true, entity[0]);

            entity.Length = 1;
            Assert.AreEqual(1, entity.Length);

            Assert.AreEqual(true, entity[0]);
        }

        /// <summary>
        /// Tests that the component can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void Serialization()
        {
            BinaryStringEntity entity = new BinaryStringEntity();
            entity.MinimumStartingLength = entity.MaximumStartingLength = 3;
            entity.IsFixedSize = true;
            entity.Initialize(new MockGeneticAlgorithm());

            BinaryStringEntity result = (BinaryStringEntity)SerializationHelper.TestSerialization(entity, new Type[] { typeof(MockGeneticAlgorithm) });

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(entity[i], result[i]);
            }

            Assert.IsTrue(result.IsFixedSize);
        }

        private static void CompareGeneticEntities(TestBinaryStringEntity expectedEntity, TestBinaryStringEntity actualEntity)
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

        private static TestBinaryStringEntity GetEntity()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(4);
            
            TestBinaryStringEntity entity = (TestBinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();

            entity[0] = true;
            entity[1] = false;
            entity[2] = true;
            entity[3] = false;

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetProperty("Age", 3);
            accessor.SetField("rawFitnessValue", 1);
            entity.ScaledFitnessValue = 2;

            return entity;
        }

        private static GeneticAlgorithm GetAlgorithm(int entityLength)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new TestBinaryStringEntity
                {
                    MinimumStartingLength = entityLength,
                    MaximumStartingLength = entityLength
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);
            return algorithm;
        }

        private class TestBinaryStringEntity : BinaryStringEntity
        {
        }
        
        private class TestRandomUtil : IRandomNumberService
        {
            private bool switcher;

            public int GetRandomValue(int maxValue)
            {
                this.switcher = !this.switcher;
                return (this.switcher) ? 1 : 0;
            }

            public double GetDouble()
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
