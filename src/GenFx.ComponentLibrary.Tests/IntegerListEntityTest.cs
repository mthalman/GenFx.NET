using GenFx.ComponentLibrary.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="IntegerListEntity"/> class.
    ///</summary>
    public class IntegerListEntityTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the UpdateStringRepresentation method works correctly.
        /// </summary>
        [Fact]
        public void IntegerListEntity_UpdateStringRepresentation()
        {
            IntegerListEntity entity = GetEntity();
            PrivateObject accessor = new PrivateObject(entity);
            accessor.Invoke("UpdateStringRepresentation");

            Assert.Equal("5, 3, 10, 127", entity.Representation);
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [Fact]
        public void IntegerListEntity_Clone()
        {
            IntegerListEntity entity = GetEntity();
            IntegerListEntity clone = (IntegerListEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [Fact]
        public void IntegerListEntity_CopyTo()
        {
            IntegerListEntity entity = GetEntity();
            IntegerListEntity entity2 = new TestIntegerListEntity();
            entity2.Initialize(GetAlgorithm(4));

            entity.CopyTo(entity2);

            CompareGeneticEntities(entity, entity2);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [Fact]
        public void IntegerListEntity_CopyTo_NullEntity()
        {
            IntegerListEntity entity = GetEntity();

            Assert.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void IntegerListEntity_Ctor()
        {
            int size = 3;
            GeneticAlgorithm algorithm = GetAlgorithm(size);
            IntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = size, MaximumStartingLength = size };
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<int>)));
            Assert.Equal(size, entity.Length);
            Assert.Equal(size, ((List<int>)accessor.GetField("genes")).Count);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [Fact]
        public void IntegerListEntity_Initialize()
        {
            RandomNumberService.Instance = new TestRandomUtil();

            GeneticAlgorithm algorithm = GetAlgorithm(4);
            IntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = 4, MaximumStartingLength = 4 };
            entity.Initialize(algorithm);
            
            Assert.Equal("1, 2, 3, 4", entity.Representation);
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [Fact]
        public void IntegerListEntity_Indexer()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(3);
            IntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = 3, MaximumStartingLength = 3 };
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(ListEntity<int>)));
            List<int> genes = (List<int>)accessor.GetField("genes");
            for (int i = 0; i < genes.Count; i++)
            {
                genes[i] = 0;
            }

            entity[0] = 1;
            Assert.Equal(1, genes[0]);
            Assert.Equal(0, genes[1]);
            Assert.Equal(0, genes[2]);
            Assert.Equal(1, entity[0]);
            entity[1] = 2;
            Assert.Equal(1, genes[0]);
            Assert.Equal(2, genes[1]);
            Assert.Equal(0, genes[2]);
            Assert.Equal(2, entity[1]);
            entity[2] = 3;
            Assert.Equal(1, genes[0]);
            Assert.Equal(2, genes[1]);
            Assert.Equal(3, genes[2]);
            Assert.Equal(3, entity[2]);
        }

        /// <summary>
        /// Tests that the Length property works correctly.
        ///</summary>
        [Fact]
        public void IntegerListEntity_Length()
        {
            int length = 50;
            GeneticAlgorithm algorithm = GetAlgorithm(length);

            IntegerListEntity entity = new TestIntegerListEntity { MinimumStartingLength = length, MaximumStartingLength = length };
            entity.Initialize(algorithm);
            Assert.Equal(length, entity.Length);

            entity.Length = length;
            Assert.Equal(length, entity.Length);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void IntegerListEntity_Serialization()
        {
            IntegerListEntity entity = new IntegerListEntity();
            entity.MinElementValue = 10;
            entity.MaxElementValue = 11;
            entity.RequiresUniqueElementValues = true;

            IntegerListEntity result = (IntegerListEntity)SerializationHelper.TestSerialization(entity, new Type[0]);

            Assert.Equal(entity.MinElementValue, result.MinElementValue);
            Assert.Equal(entity.MaxElementValue, result.MaxElementValue);
            Assert.Equal(entity.RequiresUniqueElementValues, result.RequiresUniqueElementValues);
        }

        /// <summary>
        /// Ensures that the <see cref="IntegerListEntity.RequiresUniqueElementValues"/> property produces
        /// an entity that contains all unique element values when initialized.
        /// </summary>
        [Fact]
        public void IntegerListEntity_Initialize_UniqueValues()
        {
            IntegerListEntity entity = new IntegerListEntity
            {
                MinElementValue = 1,
                MaxElementValue = 10,
                MinimumStartingLength = 10,
                MaximumStartingLength = 10,
                RequiresUniqueElementValues = true
            };

            for (int i = 0; i < 10; i++)
            {
                entity.Initialize(new MockGeneticAlgorithm());
                Assert.Equal(10, entity.Distinct().Count());
            }
        }

        private static void CompareGeneticEntities(IntegerListEntity expectedEntity, IntegerListEntity actualEntity)
        {
            Assert.NotSame(expectedEntity, actualEntity);
            Assert.Equal(expectedEntity.Representation, actualEntity.Representation);
            Assert.Equal(expectedEntity[0], actualEntity[0]);
            Assert.Equal(expectedEntity[1], actualEntity[1]);
            Assert.Equal(expectedEntity[2], actualEntity[2]);
            Assert.Equal(expectedEntity[3], actualEntity[3]);
            Assert.Equal(expectedEntity.Age, actualEntity.Age);
            Assert.Equal(expectedEntity.RawFitnessValue, actualEntity.RawFitnessValue);
            Assert.Equal(expectedEntity.ScaledFitnessValue, actualEntity.ScaledFitnessValue);
        }

        private static IntegerListEntity GetEntity()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(4);

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

        private static GeneticAlgorithm GetAlgorithm(int entityLength)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
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
