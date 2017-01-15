using System;
using System.Collections.Generic;
using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFxTests.Mocks;
using GenFxTests.Helpers;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

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
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor_NullAlgorithm()
        {
            FakeCrossoverOperator op = new FakeCrossoverOperator();
            AssertEx.Throws<ArgumentNullException>(() => op.Initialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the CrossoverRate setting.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor_InvalidSetting1()
        {
            FakeCrossoverOperator config = new FakeCrossoverOperator();
            AssertEx.Throws<ValidationException>(() => config.CrossoverRate = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the CrossoverRate setting.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Ctor_InvalidSetting2()
        {
            FakeCrossoverOperator config = new FakeCrossoverOperator();
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
            FakeCrossoverOperator op = new FakeCrossoverOperator { CrossoverRate = crossoverRate };
            op.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.Age = 2;
            entity1.Identifier = "1";
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.Age = 5;
            entity2.Identifier = "3";
            IList<IGeneticEntity> geneticEntities = op.Crossover(entity1, entity2);
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
            FakeCrossoverOperator op = (FakeCrossoverOperator)algorithm.CrossoverOperator;
            op.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.Identifier = "1";
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.Identifier = "3";
            IList<IGeneticEntity> geneticEntities = op.Crossover(entity1, entity2);
            Assert.AreSame(entity1, geneticEntities[0], "Different entity was returned.");
            Assert.AreSame(entity2, geneticEntities[1], "Different entity was returned.");
        }

        private static MockGeneticAlgorithm GetGeneticAlgorithm(double crossoverRate)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                CrossoverOperator = new FakeCrossoverOperator
                {
                    CrossoverRate = crossoverRate
                }
            };
            return algorithm;
        }

        private class FakeCrossoverOperator : CrossoverOperatorBase
        {
            protected override IList<IGeneticEntity> GenerateCrossover(IGeneticEntity entity1, IGeneticEntity entity2)
            {
                MockEntity mockEntity1 = (MockEntity)entity1;
                MockEntity mockEntity2 = (MockEntity)entity2;
                List<IGeneticEntity> geneticEntities = new List<IGeneticEntity>();
                geneticEntities.Add(mockEntity2);
                geneticEntities.Add(mockEntity1);
                return geneticEntities;
            }
        }
    }
}
