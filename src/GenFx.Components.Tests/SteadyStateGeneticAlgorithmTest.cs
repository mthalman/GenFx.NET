using GenFx.Components.Algorithms;
using GenFx.Components.Populations;
using GenFx.Validation;
using System;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="SteadyStateGeneticAlgorithm"/> class.
    /// </summary>
    public class SteadyStateGeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the PopulationReplacement setting.
        /// </summary>
        [Fact]
        public void SteadyStateGeneticAlgorithm_Initialize_InvalidPopulationReplacement()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PopulationReplacementValue(-1, ReplacementValueKind.FixedCount));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the PopulationReplacement setting.
        /// </summary>
        [Fact]
        public void SteadyStateGeneticAlgorithm_Initialize_InvalidPopulationReplacement2()
        {
            SteadyStateGeneticAlgorithm config = new SteadyStateGeneticAlgorithm();
            PopulationReplacementValue val = new PopulationReplacementValue(101, ReplacementValueKind.Percentage);
            Assert.Throws<ValidationException>(() => config.PopulationReplacementValue = val);
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [Fact]
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

            await algorithm.InitializeAsync();

            PrivateObject ssAccessor = new PrivateObject(algorithm);
            SimplePopulation population = GetPopulation(algorithm, 3);

            int prevPopCount = population.Entities.Count;
            await (Task)ssAccessor.Invoke("CreateNextGenerationAsync", population);

            Assert.Equal(1, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount);
            Assert.Equal(1, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount);
            Assert.Equal(2, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount);
            Assert.Equal(prevPopCount, population.Entities.Count);
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [Fact]
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

            await algorithm.InitializeAsync();

            PrivateObject ssAccessor = new PrivateObject(algorithm);
            SimplePopulation population = GetPopulation(algorithm, 10);

            int prevPopCount = population.Entities.Count;
            await (Task)ssAccessor.Invoke("CreateNextGenerationAsync", population);

            Assert.Equal(1, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount);
            Assert.Equal(1, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount);
            Assert.Equal(2, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount);
            Assert.Equal(prevPopCount, population.Entities.Count);
        }

        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [Fact]
        public async Task SteadyStateGeneticAlgorithm_CreateNextGeneration_NullPopulation()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm();
            PrivateObject accessor = new PrivateObject(algorithm);
            await Assert.ThrowsAsync<ArgumentNullException>(() => (Task)accessor.Invoke("CreateNextGenerationAsync", (Population)null));
        }
        
        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void SteadyStateGeneticAlgorithm_Serialization()
        {
            SteadyStateGeneticAlgorithm algorithm = new SteadyStateGeneticAlgorithm
            {
                PopulationReplacementValue = new PopulationReplacementValue(3, ReplacementValueKind.Percentage)
            };

            SteadyStateGeneticAlgorithm result = (SteadyStateGeneticAlgorithm)SerializationHelper.TestSerialization(
                algorithm, new Type[] { typeof(DefaultTerminator) });

            Assert.Equal(algorithm.PopulationReplacementValue.Kind, result.PopulationReplacementValue.Kind);
            Assert.Equal(algorithm.PopulationReplacementValue.Value, result.PopulationReplacementValue.Value);
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
