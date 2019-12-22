using GenFx.ComponentLibrary.Metrics;
using GenFx.ComponentLibrary.Populations;
using System;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MaximumFitness"/> class.
    /// </summary>
    public class MaximumFitnessTest
    {
        /// <summary>
        /// Tests that the <see cref="MaximumFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void MaximumFitness_GetResultValue_NoScaling()
        {
            MaximumFitness metric = new MaximumFitness();
            metric.Initialize(new MockGeneticAlgorithm());

            MockPopulation population = new MockPopulation();
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            populationAccessor.SetField("rawMax", (double)12);
            object result = metric.GetResultValue(population);
            Assert.Equal((double)12, result);
        }

        /// <summary>
        /// Tests that the <see cref="MaximumFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [Fact]
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
            Assert.Equal((double)15, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [Fact]
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
            Assert.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
