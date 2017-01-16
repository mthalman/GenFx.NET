using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists;
using GenFx.Contracts;
using GenFxTests.Helpers;
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
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the UpdateStringRepresentation method works correctly.
        /// </summary>
        [TestMethod()]
        public void IntegerListEntity_UpdateStringRepresentation()
        {
            IIntegerListEntity entity = GetEntity();
            PrivateObject accessor = new PrivateObject(entity);
            accessor.Invoke("UpdateStringRepresentation");

            Assert.AreEqual("5, 3, 10, 127", entity.Representation, "Representation was not calculated correctly.");
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [TestMethod()]
        public void IntegerListEntity_Clone()
        {
            IIntegerListEntity entity = GetEntity();
            IIntegerListEntity clone = (IIntegerListEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod()]
        public void IntegerListEntity_CopyTo()
        {
            IIntegerListEntity entity = GetEntity();
            IIntegerListEntity entity2 = new TestIntegerListEntity();
            entity2.Initialize(GetAlgorithm(4));

            entity.CopyTo(entity2);

            CompareGeneticEntities(entity, entity2);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod()]
        public void IntegerListEntity_CopyTo_NullEntity()
        {
            IIntegerListEntity entity = GetEntity();

            AssertEx.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void IntegerListEntity_Ctor()
        {
            int size = 3;
            IGeneticAlgorithm algorithm = GetAlgorithm(size);
            IIntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = size, MaximumStartingLength = size };
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<int>)));
            Assert.AreEqual(size, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(size, ((List<int>)accessor.GetField("genes")).Count, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod()]
        public void IntegerListEntity_Initialize()
        {
            RandomNumberService.Instance = new TestRandomUtil();

            IGeneticAlgorithm algorithm = GetAlgorithm(4);
            IIntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = 4, MaximumStartingLength = 4 };
            entity.Initialize(algorithm);
            
            Assert.AreEqual("1, 2, 3, 4", entity.Representation, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [TestMethod()]
        public void IntegerListEntity_Indexer()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(3);
            IIntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = 3, MaximumStartingLength = 3 };
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<int>)));
            List<int> genes = (List<int>)accessor.GetField("genes");
            for (int i = 0; i < genes.Count; i++)
            {
                genes[i] = 0;
            }

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
        public void IntegerListEntity_Length()
        {
            int length = 50;
            IGeneticAlgorithm algorithm = GetAlgorithm(length);

            IIntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = length, MaximumStartingLength = length };
            entity.Initialize(algorithm);
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");

            entity.Length = length;
            Assert.AreEqual(length, entity.Length, "Length not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when the Length is set to a different value.
        ///</summary>
        [TestMethod()]
        public void IntegerListEntity_Length_SetToDifferentValue()
        {
            int length = 50;
            IGeneticAlgorithm algorithm = GetAlgorithm(length);

            TestIntegerListEntity entity = new TestIntegerListEntity
            {
                MinimumStartingLength = 5,
                MaximumStartingLength = 5
            };
            entity.Initialize(algorithm);
            AssertEx.Throws<ArgumentException>(() => entity.Length = 51);
        }

        private static void CompareGeneticEntities(IIntegerListEntity expectedEntity, IIntegerListEntity actualEntity)
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

        private static IIntegerListEntity GetEntity()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(4);

            TestIntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = 4, MaximumStartingLength = 4 };
            entity.Initialize(algorithm);
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

        private static IGeneticAlgorithm GetAlgorithm(int entityLength)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new TestIntegerListEntity
                {
                    MinimumStartingLength = entityLength,
                    MaximumStartingLength = entityLength
                }
            };
            return algorithm;
        }

        private class TestIntegerListEntity : IntegerListEntity
        {
        }

        private class TestRandomUtil : IRandomNumberService
        {
            private int increment;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetDouble()
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
