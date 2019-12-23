using GenFx.Components.SelectionOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    ///This is a test class for GenFx.Components.SelectionOperators.BoltzmannSelectionOperator and is intended
    ///to contain all GenFx.Components.SelectionOperators.BoltzmannSelectionOperator Unit Tests
    ///</summary>
    public class BoltzmannSelectionOperatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void BoltzmannSelectionOperator_Ctor()
        {
            double initialTemp = 10;
            GeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator { InitialTemperature = initialTemp };
            op.Initialize(algorithm);
            Assert.Equal(initialTemp, op.GetTemp());
        }
        
        /// <summary>
        /// Tests that the Select method correctly returns a GeneticEntity.
        /// </summary>
        [Fact]
        public void BoltzmannSelectionOperator_Select()
        {
            double initialTemp = 10;
            GeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = (FakeBoltzmannSelectionOperator)algorithm.SelectionOperator;
            op.Initialize(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);

            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            population.Entities.Add(entity1);

            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            population.Entities.Add(entity2);

            IEnumerable<GeneticEntity> entities = op.SelectEntities(2, population);
            Assert.NotNull(entities);
            Assert.True(entities.Count() > 0, "An entity should have been selected.");
        }

        /// <summary>
        /// Tests that the temperature is adjusted correctly for each generation.
        /// </summary>
        [Fact]
        public void BoltzmannSelectionOperator_Temperature()
        {
            double initialTemp = 10;
            MockGeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = (FakeBoltzmannSelectionOperator)algorithm.SelectionOperator;
            op.Initialize(algorithm);

            double currentTemp = initialTemp;

            for (int i = 0; i < 10; i++)
            {
                algorithm.RaiseGenerationCreatedEvent();
                Assert.Equal(op.GetTemp(), currentTemp + 1);
                currentTemp++;
            }
        }

        /// <summary>
        /// Tests that an exception is thrown when the calculation has an overflow.
        /// </summary>
        [Fact]
        public void BoltzmannSelectionOperator_Select_Overflow()
        {
            double initialTemp = .0000001;
            MockGeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);
            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator();
            op.Initialize(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            entity.ScaledFitnessValue = 1;
            population.Entities.Add(entity);
            Assert.Throws<OverflowException>(() => op.SelectEntities(1, population));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void BoltzmannSelectionOperator_Serialization()
        {
            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator
            {
                InitialTemperature = 1,
                CurrentTemperature = 2
            };

            FakeBoltzmannSelectionOperator result = (FakeBoltzmannSelectionOperator)SerializationHelper.TestSerialization(op, new Type[0]);

            Assert.Equal(1, result.InitialTemperature);
            Assert.Equal(2, result.CurrentTemperature);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="BoltzmannSelectionOperator.SelectEntitiesFromPopulation"/>.
        /// </summary>
        [Fact]
        public void BoltzmannSelectionOperator_SelectEntitiesFromPopulation_NullPopulation()
        {
            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator();
            PrivateObject accessor = new PrivateObject(op, new PrivateType(typeof(BoltzmannSelectionOperator)));
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("SelectEntitiesFromPopulation", (int)0, (Population)null));
        }

        private static MockGeneticAlgorithm GetMockAlgorithm(double initialTemp)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new FakeBoltzmannSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled,
                    InitialTemperature = initialTemp
                }
            };
            return algorithm;
        }
        
        [DataContract]
        private class FakeBoltzmannSelectionOperator : BoltzmannSelectionOperator
        {
            public override void AdjustTemperature()
            {
                this.CurrentTemperature++;
            }

            public double GetTemp()
            {
                return this.CurrentTemperature;
            }
        }
    }
}
