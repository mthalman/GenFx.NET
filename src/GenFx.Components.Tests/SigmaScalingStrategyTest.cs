using GenFx.Components.Populations;
using GenFx.Components.Scaling;
using GenFx.Validation;
using System;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    ///This is a test class for GenFx.Components.Scaling.SigmaScalingStrategy and is intended
    ///to contain all GenFx.Components.Scaling.SigmaScalingStrategy Unit Tests
    ///</summary>
    public class SigmaScalingStrategyTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_Ctor_NullAlgorithm()
        {
            SigmaScalingStrategy strategy = new SigmaScalingStrategy();
            Assert.Throws<ArgumentNullException>(() => strategy.Initialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the SigmaScalingMultiplier setting.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_Ctor_InvalidSetting()
        {
            SigmaScalingStrategy config = new SigmaScalingStrategy();
            Assert.Throws<ValidationException>(() => config.Multiplier = -2);
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_Scale_Multiplier5()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(5);
            SigmaScalingStrategy strategy = new SigmaScalingStrategy { Multiplier = 5 };
            strategy.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            AddEntity(algorithm, 4, population);
            AddEntity(algorithm, 10, population);
            AddEntity(algorithm, 20, population);
            AddEntity(algorithm, 0, population);

            populationAccessor.SetField("rawMean", (double)(4 + 10 + 20) / 4);
            populationAccessor.SetField("rawStandardDeviation", MathHelper.GetStandardDeviation(population.Entities, population.RawMean.Value, FitnessType.Raw));

            strategy.Scale(population);

            Assert.Equal(33.17, Math.Round(population.Entities[0].ScaledFitnessValue, 2));
            Assert.Equal(39.17, Math.Round(population.Entities[1].ScaledFitnessValue, 2));
            Assert.Equal(49.17, Math.Round(population.Entities[2].ScaledFitnessValue, 2));
            Assert.Equal(29.17, Math.Round(population.Entities[3].ScaledFitnessValue, 2));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_Scale_Multiplier1()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(5);
            SigmaScalingStrategy strategy = new SigmaScalingStrategy { Multiplier = 1 };
            strategy.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            AddEntity(algorithm, 4, population);
            AddEntity(algorithm, 10, population);
            AddEntity(algorithm, 20, population);
            AddEntity(algorithm, 0, population);

            populationAccessor.SetField("rawMean", (double)(4 + 10 + 20) / 4);
            populationAccessor.SetField("rawStandardDeviation", MathHelper.GetStandardDeviation(population.Entities, population.RawMean.Value, FitnessType.Raw));

            strategy.Scale(population);

            Assert.Equal(3.03, Math.Round(population.Entities[0].ScaledFitnessValue, 2));
            Assert.Equal(9.03, Math.Round(population.Entities[1].ScaledFitnessValue, 2));
            Assert.Equal(19.03, Math.Round(population.Entities[2].ScaledFitnessValue, 2));
            Assert.Equal(0, Math.Round(population.Entities[3].ScaledFitnessValue, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_Scale_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(10);
            SigmaScalingStrategy op = new SigmaScalingStrategy();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            Assert.Throws<ArgumentException>(() => op.Scale(population));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_Scale_NullPopulation()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(10);
            SigmaScalingStrategy op = new SigmaScalingStrategy();
            op.Initialize(algorithm);
            Assert.Throws<ArgumentNullException>(() => op.Scale(null));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_Serialization()
        {
            SigmaScalingStrategy strategy = new SigmaScalingStrategy
            {
                Multiplier = 11
            };

            SigmaScalingStrategy result = (SigmaScalingStrategy)SerializationHelper.TestSerialization(strategy, new Type[0]);

            Assert.Equal(strategy.Multiplier, result.Multiplier);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="SigmaScalingStrategy.UpdateScaledFitnessValues"/>.
        /// </summary>
        [Fact]
        public void SigmaScalingStrategy_UpdateScaledFitnessValues_NullPopulation()
        {
            SigmaScalingStrategy strategy = new SigmaScalingStrategy();
            PrivateObject accessor = new PrivateObject(strategy);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("UpdateScaledFitnessValues", (Population)null));
        }

        private void AddEntity(GeneticAlgorithm algorithm, double fitness, Population population)
        {
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("rawFitnessValue", fitness);
            population.Entities.Add(entity);
        }

        private GeneticAlgorithm GetAlgorithm(int multiplier)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessScalingStrategy = new SigmaScalingStrategy
                {
                    Multiplier = multiplier
                }
            };
            return algorithm;
        }
    }


}
