using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            IPopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(population);

            Assert.AreSame(algorithm, accessor.GetProperty("Algorithm"), "Algorithm not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Population_Ctor_NullAlgorithm()
        {
            SimplePopulation population = new SimplePopulation();
            AssertEx.Throws<ArgumentNullException>(() => population.Initialize(null));
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_EvaluateFitness_NoScaling_Async()
        {
            await TestEvaluateFitnessAsync(false);
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_EvaluateFitness_Scaling_Async()
        {
            await TestEvaluateFitnessAsync(true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Population_Initialize_Async()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation
                {
                    PopulationSize = 10
                }
            };
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            await population.InitializeAsync();
            Assert.AreEqual(algorithm.PopulationSeed.PopulationSize, population.Entities.Count, "Population not generated correctly.");
        }

        /// <summary>
        /// Tests that PopulationConfiguration.PopulationSize can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void PopulationSizeTest_Valid()
        {
            SimplePopulation target = new SimplePopulation();
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
            SimplePopulation target = new SimplePopulation();
            AssertEx.Throws<ValidationException>(() => target.PopulationSize = 0);
        }

        private static async Task TestEvaluateFitnessAsync(bool useScaling)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };

            if (useScaling)
            {
                algorithm.FitnessScalingStrategy = new FakeFitnessScalingStrategy();
            }

            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);
            if (useScaling)
            {
                algorithm.FitnessScalingStrategy = new FakeFitnessScalingStrategy();
                algorithm.FitnessScalingStrategy.Initialize(algorithm);
            }

            SimplePopulation population = new SimplePopulation();
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

            Assert.AreEqual(entity2.RawFitnessValue, population.RawMax, "RawMax not set correctly.");
            Assert.AreEqual(entity1.RawFitnessValue, population.RawMin, "RawMax not set correctly.");
            Assert.AreEqual((entity1.RawFitnessValue + entity2.RawFitnessValue) / 2, population.RawMean, "RawMean not set correctly.");

            Assert.AreEqual(entity2.ScaledFitnessValue, population.ScaledMax, "ScaledMax not set correctly.");
            Assert.AreEqual(entity1.ScaledFitnessValue, population.ScaledMin, "ScaledMin not set correctly.");
            Assert.AreEqual((entity1.ScaledFitnessValue + entity2.ScaledFitnessValue) / 2, population.ScaledMean, "ScaledMean not set correctly.");
        }

        private class FakeFitnessScalingStrategy : FitnessScalingStrategyBase
        {
            protected override void UpdateScaledFitnessValues(IPopulation population)
            {
                for (int i = 0; i < population.Entities.Count; i++)
                {
                    population.Entities[i].ScaledFitnessValue = population.Entities[i].RawFitnessValue - 1;
                }
            }
        }

        private class TestPopulation : PopulationBase
        {
        }
    }
}
