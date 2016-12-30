using GenFx;
using GenFx.ComponentLibrary.Base;
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
            });
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration()
            });
            MockEntity entity = new MockEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity<MockEntity, MockEntityConfiguration>)));
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration()
            });
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration()
            });
            MockEntity entity = new MockEntity(algorithm);
            entity.Age = 10;
            entity.ScaledFitnessValue = 10;
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity<MockEntity, MockEntityConfiguration>)));
            accessor.SetField("rawFitnessValue", 123);

            MockEntity newEntity = new MockEntity(algorithm);
            entity.CopyTo(newEntity);

            Assert.AreEqual(entity.Age, newEntity.Age, "Age value not copied correctly.");
            Assert.AreEqual(entity.RawFitnessValue, newEntity.RawFitnessValue, "RawFitnessValue not copied correctly.");
            Assert.AreEqual(entity.ScaledFitnessValue, newEntity.ScaledFitnessValue, "RawFitnessValue not copied correctly.");
        }

        private class TestEntity : GeneticEntity<TestEntity, TestEntityConfiguration>
        {
            public TestEntity(IGeneticAlgorithm algorithm)
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
        }

        private class TestEntityConfiguration : GeneticEntityConfiguration<TestEntityConfiguration, TestEntity>
        {
            public int Test { get; set; }
        }
    }
}
