using GenFx.ComponentLibrary.Metrics;
using GenFx.ComponentLibrary.Populations;
using System;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MeanFitness"/> class.
    /// </summary>
    public class MeanFitnessTest
    {
        /// <summary>
        /// Tests that the <see cref="MeanFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void MeanFitness_GetResultValue_NoScaling()
        {
            MeanFitness metric = new MeanFitness();
            metric.Initialize(new MockGeneticAlgorithm());

            MockPopulation population = new MockPopulation();
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            populationAccessor.SetField("rawMean", (double)12);
            object result = metric.GetResultValue(population);
            Assert.Equal((double)12, result);
        }

        /// <summary>
        /// Tests that the <see cref="MeanFitness.GetResultValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void MeanFitness_GetResultValue_WithScaling()
        {
            MeanFitness metric = new MeanFitness();
            metric.Initialize(new MockGeneticAlgorithm { FitnessScalingStrategy = new MockFitnessScalingStrategy() });

            MockPopulation population = new MockPopulation();
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 10 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 11 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 15 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 13 });
            object result = metric.GetResultValue(population);
            Assert.Equal(12.25, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [Fact]
        public void MeanFitness_GetResultValue_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Metrics.Add(new MeanFitness());

            MeanFitness target = new MeanFitness();
            target.Initialize(algorithm);
            Assert.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
