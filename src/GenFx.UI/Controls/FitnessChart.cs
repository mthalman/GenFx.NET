using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GenFx.UI.Controls
{
    /// <summary>
    /// Control that charts the fitness values of a population.
    /// </summary>
    public class FitnessChart : Control
    {
        private CategoryAxis categoryAxis;
        private ColumnSeries columnSeries;
        private IStopwatchFactory stopwatchFactory;
        private IStopwatch stopwatch;

        private static readonly TimeSpan chartDataRefreshRate = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="Population"/>.
        /// </summary>
        public static readonly DependencyProperty PopulationProperty = DependencyProperty.Register(
            nameof(Population), typeof(Population), typeof(FitnessChart),
            new PropertyMetadata(FitnessChart.OnPopulationChanged));

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="FitnessType"/>.
        /// </summary>
        public static readonly DependencyProperty FitnessTypeProperty = DependencyProperty.Register(
            nameof(FitnessType), typeof(FitnessType), typeof(FitnessChart),
            new PropertyMetadata(FitnessType.Scaled, FitnessChart.OnFitnessTypeChanged));

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="FitnessSortOption"/>.
        /// </summary>
        public static readonly DependencyProperty FitnessSortOptionProperty = DependencyProperty.Register(
            nameof(FitnessSortOption), typeof(FitnessSortOption), typeof(FitnessChart),
            new PropertyMetadata(FitnessSortOption.Entity, FitnessChart.OnFitnessSortOptionChanged));

        /// <summary>
        /// Gets or sets the <see cref="Population"/> containing the <see cref="GeneticEntity"/> objects which this control charts the fitness of.
        /// </summary>
        public Population Population
        {
            get
            {
                return ((Population)(base.GetValue(FitnessChart.PopulationProperty)));
            }
            set
            {
                base.SetValue(FitnessChart.PopulationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FitnessType"/> that is used as the value for charting.
        /// </summary>
        public FitnessType FitnessType
        {
            get
            {
                return ((FitnessType)(base.GetValue(FitnessChart.FitnessTypeProperty)));
            }
            set
            {
                base.SetValue(FitnessChart.FitnessTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FitnessSortOption"/> that is used to determine how to sort the fitness values.
        /// </summary>
        public FitnessSortOption FitnessSortOption
        {
            get
            {
                return ((FitnessSortOption)(base.GetValue(FitnessChart.FitnessSortOptionProperty)));
            }
            set
            {
                base.SetValue(FitnessChart.FitnessSortOptionProperty, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="PlotModel"/> for the chart to represent.
        /// </summary>
        public PlotModel PlotModel { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static FitnessChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FitnessChart), new FrameworkPropertyMetadata(typeof(FitnessChart)));
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public FitnessChart()
            : this(new DefaultStopwatchFactory())
        {
        }

        public FitnessChart(IStopwatchFactory stopwatchFactory)
        {
            this.stopwatchFactory = stopwatchFactory;

            this.PlotModel = new PlotModel();
            this.categoryAxis = new CategoryAxis();
            this.categoryAxis.TickStyle = TickStyle.None;
            this.categoryAxis.Title = UI.Resources.FitnessChart_XAxisTitle;
            this.PlotModel.Axes.Add(this.categoryAxis);
            this.PlotModel.Axes.Add(new LinearAxis() { Title = UI.Resources.FitnessChart_YAxisTitle });

            this.columnSeries = new ColumnSeries();
            this.columnSeries.TrackerFormatString = "{" + nameof(GeneticEntity.Representation) + "} : {2}";
            this.PlotModel.Series.Add(this.columnSeries);
        }

        /// <summary>
        /// Handles the event when the <see cref="FitnessType"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnFitnessTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FitnessChart fitnessChart = (FitnessChart)obj;
            fitnessChart.RefreshChart(true);
        }

        /// <summary>
        /// Handles the event when the <see cref="FitnessSortOption"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnFitnessSortOptionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FitnessChart fitnessChart = (FitnessChart)obj;
            fitnessChart.RefreshChart(true);
        }

        /// <summary>
        /// Handles the event when the <see cref="Population"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnPopulationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FitnessChart fitnessChart = (FitnessChart)obj;

            if (e.OldValue != null)
            {
                Population oldPopulation = (Population)e.OldValue;
                oldPopulation.Algorithm.FitnessEvaluated -= fitnessChart.Algorithm_FitnessEvaluated;
                oldPopulation.Algorithm.AlgorithmCompleted -= fitnessChart.Algorithm_AlgorithmCompleted;
            }

            if (e.NewValue != null)
            {
                fitnessChart.Population.Algorithm.FitnessEvaluated += fitnessChart.Algorithm_FitnessEvaluated;
                fitnessChart.Population.Algorithm.AlgorithmCompleted += fitnessChart.Algorithm_AlgorithmCompleted;

                // Whenever the user switches populations, refresh the chart to show the selected population.
                // The population will also be changed when the algorithm is being initialized.  In that case,
                // it may not yet be fully populated so only refresh the chart if its reached the min. pop. size.
                if (fitnessChart.Population.Entities.Count >= fitnessChart.Population.MinimumPopulationSize)
                {
                    fitnessChart.TryInitializeStopwatch();
                    fitnessChart.RefreshChart(true);
                }
            }
            else
            {
                fitnessChart.columnSeries.ItemsSource = null;
                fitnessChart.stopwatch = null;
            }
        }

        /// <summary>
        /// Handles the event when the algorithm completes execution.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The<see cref="EventArgs"/> associated with the event.</param>
        private void Algorithm_AlgorithmCompleted(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.RefreshChart(true);
            });

            this.stopwatch = null;
        }

        /// <summary>
        /// Handles the event when the algorithm has evaluated the fitness of a population.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The<see cref="EnvironmentFitnessEvaluatedEventArgs"/> associated with the event.</param>
        private void Algorithm_FitnessEvaluated(object sender, EnvironmentFitnessEvaluatedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                bool forceRefresh = false;
                if (this.TryInitializeStopwatch())
                {
                    forceRefresh = true;
                }

                this.RefreshChart(forceRefresh);
            });
        }

        /// <summary>
        /// Initializes the stopwatch if it's not yet set.
        /// </summary>
        /// <returns>True if the stopwatch was initialized; otherwise, false.</returns>
        private bool TryInitializeStopwatch()
        {
            if (this.stopwatch == null)
            {
                this.stopwatch = this.stopwatchFactory.Create();
                this.stopwatch.Start();
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Refreshes the chart's data.
        /// </summary>
        /// <param name="forceRefresh">Force the data to be refreshed.</param>
        private void RefreshChart(bool forceRefresh)
        {
            // Unless the data is being forced to refresh, skip the refresh if the 
            // specified time has not yet elapsed.
            if (!forceRefresh && this.stopwatch.Elapsed < FitnessChart.chartDataRefreshRate)
            {
                return;
            }

            Population population = null;
            population = this.Population;

            this.categoryAxis.Labels.Clear();

            if (population != null)
            {
                IEnumerable<GeneticEntity> entities = population.Entities;
                if (this.FitnessSortOption == FitnessSortOption.Entity)
                {
                    List<GeneticEntity> entityList = population.Entities.ToList();
                    entityList.Sort();
                    entities = entityList;
                }
                else
                {
                    entities = population.Entities.GetEntitiesSortedByFitness(this.FitnessType, FitnessEvaluationMode.Maximize);
                }

                this.columnSeries.ItemsSource = entities;

                if (this.FitnessType == FitnessType.Scaled)
                {
                    this.columnSeries.ValueField = nameof(GeneticEntity.ScaledFitnessValue);
                }
                else
                {
                    this.columnSeries.ValueField = nameof(GeneticEntity.RawFitnessValue);
                }

                // Fill the x-axis with empty labels
                for (int i = 0; i < population.Entities.Count; i++)
                {
                    this.categoryAxis.Labels.Add(String.Empty);
                }
            }

            this.Dispatcher.Invoke(() =>
            {
                this.PlotModel.InvalidatePlot(true);
            }, DispatcherPriority.Background);

            this.stopwatch?.Restart();
        }
    }
}
