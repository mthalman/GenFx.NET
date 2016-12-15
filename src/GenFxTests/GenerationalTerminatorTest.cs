using GenFx;
using GenFx.ComponentLibrary.Terminators;
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
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_Ctor()
        {
            int finalGeneration = 12;
            GeneticAlgorithm algorithm = GetAlgorithm(finalGeneration);

            GenerationalTerminator terminator = new GenerationalTerminator(algorithm);
            Assert.AreEqual(finalGeneration, terminator.FinalGeneration, "FinalGeneration not initialized correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new GenerationalTerminator(new MockGeneticAlgorithm()));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for a required setting.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_Ctor_InvalidSetting1()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            GenerationalTerminatorConfiguration config = new GenerationalTerminatorConfiguration();
            AssertEx.Throws<ValidationException>(() => config.FinalGeneration = -1);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for a required setting.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_Ctor_InvalidSetting2()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            GenerationalTerminatorConfiguration config = new GenerationalTerminatorConfiguration();
            AssertEx.Throws<ValidationException>(() => config.FinalGeneration = 0);
        }

        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [TestMethod()]
        public void GenerationalTerminator_IsComplete()
        {
            int finalGeneration = 10;
            GeneticAlgorithm algorithm = GetAlgorithm(finalGeneration);
            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            GenerationalTerminator terminator = new GenerationalTerminator(algorithm);
            Assert.IsFalse(terminator.IsComplete(), "Should not be complete at generation 0.");
            accessor.SetField("currentGeneration", (int)accessor.GetField("currentGeneration") + 1);
            Assert.IsFalse(terminator.IsComplete(), "Should not be complete at generation 1.");
            accessor.SetField("currentGeneration", finalGeneration);
            Assert.IsTrue(terminator.IsComplete(), "Should be complete.");
        }

        private static GeneticAlgorithm GetAlgorithm(int finalGeneration)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            GenerationalTerminatorConfiguration config = new GenerationalTerminatorConfiguration();
            config.FinalGeneration = finalGeneration;
            algorithm.ConfigurationSet.Terminator = config;
            return algorithm;
        }

    }


}
