using GenFx;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
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

            Assert.AreSame(algorithm, accessor.GetProperty("Algorithm"), "Algorithm not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Population_Ctor_NullAlgorithm()
        {
            MockPopulation population = new MockPopulation();
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
            Assert.AreEqual(algorithm.PopulationSeed.MinimumPopulationSize, population.Entities.Count, "Population not generated correctly.");
            Assert.AreEqual(3, ((MockEntity)population.Entities[0]).TestProperty);
        }

        /// <summary>
        /// Tests that PopulationConfiguration.PopulationSize can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void PopulationSizeTest_Valid()
        {
            MockPopulation target = new MockPopulation();
            int val = 100;
            target.MinimumPopulationSize = val;
            Assert.AreEqual(val, target.MinimumPopulationSize, "PopulationSize was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when ComponentConfigurationSet.PopulationSize is 
        /// set to an invalid value.
        ///</summary>
        [TestMethod()]
        public void PopulationSizeTest_Invalid()
        {
            MockPopulation target = new MockPopulation();
            AssertEx.Throws<ValidationException>(() => target.MinimumPopulationSize = 0);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void Population_Serialization()
        {
            MockPopulation population = new MockPopulation
            {
                MinimumPopulationSize = 10,
                Index = 3,
            };

            population.Entities.Add(new MockEntity());

            PrivateObject privObj = new PrivateObject(population, new PrivateType(typeof(Population)));
            privObj.SetField("rawMean", 1);
            privObj.SetField("rawStandardDeviation", 2);
            privObj.SetField("rawMax", 3);
            privObj.SetField("rawMin", 4);
            privObj.SetField("scaledMean", 5);
            privObj.SetField("scaledStandardDeviation", 6);
            privObj.SetField("scaledMax", 7);
            privObj.SetField("scaledMin", 8);

            MockPopulation result = (MockPopulation)SerializationHelper.TestSerialization(population, new Type[]
            {
                typeof(MockEntity)
            });

            PrivateObject resultPrivObj = new PrivateObject(result, new PrivateType(typeof(Population)));

            Assert.AreEqual(population.MinimumPopulationSize, result.MinimumPopulationSize);
            Assert.AreEqual(population.Index, result.Index);
            Assert.IsInstanceOfType(result.Entities[0], typeof(MockEntity));

            Assert.AreEqual((double)1, resultPrivObj.GetField("rawMean"));
            Assert.AreEqual((double)2, resultPrivObj.GetField("rawStandardDeviation"));
            Assert.AreEqual((double)3, resultPrivObj.GetField("rawMax"));
            Assert.AreEqual((double)4, resultPrivObj.GetField("rawMin"));
            Assert.AreEqual((double)5, resultPrivObj.GetField("scaledMean"));
            Assert.AreEqual((double)6, resultPrivObj.GetField("scaledStandardDeviation"));
            Assert.AreEqual((double)7, resultPrivObj.GetField("scaledMax"));
            Assert.AreEqual((double)8, resultPrivObj.GetField("scaledMin"));
        }

        private static async Task TestEvaluateFitnessAsync(bool useScaling)
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
