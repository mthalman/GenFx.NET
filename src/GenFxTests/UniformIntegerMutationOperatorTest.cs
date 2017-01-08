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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new FixedLengthIntegerListEntityFactoryConfig
                {
                    Length = 4,
                    MaxElementValue = 2,
                    MinElementValue = 1
                },
                MutationOperator = new UniformIntegerMutationOperatorFactoryConfig
                {
                    MutationRate = 1
                }
            });
            UniformIntegerMutationOperator op = new UniformIntegerMutationOperator(algorithm);
            FixedLengthIntegerListEntity entity = new FixedLengthIntegerListEntity(algorithm);
            entity.Age = 10;
            entity.Initialize();
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
