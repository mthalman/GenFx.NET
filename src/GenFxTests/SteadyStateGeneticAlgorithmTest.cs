using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentModel;
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
        /// Tests that the PopulationReplacementValue property works correctly.
        /// </summary>
        [TestMethod]
        public void SteadyStateGeneticAlgorithm_PopulationReplacementValue()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm();
            SteadyStateGeneticAlgorithmConfiguration config = new SteadyStateGeneticAlgorithmConfiguration();
            config.PopulationReplacementValue = new PopulationReplacementValue(20, ReplacementValueKind.FixedCount);
            algorithm.ConfigurationSet.GeneticAlgorithm = config;

            Assert.AreEqual(20, algorithm.PopulationReplacementValue.Value, "PopulationReplacementValue set incorrectly.");
            Assert.AreEqual(ReplacementValueKind.FixedCount, algorithm.PopulationReplacementValue.Kind, "ReplacementValueKind set incorrectly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the PopulationReplacement setting.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SteadyStateGeneticAlgorithm_Initialize_InvalidPopulationReplacement()
        {
            SteadyStateGeneticAlgorithmConfiguration config = new SteadyStateGeneticAlgorithmConfiguration();
            config.PopulationReplacementValue = new PopulationReplacementValue(-1, ReplacementValueKind.FixedCount);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the PopulationReplacement setting.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SteadyStateGeneticAlgorithm_Initialize_InvalidPopulationReplacement2()
        {
            SteadyStateGeneticAlgorithmConfiguration config = new SteadyStateGeneticAlgorithmConfiguration();
            config.PopulationReplacementValue = new PopulationReplacementValue(101, ReplacementValueKind.Percentage);
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [TestMethod]
        public async Task SteadyStateGeneticAlgorithm_CreateNextGeneration_Async()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            SteadyStateGeneticAlgorithmConfiguration algConfig = new SteadyStateGeneticAlgorithmConfiguration();
            algConfig.PopulationReplacementValue = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            algorithm.ConfigurationSet.GeneticAlgorithm = algConfig;

            MockSelectionOperatorConfiguration selConfig = new MockSelectionOperatorConfiguration();
            selConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = selConfig;

            MockCrossoverOperatorConfiguration crossConfig = new MockCrossoverOperatorConfiguration();
            crossConfig.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = crossConfig;

            MockMutationOperatorConfiguration mutConfig = new MockMutationOperatorConfiguration();
            mutConfig.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = mutConfig;

            algorithm.Operators.SelectionOperator = new MockSelectionOperator(algorithm);
            algorithm.Operators.CrossoverOperator = new MockCrossoverOperator(algorithm);
            algorithm.Operators.MutationOperator = new MockMutationOperator(algorithm);
            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);

            PrivateObject ssAccessor = new PrivateObject(algorithm);
            Population population = GetPopulation(algorithm);

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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PopulationReplacementValue_InvalidValue()
        {
            PopulationReplacementValue val = new PopulationReplacementValue(-1, ReplacementValueKind.FixedCount);
        }

        private static Population GetPopulation(GeneticAlgorithm algorithm)
        {
            Population population = new Population(algorithm);
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            population.Entities.Add(new MockEntity(algorithm));
            return population;
        }
    }


}
