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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            Population population = new Population(algorithm);
            PrivateObject accessor = new PrivateObject(population);

            Assert.AreSame(algorithm, accessor.GetProperty("Algorithm"), "Algorithm not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when required config class is missing.
        /// </summary>
        [TestMethod]
        public void Population_Ctor_MissingConfig()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            AssertEx.Throws<ArgumentException>(() => new TestPopulation(algorithm));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Population_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new Population(null));
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockEntityConfiguration entConfig = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Entity = entConfig;
            PopulationConfiguration popConfig = new PopulationConfiguration();
            popConfig.PopulationSize = 10;
            algorithm.ConfigurationSet.Population = popConfig;
            Population population = new Population(algorithm);
            await population.InitializeAsync();
            Assert.AreEqual(popConfig.PopulationSize, population.Entities.Count, "Population not generated correctly.");
        }

        /// <summary>
        /// Tests that PopulationConfiguration.PopulationSize can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void PopulationSizeTest_Valid()
        {
            PopulationConfiguration target = new PopulationConfiguration();
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
            PopulationConfiguration target = new PopulationConfiguration();
            AssertEx.Throws<ValidationException>(() => target.PopulationSize = 0);
        }

        private static async Task TestEvaluateFitnessAsync(bool useScaling, bool useStatistics)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            MockGeneticAlgorithmConfiguration config = new MockGeneticAlgorithmConfiguration();
            config.StatisticsEnabled = useStatistics;
            algorithm.ConfigurationSet.GeneticAlgorithm = config;
            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);
            if (useScaling)
            {
                algorithm.ConfigurationSet.FitnessScalingStrategy = new FakeFitnessScalingStrategyConfiguration();
                algorithm.Operators.FitnessScalingStrategy = new FakeFitnessScalingStrategy(algorithm);
            }

            Population population = new Population(algorithm);
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

        private class FakeFitnessScalingStrategy : FitnessScalingStrategy
        {
            public FakeFitnessScalingStrategy(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override void UpdateScaledFitnessValues(Population population)
            {
                for (int i = 0; i < population.Entities.Count; i++)
                {
                    population.Entities[i].ScaledFitnessValue = population.Entities[i].RawFitnessValue - 1;
                }
            }
        }

        [Component(typeof(FakeFitnessScalingStrategy))]
        private class FakeFitnessScalingStrategyConfiguration : FitnessScalingStrategyConfiguration
        {
        }

        private class TestPopulation : Population
        {
            public TestPopulation(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }
        }
    }
}
