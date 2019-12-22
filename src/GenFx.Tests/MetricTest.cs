using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Metric"/> class.
    /// </summary>
    public class MetricTest
    {
        /// <summary>
        /// Tests that the constructor correctly initializes the state.
        /// </summary>
        [Fact]
        public void Metric_Ctor()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };

            algorithm.Metrics.Add(new MockMetric());

            Metric metric = new MockMetric();
            metric.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(metric, new PrivateType(typeof(Metric)));
            Assert.Same(accessor.GetProperty("Algorithm"), algorithm);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [Fact]
        public void Metric_Ctor_NullAlgorithm()
        {
            MockMetric metric = new MockMetric();
            Assert.Throws<ArgumentNullException>(() => metric.Initialize(null));
        }

        /// <summary>
        /// Tests that the Calculate method works correctly.
        /// </summary>
        [Fact]
        public async Task Metric_Calculate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                MinimumEnvironmentSize = 2,
                GeneticEntitySeed = new MockEntity(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new MockPopulation()
            };

            algorithm.Metrics.Add(new FakeMetric());

            await algorithm.InitializeAsync();
            
            FakeMetric metric = (FakeMetric)algorithm.Metrics[0];
            metric.Calculate(algorithm.Environment, 1);

            Assert.Equal(4, metric.GetResultValueCallCount);

            ObservableCollection<MetricResult> results = metric.GetResults(0);
            Assert.Equal(2, results.Count);
            Assert.Equal(0, results[0].GenerationIndex);
            Assert.Equal(0, results[0].PopulationIndex);
            Assert.Equal(1, results[0].ResultValue);
            Assert.Same(metric, results[0].Metric);

            results = metric.GetResults(1);
            Assert.Equal(0, results[0].GenerationIndex);
            Assert.Equal(1, results[0].PopulationIndex);
            Assert.Equal(2, results[0].ResultValue);
            Assert.Same(metric, results[0].Metric);
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void Metric_Serialization()
        {
            MockMetric metric = new MockMetric();

            PrivateObject privObj = new PrivateObject(metric, new PrivateType(typeof(Metric)));

            Dictionary<int, ObservableCollection<MetricResult>> popResults = (Dictionary<int, ObservableCollection<MetricResult>>)privObj.GetField("populationResults");

            ObservableCollection<MetricResult> metricResults = new ObservableCollection<MetricResult>
            {
                new MetricResult(1, 2, 3, metric)
            };
            popResults.Add(0, metricResults);

            MockMetric result = (MockMetric)SerializationHelper.TestSerialization(metric, new Type[0]);

            PrivateObject resultPrivObj = new PrivateObject(result, new PrivateType(typeof(Metric)));

            Dictionary<int, ObservableCollection<MetricResult>> resultPopResults = (Dictionary<int, ObservableCollection<MetricResult>>)resultPrivObj.GetField("populationResults");

            ObservableCollection<MetricResult> resultStatResults = resultPopResults[0];
            Assert.Equal(metricResults[0].GenerationIndex, resultStatResults[0].GenerationIndex);
            Assert.Equal(metricResults[0].PopulationIndex, resultStatResults[0].PopulationIndex);
            Assert.Equal(metricResults[0].ResultValue, resultStatResults[0].ResultValue);
            Assert.Same(result, resultStatResults[0].Metric);
        }

        private class FakeMetric : Metric
        {
            internal int GetResultValueCallCount;
            
            public override object GetResultValue(Population population)
            {
                this.GetResultValueCallCount++;
                return this.GetResultValueCallCount;
            }
        }
        
        private class TestMetric : Metric
        {
            public override object GetResultValue(Population population)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
