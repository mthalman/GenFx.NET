using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Terminator = new MockTerminatorFactoryConfig()
            });
            MockTerminator terminator = new MockTerminator(algorithm);
            PrivateObject accessor = new PrivateObject(terminator, new PrivateType(typeof(TerminatorBase<MockTerminator, MockTerminatorFactoryConfig>)));
            Assert.AreSame(algorithm, accessor.GetProperty("Algorithm"), "Algorithm not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Terminator_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new MockTerminator(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config class is missing.
        /// </summary>
        [TestMethod]
        public void Terminator_Ctor_MissingConfig()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Entity = new MockEntityFactoryConfig()
            });
            AssertEx.Throws<InvalidOperationException>(() => new TestTerminator(algorithm));
        }

        private class TestTerminator : TerminatorBase<TestTerminator, TestTerminatorFactoryConfig>
        {
            public TestTerminator(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {

            }
            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class TestTerminatorFactoryConfig : TerminatorFactoryConfigBase<TestTerminatorFactoryConfig, TestTerminator>
        {
        }
    }
}
