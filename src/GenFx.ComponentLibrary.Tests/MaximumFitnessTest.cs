using GenFx.ComponentLibrary.Metrics;
using GenFx.ComponentLibrary.Populations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MaximumFitness"/> class.
    /// </summary>
    [TestClass]
    public class MaximumFitnessTest
    {
        /// <summary>
        /// Tests that the <see cref="MaximumFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void MaximumFitness_GetResultValue_NoScaling()
        {
            MaximumFitness metric = new MaximumFitness();
            metric.Initialize(new MockGeneticAlgorithm());

            MockPopulation population = new MockPopulation();
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            populationAccessor.SetField("rawMax", (double)12);
            object result = metric.GetResultValue(population);
            Assert.AreEqual((double)12, result);
        }

        /// <summary>
        /// Tests that the <see cref="MaximumFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void MaximumFitness_GetResultValue_WithScaling()
        {
            MaximumFitness metric = new MaximumFitness();
            metric.Initialize(new MockGeneticAlgorithm { FitnessScalingStrategy = new MockFitnessScalingStrategy() });

            MockPopulation population = new MockPopulation();
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 10 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 11 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 15 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 13 });
            object result = metric.GetResultValue(population);
            Assert.AreEqual((double)15, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void MaximumFitness_GetResultValue_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Metrics.Add(new MaximumFitness());

            MaximumFitness target = new MaximumFitness();
            target.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
