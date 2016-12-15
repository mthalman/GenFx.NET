using GenFx;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.GeneticEntity and is intended
    ///to contain all GenFx.GeneticEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void Entity_Ctor()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            TestEntityConfiguration config = new TestEntityConfiguration();
            config.Test = 9;
            algorithm.ConfigurationSet.Entity = config;

            GeneticEntity entity = new TestEntity(algorithm);

            Assert.AreSame(algorithm, entity.Algorithm, "Algorithm not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config class is missing.
        /// </summary>
        [TestMethod]
        public void Entity_Ctor_MissingConfig()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            AssertEx.Throws<ArgumentException>(() => new TestEntity(algorithm));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Entity_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new MockEntity(null));
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Entity_EvaluateFitness_Async()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);
            MockEntity entity = new MockEntity(algorithm);
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            MockEntity entity = new MockEntity(algorithm);
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            MockEntity entity = new MockEntity(algorithm);
            entity.Initialize();
            Assert.AreEqual("11111", entity.Identifier, "Entity not initialized correctly.");
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod]
        public void Entity_CopyTo()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            MockEntity entity = new MockEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("age", 10);
            accessor.SetField("rawFitnessValue", 123);
            accessor.SetField("scaledFitnessValue", 10);

            MockEntity newEntity = new MockEntity(algorithm);
            entity.CopyTo(newEntity);

            Assert.AreEqual(entity.Age, newEntity.Age, "Age value not copied correctly.");
            Assert.AreEqual(entity.RawFitnessValue, newEntity.RawFitnessValue, "RawFitnessValue not copied correctly.");
            Assert.AreEqual(entity.ScaledFitnessValue, newEntity.ScaledFitnessValue, "RawFitnessValue not copied correctly.");
            Assert.AreSame(entity.Algorithm, newEntity.Algorithm, "Algorithm not copied correctly.");
        }

        private class TestEntity : GeneticEntity
        {
            public TestEntity(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            protected override void InitializeCore()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public override GeneticEntity Clone()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(TestEntity))]
        private class TestEntityConfiguration : GeneticEntityConfiguration
        {
            public int Test { get; set; }
        }
    }
}
