using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.SelectionOperators.UniformSelectionOperator and is intended
    ///to contain all GenFx.ComponentLibrary.SelectionOperators.UniformSelectionOperator Unit Tests
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
            GeneticEntity selectedEntity = op.SelectEntity(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");

            randomUtil.Value = 2;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");

            randomUtil.Value = 1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");

            randomUtil.Value = 0;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(population.Entities[randomUtil.Value], selectedEntity, "Incorrect selected entity.");
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
