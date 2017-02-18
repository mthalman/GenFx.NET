using GenFx;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
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
            IList<GeneticEntity> geneticEntities = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();
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
            IList<GeneticEntity> geneticEntities = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();
            Assert.AreSame(entity1, geneticEntities[0], "Different entity was returned.");
            Assert.AreSame(entity2, geneticEntities[1], "Different entity was returned.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_Serialization()
        {
            MockCrossoverOperator op = new MockCrossoverOperator();
            op.CrossoverRate = .5;

            MockCrossoverOperator result = (MockCrossoverOperator)SerializationHelper.TestSerialization(op, new Type[0]);
            Assert.AreEqual(2, result.RequiredParentCount);
            Assert.AreEqual(.5, result.CrossoverRate);
        }

        /// <summary>
        /// Tests that an exception is thrown when null parents are provided to crossover.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_NullParents()
        {
            MockCrossoverOperator op = new MockCrossoverOperator();
            AssertEx.Throws<ArgumentNullException>(() => op.Crossover(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when null is returned by the derived <see cref="CrossoverOperator.GenerateCrossover(IList{GeneticEntity})"/> method.
        /// </summary>
        [TestMethod]
        public void CrossoverOperator_NullCrossoverResult()
        {
            MockCrossoverOperator3 op = new MockCrossoverOperator3();
            op.CrossoverRate = 1;
            List<GeneticEntity> parents = new List<GeneticEntity>
            {
                new MockEntity(),
                new MockEntity(),
            };
            AssertEx.Throws<InvalidOperationException>(() => op.Crossover(parents));
            Assert.IsTrue(op.GenerateCrossoverCalled);
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

        private class FakeCrossoverOperator : CrossoverOperator
        {
            public FakeCrossoverOperator() : base(2)
            {
            }

            protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
            {
                MockEntity mockEntity1 = (MockEntity)parents[0];
                MockEntity mockEntity2 = (MockEntity)parents[1];
                List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
                geneticEntities.Add(mockEntity2);
                geneticEntities.Add(mockEntity1);
                return geneticEntities;
            }
        }
    }
}
