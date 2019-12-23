using GenFx.Components.Metrics;
using GenFx.Components.Populations;
using System;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MinimumFitness"/> class.
    /// </summary>
    public class MinimumFitnessTest
    {
        /// <summary>
        /// Tests that the <see cref="MinimumFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void MinimumFitness_GetResultValue_NoScaling()
        {
            MinimumFitness metric = new MinimumFitness();
            metric.Initialize(new MockGeneticAlgorithm());

            MockPopulation population = new MockPopulation();
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            populationAccessor.SetField("rawMin", (double)2);
            object result = metric.GetResultValue(population);
            Assert.Equal((double)2, result);
        }

        /// <summary>
        /// Tests that the <see cref="MinimumFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void MinimumFitness_GetResultValue_WithScaling()
        {
            MinimumFitness metric = new MinimumFitness();
            metric.Initialize(new MockGeneticAlgorithm { FitnessScalingStrategy = new MockFitnessScalingStrategy() });

            MockPopulation population = new MockPopulation();
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 11 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 10 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 15 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 13 });
            object result = metric.GetResultValue(population);
            Assert.Equal((double)10, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [Fact]
        public void MinimumFitness_GetResultValue_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Metrics.Add(new MinimumFitness());

            MinimumFitness target = new MinimumFitness();
            target.Initialize(algorithm);
            Assert.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
