using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestCommon.Helpers;
using System.Reflection;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="UniformSelectionOperator"/> class.
    ///</summary>
    [TestClass()]
    public class UniformSelectionOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [TestMethod]
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
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntities[0], "Incorrect selected entity.");

            randomUtil.Value = 2;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntities[0], "Incorrect selected entity.");

            randomUtil.Value = 1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntities[0], "Incorrect selected entity.");

            randomUtil.Value = 0;
            selectedEntities = op.SelectEntities(1, population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntities[0], "Incorrect selected entity.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="UniformSelectionOperator.SelectEntitiesFromPopulation"/>.
        /// </summary>
        [TestMethod]
        public void UniformSelectionOperator_SelectEntitiesFromPopulation_NullPopulation()
        {
            UniformSelectionOperator op = new UniformSelectionOperator();
            PrivateObject accessor = new PrivateObject(op);
            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("SelectEntitiesFromPopulation", BindingFlags.Instance | BindingFlags.NonPublic, (int)0, (Population)null));
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
