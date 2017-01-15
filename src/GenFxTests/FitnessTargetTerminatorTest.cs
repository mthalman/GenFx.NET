using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Terminators;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

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
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [TestMethod()]
        public async Task FitnessTargetTerminator_IsComplete()
        {
            double fitnessTarget = 15;
            IGeneticAlgorithm algorithm = GetAlgorithm(fitnessTarget);
            await algorithm.InitializeAsync();

            FitnessTargetTerminator terminator = (FitnessTargetTerminator)algorithm.Terminator;
            terminator.Initialize(algorithm);

            // Check with no populations
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            population.Entities.Add(entity);
            algorithm.Environment.Populations.Add(population);

            // Check with a population with one entity
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            entity.ScaledFitnessValue = 15;
            Assert.IsTrue(terminator.IsComplete(), "A entity does have the fitness target.");
        }

        private static IGeneticAlgorithm GetAlgorithm(double fitnessTarget)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new SimplePopulation(),
                GeneticEntitySeed = new MockEntity(),
                Terminator = new FitnessTargetTerminator
                {
                    FitnessTarget = fitnessTarget
                }
            };
            return algorithm;
        }
    }


}
