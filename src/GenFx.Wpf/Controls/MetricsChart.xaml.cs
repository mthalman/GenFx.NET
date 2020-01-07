using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GenFx.Wpf.Controls
{
    /// <summary>
    /// Control that charts the values of a genetic algorithm's <see cref="Metric"/> objects.
    /// </summary>
    public class MetricsChart : Control
    {
        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="Algorithm"/>.
        /// </summary>
        public static readonly DependencyProperty AlgorithmProperty = DependencyProperty.Register(
            nameof(Algorithm), typeof(GeneticAlgorithm), typeof(MetricsChart),
            new PropertyMetadata(MetricsChart.OnAlgorithmChanged));

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="Population"/>.
        /// </summary>
        public static readonly DependencyProperty PopulationProperty = DependencyProperty.Register(
            nameof(Population), typeof(Population), typeof(MetricsChart),
            new PropertyMetadata(MetricsChart.OnPopulationChanged));

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="SelectedMetrics"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedMetricsProperty = DependencyProperty.Register(
            nameof(SelectedMetrics), typeof(IEnumerable<Metric>), typeof(MetricsChart),
            new PropertyMetadata(MetricsChart.OnSelectedMetricsChanged));

        /// <summary>
        /// Gets or sets the <see cref="Population"/> associated with the metric values to be charted.
        /// </summary>
        public Population Population
        {
            get
            {
                return (Population)base.GetValue(MetricsChart.PopulationProperty);
            }
            set
            {
                base.SetValue(MetricsChart.PopulationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Metric"/> objects which should be plotted in the chart. If this value
        /// is null, all metrics contained within the <see cref="Algorithm"/> are used.
        /// </summary>
        public IEnumerable<Metric> SelectedMetrics
        {
            get
            {
                return (IEnumerable<Metric>)base.GetValue(MetricsChart.SelectedMetricsProperty);
            }
            set
            {
                base.SetValue(MetricsChart.SelectedMetricsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="GeneticAlgorithm"/> containing the <see cref="Metric"/> objects to chart.
        /// </summary>
        public GeneticAlgorithm Algorithm
        {
            get
            {
                return (GeneticAlgorithm)base.GetValue(MetricsChart.AlgorithmProperty);
            }
            set
            {
                base.SetValue(MetricsChart.AlgorithmProperty, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="PlotModel"/> for the chart to represent.
        /// </summary>
        public PlotModel PlotModel { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static MetricsChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetricsChart), new FrameworkPropertyMetadata(typeof(MetricsChart)));
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public MetricsChart()
        {
            this.PlotModel = new PlotModel();
            this.PlotModel.Axes.Add(
                new LinearAxis
                {
                    Title = Wpf.Resources.MetricsChart_XAxisTitle,
                    Position = AxisPosition.Bottom,
                    Minimum = 0,
                    MinorStep = 1,
                    MajorStep = 10
                });

            this.PlotModel.Axes.Add(
                new LinearAxis
                {
                    Title = Wpf.Resources.MetricsChart_YAxisTitle,
                    Position = AxisPosition.Left
                });
        }

        /// <summary>
        /// Handles the event when the <see cref="Algorithm"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnAlgorithmChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            MetricsChart control = (MetricsChart)obj;
            if (e.OldValue != null)
            {
                GeneticAlgorithm algorithm = (GeneticAlgorithm)e.OldValue;
                algorithm.FitnessEvaluated -= control.Algorithm_FitnessEvaluated;
            }

            if (e.NewValue != null)
            {
                GeneticAlgorithm algorithm = (GeneticAlgorithm)e.NewValue;
                algorithm.FitnessEvaluated += control.Algorithm_FitnessEvaluated;
            }

            control.RefreshAllSeries();
        }

        /// <summary>
        /// Handles the event when the <see cref="SelectedMetrics"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnSelectedMetricsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            MetricsChart control = (MetricsChart)obj;
            control.RefreshAllSeries();
        }

        /// <summary>
        /// Handles the event when the <see cref="Population"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnPopulationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            MetricsChart control = (MetricsChart)obj;
            control.RefreshAllSeries();
        }

        /// <summary>
        /// Refreshes all of the plot model's series.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void RefreshAllSeries()
        {
            GeneticAlgorithm? algorithm = null;
            Population? population = null;
            IEnumerable<Metric>? selectedMetrics = null;
            this.Dispatcher.Invoke(() =>
            {
                algorithm = this.Algorithm;
                population = this.Population;
                selectedMetrics = this.SelectedMetrics;
            });

            this.PlotModel.Series.Clear();

            if (algorithm != null && population != null)
            {
                IEnumerable<Metric> metrics = selectedMetrics ?? algorithm.Metrics;
                foreach (Metric metric in metrics)
                {
                    LineSeries series = new LineSeries
                    {
                        Title = DisplayNameHelper.GetDisplayName(metric),
                        ToolTip = DisplayNameHelper.GetDisplayNameWithTypeInfo(metric),
                        DataFieldX = nameof(MetricResult.GenerationIndex),
                        DataFieldY = nameof(MetricResult.ResultValue)
                    };

                    ObservableCollection<MetricResult> results = metric.GetResults(population.Index);
                    series.ItemsSource = results;

                    // Verify the results contain values of types that can be converted to double
                    // for the chart to plot.
                    MetricResult firstResult = results.FirstOrDefault();
                    if (firstResult != null)
                    {
                        bool exceptionThrown = false;
                        try
                        {
                            Convert.ToDouble(firstResult.ResultValue, CultureInfo.CurrentCulture);
                        }
                        catch (Exception)
                        {
                            exceptionThrown = true;
                        }

                        if (!exceptionThrown)
                        {
                            this.PlotModel.Series.Add(series);
                        }
                    }
                }
            }

            this.RefreshPlot();
        }

        /// <summary>
        /// Handles the event when the algorithm has evaluated the fitness of a population.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The<see cref="EnvironmentFitnessEvaluatedEventArgs"/> associated with the event.</param>
        private void Algorithm_FitnessEvaluated(object? sender, EnvironmentFitnessEvaluatedEventArgs e)
        {
            if (e.GenerationIndex == 0)
            {
                this.RefreshAllSeries();
            }
            else
            {
                this.RefreshPlot();
            }
        }

        /// <summary>
        /// Refreshes the plot to display the latest updates to its content.
        /// </summary>
        private void RefreshPlot()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.PlotModel.InvalidatePlot(true);
            }, DispatcherPriority.Background);
        }
    }
}
