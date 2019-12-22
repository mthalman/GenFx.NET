using GenFx.Validation;
using System;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// This is a test class for GenFx.Population and is intended
    /// to contain all GenFx.Population Unit Tests
    /// </summary>
    public class PopulationTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void Population_Ctor()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
            };
            Population population = new MockPopulation();
            population.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(population);

            Assert.Same(algorithm, accessor.GetProperty("Algorithm"));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void Population_Ctor_NullAlgorithm()
        {
            MockPopulation population = new MockPopulation();
            Assert.Throws<ArgumentNullException>(() => population.Initialize(null));
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [Fact]
        public async Task Population_EvaluateFitness()
        {
            await TestEvaluateFitnessAsync(false, false);
            await TestEvaluateFitnessAsync(false, true);
            await TestEvaluateFitnessAsync(true, false);
            await TestEvaluateFitnessAsync(true, true);
        }
        
        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [Fact]
        public async Task Population_Initialize_Async()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity
                {
                    TestProperty = 3
                },
                PopulationSeed = new MockPopulation
                {
                    MinimumPopulationSize = 10
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            await population.InitializeAsync();
            Assert.Equal(algorithm.PopulationSeed.MinimumPopulationSize, population.Entities.Count);
            Assert.Equal(3, ((MockEntity)population.Entities[0]).TestProperty);
        }

        /// <summary>
        /// Tests that PopulationConfiguration.PopulationSize can be set to a valid value.
        ///</summary>
        [Fact]
        public void PopulationSizeTest_Valid()
        {
            MockPopulation target = new MockPopulation();
            int val = 100;
            target.MinimumPopulationSize = val;
            Assert.Equal(val, target.MinimumPopulationSize);
        }

        /// <summary>
        /// Tests that an exception is thrown when ComponentConfigurationSet.PopulationSize is 
        /// set to an invalid value.
        ///</summary>
        [Fact]
        public void PopulationSizeTest_Invalid()
        {
            MockPopulation target = new MockPopulation();
            Assert.Throws<ValidationException>(() => target.MinimumPopulationSize = 0);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void Population_Serialization()
        {
            MockPopulation population = new MockPopulation
            {
                MinimumPopulationSize = 10,
                Index = 3,
            };

            population.Entities.Add(new MockEntity());

            PrivateObject privObj = new PrivateObject(population, new PrivateType(typeof(Population)));
            privObj.SetField("rawMean", (double)1);
            privObj.SetField("rawStandardDeviation", (double)2);
            privObj.SetField("rawMax", (double)3);
            privObj.SetField("rawMin", (double)4);

            MockPopulation result = (MockPopulation)SerializationHelper.TestSerialization(population, new Type[]
            {
                typeof(MockEntity)
            });

            PrivateObject resultPrivObj = new PrivateObject(result, new PrivateType(typeof(Population)));

            Assert.Equal(population.MinimumPopulationSize, result.MinimumPopulationSize);
            Assert.Equal(population.Index, result.Index);
            Assert.IsType<MockEntity>(result.Entities[0]);

            Assert.Equal((double)1, resultPrivObj.GetField("rawMean"));
            Assert.Equal((double)2, resultPrivObj.GetField("rawStandardDeviation"));
            Assert.Equal((double)3, resultPrivObj.GetField("rawMax"));
            Assert.Equal((double)4, resultPrivObj.GetField("rawMin"));
        }

        private static async Task TestEvaluateFitnessAsync(bool useScaling, bool useMetric)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };

            if (useScaling)
            {
                algorithm.FitnessScalingStrategy = new FakeFitnessScalingStrategy();
            }

            if (useMetric)
            {
                algorithm.Metrics.Add(new MockMetric());
            }

            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);
            if (useScaling)
            {
                algorithm.FitnessScalingStrategy = new FakeFitnessScalingStrategy();
                algorithm.FitnessScalingStrategy.Initialize(algorithm);
            }

            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.Identifier = "123";
            population.Entities.Add(entity1);

            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.Identifier = "456";
            population.Entities.Add(entity2);

            await population.EvaluateFitnessAsync();

            Assert.Equal((double)123, entity1.RawFitnessValue);
            Assert.Equal((double)456, entity2.RawFitnessValue);

            if (useScaling)
            {
                Assert.Equal(entity1.RawFitnessValue - 1, entity1.ScaledFitnessValue);
                Assert.Equal(entity2.RawFitnessValue - 1, entity2.ScaledFitnessValue);
            }
            else
            {
                Assert.Equal(entity1.RawFitnessValue, entity1.ScaledFitnessValue);
                Assert.Equal(entity2.RawFitnessValue, entity2.ScaledFitnessValue);
            }

            if (!useMetric && !useScaling)
            {
                Assert.False(population.RawMax.HasValue, "RawMax not set correctly.");
                Assert.False(population.RawMin.HasValue, "RawMax not set correctly.");
                Assert.False(population.RawMean.HasValue, "RawMean not set correctly.");
                Assert.False(population.RawStandardDeviation.HasValue, "RawStandardDeviation not set correctly.");
            }
            else
            {
                Assert.Equal(entity2.RawFitnessValue, population.RawMax);
                Assert.Equal(entity1.RawFitnessValue, population.RawMin);
                Assert.Equal((entity1.RawFitnessValue + entity2.RawFitnessValue) / 2, population.RawMean);
            }
        }

        private class FakeFitnessScalingStrategy : FitnessScalingStrategy
        {
            protected override void UpdateScaledFitnessValues(Population population)
            {
                for (int i = 0; i < population.Entities.Count; i++)
                {
                    population.Entities[i].ScaledFitnessValue = population.Entities[i].RawFitnessValue - 1;
                }
            }
        }

        private class TestPopulation : Population
        {
        }
    }
}
