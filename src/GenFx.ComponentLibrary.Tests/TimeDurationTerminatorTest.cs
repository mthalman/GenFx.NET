using GenFx.ComponentLibrary.Terminators;
using System;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Terminators.TimeDurationTerminator and is intended
    ///to contain all GenFx.ComponentLibrary.Terminators.TimeDurationTerminator Unit Tests
    ///</summary>
    public class TimeDurationTerminatorTest
    {
        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [Fact]
        public void TimeDurationTerminator_IsComplete()
        {
            TimeSpan timeLimit = new TimeSpan(0, 1, 0);
            GeneticAlgorithm algorithm = GetAlgorithm(timeLimit);

            TimeDurationTerminator terminator = new TimeDurationTerminator { TimeLimit = timeLimit };
            terminator.Initialize(algorithm);

            // "Start" the algorithm to trigger the start time
            PrivateObject algorithmAccessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            algorithmAccessor.Invoke("OnAlgorithmStarting");

            Assert.False(terminator.IsComplete(), "Time limit has not been reached.");

            // Make the start time earlier than it really was to simulate passed time.
            PrivateObject accessor = new PrivateObject(terminator);
            accessor.SetField("timeStarted", DateTime.Now - new TimeSpan(0, 1, 1));

            Assert.True(terminator.IsComplete(), "Time limit has been reached.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void TimeDurationTerminator_Serialization()
        {
            TimeDurationTerminator terminator = new TimeDurationTerminator
            {
                TimeLimit = new TimeSpan(123)
            };

            PrivateObject privObj = new PrivateObject(terminator);
            DateTime now = DateTime.Now;
            privObj.SetField("timeStarted", now);

            TimeDurationTerminator result = (TimeDurationTerminator)SerializationHelper.TestSerialization(terminator, new Type[0]);

            Assert.Equal(terminator.TimeLimit, result.TimeLimit);
            PrivateObject resultPrivObj = new PrivateObject(result);
            Assert.Equal(now, resultPrivObj.GetField("timeStarted"));
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
