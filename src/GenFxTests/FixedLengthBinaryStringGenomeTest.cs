using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.Contracts;
using GenFxTests.Helpers;
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
            IGeneticAlgorithm algorithm = GetAlgorithm(size);
            FixedLengthBinaryStringEntity entity = (FixedLengthBinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(BinaryStringEntity)));
            Assert.AreEqual(size, entity.Length, "Length not initialized correctly.");
            Assert.AreEqual(size, ((BitArray)accessor.GetField("genes")).Length, "Genes not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid binary length is used.
        /// </summary>
        [TestMethod()]
        public void FixedLengthBinaryStringEntity_Ctor_InvalidLength()
        {
            FixedLengthBinaryStringEntity entity = new FixedLengthBinaryStringEntity();
            AssertEx.Throws<ValidationException>(() => entity.FixedLength = 0);
        }

        private static void CompareGeneticEntities(FixedLengthBinaryStringEntity expectedEntity, FixedLengthBinaryStringEntity actualEntity)
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

        private static FixedLengthBinaryStringEntity GetEntity()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(4);
            
            FixedLengthBinaryStringEntity entity = (FixedLengthBinaryStringEntity)algorithm.GeneticEntitySeed.CreateNewAndInitialize();
            entity.Initialize(algorithm);
            entity[0] = true;
            entity[1] = true;
            entity[2] = false;
            entity[3] = true;

            entity.Age = 3;
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("rawFitnessValue", 1);
            entity.ScaledFitnessValue = 2;

            return entity;
        }

        private static IGeneticAlgorithm GetAlgorithm(int entityLength)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new FixedLengthBinaryStringEntity
                {
                    FixedLength = entityLength
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);
            return algorithm;
        }

    }


}
