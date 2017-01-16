using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFxTests.Helpers;
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                Terminator = new MockTerminator()
            };
            MockTerminator terminator = new MockTerminator();
            terminator.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(terminator, new PrivateType(typeof(Terminator)));
            Assert.AreSame(algorithm, accessor.GetProperty("Algorithm"), "Algorithm not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Terminator_Ctor_NullAlgorithm()
        {
            MockTerminator terminator = new MockTerminator();
            AssertEx.Throws<ArgumentNullException>(() => terminator.Initialize(null));
        }

        private class TestTerminator : Terminator
        {
            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
