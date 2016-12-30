using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Algorithms.SteadyStateGeneticAlgorithm and is intended
    ///to contain all GenFx.ComponentLibrary.Algorithms.SteadyStateGeneticAlgorithm Unit Tests
    ///</summary>
    [TestClass()]
    public class SteadyStateGeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the PopulationReplacement setting.
        /// </summary>
        [TestMethod]
        public void SteadyStateGeneticAlgorithm_Initialize_InvalidPopulationReplacement()
        {
            AssertEx.Throws<ArgumentOutOfRangeException>(() => new PopulationReplacementValue(-1, ReplacementValueKind.FixedCount));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the PopulationReplacement setting.
        /// </summary>
        [TestMethod]
        public void SteadyStateGeneticAlgorithm_Initialize_InvalidPopulationReplacement2()
        {
            SteadyStateGeneticAlgorithmConfiguration config = new SteadyStateGeneticAlgorithmConfiguration();
            PopulationReplacementValue val = new PopulationReplacementValue(101, ReplacementValueKind.Percentage);
            AssertEx.Throws<ValidationException>(() => config.PopulationReplacementValue = val);
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [TestMethod]
        public async Task SteadyStateGeneticAlgorithm_CreateNextGeneration_Async()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm(new ComponentConfigurationSet
            {
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                GeneticAlgorithm = new SteadyStateGeneticAlgorithmConfiguration
                {
                    PopulationReplacementValue = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount)
                },
                SelectionOperator = new MockSelectionOperatorConfiguration
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                CrossoverOperator = new MockCrossoverOperatorConfiguration
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperatorConfiguration
                {
                    MutationRate = 1
                }
            });

            algorithm.Operators.SelectionOperator = new MockSelectionOperator(algorithm);
            algorithm.Operators.CrossoverOperator = new MockCrossoverOperator(algorithm);
            algorithm.Operators.MutationOperator = new MockMutationOperator(algorithm);
            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);

            PrivateObject ssAccessor = new PrivateObject(algorithm);
            SimplePopulation population = GetPopulation(algorithm);

            int prevPopCount = population.Entities.Count;
            await (Task)ssAccessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(4, ((MockSelectionOperator)algorithm.Operators.SelectionOperator).DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(2, ((MockCrossoverOperator)algorithm.Operators.CrossoverOperator).DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(4, ((MockMutationOperator)algorithm.Operators.MutationOperator).DoMutateCallCount, "Mutation not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a negative value is passed to the PopulationReplacementValue.
        /// </summary>
        [TestMethod]
        public void PopulationReplacementValue_InvalidValue()
        {
            AssertEx.Throws<ArgumentOutOfRangeException>(() => new PopulationReplacementValue(-1, ReplacementValueKind.FixedCount));
        }

        private static SimplePopulation GetPopulation(IGeneticAlgorithm algorithm)
        {
            SimplePopulation population = new SimplePopulation(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            return population;
        }
    }


}
