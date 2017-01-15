using GenFx;
using GenFx.ComponentLibrary.Terminators;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Terminators.GenerationalTerminator and is intended
    /// to contain all GenFx.ComponentLibrary.Terminators.GenerationalTerminator Unit Tests
    /// </summary>
    [TestClass()]
    public class GenerationalTerminatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for a required setting.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_Ctor_InvalidSetting1()
        {
            GenerationalTerminator config = new GenerationalTerminator();
            AssertEx.Throws<ValidationException>(() => config.FinalGeneration = -1);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for a required setting.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_Ctor_InvalidSetting2()
        {
            GenerationalTerminator config = new GenerationalTerminator();
            AssertEx.Throws<ValidationException>(() => config.FinalGeneration = 0);
        }

        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_IsComplete()
        {
            int finalGeneration = 10;
            IGeneticAlgorithm algorithm = GetAlgorithm(finalGeneration);
            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            GenerationalTerminator terminator = (GenerationalTerminator)algorithm.Terminator;
            terminator.Initialize(algorithm);
            Assert.IsFalse(terminator.IsComplete(), "Should not be complete at generation 0.");
            accessor.SetField("currentGeneration", (int)accessor.GetField("currentGeneration") + 1);
            Assert.IsFalse(terminator.IsComplete(), "Should not be complete at generation 1.");
            accessor.SetField("currentGeneration", finalGeneration);
            Assert.IsTrue(terminator.IsComplete(), "Should be complete.");
        }

        private static IGeneticAlgorithm GetAlgorithm(int finalGeneration)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                Terminator = new GenerationalTerminator
                {
                    FinalGeneration = finalGeneration
                }
            };
            return algorithm;
        }
    }
}
