using System;
using System.Collections.Generic;
using GenFx;
using GenFx.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFxTests.Mocks;
using GenFxTests.Helpers;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.CrossoverOperator and is intended
    ///to contain all GenFx.CrossoverOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class CrossoverOperatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor()
        {
            double crossoverRate = .8;
            MockGeneticAlgorithm algorithm = GetGeneticAlgorithm(crossoverRate);
            FakeCrossoverOperator op = new FakeCrossoverOperator(algorithm);
            Assert.IsTrue(op.CrossoverRate.Equals(crossoverRate), "Crossover rate was not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new FakeCrossoverOperator(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a setting is missing.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FakeCrossoverOperator(new MockGeneticAlgorithm()));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the CrossoverRate setting.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor_InvalidSetting1()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            FakeCrossoverOperatorConfiguration config = new FakeCrossoverOperatorConfiguration();
            AssertEx.Throws<ValidationException>(() => config.CrossoverRate = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the CrossoverRate setting.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor_InvalidSetting2()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            FakeCrossoverOperatorConfiguration config = new FakeCrossoverOperatorConfiguration();
            AssertEx.Throws<ValidationException>(()=> config.CrossoverRate = -1);
        }

        /// <summary>
        /// Tests that the crossover works correctly when the crossover rate is hit.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Crossover()
        {
            double crossoverRate = 1; // force crossover to occur
            MockGeneticAlgorithm algorithm = GetGeneticAlgorithm(crossoverRate);
            FakeCrossoverOperator op = new FakeCrossoverOperator(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            PrivateObject entAccessor = new PrivateObject(entity1, new PrivateType(typeof(GeneticEntity)));
            entAccessor.SetField("age", 2);
            entity1.Identifier = "1";
            MockEntity entity2 = new MockEntity(algorithm);
            entAccessor = new PrivateObject(entity2, new PrivateType(typeof(GeneticEntity)));
            entAccessor.SetField("age", 5);
            entity2.Identifier = "3";
            IList<GeneticEntity> geneticEntities = op.Crossover(entity1, entity2);
            Assert.AreNotSame(entity1, geneticEntities[1], "Clone was not called correctly.");
            Assert.AreNotSame(entity2, geneticEntities[0], "Clone was not called correctly.");
            Assert.AreEqual(entity1.Identifier, ((MockEntity)geneticEntities[1]).Identifier, "Entity value was not swapped.");
            Assert.AreEqual(entity2.Identifier, ((MockEntity)geneticEntities[0]).Identifier, "Entity value was not swapped.");

            Assert.AreEqual(0, geneticEntities[0].Age, "Age should have been reset.");
            Assert.AreEqual(0, geneticEntities[1].Age, "Age should have been reset.");
        }

        /// <summary>
        /// Tests that the crossover works correctly when the crossover rate is not hit.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Crossover_NoOp()
        {
            double crossoverRate = 0; // force crossover not to occur
            MockGeneticAlgorithm algorithm = GetGeneticAlgorithm(crossoverRate);
            FakeCrossoverOperator op = new FakeCrossoverOperator(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            entity1.Identifier = "1";
            MockEntity entity2 = new MockEntity(algorithm);
            entity2.Identifier = "3";
            IList<GeneticEntity> geneticEntities = op.Crossover(entity1, entity2);
            Assert.AreSame(entity1, geneticEntities[0], "Different entity was returned.");
            Assert.AreSame(entity2, geneticEntities[1], "Different entity was returned.");
        }

        private static MockGeneticAlgorithm GetGeneticAlgorithm(double crossoverRate)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            FakeCrossoverOperatorConfiguration config = new FakeCrossoverOperatorConfiguration();
            config.CrossoverRate = crossoverRate;
            algorithm.ConfigurationSet.CrossoverOperator = config;
            return algorithm;
        }

        private class FakeCrossoverOperator : CrossoverOperator
        {
            public FakeCrossoverOperator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2)
            {
                MockEntity mockEntity1 = (MockEntity)entity1;
                MockEntity mockEntity2 = (MockEntity)entity2;
                List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
                geneticEntities.Add(mockEntity2);
                geneticEntities.Add(mockEntity1);
                return geneticEntities;
            }
        }

        [Component(typeof(FakeCrossoverOperator))]
        private class FakeCrossoverOperatorConfiguration : CrossoverOperatorConfiguration
        {
        }
    }
}
