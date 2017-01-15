using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Scaling;
using GenFx.Contracts;
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
            IGeneticAlgorithm algorithm = GetAlgorithm(scalingCurvature, scalingDistance);
            FakeFitnessSharingScalingStrategy strategy = (FakeFitnessSharingScalingStrategy)algorithm.FitnessScalingStrategy;
            strategy.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            IGeneticEntity entity1 = AddEntity(algorithm, population, 5);
            IGeneticEntity entity2 = AddEntity(algorithm, population, 6);
            IGeneticEntity entity3 = AddEntity(algorithm, population, 9);
            IGeneticEntity entity4 = AddEntity(algorithm, population, 11.5);
            IGeneticEntity entity5 = AddEntity(algorithm, population, 20);
            IGeneticEntity entity6 = AddEntity(algorithm, population, 25);
            strategy.Scale(population);

            ValidateScale(entity1, 3.16);
            ValidateScale(entity2, 3.79);
            ValidateScale(entity3, 7.92);
            ValidateScale(entity4, 10.13);
            ValidateScale(entity5, 20);
            ValidateScale(entity6, 25);
        }

        private static void ValidateScale(IGeneticEntity entity, double expectedValue)
        {
            Assert.AreEqual(expectedValue, Math.Round(entity.ScaledFitnessValue, 2), "ScaledFitnessValue not scaled correctly.");
        }

        private static IGeneticEntity AddEntity(IGeneticAlgorithm algorithm, SimplePopulation population, double scaledFitnessValue)
        {
            IGeneticEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            entity.ScaledFitnessValue = scaledFitnessValue;
            population.Entities.Add(entity);
            return entity;
        }

        private static IGeneticAlgorithm GetAlgorithm(double scalingCurvature, double scalingDistance)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
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

        private class FakeFitnessSharingScalingStrategy : FitnessSharingScalingStrategy
        {
            public override double EvaluateFitnessDistance(IGeneticEntity entity1, IGeneticEntity entity2)
            {
                return Math.Abs(entity1.ScaledFitnessValue - entity2.ScaledFitnessValue);
            }
        }
    }
}
