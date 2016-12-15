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
    /// This is a test class for GenFx.ComponentLibrary.Scaling.FitnessSharingScalingStrategy and is intended
    /// to contain all GenFx.ComponentLibrary.Scaling.FitnessSharingScalingStrategy Unit Tests
    /// </summary>
    [TestClass()]
    public class FitnessSharingScalingStrategyTest
    {

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void FitnessSharingScalingStrategy_Ctor()
        {
            double scalingCurvature = .8;
            double scalingDistance = 1.5;
            GeneticAlgorithm algorithm = GetAlgorithm(scalingCurvature, scalingDistance);
            FitnessSharingScalingStrategy strategy = new FakeFitnessSharingScalingStrategy(algorithm);
            Assert.IsTrue(scalingCurvature.Equals(strategy.ScalingCurvature), "ScalingCurvature not initialized correctly.");
            Assert.IsTrue(scalingDistance.Equals(strategy.ScalingDistanceCutoff), "ScalingDistanceCutoff not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when required settings are missing.
        /// </summary>
        [TestMethod]
        public void FitnessSharingScalingStrategy_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FakeFitnessSharingScalingStrategy(new MockGeneticAlgorithm()));
        }

        // <summary>
        /// Tests that an exception is thrown when an invalid value is used for the cutoff setting.
        /// </summary>
        [TestMethod]
        public void FitnessSharingScalingStrategy_Ctor_InvalidCutoffSetting()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            FakeFitnessSharingScalingStrategyConfiguration config = new FakeFitnessSharingScalingStrategyConfiguration();
            config.ScalingCurvature = 3;
            AssertEx.Throws<ValidationException>(() => config.ScalingDistanceCutoff = 0);
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod]
        public void FitnessSharingScalingStrategy_Scale()
        {
            double scalingCurvature = .8;
            double scalingDistance = 3;
            GeneticAlgorithm algorithm = GetAlgorithm(scalingCurvature, scalingDistance);
            FitnessSharingScalingStrategy strategy = new FakeFitnessSharingScalingStrategy(algorithm);
            Population population = new Population(algorithm);
            GeneticEntity entity1 = AddEntity(algorithm, population, 5);
            GeneticEntity entity2 = AddEntity(algorithm, population, 6);
            GeneticEntity entity3 = AddEntity(algorithm, population, 9);
            GeneticEntity entity4 = AddEntity(algorithm, population, 11.5);
            GeneticEntity entity5 = AddEntity(algorithm, population, 20);
            GeneticEntity entity6 = AddEntity(algorithm, population, 25);
            strategy.Scale(population);

            ValidateScale(entity1, 3.16);
            ValidateScale(entity2, 3.79);
            ValidateScale(entity3, 7.92);
            ValidateScale(entity4, 10.13);
            ValidateScale(entity5, 20);
            ValidateScale(entity6, 25);
        }

        private static void ValidateScale(GeneticEntity entity, double expectedValue)
        {
            Assert.AreEqual(expectedValue, Math.Round(entity.ScaledFitnessValue, 2), "ScaledFitnessValue not scaled correctly.");
        }

        private static GeneticEntity AddEntity(GeneticAlgorithm algorithm, Population population, double scaledFitnessValue)
        {
            GeneticEntity entity = new MockEntity(algorithm);
            entity.ScaledFitnessValue = scaledFitnessValue;
            population.Entities.Add(entity);
            return entity;
        }

        private static GeneticAlgorithm GetAlgorithm(double scalingCurvature, double scalingDistance)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            FakeFitnessSharingScalingStrategyConfiguration config = new FakeFitnessSharingScalingStrategyConfiguration();
            config.ScalingCurvature = scalingCurvature;
            config.ScalingDistanceCutoff = scalingDistance;
            algorithm.ConfigurationSet.FitnessScalingStrategy = config;
            return algorithm;
        }

        private class FakeFitnessSharingScalingStrategy : FitnessSharingScalingStrategy
        {
            public FakeFitnessSharingScalingStrategy(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override double EvaluateFitnessDistance(GeneticEntity entity1, GeneticEntity entity2)
            {
                return Math.Abs(entity1.ScaledFitnessValue - entity2.ScaledFitnessValue);
            }
        }

        [Component(typeof(FakeFitnessSharingScalingStrategy))]
        private class FakeFitnessSharingScalingStrategyConfiguration : FitnessSharingScalingStrategyConfiguration
        {
        }
    }
}
