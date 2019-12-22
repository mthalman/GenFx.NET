using GenFx.UI.Controls;
using Moq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCommon;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MetricsChart"/> class.
    /// </summary>
    public class MetricsChartTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [StaFact]
        public void MetricsChart_Ctor()
        {
            MetricsChart chart = new MetricsChart();
            Assert.Null(chart.Population);
            Assert.Null(chart.Algorithm);
            Assert.Null(chart.SelectedMetrics);

            PlotModel model = chart.PlotModel;

            Assert.Equal(2, model.Axes.Count);
            Assert.IsType<LinearAxis>(model.Axes[0]);
            Assert.IsType<LinearAxis>(model.Axes[1]);
        }

        /// <summary>
        /// Tests that the chart refreshes when the algorithm changes from null.
        /// </summary>
        [StaFact]
        public void MetricsChart_OnAlgorithmChanged_FromNull()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 1, metric1),
                new MetricResult(0, 1, 2, metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            Metric metric2 = Mock.Of<Metric>();
            List<MetricResult> metric2Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric2.GetResults(0).AddRange(metric2Results);

            algorithm.Metrics.Add(metric1);
            algorithm.Metrics.Add(metric2);

            VerifyChartRefresh(chart =>
            {
                chart.Population = population;
            }, chart =>
            {
                chart.Algorithm = algorithm;
            }, (metric, series) =>
            {
                if (metric == metric1)
                {
                    Assert.Equal(metric1Results, (ICollection)series.ItemsSource);
                }
                else if (metric == metric2)
                {
                    Assert.Equal(metric2Results, (ICollection)series.ItemsSource);
                }
            });
        }

        /// <summary>
        /// Tests that the chart refreshes when the algorithm changes from a previous algorithm.
        /// </summary>
        [StaFact]
        public void MetricsChart_OnAlgorithmChanged_FromOtherAlgorithm()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 1, metric1),
                new MetricResult(0, 1, 2, metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            Metric metric2 = Mock.Of<Metric>();
            List<MetricResult> metric2Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric2.GetResults(0).AddRange(metric2Results);

            algorithm.Metrics.Add(metric1);
            algorithm.Metrics.Add(metric2);

            VerifyChartRefresh(chart =>
            {
                chart.Population = population;
                chart.Algorithm = new TestAlgorithm();
            }, chart =>
            {
                chart.Algorithm = algorithm;
            }, (metric, series) =>
            {
                if (metric == metric1)
                {
                    Assert.Equal(metric1Results, (ICollection)series.ItemsSource);
                }
                else if (metric == metric2)
                {
                    Assert.Equal(metric2Results, (ICollection)series.ItemsSource);
                }
            });
        }

        /// <summary>
        /// Tests that the chart refreshes when the population changes from null.
        /// </summary>
        [StaFact]
        public void MetricsChart_OnPopulationChanged_FromNull()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 1, metric1),
                new MetricResult(0, 1, 2, metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            Metric metric2 = Mock.Of<Metric>();
            List<MetricResult> metric2Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric2.GetResults(0).AddRange(metric2Results);

            algorithm.Metrics.Add(metric1);
            algorithm.Metrics.Add(metric2);

            VerifyChartRefresh(chart =>
            {
                chart.Algorithm = algorithm;
            }, chart =>
            {
                chart.Population = population;
            }, (metric, series) =>
            {
                if (metric == metric1)
                {
                    Assert.Equal(metric1Results, (ICollection)series.ItemsSource);
                }
                else if (metric == metric2)
                {
                    Assert.Equal(metric2Results, (ICollection)series.ItemsSource);
                }
            });
        }

        /// <summary>
        /// Tests that the chart refreshes when the population changes from a previous population.
        /// </summary>
        [StaFact]
        public void MetricsChart_OnPopulationChanged_FromOtherPopulation()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 1, metric1),
                new MetricResult(0, 1, 2, metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            Metric metric2 = Mock.Of<Metric>();
            List<MetricResult> metric2Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric2.GetResults(0).AddRange(metric2Results);

            algorithm.Metrics.Add(metric1);
            algorithm.Metrics.Add(metric2);

            VerifyChartRefresh(chart =>
            {
                chart.Algorithm = new TestAlgorithm();
                chart.Population = new TestPopulation();
            }, chart =>
            {
                chart.Algorithm = algorithm;
                chart.Population = population;
            }, (metric, series) =>
            {
                if (metric == metric1)
                {
                    Assert.Equal(metric1Results, (ICollection)series.ItemsSource);
                }
                else if (metric == metric2)
                {
                    Assert.Equal(metric2Results, (ICollection)series.ItemsSource);
                }
            });
        }

        /// <summary>
        /// Tests that the chart does not add a series for a metric that doesn't produce values convertible to double.
        /// </summary>
        [StaFact]
        public void MetricsChart_MetricWithNonDoubleResult()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, "foo", metric1),
                new MetricResult(0, 1, "bar", metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            Metric metric2 = Mock.Of<Metric>();
            List<MetricResult> metric2Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric2.GetResults(0).AddRange(metric2Results);

            algorithm.Metrics.Add(metric1);
            algorithm.Metrics.Add(metric2);

            MetricsChart chart = new MetricsChart();
            chart.Algorithm = algorithm;
            chart.Population = population;

            Assert.Single(chart.PlotModel.Series);
            Assert.Equal(metric2Results, (ICollection)((LineSeries)chart.PlotModel.Series[0]).ItemsSource);
        }

        /// <summary>
        /// Tests that the chart refreshes the series when the fitness is evaluated for the initial generation.
        /// </summary>
        [StaFact]
        public void MetricsChart_RefreshSeriesOnFitnessEvaluated()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            algorithm.Metrics.Add(metric1);

            MetricsChart chart = new MetricsChart();
            chart.Algorithm = algorithm;
            chart.Population = population;

            Assert.Single(chart.PlotModel.Series);

            // Clear the series to ensure it gets refreshed when the fitness is evaluated
            chart.PlotModel.Series.Clear();

            PrivateObject algorithmAccessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            algorithmAccessor.Invoke("OnFitnessEvaluated",
                new EnvironmentFitnessEvaluatedEventArgs(new GeneticEnvironment(algorithm), 0));

            Assert.Single(chart.PlotModel.Series);
        }

        /// <summary>
        /// Tests that the chart refreshes the series when the fitness is evaluated after the initial generation.
        /// </summary>
        [StaFact]
        public void MetricsChart_RefreshPlotOnFitnessEvaluated()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            algorithm.Metrics.Add(metric1);

            MetricsChart chart = new MetricsChart();
            chart.Algorithm = algorithm;
            chart.Population = population;

            Assert.Single(chart.PlotModel.Series);

            // Clear the series to ensure it doesn't gets refreshed when the fitness is evaluated
            chart.PlotModel.Series.Clear();

            PrivateObject algorithmAccessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            algorithmAccessor.Invoke("OnFitnessEvaluated",
                new EnvironmentFitnessEvaluatedEventArgs(new GeneticEnvironment(algorithm), 1));

            Assert.Empty(chart.PlotModel.Series);
        }

        /// <summary>
        /// Tests that the chart refreshes the series when the <see cref="MetricsChart.SelectedMetrics"/> property changes.
        /// </summary>
        [StaFact]
        public void MetricsChart_OnSelectedMetricsChanged()
        {
            TestPopulation population = new TestPopulation();
            TestAlgorithm algorithm = new TestAlgorithm();

            Metric metric1 = Mock.Of<Metric>();
            List<MetricResult> metric1Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric1.GetResults(0).AddRange(metric1Results);

            algorithm.Metrics.Add(metric1);

            Metric metric2 = Mock.Of<Metric>();
            List<MetricResult> metric2Results = new List<MetricResult>
            {
                new MetricResult(0, 0, 3, metric1),
                new MetricResult(0, 1, 4, metric1)
            };
            metric2.GetResults(0).AddRange(metric2Results);

            algorithm.Metrics.Add(metric2);

            MetricsChart chart = new MetricsChart();
            chart.Algorithm = algorithm;
            chart.Population = population;

            Assert.Equal(2, chart.PlotModel.Series.Count);

            chart.PlotModel.Series.Clear();

            chart.SelectedMetrics = new List<Metric> { algorithm.Metrics[0] };

            Assert.Single(chart.PlotModel.Series);
        }

        private static void VerifyChartRefresh(
            Action<MetricsChart> initializeChart,
            Action<MetricsChart> triggerRefresh,
            Action<Metric, LineSeries> verifySeries)
        {
            MetricsChart chart = new MetricsChart();

            initializeChart(chart);

            // Verify the default state of the chart
            Assert.Empty(chart.PlotModel.Series);

            triggerRefresh(chart);

            // Verify the chart has been refreshed
            Metric[] metrics = (chart.SelectedMetrics != null ? chart.SelectedMetrics : chart.Algorithm.Metrics).ToArray();
            Assert.Equal(metrics.Length, chart.PlotModel.Series.Count);
            for (int i = 0; i < metrics.Length; i++)
            {
                verifySeries(metrics[i], (LineSeries)chart.PlotModel.Series[i]);
            }
        }
        
        private class TestPopulation : Population
        {
        }

        private class TestAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                throw new NotImplementedException();
            }
        }
    }
}
