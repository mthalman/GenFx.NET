using GenFx.Components.Plugins;
using GenFx.Validation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MetricLogger"/> class.
    /// </summary>
    public class MetricLoggerTest : IDisposable
    {
        private readonly TestTraceListener traceListener;

        public MetricLoggerTest()
        {
            this.traceListener = new TestTraceListener();
            Trace.Listeners.Add(this.traceListener);
        }

        public void Dispose()
        {
            Trace.Listeners.Remove(this.traceListener);
        }

        /// <summary>
        /// Tests that the <see cref="MetricLogger"/> can be validated successfully.
        /// </summary>
        [Fact]
        public void MetricLogger_Validation()
        {
            MetricLogger logger = new MetricLogger
            {
                TraceCategory = "test"
            };

            logger.Validate();
        }

        /// <summary>
        /// Tests that validation fails when no value is provided for <see cref="MetricLogger.TraceCategory"/>.
        /// </summary>
        [Fact]
        public void MetricLogger_Validation_MissingTraceCategory()
        {
            MetricLogger logger = new MetricLogger();
            Assert.Throws<ValidationException>(() => logger.Validate());
        }

        /// <summary>
        /// Tests that the <see cref="MetricLogger.OnAlgorithmStarting"/> method works correctly.
        /// </summary>
        [Fact]
        public void MetricLogger_AlgorithmStarting()
        {
            MetricLogger logger = new MetricLogger
            {
                TraceCategory = "test"
            };

            Assert.Equal(String.Empty, this.traceListener.Output.ToString());

            PrivateObject accessor = new PrivateObject(logger);
            accessor.Invoke("OnAlgorithmStarting");

            Assert.StartsWith(logger.TraceCategory, this.traceListener.Output.ToString());
            Assert.Contains(Resources.MetricLogger_AlgorithmStarted, this.traceListener.Output.ToString());
        }

        /// <summary>
        /// Tests that the <see cref="MetricLogger.OnAlgorithmCompleted"/> method works correctly.
        /// </summary>
        [Fact]
        public void MetricLogger_AlgorithmCompleted()
        {
            MetricLogger logger = new MetricLogger
            {
                TraceCategory = "test"
            };

            Assert.Equal(String.Empty, this.traceListener.Output.ToString());

            PrivateObject accessor = new PrivateObject(logger);
            accessor.Invoke("OnAlgorithmCompleted");

            Assert.StartsWith(logger.TraceCategory, this.traceListener.Output.ToString());
            Assert.Contains(Resources.MetricLogger_AlgorithmCompleted, this.traceListener.Output.ToString());
        }

        /// <summary>
        /// Tests that the <see cref="MetricLogger.OnFitnessEvaluated"/> method works correctly.
        /// </summary>
        [Fact]
        public void MetricLogger_FitnessEvaluated()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.Metrics.Add(new TestMetric1());
            algorithm.Metrics.Add(new TestMetric2());
            algorithm.Metrics.Add(new TestMetric3());

            MetricLogger logger = new MetricLogger
            {
                TraceCategory = "test"
            };
            logger.Initialize(algorithm);

            Assert.Equal(String.Empty, this.traceListener.Output.ToString());
            
            GeneticEnvironment environment = new GeneticEnvironment(algorithm);
            environment.Populations.Add(new MockPopulation { Index = 0 });
            environment.Populations.Add(new MockPopulation { Index = 1 });

            PrivateObject accessor = new PrivateObject(logger);
            accessor.Invoke("OnFitnessEvaluated", environment, 0);

            string[] lines = this.traceListener.Output.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(24, lines.Length);
            this.VerifyMetricOutput(lines.Take(4).ToArray(), logger.TraceCategory, "Metric 1", "1:0", 0, 0);
            this.VerifyMetricOutput(lines.Skip(4).Take(4).ToArray(), logger.TraceCategory, "Metric 2", "2:0", 0, 0);
            this.VerifyMetricOutput(lines.Skip(8).Take(4).ToArray(), logger.TraceCategory, typeof(TestMetric3).FullName, "3:0", 0, 0);
            this.VerifyMetricOutput(lines.Skip(12).Take(4).ToArray(), logger.TraceCategory, "Metric 1", "1:1", 1, 0);
            this.VerifyMetricOutput(lines.Skip(16).Take(4).ToArray(), logger.TraceCategory, "Metric 2", "2:1", 1, 0);
            this.VerifyMetricOutput(lines.Skip(20).Take(4).ToArray(), logger.TraceCategory, typeof(TestMetric3).FullName, "3:1", 1, 0);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null environment to <see cref="MetricLogger.OnFitnessEvaluated"/>.
        /// </summary>
        [Fact]
        public void MetricLogger_OnFitnessEvaluated_NullEnvironment()
        {
            MetricLogger logger = new MetricLogger();
            PrivateObject accessor = new PrivateObject(logger);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("OnFitnessEvaluated", null, 0));
        }

        private void VerifyMetricOutput(string[] metricOutput, string traceCategory, string metricName, string metricValue, int populationIndex, int generationIndex)
        {
            Assert.StartsWith(traceCategory, metricOutput[0]);
            Assert.Contains("Metric Name: " + metricName, metricOutput[0]);
            Assert.Equal("Metric Value: " + metricValue, metricOutput[1]);
            Assert.Equal("Population Index: " + populationIndex, metricOutput[2]);
            Assert.Equal("Generation Index: " + generationIndex, metricOutput[3]);
        }

        private class TestTraceListener : TraceListener
        {
            public StringBuilder Output = new StringBuilder();

            public override void Write(string message)
            {
                this.Output.Append(message);
            }

            public override void WriteLine(string message)
            {
                this.Output.AppendLine(message);
            }
        }

        [DisplayName("Metric 1")]
        private class TestMetric1 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return "1:" + population.Index;
            }
        }

        private class TestMetric2 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return "2:" + population.Index;
            }

            public override string ToString()
            {
                return "Metric 2";
            }
        }

        [DisplayName]
        private class TestMetric3 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return "3:" + population.Index;
            }
        }
    }
}
