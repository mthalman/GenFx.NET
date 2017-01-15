using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Lists;
using GenFx.Contracts;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Lists.UniformIntegerMutationOperator and is intended
    ///to contain all GenFx.ComponentLibrary.Lists.UniformIntegerMutationOperator Unit Tests
    ///</summary>
    [TestClass]
    public class UniformIntegerMutationOperatorTest
    {
        /// <summary>
        /// Tests that the Mutate method works correctly.
        /// </summary>
        [TestMethod]
        public void UniformIntegerMutationOperatorTest_Mutate()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new FixedLengthIntegerListEntity
                {
                    FixedLength = 4,
                    MaxElementValue = 2,
                    MinElementValue = 1
                },
                MutationOperator = new UniformIntegerMutationOperator
                {
                    MutationRate = 1
                }
            };
            UniformIntegerMutationOperator op = new UniformIntegerMutationOperator { MutationRate = 1 };
            op.Initialize(algorithm);
            FixedLengthIntegerListEntity entity = new FixedLengthIntegerListEntity { FixedLength = 4, MaxElementValue = 2, MinElementValue = 1 };
            entity.Age = 10;
            entity.Initialize(algorithm);
            entity[0] = 1;
            entity[1] = 1;
            entity[2] = 2;
            entity[3] = 1;
            IGeneticEntity mutant = op.Mutate(entity);

            Assert.AreEqual("2, 2, 1, 2", mutant.Representation, "Mutation not called correctly.");
            Assert.AreEqual(0, mutant.Age, "Age should have been reset.");
        }
    }
}
