using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    ///This is a test class for GenFx.GeneticEntity and is intended
    ///to contain all GenFx.GeneticEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class GeneticEntityTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Entity_Ctor_NullAlgorithm()
        {
            MockEntity entity = new MockEntity();
            AssertEx.Throws<ArgumentNullException>(() => entity.Initialize(null));
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Entity_EvaluateFitness_Async()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            entity.Identifier = "123";
            await entity.EvaluateFitnessAsync();
            Assert.AreEqual((double)123, entity.RawFitnessValue, "RawFitnessValue not set correctly.");
            Assert.AreEqual((double)123, entity.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
        }

        /// <summary>
        /// Tests that the GetFitnessValue method works correctly.
        /// </summary>
        [TestMethod]
        public void Entity_GetFitnessValue()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity()
            };
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            entity.ScaledFitnessValue = 12;
            accessor.SetField("rawFitnessValue", 10);
            Assert.AreEqual(entity.ScaledFitnessValue, entity.GetFitnessValue(FitnessType.Scaled), "Incorrect fitness value returned.");
            Assert.AreEqual(accessor.GetField("rawFitnessValue"), entity.GetFitnessValue(FitnessType.Raw), "Incorrect fitness value returned.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public void Entity_Initialize()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity()
            };
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            Assert.AreEqual("11111", entity.Identifier, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod]
        public void Entity_CopyTo()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity()
            };
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            entity.Age = 10;
            entity.ScaledFitnessValue = 10;
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("rawFitnessValue", 123);

            MockEntity newEntity = new MockEntity();
            newEntity.Initialize(algorithm);
            entity.CopyTo(newEntity);

            Assert.AreEqual(entity.Age, newEntity.Age, "Age value not copied correctly.");
            Assert.AreEqual(entity.RawFitnessValue, newEntity.RawFitnessValue, "RawFitnessValue not copied correctly.");
            Assert.AreEqual(entity.ScaledFitnessValue, newEntity.ScaledFitnessValue, "RawFitnessValue not copied correctly.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void GeneticEntity_Serialization()
        {
            MockEntity entity = new MockEntity();
            entity.Age = 33;
            entity.ScaledFitnessValue = 2;

            PrivateObject privObj = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            privObj.SetField("rawFitnessValue", 7);

            MockEntity result = (MockEntity)SerializationHelper.TestSerialization(entity, new Type[0]);

            Assert.AreEqual(entity.Age, result.Age);
            Assert.AreEqual(entity.ScaledFitnessValue, result.ScaledFitnessValue);
            Assert.AreEqual(entity.RawFitnessValue, result.RawFitnessValue);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid fitness type is passed to <see cref="GeneticEntity.GetFitnessValue"/>.
        /// </summary>
        [TestMethod]
        public void GeneticEntity_GetFitnessValue_InvalidFitnessType()
        {
            TestEntity entity = new TestEntity();
            AssertEx.Throws<ArgumentException>(() => entity.GetFitnessValue((FitnessType)3));
        }

        private class TestEntity : GeneticEntity
        {
            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }
        }
    }
}
