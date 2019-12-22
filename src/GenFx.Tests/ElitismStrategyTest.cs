using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    ///This is a test class for GenFx.ElitismStrategy and is intended
    ///to contain all GenFx.ElitismStrategy Unit Tests
    ///</summary>
    public class ElitismStrategyTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void ElitismStrategy_Ctor_NullAlgorithm()
        {
            MockElitismStrategy strategy = new MockElitismStrategy();
            Assert.Throws<ArgumentNullException>(() => strategy.Initialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [Fact]
        public void ElitismStrategy_Ctor_InvalidSetting1()
        {
            MockElitismStrategy strategy = new MockElitismStrategy();
            Assert.Throws<ValidationException>(() => strategy.ElitistRatio = 2);
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the ElitistRatio setting.
        /// </summary>
        [Fact]
        public void ElitismStrategy_Ctor_InvalidSetting2()
        {
            MockElitismStrategy strategy = new MockElitismStrategy();
            Assert.Throws<ValidationException>(() => strategy.ElitistRatio = -1);
        }

        /// <summary>
        /// Tests that ApplyElitism works correctly.
        /// </summary>
        [Fact]
        public async Task ElitismStrategy_GetElitistGeneticEntities()
        {
            double elitismRatio = .1;
            int totalGeneticEntities = 100;
            GeneticAlgorithm algorithm = GetGeneticAlgorithm(elitismRatio);
            await algorithm.InitializeAsync();
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            for (int i = 0; i < totalGeneticEntities; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            algorithm.Environment.Populations.Add(population);
            MockElitismStrategy strategy = (MockElitismStrategy)algorithm.ElitismStrategy;
            strategy.Initialize(algorithm);

            IList<GeneticEntity> geneticEntities = strategy.GetEliteEntities(population);

            Assert.Equal(Convert.ToInt32(Math.Round(elitismRatio * totalGeneticEntities)), geneticEntities.Count);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [Fact]
        public void ElitismStrategy_GetElitistGeneticEntities_NullPopulation()
        {
            MockElitismStrategy strategy = new MockElitismStrategy();
            strategy.Initialize(GetGeneticAlgorithm(.1));
            Assert.Throws<ArgumentNullException>(() => strategy.GetEliteEntities(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [Fact]
        public void ElitismStrategy_GetElitistGeneticEntities_EmptyPopulation()
        {
            GeneticAlgorithm algorithm = GetGeneticAlgorithm(.1);
            MockElitismStrategy strategy = new MockElitismStrategy();
            strategy.Initialize(algorithm);
            MockPopulation pop = new MockPopulation();
            pop.Initialize(algorithm);
            Assert.Throws<ArgumentException>(() => strategy.GetEliteEntities(pop));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void ElitismStrategy_Serialization()
        {
            MockElitismStrategy strategy = new MockElitismStrategy();
            strategy.ElitistRatio = 0.3;

            MockElitismStrategy result = (MockElitismStrategy)SerializationHelper.TestSerialization(strategy, new Type[0]);

            Assert.Equal(0.3, result.ElitistRatio);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed to <see cref="ElitismStrategy.GetEliteGeneticEntitiesCore(Population)"/>.
        /// </summary>
        [Fact]
        public void ElitismStrategy_NullPopulation()
        {
            MockElitismStrategy3 strategy = new MockElitismStrategy3();
            MockPopulation population = new MockPopulation();
            population.Entities.Add(new MockEntity());
            Assert.Throws<ArgumentNullException>(() => strategy.GetEliteEntities(population));
            Assert.True(strategy.GetEliteGeneticEntitiesCoreCalled);
        }

        private static MockGeneticAlgorithm GetGeneticAlgorithm(double elitismRatio)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                ElitismStrategy = new MockElitismStrategy
                {
                    ElitistRatio = elitismRatio
                }
            };
            return algorithm;
        }
    }
}
