using GenFx.Components.Populations;
using GenFx.Components.Scaling;
using System;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// This is a test class for GenFx.Components.Scaling.ExponentialScalingStrategy and is intended
    /// to contain all GenFx.Components.Scaling.ExponentialScalingStrategy Unit Tests
    /// </summary>
    public class ExponentialScalingStrategyTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void ExponentialScalingStrategy_Ctor_NullAlgorithm()
        {
            ExponentialScalingStrategy strategy = new ExponentialScalingStrategy();
            Assert.Throws<ArgumentNullException>(() => strategy.Initialize(null));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [Fact]
        public void ExponentialScalingStrategy_Scale()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(2);

            ExponentialScalingStrategy target = (ExponentialScalingStrategy)algorithm.FitnessScalingStrategy;
            target.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            PrivateObject entity1Accessor = new PrivateObject(entity1, new PrivateType(typeof(GeneticEntity)));
            entity1Accessor.SetField("rawFitnessValue", 5);
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            PrivateObject entity2Accessor = new PrivateObject(entity2, new PrivateType(typeof(GeneticEntity)));
            entity2Accessor.SetField("rawFitnessValue", 7);
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            target.Scale(population);

            Assert.Equal((double)25, entity1.ScaledFitnessValue);
            Assert.Equal((double)49, entity2.ScaledFitnessValue);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void ExponentialScalingStrategy_Serialization()
        {
            ExponentialScalingStrategy strategy = new ExponentialScalingStrategy
            {
                ScalingPower = 5
            };

            ExponentialScalingStrategy result = (ExponentialScalingStrategy)SerializationHelper.TestSerialization(strategy, new Type[0]);

            Assert.Equal(5, result.ScalingPower);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="ExponentialScalingStrategy.UpdateScaledFitnessValues"/>.
        /// </summary>
        [Fact]
        public void ExponentialScalingStrategy_UpdateScaledFitnessValues_NullPopulation()
        {
            ExponentialScalingStrategy strategy = new ExponentialScalingStrategy();
            PrivateObject accessor = new PrivateObject(strategy);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("UpdateScaledFitnessValues", (Population)null));
        }

        private static GeneticAlgorithm GetAlgorithm(double scalingPower)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessScalingStrategy = new ExponentialScalingStrategy
                {
                    ScalingPower = scalingPower
                }
            };
            return algorithm;
        }
    }


}
