using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Terminators;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Terminators.FitnessTargetTerminator and is intended
    ///to contain all GenFx.ComponentLibrary.Terminators.FitnessTargetTerminator Unit Tests
    ///</summary>
    [TestClass()]
    public class FitnessTargetTerminatorTest
    {

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void FitnessTargetTerminator_Ctor()
        {
            double fitnessTarget = 5;
            IGeneticAlgorithm algorithm = GetAlgorithm(fitnessTarget);

            FitnessTargetTerminator terminator = new FitnessTargetTerminator(algorithm);
            Assert.IsInstanceOfType(terminator.Configuration, typeof(FitnessTargetTerminatorConfiguration));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        public void FitnessTargetTerminator_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FitnessTargetTerminator(new MockGeneticAlgorithm(new ComponentConfigurationSet())));
        }

        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [TestMethod()]
        public void FitnessTargetTerminator_IsComplete()
        {
            double fitnessTarget = 15;
            IGeneticAlgorithm algorithm = GetAlgorithm(fitnessTarget);

            FitnessTargetTerminator terminator = new FitnessTargetTerminator(algorithm);

            // Check with no populations
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            MockEntity entity = new MockEntity(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            population.Entities.Add(entity);
            algorithm.Environment.Populations.Add(population);

            // Check with a population with one entity
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            entity.ScaledFitnessValue = 15;
            Assert.IsTrue(terminator.IsComplete(), "A entity does have the fitness target.");
        }

        private static IGeneticAlgorithm GetAlgorithm(double fitnessTarget)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Population = new SimplePopulationConfiguration(),
                Entity = new MockEntityConfiguration(),
                Terminator = new FitnessTargetTerminatorConfiguration
                {
                    FitnessTarget = fitnessTarget
                }
            });
            return algorithm;
        }
    }


}
