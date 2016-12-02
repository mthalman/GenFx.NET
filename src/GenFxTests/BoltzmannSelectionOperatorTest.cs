using System;
using GenFx;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFxTests.Mocks;

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
            GeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(algorithm);
            Assert.AreEqual(initialTemp, op.GetTemp(), "Initial temperature was not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor throws an exception when the genetic algorithm is missing a setting.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BoltzmannSelectionOperator_Ctor_MissingSetting()
        {
            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(new MockGeneticAlgorithm());
        }

        /// <summary>
        /// Tests that the Select method correctly returns a GeneticEntity.
        /// </summary>
        [TestMethod]
        public void BoltzmannSelectionOperator_Select()
        {
            double initialTemp = 10;
            GeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);

            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(algorithm);
            MockPopulation population = new MockPopulation(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            GeneticEntity entity = op.Select(population);
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
                algorithm.RaiseFitnessEvaluatedEvent();
                Assert.IsTrue(op.GetTemp().Equals(currentTemp + 1), "Loop index {0}: Temperature was not adjusted correctly.", i);
                currentTemp++;
            }
        }

        /// <summary>
        /// Tests that an exception is thrown when the calculation has an overflow.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void BoltzmannSelectionOperator_Select_Overflow()
        {
            double initialTemp = .0000001;
            MockGeneticAlgorithm algorithm = GetMockAlgorithm(initialTemp);
            FakeBoltzmannSelectionOperator op = new FakeBoltzmannSelectionOperator(algorithm);
            MockPopulation population = new MockPopulation(algorithm);
            MockEntity entity = new MockEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("scaledFitnessValue", 1);
            population.Entities.Add(entity);
            op.Select(population);
        }

        private static MockGeneticAlgorithm GetMockAlgorithm(double initialTemp)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new MockPopulationConfiguration();
            FakeBoltzmannSelectionOperatorConfiguration config = new FakeBoltzmannSelectionOperatorConfiguration();
            config.SelectionBasedOnFitnessType = FitnessType.Scaled;
            config.Temperature = initialTemp;
            algorithm.ConfigurationSet.SelectionOperator = config;
            return algorithm;
        }

        private class FakeBoltzmannSelectionOperator : BoltzmannSelectionOperator
        {
            public FakeBoltzmannSelectionOperator(GeneticAlgorithm algorithm)
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

        [Component(typeof(FakeBoltzmannSelectionOperator))]
        private class FakeBoltzmannSelectionOperatorConfiguration : BoltzmannSelectionOperatorConfiguration
        {
        }
    }
}
