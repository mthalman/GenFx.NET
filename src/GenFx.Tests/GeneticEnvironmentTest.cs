using System;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// This is a test class for GenFx.GeneticEnvironment and is intended
    /// to contain all GenFx.GeneticEnvironment Unit Tests
    ///</summary>
    public class GeneticEnvironmentTest
    {
        /// <summary>
        /// Tests that EvaluateFitness works correctly.
        /// </summary>
        [Fact]
        public async Task GeneticEnvironment_EvaluateFitness_Async()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };
            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);
            algorithm.SelectionOperator = new MockSelectionOperator();
            algorithm.SelectionOperator.Initialize(algorithm);

            GeneticEnvironment environment = new GeneticEnvironment(algorithm);

            MockPopulation population1 = GetPopulation(algorithm);
            MockPopulation population2 = GetPopulation(algorithm);
            environment.Populations.Add(population1);
            environment.Populations.Add(population2);

            await environment.EvaluateFitnessAsync();
            VerifyFitnessEvaluation(population1);
            VerifyFitnessEvaluation(population2);
        }

        /// <summary>
        /// Tests that Generate works correctly.
        /// </summary>
        [Fact]
        public async Task GeneticEnvironment_Initialize_Async()
        {
            int environmentSize = 2;
            int populationSize = 5;

            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                MinimumEnvironmentSize = environmentSize,
                PopulationSeed = new MockPopulation
                {
                    MinimumPopulationSize = populationSize
                }
            };
            algorithm.GeneticEntitySeed.Initialize(algorithm);
            algorithm.PopulationSeed.Initialize(algorithm);

            GeneticEnvironment environment = new GeneticEnvironment(algorithm);

            await environment.InitializeAsync();

            Assert.Equal(environmentSize, environment.Populations.Count);

            for (int i = 0; i < environmentSize; i++)
            {
                Assert.Equal(i, environment.Populations[i].Index);
                Assert.IsType<MockPopulation>(environment.Populations[i]);
                Assert.Equal(populationSize, environment.Populations[i].Entities.Count);
            }
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void GeneticEnvironment_Serialization()
        {
            GeneticEnvironment environment = new GeneticEnvironment(new MockGeneticAlgorithm());
            environment.Populations.Add(new MockPopulation());

            GeneticEnvironment result = (GeneticEnvironment)SerializationHelper.TestSerialization(environment, new Type[]
            {
                typeof(MockGeneticAlgorithm),
                typeof(MockPopulation),
                typeof(DefaultTerminator)
            });

            Assert.IsType<MockPopulation>(result.Populations[0]);

            PrivateObject privObj = new PrivateObject(environment);
            Assert.IsType<MockGeneticAlgorithm>(privObj.GetField("algorithm"));
        }

        private static MockPopulation GetPopulation(GeneticAlgorithm algorithm)
        {
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.Identifier = "5";
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.Identifier = "2";
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            return population;
        }

        private static void VerifyFitnessEvaluation(MockPopulation population)
        {
            Assert.Equal("5", ((MockEntity)population.Entities[0]).Identifier);
            Assert.Equal("2", ((MockEntity)population.Entities[1]).Identifier);

            Assert.Equal((double)5, population.Entities[0].RawFitnessValue);
            Assert.Equal((double)2, population.Entities[1].RawFitnessValue);
        }
    }
}
