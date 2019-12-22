using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using System;
using System.Collections.Generic;
using System.Reflection;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="UniformSelectionOperator"/> class.
    ///</summary>
    public class UniformSelectionOperatorTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [Fact]
        public void UniformSelectionOperator_Select()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                SelectionOperator = new UniformSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };
            UniformSelectionOperator op = new UniformSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);

            for (int i = 0; i < 4; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }

            TestRandomUtil randomUtil = new TestRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.Value = 3;
            IList<GeneticEntity> selectedEntities = op.SelectEntities(1, population);
            Assert.Same(population.Entities[randomUtil.Value], selectedEntities[0]);

            randomUtil.Value = 2;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(population.Entities[randomUtil.Value], selectedEntities[0]);

            randomUtil.Value = 1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(population.Entities[randomUtil.Value], selectedEntities[0]);

            randomUtil.Value = 0;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(population.Entities[randomUtil.Value], selectedEntities[0]);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="UniformSelectionOperator.SelectEntitiesFromPopulation"/>.
        /// </summary>
        [Fact]
        public void UniformSelectionOperator_SelectEntitiesFromPopulation_NullPopulation()
        {
            UniformSelectionOperator op = new UniformSelectionOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("SelectEntitiesFromPopulation", (int)0, (Population)null));
        }

        private class TestRandomUtil : IRandomNumberService
        {
            internal int Value;

            public int GetRandomValue(int maxValue)
            {
                return Value;
            }

            public double GetDouble()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }


}
