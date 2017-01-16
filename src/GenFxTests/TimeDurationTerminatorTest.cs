using GenFx;
using GenFx.ComponentLibrary.Terminators;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Terminators.TimeDurationTerminator and is intended
    ///to contain all GenFx.ComponentLibrary.Terminators.TimeDurationTerminator Unit Tests
    ///</summary>
    [TestClass]
    public class TimeDurationTerminatorTest
    {
        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [TestMethod]
        public void TimeDurationTerminator_IsComplete()
        {
            TimeSpan timeLimit = new TimeSpan(0, 1, 0);
            GeneticAlgorithm algorithm = GetAlgorithm(timeLimit);

            TimeDurationTerminator terminator = new TimeDurationTerminator { TimeLimit = timeLimit };
            terminator.Initialize(algorithm);

            // "Start" the algorithm to trigger the start time
            PrivateObject algorithmAccessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            algorithmAccessor.Invoke("OnAlgorithmStarting");

            Assert.IsFalse(terminator.IsComplete(), "Time limit has not been reached.");

            // Make the start time earlier than it really was to simulate passed time.
            PrivateObject accessor = new PrivateObject(terminator);
            accessor.SetField("timeStarted", DateTime.Now - new TimeSpan(0, 1, 1));

            Assert.IsTrue(terminator.IsComplete(), "Time limit has been reached.");
        }

        private static GeneticAlgorithm GetAlgorithm(TimeSpan timeLimit)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                Terminator = new TimeDurationTerminator
                {
                    TimeLimit = timeLimit
                }
            };
            return algorithm;
        }
    }
}
