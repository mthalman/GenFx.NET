using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Scaling;
using GenFx.Validation;
using System;
using System.Runtime.Serialization;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Scaling.FitnessSharingScalingStrategy and is intended
    /// to contain all GenFx.ComponentLibrary.Scaling.FitnessSharingScalingStrategy Unit Tests
    /// </summary>
    public class FitnessSharingScalingStrategyTest
    {
        // <summary>
        /// Tests that an exception is thrown when an invalid value is used for the cutoff setting.
        /// </summary>
        [Fact]
        public void FitnessSharingScalingStrategy_Ctor_InvalidCutoffSetting()
        {
            FakeFitnessSharingScalingStrategy config = new FakeFitnessSharingScalingStrategy
            {
                ScalingCurvature = 3
            };
            Assert.Throws<ValidationException>(() => config.ScalingDistanceCutoff = 0);
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [Fact]
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

            // Change the population size to verify fitness distances are recalculated
            GeneticEntity entity7 = AddEntity(algorithm, population, 10);

            strategy.Scale(population);

            ValidateScale(entity1, 1.84);
            ValidateScale(entity2, 2.21);
            ValidateScale(entity3, 5.37);
            ValidateScale(entity4, 4.73);
            ValidateScale(entity5, 20);
            ValidateScale(entity6, 25);
            ValidateScale(entity7, 4.6);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void FitnessSharingScalingStrategy_Serialization()
        {
            FakeFitnessSharingScalingStrategy strategy = new FakeFitnessSharingScalingStrategy
            {
                ScalingCurvature = 10,
                ScalingDistanceCutoff = 5
            };

            FakeFitnessSharingScalingStrategy result = (FakeFitnessSharingScalingStrategy)SerializationHelper.TestSerialization(strategy, new Type[0]);

            Assert.Equal(strategy.ScalingCurvature, result.ScalingCurvature);
            Assert.Equal(strategy.ScalingDistanceCutoff, result.ScalingDistanceCutoff);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="FitnessSharingScalingStrategy.UpdateScaledFitnessValues"/>.
        /// </summary>
        [Fact]
        public void FitnessSharingScalingStrategy_UpdateScaledFitnessValues_NullPopulation()
        {
            FakeFitnessSharingScalingStrategy strategy = new FakeFitnessSharingScalingStrategy();
            PrivateObject accessor = new PrivateObject(strategy);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("UpdateScaledFitnessValues", (Population)null));
        }

        private static void ValidateScale(GeneticEntity entity, double expectedValue)
        {
            Assert.Equal(expectedValue, Math.Round(entity.ScaledFitnessValue, 2));
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
