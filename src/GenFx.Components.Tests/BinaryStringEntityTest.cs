using GenFx.Components.Lists;
using System;
using System.Collections;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BinaryStringEntity"/> class.
    ///</summary>
    public class BinaryStringEntityTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the UpdateStringRepresentation method works correctly.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_UpdateStringRepresentation()
        {
            TestBinaryStringEntity entity = GetEntity();
            PrivateObject accessor = new PrivateObject(entity);
            accessor.Invoke("UpdateStringRepresentation");

            Assert.Equal("1010", entity.Representation);
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_Clone()
        {
            TestBinaryStringEntity entity = GetEntity();
            TestBinaryStringEntity clone = (TestBinaryStringEntity)entity.Clone();
            CompareGeneticEntities(entity, clone);
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [Fact]
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
        [Fact]
        public void BinaryStringEntity_CopyTo_NullEntity()
        {
            TestBinaryStringEntity entity = GetEntity();

            Assert.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_Ctor()
        {
            int size = 3;
            GeneticAlgorithm algorithm = GetAlgorithm(size);
            TestBinaryStringEntity entity = new TestBinaryStringEntity { MinimumStartingLength = size, MaximumStartingLength = size };
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
            Assert.Equal(size, entity.Length);
            Assert.Equal(size, ((BitArray)accessor.GetField("genes")).Length);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_Initialize()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(4);
            TestBinaryStringEntity entity = new TestBinaryStringEntity { MinimumStartingLength = 4, MaximumStartingLength = 4 };
            RandomNumberService.Instance = new TestRandomUtil();
            entity.Initialize(algorithm);
            Assert.Equal("1010", entity.Representation);
        }

        /// <summary>
        /// Tests that the indexer works correctly.
        ///</summary>
        [Fact]
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
            Assert.True(genes[0]);
            Assert.False(genes[1]);
            Assert.False(genes[2]);
            Assert.True(entity[0]);
            entity[1] = true;
            Assert.True(genes[0]);
            Assert.True(genes[1]);
            Assert.False(genes[2]);
            Assert.True(entity[1]);
            entity[2] = true;
            Assert.True(genes[0]);
            Assert.True(genes[1]);
            Assert.True(genes[2]);
            Assert.True(entity[2]);
        }

        /// <summary>
        /// Tests that the Length property works correctly.
        ///</summary>
        [Fact]
        public void BinaryStringEntity_Length()
        {
            int length = 50;
            GeneticAlgorithm algorithm = GetAlgorithm(length);

            TestBinaryStringEntity entity = new TestBinaryStringEntity { MinimumStartingLength = length, MaximumStartingLength = length };
            entity.Initialize(algorithm);
            Assert.Equal(length, entity.Length);

            entity.Length = length;
            Assert.Equal(length, entity.Length);
        }

        /// <summary>
        /// Tests that an exception is thrown when the Length is set to a different value when the string is fixed size.
        ///</summary>
        [Fact]
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
            Assert.Throws<ArgumentException>(() => entity.Length = 51);
        }

        /// <summary>
        /// Tests that the length can be expanded to contain more items.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_SetLengthToExpand()
        {
            BinaryStringEntity entity = new BinaryStringEntity
            {
                MinimumStartingLength = 2,
                MaximumStartingLength = 2,
            };

            entity.Initialize(new MockGeneticAlgorithm());

            Assert.Equal(2, entity.Length);

            entity.Length = 4;
            Assert.Equal(4, entity.Length);

            Assert.False(entity[2]);
            Assert.False(entity[3]);
        }

        /// <summary>
        /// Tests that the length can be contracted to decrease the number of items.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_SetLengthToContract()
        {
            BinaryStringEntity entity = new BinaryStringEntity
            {
                MinimumStartingLength = 4,
                MaximumStartingLength = 4,
            };

            entity.Initialize(new MockGeneticAlgorithm());
            Assert.Equal(4, entity.Length);

            entity[0] = true;
            Assert.True(entity[0]);

            entity.Length = 1;
            Assert.Equal(1, entity.Length);

            Assert.True(entity[0]);
        }

        /// <summary>
        /// Tests that the component can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_Serialization()
        {
            BinaryStringEntity entity = new BinaryStringEntity();
            entity.MinimumStartingLength = entity.MaximumStartingLength = 3;
            entity.IsFixedSize = true;
            entity.Initialize(new MockGeneticAlgorithm());

            BinaryStringEntity result = (BinaryStringEntity)SerializationHelper.TestSerialization(entity, new Type[] { typeof(MockGeneticAlgorithm) });

            for (int i = 0; i < 3; i++)
            {
                Assert.Equal(entity[i], result[i]);
            }

            Assert.True(result.IsFixedSize);
        }

        /// <summary>
        /// Tests that an exception is thrown when attempting to access state when the entity is not initialized.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_Uninitialized()
        {
            BinaryStringEntity entity = new BinaryStringEntity();
            Assert.Throws<InvalidOperationException>(() => { object x = entity[0]; });
            Assert.Throws<InvalidOperationException>(() => { entity[0] = true; });
            Assert.Throws<InvalidOperationException>(() => { int x = entity.Length; });
            Assert.Throws<InvalidOperationException>(() =>
            {
                BinaryStringEntity entity2 = new BinaryStringEntity();
                entity.CopyTo(entity2);
            });
        }

        /// <summary>
        /// Tests that the <see cref="BinaryStringEntity.Genes"/> property works correctly.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_Genes()
        {
            TestBinaryStringEntity entity = new TestBinaryStringEntity
            {
                MinimumStartingLength = 2,
                MaximumStartingLength = 2
            };
            entity.Initialize(new MockGeneticAlgorithm());

            BitArray bits = entity.GetGenes();
            Assert.Equal(entity.Length, bits.Length);

            for (int i = 0; i < bits.Length; i++)
            {
                Assert.Equal(entity[i], bits[i]);
            }
        }

        /// <summary>
        /// Tests that the <see cref="BinaryStringEntity.RequiresUniqueElementValues"/> property works correctly.
        /// </summary>
        [Fact]
        public void BinaryStringEntity_UseUniqueElementValues()
        {
            BinaryStringEntity entity = new BinaryStringEntity();
            Assert.False(entity.RequiresUniqueElementValues);
            Assert.Throws<NotSupportedException>(() => entity.RequiresUniqueElementValues = true);
        }

        private static void CompareGeneticEntities(TestBinaryStringEntity expectedEntity, TestBinaryStringEntity actualEntity)
        {
            PrivateObject accessor = new PrivateObject(expectedEntity, new PrivateType(typeof(GeneticEntity)));
            PrivateObject actualEntityAccessor = new PrivateObject(actualEntity, new PrivateType(typeof(GeneticEntity)));
            Assert.NotSame(expectedEntity, actualEntity);
            Assert.Equal(expectedEntity.Representation, actualEntity.Representation);
            Assert.Equal(expectedEntity[0], actualEntity[0]);
            Assert.Equal(expectedEntity[1], actualEntity[1]);
            Assert.Equal(expectedEntity[2], actualEntity[2]);
            Assert.Equal(expectedEntity[3], actualEntity[3]);
            Assert.Same(accessor.GetProperty("Algorithm"), actualEntityAccessor.GetProperty("Algorithm"));
            Assert.Equal(expectedEntity.Age, actualEntity.Age);
            Assert.Equal(accessor.GetField("rawFitnessValue"), actualEntityAccessor.GetField("rawFitnessValue"));
            Assert.Equal(expectedEntity.ScaledFitnessValue, actualEntity.ScaledFitnessValue);
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
            public BitArray GetGenes()
            {
                return this.Genes;
            }
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
