using GenFx.Components.Algorithms;
using GenFx.Components.Populations;
using System;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// This is a test class for GenFx.Components.Algorithms.MultiDemeGeneticAlgorithm and is intended
    /// to contain all GenFx.Components.Algorithms.MultiDemeGeneticAlgorithm Unit Tests
    /// </summary>
    public class MultiDemeGeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [Fact]
        public async Task MultiDemeGeneticAlgorithm_CreateNextGeneration_Async()
        {
            TestMultiDemeGeneticAlgorithm algorithm = new TestMultiDemeGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation
                {
                    MinimumPopulationSize = 3
                },
                MigrantCount = 1,
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Raw
                }
            };
            MockSelectionOperator selectionOp = new MockSelectionOperator();
            algorithm.SelectionOperator = selectionOp;
            await algorithm.InitializeAsync();

            PrivateObject accessor = new PrivateObject(algorithm);

            algorithm.Environment.Populations.Add(GetPopulation(algorithm));
            algorithm.Environment.Populations.Add(GetPopulation(algorithm));
            SimplePopulation population = GetPopulation(algorithm);
            algorithm.Environment.Populations.Add(population);

            int prevPopCount = population.Entities.Count;
            await (Task)accessor.Invoke("CreateNextGenerationAsync", population);

            Assert.Equal(1, selectionOp.DoSelectCallCount);
            Assert.Equal(prevPopCount, population.Entities.Count);
        }

        /// <summary>
        /// Tests that the Migrate method works correctly.
        /// </summary>
        [Fact]
        public async Task MultiDemeGeneticAlgorithm_Migrate()
        {
            MultiDemeGeneticAlgorithm algorithm = new MultiDemeGeneticAlgorithm
            {
                MinimumEnvironmentSize = 3,
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation
                {
                    MinimumPopulationSize = 4
                },
                FitnessEvaluator = new MockFitnessEvaluator(),
                MigrantCount = 2,
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
            };

            algorithm.SelectionOperator = new MockSelectionOperator();
            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            await algorithm.InitializeAsync();

            SimplePopulation population1 = (SimplePopulation)algorithm.Environment.Populations[0];
            population1.Entities[0].ScaledFitnessValue = 1;
            population1.Entities[1].ScaledFitnessValue = 5;
            population1.Entities[2].ScaledFitnessValue = 2;
            population1.Entities[3].ScaledFitnessValue = 4;

            SimplePopulation population2 = (SimplePopulation)algorithm.Environment.Populations[1];
            population2.Entities[0].ScaledFitnessValue = 6;
            population2.Entities[1].ScaledFitnessValue = 3;
            population2.Entities[2].ScaledFitnessValue = 8;
            population2.Entities[3].ScaledFitnessValue = 7;

            SimplePopulation population3 = (SimplePopulation)algorithm.Environment.Populations[2];
            population3.Entities[0].ScaledFitnessValue = 9;
            population3.Entities[1].ScaledFitnessValue = 13;
            population3.Entities[2].ScaledFitnessValue = 10;
            population3.Entities[3].ScaledFitnessValue = 12;

            algorithm.Migrate();

            Assert.Equal((double)1, population1.Entities[0].ScaledFitnessValue);
            Assert.Equal((double)2, population1.Entities[1].ScaledFitnessValue);
            Assert.Equal((double)13, population1.Entities[2].ScaledFitnessValue);
            Assert.Equal((double)12, population1.Entities[3].ScaledFitnessValue);

            Assert.Equal((double)6, population2.Entities[0].ScaledFitnessValue);
            Assert.Equal((double)3, population2.Entities[1].ScaledFitnessValue);
            Assert.Equal((double)5, population2.Entities[2].ScaledFitnessValue);
            Assert.Equal((double)4, population2.Entities[3].ScaledFitnessValue);

            Assert.Equal((double)9, population3.Entities[0].ScaledFitnessValue);
            Assert.Equal((double)10, population3.Entities[1].ScaledFitnessValue);
            Assert.Equal((double)8, population3.Entities[2].ScaledFitnessValue);
            Assert.Equal((double)7, population3.Entities[3].ScaledFitnessValue);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void MultiDemeGeneticAlgorithm_Serialization()
        {
            MultiDemeGeneticAlgorithm algorithm = new MultiDemeGeneticAlgorithm
            {
                MigrantCount = 11,
                MigrateEachGeneration = 3
            };

            MultiDemeGeneticAlgorithm result = (MultiDemeGeneticAlgorithm)SerializationHelper.TestSerialization(algorithm, new Type[0]);

            Assert.Equal(algorithm.MigrantCount, result.MigrantCount);
            Assert.Equal(algorithm.MigrateEachGeneration, result.MigrateEachGeneration);
        }

        private static SimplePopulation GetPopulation(GeneticAlgorithm algorithm)
        {
            SimplePopulation population = new SimplePopulation { MinimumPopulationSize = 3 };
            population.Initialize(algorithm);

            for (int i = 0; i < 3; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            
            return population;
        }

        private class TestMultiDemeGeneticAlgorithm : MultiDemeGeneticAlgorithm
        {
            internal bool OnMigrateCalled;
            
            protected override void OnMigrate()
            {
                this.OnMigrateCalled = true;
            }
        }
    }
}
