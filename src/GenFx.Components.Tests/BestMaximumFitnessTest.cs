using GenFx.Components.Metrics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BestMaximumFitness"/> class.
    /// </summary>
    public class BestMaximumFitnessTest
    {
        /// <summary>
        /// Tests that the correct value is returned from <see cref="BestMaximumFitness.GetResultValue"/>.
        /// </summary>
        [Fact]
        public void BestMaximumFitness_GetResultValue()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MaximumFitness maximumFitness = new MaximumFitness();
            algorithm.Metrics.Add(maximumFitness);

            ObservableCollection<MetricResult> population1Results = maximumFitness.GetResults(0);
            population1Results.Add(new MetricResult(0, 0, (double)5, maximumFitness));

            BestMaximumFitness target = new BestMaximumFitness();
            target.Initialize(algorithm);

            MockPopulation population = new MockPopulation
            {
                Index = 0
            };
            object result = target.GetResultValue(population);
            Assert.Equal((double)5, result);

            population1Results.Add(new MetricResult(1, 0, (double)6, maximumFitness));

            result = target.GetResultValue(population);
            Assert.Equal((double)6, result);

            ObservableCollection<MetricResult> population2Results = maximumFitness.GetResults(1);
            population2Results.Add(new MetricResult(0, 2, (double)10, maximumFitness));

            MockPopulation population2 = new MockPopulation
            {
                Index = 1
            };
            result = target.GetResultValue(population2);
            Assert.Equal((double)10, result);

            population2Results.Add(new MetricResult(1, 1, (double)4, maximumFitness));

            result = target.GetResultValue(population2);
            Assert.Equal((double)10, result);
        }

        /// <summary>
        /// Tests that the correct value is returned from <see cref="BestMaximumFitness.GetResultValue"/> when every
        /// entity has a negative fitness value.
        /// </summary>
        [Fact]
        public void BestMaximumFitness_GetResultValue_AllNegative()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MaximumFitness maximumFitness = new MaximumFitness();
            algorithm.Metrics.Add(maximumFitness);

            ObservableCollection<MetricResult> population1Results = maximumFitness.GetResults(0);
            population1Results.Add(new MetricResult(0, 0, (double)-5, maximumFitness));

            BestMaximumFitness target = new BestMaximumFitness();
            target.Initialize(algorithm);

            MockPopulation population = new MockPopulation
            {
                Index = 0
            };
            object result = target.GetResultValue(population);
            Assert.Equal((double)-5, result);

            population1Results.Add(new MetricResult(1, 0, (double)-6, maximumFitness));

            result = target.GetResultValue(population);
            Assert.Equal((double)-5, result);

            ObservableCollection<MetricResult> population2Results = maximumFitness.GetResults(1);
            population2Results.Add(new MetricResult(0, 2, (double)-10, maximumFitness));

            MockPopulation population2 = new MockPopulation
            {
                Index = 1
            };
            result = target.GetResultValue(population2);
            Assert.Equal((double)-10, result);

            population2Results.Add(new MetricResult(1, 1, (double)-4, maximumFitness));

            result = target.GetResultValue(population2);
            Assert.Equal((double)-4, result);
        }

        /// <summary>
        /// Tests that an exception will be thrown when a null population1 is passed to <see cref="BestMaximumFitness.GetResultValue"/>.
        /// </summary>
        [Fact]
        public void BestMaximumFitness_GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Metrics.Add(new BestMaximumFitness());

            BestMaximumFitness target = new BestMaximumFitness();
            target.Initialize(algorithm);

            Assert.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        /// <summary>
        /// Tests that the component can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void BestMaximumFitness_Serialization()
        {
            BestMaximumFitness metric = new BestMaximumFitness();
            PrivateObject privObj = new PrivateObject(metric);
            Dictionary<int, double> bestMaxValues = (Dictionary<int, double>)privObj.GetField("bestMaxValues");
            bestMaxValues.Add(10, 2.4);

            BestMaximumFitness result = (BestMaximumFitness)SerializationHelper.TestSerialization(metric, new Type[] { typeof(MockEntity) });

            PrivateObject resultPrivObj = new PrivateObject(result);
            Dictionary<int, double> resultBestMaxValues = (Dictionary<int, double>)resultPrivObj.GetField("bestMaxValues");
            Assert.Equal(2.4, resultBestMaxValues[10]);

        }
    }
}
