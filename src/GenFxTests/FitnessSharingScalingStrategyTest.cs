using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Scaling;
using GenFx.Validation;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Scaling.FitnessSharingScalingStrategy and is intended
    /// to contain all GenFx.ComponentLibrary.Scaling.FitnessSharingScalingStrategy Unit Tests
    /// </summary>
    [TestClass()]
    public class FitnessSharingScalingStrategyTest
    {
        // <summary>
        /// Tests that an exception is thrown when an invalid value is used for the cutoff setting.
        /// </summary>
        [TestMethod]
        public void FitnessSharingScalingStrategy_Ctor_InvalidCutoffSetting()
        {
            FakeFitnessSharingScalingStrategy config = new FakeFitnessSharingScalingStrategy();
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
            FakeFitnessSharingScalingStrategy strategy = (FakeFitnessSharingScalingStrategy)algorithm.FitnessScalingStrategy;
            strategy.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
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

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void Serialization()
        {
            FakeFitnessSharingScalingStrategy strategy = new FakeFitnessSharingScalingStrategy();
            strategy.ScalingCurvature = 10;
            strategy.ScalingDistanceCutoff = 5;

            FakeFitnessSharingScalingStrategy result = (FakeFitnessSharingScalingStrategy)SerializationHelper.TestSerialization(strategy, new Type[0]);

            Assert.AreEqual(strategy.ScalingCurvature, result.ScalingCurvature);
            Assert.AreEqual(strategy.ScalingDistanceCutoff, result.ScalingDistanceCutoff);
        }

        private static void ValidateScale(GeneticEntity entity, double expectedValue)
        {
            Assert.AreEqual(expectedValue, Math.Round(entity.ScaledFitnessValue, 2), "ScaledFitnessValue not scaled correctly.");
        }

        private static GeneticEntity AddEntity(GeneticAlgorithm algorithm, SimplePopulation population, double scaledFitnessValue)
        {
            GeneticEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            entity.ScaledFitnessValue = scaledFitnessValue;
            population.Entities.Add(entity);
            return entity;
        }

        private static GeneticAlgorithm GetAlgorithm(double scalingCurvature, double scalingDistance)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessScalingStrategy = new FakeFitnessSharingScalingStrategy
                {
                    ScalingCurvature = scalingCurvature,
                    ScalingDistanceCutoff = scalingDistance
                }
            };
            return algorithm;
        }

        [DataContract]
        private class FakeFitnessSharingScalingStrategy : FitnessSharingScalingStrategy
        {
            public override double EvaluateFitnessDistance(GeneticEntity entity1, GeneticEntity entity2)
            {
                return Math.Abs(entity1.ScaledFitnessValue - entity2.ScaledFitnessValue);
            }
        }
    }
}
