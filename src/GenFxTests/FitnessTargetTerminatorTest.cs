using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Terminators;
using GenFx.Contracts;
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
            Assert.IsInstanceOfType(terminator.Configuration, typeof(FitnessTargetTerminatorFactoryConfig));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        public void FitnessTargetTerminator_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new FitnessTargetTerminator(new MockGeneticAlgorithm(new ComponentFactoryConfigSet())));
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Terminator = new FitnessTargetTerminatorFactoryConfig
                {
                    FitnessTarget = fitnessTarget
                }
            });
            return algorithm;
        }
    }


}
