using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    ///This is a test class for GenFx.CrossoverOperator and is intended
    ///to contain all GenFx.CrossoverOperator Unit Tests
    ///</summary>
    public class CrossoverOperatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void CrossoverOperator_Ctor_NullAlgorithm()
        {
            FakeCrossoverOperator op = new FakeCrossoverOperator();
            Assert.Throws<ArgumentNullException>(() => op.Initialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the CrossoverRate setting.
        /// </summary>
        [Fact]
        public void CrossoverOperator_Ctor_InvalidSetting1()
        {
            FakeCrossoverOperator config = new FakeCrossoverOperator();
            Assert.Throws<ValidationException>(() => config.CrossoverRate = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the CrossoverRate setting.
        /// </summary>
        [Fact]
        public void CrossoverOperator_Ctor_InvalidSetting2()
        {
            FakeCrossoverOperator config = new FakeCrossoverOperator();
            Assert.Throws<ValidationException>(()=> config.CrossoverRate = -1);
        }

        /// <summary>
        /// Tests that the crossover works correctly when the crossover rate is hit.
        /// </summary>
        [Fact]
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
            Assert.NotSame(entity1, geneticEntities[1]);
            Assert.NotSame(entity2, geneticEntities[0]);
            Assert.Equal(entity1.Identifier, ((MockEntity)geneticEntities[1]).Identifier);
            Assert.Equal(entity2.Identifier, ((MockEntity)geneticEntities[0]).Identifier);

            Assert.Equal(0, geneticEntities[0].Age);
            Assert.Equal(0, geneticEntities[1].Age);
        }

        /// <summary>
        /// Tests that the crossover works correctly when the crossover rate is not hit.
        /// </summary>
        [Fact]
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
            Assert.Same(entity1, geneticEntities[0]);
            Assert.Same(entity2, geneticEntities[1]);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void CrossoverOperator_Serialization()
        {
            MockCrossoverOperator op = new MockCrossoverOperator
            {
                CrossoverRate = .5
            };

            MockCrossoverOperator result = (MockCrossoverOperator)SerializationHelper.TestSerialization(op, new Type[0]);
            Assert.Equal(2, result.RequiredParentCount);
            Assert.Equal(.5, result.CrossoverRate);
        }

        /// <summary>
        /// Tests that an exception is thrown when null parents are provided to crossover.
        /// </summary>
        [Fact]
        public void CrossoverOperator_NullParents()
        {
            MockCrossoverOperator op = new MockCrossoverOperator();
            Assert.Throws<ArgumentNullException>(() => op.Crossover(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when null is returned by the derived <see cref="CrossoverOperator.GenerateCrossover(IList{GeneticEntity})"/> method.
        /// </summary>
        [Fact]
        public void CrossoverOperator_NullCrossoverResult()
        {
            MockCrossoverOperator3 op = new MockCrossoverOperator3
            {
                CrossoverRate = 1
            };
            List<GeneticEntity> parents = new List<GeneticEntity>
            {
                new MockEntity(),
                new MockEntity(),
            };
            Assert.Throws<InvalidOperationException>(() => op.Crossover(parents));
            Assert.True(op.GenerateCrossoverCalled);
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
                List<GeneticEntity> geneticEntities = new List<GeneticEntity>
                {
                    mockEntity2,
                    mockEntity1
                };
                return geneticEntities;
            }
        }
    }
}
