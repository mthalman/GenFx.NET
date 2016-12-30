using System;
using GenFx;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFxTests.Mocks;
using GenFxTests.Helpers;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.SelectionOperators.BoltzmannSelectionOperator and is intended
    ///to contain all GenFx.ComponentLibrary.SelectionOperators.BoltzmannSelectionOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class BoltzmannSelectionOperatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void BoltzmannSelectionOperator_Ctor()
        {
            double initialTemp = 10;
            IGeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(algorithm);
            Assert.AreEqual(initialTemp, op.GetTemp(), "Initial temperature was not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor throws an exception when the genetic algorithm is missing a setting.
        /// </summary>
        [TestMethod]
        public void BoltzmannSelectionOperator_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FakeBoltzmannSelectionOperator(new MockGeneticAlgorithm(new ComponentConfigurationSet())));
        }

        /// <summary>
        /// Tests that the Select method correctly returns a GeneticEntity.
        /// </summary>
        [TestMethod]
        public void BoltzmannSelectionOperator_Select()
        {
            double initialTemp = 10;
            IGeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(algorithm);
            MockPopulation population = new MockPopulation(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            IGeneticEntity entity = op.SelectEntity(population);
            Assert.IsNotNull(entity, "An entity should have been selected.");
        }

        /// <summary>
        /// Tests that the temperature is adjusted correctly for each generation.
        /// </summary>
        [TestMethod]
        public void BoltzmannSelectionOperator_Temperature()
        {
            double initialTemp = 10;
            MockGeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(algorithm);

            double currentTemp = initialTemp;

            for (int i = 0; i < 10; i++)
            {
                algorithm.RaiseGenerationCreatedEvent();
                Assert.AreEqual(op.GetTemp(), currentTemp + 1, "Loop index {0}: Temperature was not adjusted correctly.", i);
                currentTemp++;
            }
        }

        /// <summary>
        /// Tests that an exception is thrown when the calculation has an overflow.
        /// </summary>
        [TestMethod]
        public void BoltzmannSelectionOperator_Select_Overflow()
        {
            double initialTemp = .0000001;
            MockGeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);
            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(algorithm);
            MockPopulation population = new MockPopulation(algorithm);
            MockEntity entity = new MockEntity(algorithm);
            entity.ScaledFitnessValue = 1;
            population.Entities.Add(entity);
            AssertEx.Throws<OverflowException>(() => op.SelectEntity(population));
        }

        private static MockGeneticAlgorithm GetMockAlgorithm(double initialTemp)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new FakeBoltzmannSelectionOperatorConfiguration
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled,
                    InitialTemperature = initialTemp
                }
            });
            return algorithm;
        }

        private class FakeBoltzmannSelectionOperator : BoltzmannSelectionOperator<FakeBoltzmannSelectionOperator, FakeBoltzmannSelectionOperatorConfiguration>
        {
            public FakeBoltzmannSelectionOperator(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override void AdjustTemperature()
            {
                this.Temperature++;
            }

            public double GetTemp()
            {
                return this.Temperature;
            }
        }

        private class FakeBoltzmannSelectionOperatorConfiguration : BoltzmannSelectionOperatorConfiguration<FakeBoltzmannSelectionOperatorConfiguration, FakeBoltzmannSelectionOperator>
        {
        }
    }
}
