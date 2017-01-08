using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.Contracts;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.SelectionOperators.FitnessProportionateSelectionOperator and is intended
    /// to contain all GenFx.ComponentLibrary.SelectionOperators.FitnessProportionateSelectionOperator Unit Tests
    /// </summary>
    [TestClass()]
    public class FitnessProportionateSelectionOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [TestMethod()]
        public void FitnessProportionateSelectionOperator_Select()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            entity1.ScaledFitnessValue = 1;
            MockEntity entity2 = new MockEntity(algorithm);
            entity2.ScaledFitnessValue = 5;
            MockEntity entity3 = new MockEntity(algorithm);
            entity3.ScaledFitnessValue = 4;
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IGeneticEntity selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .099999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .599999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .6;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = 1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");
        }

        /// <summary>
        /// Tests that the Select method works correctly when trying to minimize the fitness.
        /// </summary>
        [TestMethod()]
        public void FitnessProportionateSelectionOperator_Select_MinimizeFitness()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            ((MockFitnessEvaluatorFactoryConfig)algorithm.ConfigurationSet.FitnessEvaluator).EvaluationMode = FitnessEvaluationMode.Minimize;

            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            entity1.ScaledFitnessValue = 1; // Slice size: 5
            MockEntity entity2 = new MockEntity(algorithm);
            entity2.ScaledFitnessValue = 5; // Slice size: 1
            MockEntity entity3 = new MockEntity(algorithm);
            entity3.ScaledFitnessValue = 4; // Slice size: 4
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IGeneticEntity selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .099999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .499999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .5;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = 1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");
        }

        /// <summary>
        /// Tests that the Select method works correctly when there exists a fitness value equal to zero.
        /// </summary>
        [TestMethod()]
        public void FitnessProportionateSelectionOperator_Select_FitnessValueZero()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            entity1.ScaledFitnessValue = 1; // Slice size: 2
            MockEntity entity2 = new MockEntity(algorithm);
            entity2.ScaledFitnessValue = 0; // Slice size: 1
            MockEntity entity3 = new MockEntity(algorithm);
            entity3.ScaledFitnessValue = 6; // Slice size: 7
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IGeneticEntity selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .199999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .2;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .299999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .3;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = 1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");
        }

        /// <summary>
        /// Tests that the Select method works correctly when there exists a fitness value less than zero.
        /// </summary>
        [TestMethod()]
        public void FitnessProportionateSelectionOperator_Select_FitnessValueNegative()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            entity1.ScaledFitnessValue = -1; // Slice size: 1
            MockEntity entity2 = new MockEntity(algorithm);
            entity2.ScaledFitnessValue = 1; // Slice size: 3
            MockEntity entity3 = new MockEntity(algorithm);
            entity3.ScaledFitnessValue = 4; // Slice size: 6
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IGeneticEntity selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .099999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity1, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .399999;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity2, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = .4;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");

            randomUtil.RandomRatio = 1;
            selectedEntity = op.SelectEntity(population);
            Assert.AreSame(entity3, selectedEntity, "Incorrect entity selected.");
        }

        private static IGeneticAlgorithm GetAlgorithm()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                SelectionOperator = new FitnessProportionateSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            });

            algorithm.Operators.FitnessEvaluator = new MockFitnessEvaluator(algorithm);

            return algorithm;
        }

        private class FakeRandomUtil : IRandomNumberService
        {
            public double RandomRatio;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetRandomPercentRatio()
            {
                return RandomRatio;
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }


}
