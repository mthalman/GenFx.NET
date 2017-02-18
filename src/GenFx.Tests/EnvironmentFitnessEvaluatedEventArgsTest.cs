using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnvironmentFitnessEvaluatedEventArgs"/> class.
    /// </summary>
    [TestClass]
    public class EnvironmentFitnessEvaluatedEventArgsTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void EnvironmentFitnessEvaluatedEventArgs_Constructor()
        {
            GeneticEnvironment environment = new GeneticEnvironment(new MockGeneticAlgorithm());
            int generationIndex = 2;
            EnvironmentFitnessEvaluatedEventArgs args = new EnvironmentFitnessEvaluatedEventArgs(
                environment, generationIndex);

            Assert.AreSame(environment, args.Environment);
            Assert.AreEqual(generationIndex, args.GenerationIndex);
        }

        /// <summary>
        /// Tests that an exception is thrown in an invalid generation index is passed.
        /// </summary>
        [TestMethod]
        public void EnvironmentFitnessEvaluatedEventArgs_InvalidGenerationIndex()
        {
            GeneticEnvironment environment = new GeneticEnvironment(new MockGeneticAlgorithm());
            int generationIndex = -1;
            AssertEx.Throws<ArgumentException>(() => new EnvironmentFitnessEvaluatedEventArgs(
                environment, generationIndex));
        }

        /// <summary>
        /// Tests that an exception is thrown in an invalid generation index is passed.
        /// </summary>
        [TestMethod]
        public void EnvironmentFitnessEvaluatedEventArgs_NullEnvironment()
        {
            AssertEx.Throws<ArgumentNullException>(() => new EnvironmentFitnessEvaluatedEventArgs(
                null, 0));
        }
    }
}
