using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnvironmentFitnessEvaluatedEventArgs"/> class.
    /// </summary>
    public class EnvironmentFitnessEvaluatedEventArgsTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void EnvironmentFitnessEvaluatedEventArgs_Constructor()
        {
            GeneticEnvironment environment = new GeneticEnvironment(new MockGeneticAlgorithm());
            int generationIndex = 2;
            EnvironmentFitnessEvaluatedEventArgs args = new EnvironmentFitnessEvaluatedEventArgs(
                environment, generationIndex);

            Assert.Same(environment, args.Environment);
            Assert.Equal(generationIndex, args.GenerationIndex);
        }

        /// <summary>
        /// Tests that an exception is thrown in an invalid generation index is passed.
        /// </summary>
        [Fact]
        public void EnvironmentFitnessEvaluatedEventArgs_InvalidGenerationIndex()
        {
            GeneticEnvironment environment = new GeneticEnvironment(new MockGeneticAlgorithm());
            int generationIndex = -1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new EnvironmentFitnessEvaluatedEventArgs(
                environment, generationIndex));
        }

        /// <summary>
        /// Tests that an exception is thrown in an invalid generation index is passed.
        /// </summary>
        [Fact]
        public void EnvironmentFitnessEvaluatedEventArgs_NullEnvironment()
        {
            Assert.Throws<ArgumentNullException>(() => new EnvironmentFitnessEvaluatedEventArgs(
                null, 0));
        }
    }
}
