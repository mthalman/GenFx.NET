using GenFx;
using GenFx.ComponentModel;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.Terminator and is intended
    ///to contain all GenFx.Terminator Unit Tests
    ///</summary>
    [TestClass()]
    public class TerminatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void Terminator_Ctor()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Terminator = new MockTerminatorConfiguration();
            Terminator terminator = new MockTerminator(algorithm);
            PrivateObject accessor = new PrivateObject(terminator, new PrivateType(typeof(Terminator)));
            Assert.AreSame(algorithm, accessor.GetProperty("Algorithm"), "Algorithm not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Terminator_Ctor_NullAlgorithm()
        {
            Terminator terminator = new MockTerminator(null);
        }


        /// <summary>
        /// Tests that an exception is thrown when a required config class is missing.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Terminator_Ctor_MissingConfig()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            Terminator terminator = new TestTerminator(algorithm);
        }

        /// <summary>
        /// Tests that the IsComplete method works correctly.
        /// </summary>
        [TestMethod]
        public void EmptyTerminator_IsComplete()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Terminator = new EmptyTerminatorConfiguration();
            EmptyTerminator terminator = new EmptyTerminator(algorithm);
            Assert.IsFalse(terminator.IsComplete(), "Always returns false.");
        }

        private class TestTerminator : Terminator
        {
            public TestTerminator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {

            }
            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
