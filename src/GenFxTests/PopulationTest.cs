using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
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
    /// This is a test class for GenFx.Population and is intended
    /// to contain all GenFx.Population Unit Tests
    /// </summary>
    [TestClass()]
    public class PopulationTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void Population_Ctor()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
            });
            IPopulation population = new SimplePopulation(algorithm);
            PrivateObject accessor = new PrivateObject(population);

            Assert.AreSame(algorithm, accessor.GetProperty("Algorithm"), "Algorithm not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Population_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SimplePopulation(null));
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_EvaluateFitness_NoScaling_NoStatistics_Async()
        {
            await TestEvaluateFitnessAsync(false, false);
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_EvaluateFitness_Scaling_NoStatistics_Async()
        {
            await TestEvaluateFitnessAsync(true, false);
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_EvaluateFitness_NoScaling_Statistics_Async()
        {
            await TestEvaluateFitnessAsync(false, true);
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_EvaluateFitness_Scaling_Statistics_Async()
        {
            await TestEvaluateFitnessAsync(true, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_Initialize_Async()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration
                {
                    PopulationSize = 10
                }
            });
            SimplePopulation population = new SimplePopulation(algorithm);
            await population.InitializeAsync();
            Assert.AreEqual(algorithm.ConfigurationSet.Population.PopulationSize, population.Entities.Count, "Population not generated correctly.");
        }

        /// <summary>
        /// Tests that PopulationConfiguration.PopulationSize can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void PopulationSizeTest_Valid()
        {
            SimplePopulationConfiguration target = new SimplePopulationConfiguration();
            int val = 100;
            target.PopulationSize = val;
            Assert.AreEqual(val, target.PopulationSize, "PopulationSize was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when ComponentConfigurationSet.PopulationSize is 
        /// set to an invalid value.
        ///</summary>
        [TestMethod()]
        public void PopulationSizeTest_Invalid()
        {
            SimplePopulationConfiguration target = new SimplePopulationConfiguration();
            AssertEx.Throws<ValidationException>(() => target.PopulationSize = 0);
        }

        private static async Task TestEvaluateFitnessAsync(bool useScaling, bool useStatistics)
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration
                {
                    StatisticsEnabled = useStatistics
                }
            };
            
            if (useScaling)
            {
                config.FitnessScalingStrategy = new FakeFitnessScalingStrategyConfiguration();
            }

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);

            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);
            if (useScaling)
            {
                algorithm.Operators.FitnessScalingStrategy = new FakeFitnessScalingStrategy(algorithm);
            }

            SimplePopulation population = new SimplePopulation(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            entity1.Identifier = "123";
            population.Entities.Add(entity1);

            MockEntity entity2 = new MockEntity(algorithm);
            entity2.Identifier = "456";
            population.Entities.Add(entity2);

            await population.EvaluateFitnessAsync();

            Assert.AreEqual((double)123, entity1.RawFitnessValue, "RawFitnessValue not set correctly.");
            Assert.AreEqual((double)456, entity2.RawFitnessValue, "RawFitnessValue not set correctly.");

            if (useScaling)
            {
                Assert.AreEqual(entity1.RawFitnessValue - 1, entity1.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
                Assert.AreEqual(entity2.RawFitnessValue - 1, entity2.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
            }
            else
            {
                Assert.AreEqual(entity1.RawFitnessValue, entity1.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
                Assert.AreEqual(entity2.RawFitnessValue, entity2.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
            }

            if (useStatistics)
            {
                Assert.AreEqual(entity2.RawFitnessValue, population.RawMax, "RawMax not set correctly.");
                Assert.AreEqual(entity1.RawFitnessValue, population.RawMin, "RawMax not set correctly.");
                Assert.AreEqual((entity1.RawFitnessValue + entity2.RawFitnessValue) / 2, population.RawMean, "RawMean not set correctly.");

                Assert.AreEqual(entity2.ScaledFitnessValue, population.ScaledMax, "ScaledMax not set correctly.");
                Assert.AreEqual(entity1.ScaledFitnessValue, population.ScaledMin, "ScaledMin not set correctly.");
                Assert.AreEqual((entity1.ScaledFitnessValue + entity2.ScaledFitnessValue) / 2, population.ScaledMean, "ScaledMean not set correctly.");
            }
        }

        private class FakeFitnessScalingStrategy : FitnessScalingStrategyBase<FakeFitnessScalingStrategy, FakeFitnessScalingStrategyConfiguration>
        {
            public FakeFitnessScalingStrategy(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override void UpdateScaledFitnessValues(IPopulation population)
            {
                for (int i = 0; i < population.Entities.Count; i++)
                {
                    population.Entities[i].ScaledFitnessValue = population.Entities[i].RawFitnessValue - 1;
                }
            }
        }

        private class FakeFitnessScalingStrategyConfiguration : FitnessScalingStrategyConfigurationBase<FakeFitnessScalingStrategyConfiguration, FakeFitnessScalingStrategy>
        {
        }

        private class TestPopulation : PopulationBase<TestPopulation, TestPopulationConfiguration>
        {
            public TestPopulation(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }
        }

        private class TestPopulationConfiguration : PopulationConfigurationBase<TestPopulationConfiguration, TestPopulation>
        {
        }
    }
}
