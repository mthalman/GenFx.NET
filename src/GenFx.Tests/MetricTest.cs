using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Metric"/> class.
    /// </summary>
    [TestClass]
    public class MetricTest
    {
        /// <summary>
        /// Tests that the constructor correctly initializes the state.
        /// </summary>
        [TestMethod]
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
            Assert.AreSame(accessor.GetProperty("Algorithm"), algorithm, "Algorithm not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Metric_Ctor_NullAlgorithm()
        {
            MockMetric metric = new MockMetric();
            AssertEx.Throws<ArgumentNullException>(() => metric.Initialize(null));
        }

        /// <summary>
        /// Tests that the Calculate method works correctly.
        /// </summary>
        [TestMethod]
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

            Assert.AreEqual(4, metric.GetResultValueCallCount, "Metric not called correctly.");

            ObservableCollection<MetricResult> results = metric.GetResults(0);
            Assert.AreEqual(2, results.Count, "Incorrect number of results.");
            Assert.AreEqual(0, results[0].GenerationIndex, "Result's GenerationIndex not set correctly.");
            Assert.AreEqual(0, results[0].PopulationIndex, "Result's PopulationId not set correctly.");
            Assert.AreEqual(1, results[0].ResultValue, "Result's ResultValue not set correctly.");
            Assert.AreSame(metric, results[0].Metric, "Result's Metric not set correctly.");

            results = metric.GetResults(1);
            Assert.AreEqual(0, results[0].GenerationIndex, "Result's GenerationIndex not set correctly.");
            Assert.AreEqual(1, results[0].PopulationIndex, "Result's PopulationId not set correctly.");
            Assert.AreEqual(2, results[0].ResultValue, "Result's ResultValue not set correctly.");
            Assert.AreSame(metric, results[0].Metric, "Result's Metric not set correctly.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void Metric_Serialization()
        {
            MockMetric metric = new MockMetric();

            PrivateObject privObj = new PrivateObject(metric, new PrivateType(typeof(Metric)));

            Dictionary<int, ObservableCollection<MetricResult>> popResults = (Dictionary<int, ObservableCollection<MetricResult>>)privObj.GetField("populationResults");

            ObservableCollection<MetricResult> metricResults = new ObservableCollection<MetricResult>();
            metricResults.Add(new MetricResult(1, 2, 3, metric));
            popResults.Add(0, metricResults);

            MockMetric result = (MockMetric)SerializationHelper.TestSerialization(metric, new Type[0]);

            PrivateObject resultPrivObj = new PrivateObject(result, new PrivateType(typeof(Metric)));

            Dictionary<int, ObservableCollection<MetricResult>> resultPopResults = (Dictionary<int, ObservableCollection<MetricResult>>)resultPrivObj.GetField("populationResults");

            ObservableCollection<MetricResult> resultStatResults = resultPopResults[0];
            Assert.AreEqual(metricResults[0].GenerationIndex, resultStatResults[0].GenerationIndex);
            Assert.AreEqual(metricResults[0].PopulationIndex, resultStatResults[0].PopulationIndex);
            Assert.AreEqual(metricResults[0].ResultValue, resultStatResults[0].ResultValue);
            Assert.AreSame(result, resultStatResults[0].Metric);
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
