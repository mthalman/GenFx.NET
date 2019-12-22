using System;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    ///This is a test class for GenFx.Terminator and is intended
    ///to contain all GenFx.Terminator Unit Tests
    ///</summary>
    public class TerminatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void Terminator_Ctor()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                Terminator = new MockTerminator()
            };
            MockTerminator terminator = new MockTerminator();
            terminator.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(terminator, new PrivateType(typeof(Terminator)));
            Assert.Same(algorithm, accessor.GetProperty("Algorithm"));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void Terminator_Ctor_NullAlgorithm()
        {
            MockTerminator terminator = new MockTerminator();
            Assert.Throws<ArgumentNullException>(() => terminator.Initialize(null));
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
