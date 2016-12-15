using GenFx;
using GenFx.ComponentLibrary.Scaling;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Scaling.ExponentialScalingStrategy and is intended
    /// to contain all GenFx.ComponentLibrary.Scaling.ExponentialScalingStrategy Unit Tests
    /// </summary>
    [TestClass()]
    public class ExponentialScalingStrategyTest
    {

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Ctor()
        {
            double scalingPower = 2;
            GeneticAlgorithm algorithm = GetAlgorithm(2);

            ExponentialScalingStrategy strategy = new ExponentialScalingStrategy(algorithm);

            Assert.AreEqual(scalingPower, strategy.ScalingPower, "Scaling power not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new ExponentialScalingStrategy(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new ExponentialScalingStrategy(new MockGeneticAlgorithm()));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Scale()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(2);

            ExponentialScalingStrategy target = new ExponentialScalingStrategy(algorithm);
            Population population = new Population(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            PrivateObject entity1Accessor = new PrivateObject(entity1, new PrivateType(typeof(GeneticEntity)));
            entity1Accessor.SetField("rawFitnessValue", 5);
            MockEntity entity2 = new MockEntity(algorithm);
            PrivateObject entity2Accessor = new PrivateObject(entity2, new PrivateType(typeof(GeneticEntity)));
            entity2Accessor.SetField("rawFitnessValue", 7);
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            target.Scale(population);

            Assert.AreEqual((double)25, entity1.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
            Assert.AreEqual((double)49, entity2.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
        }

        private static GeneticAlgorithm GetAlgorithm(double scalingPower)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            ExponentialScalingStrategyConfiguration config = new ExponentialScalingStrategyConfiguration();
            config.ScalingPower = scalingPower;
            algorithm.ConfigurationSet.FitnessScalingStrategy = config;
            return algorithm;
        }
    }


}
