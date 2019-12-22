using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.SelectionOperators.FitnessProportionateSelectionOperator and is intended
    /// to contain all GenFx.ComponentLibrary.SelectionOperators.FitnessProportionateSelectionOperator Unit Tests
    /// </summary>
    public class FitnessProportionateSelectionOperatorTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Select method works correctly.
        /// </summary>
        [Fact]
        public void FitnessProportionateSelectionOperator_Select()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.ScaledFitnessValue = 1;
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.ScaledFitnessValue = 5;
            MockEntity entity3 = new MockEntity();
            entity3.Initialize(algorithm);
            entity3.ScaledFitnessValue = 4;
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IList<GeneticEntity> selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);

            randomUtil.RandomRatio = .099999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);

            randomUtil.RandomRatio = .1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .599999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .6;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);

            randomUtil.RandomRatio = 1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);
        }

        /// <summary>
        /// Tests that the Select method works correctly when trying to minimize the fitness.
        /// </summary>
        [Fact]
        public void FitnessProportionateSelectionOperator_Select_MinimizeFitness()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            ((MockFitnessEvaluator)algorithm.FitnessEvaluator).EvaluationMode = FitnessEvaluationMode.Minimize;

            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.ScaledFitnessValue = 1; // Slice size: 5
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.ScaledFitnessValue = 5; // Slice size: 1
            MockEntity entity3 = new MockEntity();
            entity3.Initialize(algorithm);
            entity3.ScaledFitnessValue = 4; // Slice size: 4
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IList<GeneticEntity> selectedEntities = op.SelectEntities(1, population).ToList();
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .099999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);

            randomUtil.RandomRatio = .499999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);

            randomUtil.RandomRatio = .5;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);

            randomUtil.RandomRatio = 1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);
        }

        /// <summary>
        /// Tests that the Select method works correctly when there exists a fitness value equal to zero.
        /// </summary>
        [Fact]
        public void FitnessProportionateSelectionOperator_Select_FitnessValueZero()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.ScaledFitnessValue = 1; // Slice size: 2
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.ScaledFitnessValue = 0; // Slice size: 1
            MockEntity entity3 = new MockEntity();
            entity3.Initialize(algorithm);
            entity3.ScaledFitnessValue = 6; // Slice size: 7
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IList<GeneticEntity> selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);

            randomUtil.RandomRatio = .199999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);

            randomUtil.RandomRatio = .2;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .299999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .3;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);

            randomUtil.RandomRatio = 1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);
        }

        /// <summary>
        /// Tests that the Select method works correctly when there exists a fitness value less than zero.
        /// </summary>
        [Fact]
        public void FitnessProportionateSelectionOperator_Select_FitnessValueNegative()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator();
            op.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            entity1.ScaledFitnessValue = -1; // Slice size: 1
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            entity2.ScaledFitnessValue = 1; // Slice size: 3
            MockEntity entity3 = new MockEntity();
            entity3.Initialize(algorithm);
            entity3.ScaledFitnessValue = 4; // Slice size: 6
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            population.Entities.Add(entity3);
            FakeRandomUtil randomUtil = new FakeRandomUtil();
            RandomNumberService.Instance = randomUtil;

            randomUtil.RandomRatio = 0;
            IList<GeneticEntity> selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);

            randomUtil.RandomRatio = .099999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity1, selectedEntities[0]);

            randomUtil.RandomRatio = .1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .399999;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity2, selectedEntities[0]);

            randomUtil.RandomRatio = .4;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);

            randomUtil.RandomRatio = 1;
            selectedEntities = op.SelectEntities(1, population);
            Assert.Same(entity3, selectedEntities[0]);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null population to <see cref="FitnessProportionateSelectionOperator.SelectEntitiesFromPopulation"/>.
        /// </summary>
        [Fact]
        public void FitnessProportionateSelectionOperator_SelectEntitiesFromPopulation_NullPopulation()
        {
            FitnessProportionateSelectionOperator op = new FitnessProportionateSelectionOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("SelectEntitiesFromPopulation", (int)0, (Population)null));
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new FitnessProportionateSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };

            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessEvaluator.Initialize(algorithm);

            return algorithm;
        }

        private class FakeRandomUtil : IRandomNumberService
        {
            public double RandomRatio;

            public int GetRandomValue(int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public double GetDouble()
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
