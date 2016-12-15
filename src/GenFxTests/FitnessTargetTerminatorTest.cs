using GenFx;
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
            GeneticAlgorithm algorithm = GetAlgorithm(fitnessTarget);

            FitnessTargetTerminator terminator = new FitnessTargetTerminator(algorithm);
            Assert.AreEqual(fitnessTarget, terminator.FitnessTarget, "FitnessTarget was not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        public void FitnessTargetTerminator_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FitnessTargetTerminator(new MockGeneticAlgorithm()));
        }

        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [TestMethod()]
        public void FitnessTargetTerminator_IsComplete()
        {
            double fitnessTarget = 15;
            GeneticAlgorithm algorithm = GetAlgorithm(fitnessTarget);

            FitnessTargetTerminator terminator = new FitnessTargetTerminator(algorithm);

            // Check with no populations
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            MockEntity entity = new MockEntity(algorithm);
            Population population = new Population(algorithm);
            population.Entities.Add(entity);
            algorithm.Environment.Populations.Add(population);

            // Check with a population with one entity
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            entity.ScaledFitnessValue = 15;
            Assert.IsTrue(terminator.IsComplete(), "A entity does have the fitness target.");
        }

        private static GeneticAlgorithm GetAlgorithm(double fitnessTarget)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            FitnessTargetTerminatorConfiguration config = new FitnessTargetTerminatorConfiguration();
            config.FitnessTarget = fitnessTarget;
            algorithm.ConfigurationSet.Terminator = config;
            return algorithm;
        }
    }


}
