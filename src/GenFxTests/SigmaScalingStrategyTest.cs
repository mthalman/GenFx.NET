using GenFx;
using GenFx.ComponentLibrary.Scaling;
using GenFx.ComponentModel;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Scaling.SigmaScalingStrategy and is intended
    ///to contain all GenFx.ComponentLibrary.Scaling.SigmaScalingStrategy Unit Tests
    ///</summary>
    [TestClass()]
    public class SigmaScalingStrategyTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Ctor()
        {
            int multiplier = 3;
            GeneticAlgorithm algorithm = GetAlgorithm(multiplier);
            SigmaScalingStrategy strategy = new SigmaScalingStrategy(algorithm);
            Assert.AreEqual(multiplier, strategy.Multiplier, "Multiplier not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SigmaScalingStrategy_Ctor_NullAlgorithm()
        {
            SigmaScalingStrategy op = new SigmaScalingStrategy(null);
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config is missing.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SigmaScalingStrategy_Ctor_MissingConfig()
        {
            SigmaScalingStrategy op = new SigmaScalingStrategy(new MockGeneticAlgorithm());
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the SigmaScalingMultiplier setting.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SigmaScalingStrategy_Ctor_InvalidSetting()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            SigmaScalingStrategyConfiguration config = new SigmaScalingStrategyConfiguration();
            config.Multiplier = -2;
            algorithm.ConfigurationSet.FitnessScalingStrategy = config;
            SigmaScalingStrategy op = new SigmaScalingStrategy(algorithm);
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Scale()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(5);
            SigmaScalingStrategy strategy = new SigmaScalingStrategy(algorithm);
            Population population = new Population(algorithm);
            PrivateObject populationAccessor = new PrivateObject(population);
            AddEntity(algorithm, 4, population);
            AddEntity(algorithm, 10, population);
            AddEntity(algorithm, 20, population);
            AddEntity(algorithm, 0, population);

            populationAccessor.SetField("rawMean", (double)(4 + 10 + 20) / 4);
            populationAccessor.SetField("rawStandardDeviation", populationAccessor.Invoke("GetStandardDeviation", population.RawMean, false));

            strategy.Scale(population);

            Assert.AreEqual(33.17, Math.Round(population.Entities[0].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
            Assert.AreEqual(39.17, Math.Round(population.Entities[1].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
            Assert.AreEqual(49.17, Math.Round(population.Entities[2].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
            Assert.AreEqual(29.17, Math.Round(population.Entities[3].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SigmaScalingStrategy_Scale_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(10);
            SigmaScalingStrategy op = new SigmaScalingStrategy(algorithm);
            Population population = new Population(algorithm);
            op.Scale(population);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SigmaScalingStrategy_Scale_NullPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(10);
            SigmaScalingStrategy op = new SigmaScalingStrategy(algorithm);
            op.Scale(null);
        }

        private void AddEntity(GeneticAlgorithm algorithm, double fitness, Population population)
        {
            MockEntity entity = new MockEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("rawFitnessValue", fitness);
            population.Entities.Add(entity);
        }

        private GeneticAlgorithm GetAlgorithm(int multiplier)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            SigmaScalingStrategyConfiguration config = new SigmaScalingStrategyConfiguration();
            config.Multiplier = multiplier;
            algorithm.ConfigurationSet.FitnessScalingStrategy = config;
            return algorithm;
        }
    }


}
