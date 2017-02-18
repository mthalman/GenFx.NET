using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
using GenFx.Validation;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="SteadyStateGeneticAlgorithm"/> class.
    /// </summary>
    [TestClass]
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
            SteadyStateGeneticAlgorithm config = new SteadyStateGeneticAlgorithm();
            PopulationReplacementValue val = new PopulationReplacementValue(101, ReplacementValueKind.Percentage);
            AssertEx.Throws<ValidationException>(() => config.PopulationReplacementValue = val);
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [TestMethod]
        public async Task SteadyStateGeneticAlgorithm_CreateNextGeneration_FixedCount()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationReplacementValue = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };

            algorithm.SelectionOperator = new MockSelectionOperator { SelectionBasedOnFitnessType = FitnessType.Scaled };
            algorithm.SelectionOperator.Initialize(algorithm);
            algorithm.CrossoverOperator = new MockCrossoverOperator { CrossoverRate = 1 };
            algorithm.CrossoverOperator.Initialize(algorithm);
            algorithm.MutationOperator = new MockMutationOperator { MutationRate = 1 };
            algorithm.MutationOperator.Initialize(algorithm);
            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);

            PrivateObject ssAccessor = new PrivateObject(algorithm);
            SimplePopulation population = GetPopulation(algorithm, 3);

            int prevPopCount = population.Entities.Count;
            await (Task)ssAccessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(1, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(1, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(2, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [TestMethod]
        public async Task SteadyStateGeneticAlgorithm_CreateNextGeneration_Percentage()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationReplacementValue = new PopulationReplacementValue(20, ReplacementValueKind.Percentage),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };

            algorithm.SelectionOperator = new MockSelectionOperator { SelectionBasedOnFitnessType = FitnessType.Scaled };
            algorithm.SelectionOperator.Initialize(algorithm);
            algorithm.CrossoverOperator = new MockCrossoverOperator { CrossoverRate = 1 };
            algorithm.CrossoverOperator.Initialize(algorithm);
            algorithm.MutationOperator = new MockMutationOperator { MutationRate = 1 };
            algorithm.MutationOperator.Initialize(algorithm);
            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);

            PrivateObject ssAccessor = new PrivateObject(algorithm);
            SimplePopulation population = GetPopulation(algorithm, 10);

            int prevPopCount = population.Entities.Count;
            await (Task)ssAccessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(1, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(1, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(2, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [TestMethod]
        public async Task SteadyStateGeneticAlgorithm_CreateNextGeneration_NullPopulation()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm();
            PrivateObject accessor = new PrivateObject(algorithm);
            await AssertEx.ThrowsAsync<ArgumentNullException>(() => (Task)accessor.Invoke("CreateNextGenerationAsync", (Population)null));
        }
        
        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void SteadyStateGeneticAlgorithm_Serialization()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm();
            algorithm.PopulationReplacementValue = new PopulationReplacementValue(3, ReplacementValueKind.Percentage);

            SteadyStateGeneticAlgorithm result = (SteadyStateGeneticAlgorithm)SerializationHelper.TestSerialization(algorithm, new Type[0]);

            Assert.AreEqual(algorithm.PopulationReplacementValue.Kind, result.PopulationReplacementValue.Kind);
            Assert.AreEqual(algorithm.PopulationReplacementValue.Value, result.PopulationReplacementValue.Value);
        }

        private static SimplePopulation GetPopulation(GeneticAlgorithm algorithm, int populationSize)
        {
            SimplePopulation population = new SimplePopulation { MinimumPopulationSize = populationSize };
            population.Initialize(algorithm);

            for (int i = 0; i < population.MinimumPopulationSize; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            
            return population;
        }
    }


}
