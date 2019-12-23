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
    /// Contains unit tests for the <see cref="BestMinimumFitness"/> class.
    /// </summary>
    public class BestMinimumFitnessTest
    {
        /// <summary>
        /// Tests that the correct value is returned from <see cref="BestMinimumFitness.GetResultValue"/>.
        /// </summary>
        [Fact]
        public void BestMinimumFitness_GetResultValue()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MinimumFitness minimumFitness = new MinimumFitness();
            algorithm.Metrics.Add(minimumFitness);

            ObservableCollection<MetricResult> population1Results = minimumFitness.GetResults(0);
            population1Results.Add(new MetricResult(0, 0, (double)5, minimumFitness));

            BestMinimumFitness target = new BestMinimumFitness();
            target.Initialize(algorithm);

            MockPopulation population = new MockPopulation
            {
                Index = 0
            };
            object result = target.GetResultValue(population);
            Assert.Equal((double)5, result);

            population1Results.Add(new MetricResult(1, 0, (double)6, minimumFitness));

            result = target.GetResultValue(population);
            Assert.Equal((double)5, result);

            ObservableCollection<MetricResult> population2Results = minimumFitness.GetResults(1);
            population2Results.Add(new MetricResult(0, 2, (double)10, minimumFitness));

            MockPopulation population2 = new MockPopulation
            {
                Index = 1
            };
            result = target.GetResultValue(population2);
            Assert.Equal((double)10, result);

            population2Results.Add(new MetricResult(1, 1, (double)4, minimumFitness));

            result = target.GetResultValue(population2);
            Assert.Equal((double)4, result);
        }

        /// <summary>
        /// Tests that the correct value is returned from <see cref="BestMinimumFitness.GetResultValue"/> when every
        /// entity has a negative fitness value.
        /// </summary>
        [Fact]
        public void BestMinimumFitness_GetResultValue_AllNegative()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MinimumFitness minimumFitness = new MinimumFitness();
            algorithm.Metrics.Add(minimumFitness);

            ObservableCollection<MetricResult> population1Results = minimumFitness.GetResults(0);
            population1Results.Add(new MetricResult(0, 0, (double)-5, minimumFitness));

            BestMinimumFitness target = new BestMinimumFitness();
            target.Initialize(algorithm);

            MockPopulation population = new MockPopulation
            {
                Index = 0
            };
            object result = target.GetResultValue(population);
            Assert.Equal((double)-5, result);

            population1Results.Add(new MetricResult(1, 0, (double)-6, minimumFitness));

            result = target.GetResultValue(population);
            Assert.Equal((double)-6, result);

            ObservableCollection<MetricResult> population2Results = minimumFitness.GetResults(1);
            population2Results.Add(new MetricResult(0, 2, (double)-10, minimumFitness));

            MockPopulation population2 = new MockPopulation
            {
                Index = 1
            };
            result = target.GetResultValue(population2);
            Assert.Equal((double)-10, result);

            population2Results.Add(new MetricResult(1, 1, (double)-4, minimumFitness));

            result = target.GetResultValue(population2);
            Assert.Equal((double)-10, result);
        }

        /// <summary>
        /// Tests that an exception will be thrown when a null population1 is passed to <see cref="BestMinimumFitness.GetResultValue"/>.
        /// </summary>
        [Fact]
        public void BestMinimumFitness_GetResultValue_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Metrics.Add(new BestMinimumFitness());

            BestMinimumFitness target = new BestMinimumFitness();
            target.Initialize(algorithm);

            Assert.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        /// <summary>
        /// Tests that the component can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void BestMinimumFitness_Serialization()
        {
            BestMinimumFitness metric = new BestMinimumFitness();
            PrivateObject privObj = new PrivateObject(metric);
            Dictionary<int, double?> bestMinValues = (Dictionary<int, double?>)privObj.GetField("bestMinValues");
            bestMinValues.Add(10, 2.4);

            BestMinimumFitness result = (BestMinimumFitness)SerializationHelper.TestSerialization(metric, new Type[] { typeof(MockEntity) });

            PrivateObject resultPrivObj = new PrivateObject(result);
            Dictionary<int, double?> resultBestMinValues = (Dictionary<int, double?>)resultPrivObj.GetField("bestMinValues");
            Assert.Equal(2.4, resultBestMinValues[10]);
        }
    }
}
