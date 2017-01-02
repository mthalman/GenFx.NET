using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists;
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
        public void BinaryStringEntity_Clone()
        {
            IIntegerListEntity entity = GetEntity();
            IIntegerListEntity clone = (IIntegerListEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_CopyTo()
        {
            IIntegerListEntity entity = GetEntity();
            IIntegerListEntity entity2 = new TestIntegerListEntity(GetAlgorithm(4));

            entity.CopyTo(entity2);

            CompareGeneticEntities(entity, entity2);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_CopyTo_NullEntity()
        {
            IIntegerListEntity entity = GetEntity();

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
            IIntegerListEntity entity = new TestIntegerListEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<TestIntegerListEntity, TestIntegerListEntityConfiguration, int>)));
            Assert.AreEqual(size, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(size, ((List<int>)accessor.GetField("genes")).Count, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod()]
        public void BinaryStringEntity_Initialize()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(4);
            IIntegerListEntity entity = new TestIntegerListEntity(algorithm);
            RandomNumberService.Instance = new TestRandomUtil();
            entity.Initialize();
            Assert.AreEqual("0, 1, 2, 3", entity.Representation, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [TestMethod()]
        public void BinaryStringEntity_Indexer()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(3);
            IIntegerListEntity entity = new TestIntegerListEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<TestIntegerListEntity, TestIntegerListEntityConfiguration, int>)));
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
            int length = 50;
            IGeneticAlgorithm algorithm = GetAlgorithm(length);
            
            IIntegerListEntity entity = new TestIntegerListEntity(algorithm);
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
            
            TestIntegerListEntity entity = new TestIntegerListEntity(algorithm);
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

            TestIntegerListEntity entity = new TestIntegerListEntity(algorithm);
            entity[0] = 5;
            entity[1] = 3;
            entity[2] = 10;
            entity[3] = 127;

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity<TestIntegerListEntity, TestIntegerListEntityConfiguration>)));
            entity.Age = 3;
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
                Entity = new TestIntegerListEntityConfiguration
                {
                    Length = entityLength
                }
            });
            return algorithm;
        }

        private class TestIntegerListEntity : IntegerListEntity<TestIntegerListEntity, TestIntegerListEntityConfiguration>
        {
            public TestIntegerListEntity(IGeneticAlgorithm algorithm)
                : base(algorithm, ((TestIntegerListEntityConfiguration)algorithm.ConfigurationSet.Entity).Length)
            {

            }

            public override bool IsFixedSize
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        private class TestIntegerListEntityConfiguration : IntegerListEntityConfiguration<TestIntegerListEntityConfiguration, TestIntegerListEntity>
        {
            public int Length { get; set; }
        }

        private class TestRandomUtil : IRandomNumberService
        {
            private int increment;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetRandomPercentRatio()
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
