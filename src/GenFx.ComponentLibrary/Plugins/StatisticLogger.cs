using System;
using System.Collections.Generic;
using System.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Plugins
{
    /// <summary>
    /// Logs statistics to a database for each generation.
    /// </summary>
    public class StatisticLogger : Plugin, IDisposable
    {
        private Run currentRun;
        private StatisticLogContext context;
        private bool isDisposed;
        private Dictionary<string, StatisticItem> stats;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticLogger"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="Plugin"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public StatisticLogger(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Gets the configuration for the statistic logger.
        /// </summary>
        public new StatisticLoggerConfiguration Configuration
        {
            get { return (StatisticLoggerConfiguration)base.Configuration; }
        }

        /// <summary>
        /// Handles the event when a genetic algorithm is about to start execution.
        /// </summary>
        public override void OnAlgorithmStarting()
        {
            this.context = new StatisticLogContext();

            this.stats = new Dictionary<string, StatisticItem>();
            foreach (StatisticItem item in this.context.StatisticItems)
            {
                this.stats.Add(item.StatisticName, item);
            }

            this.currentRun = new Run { StartTime = DateTime.Now, Label = this.Configuration.Label };
            this.context.Runs.AddObject(this.currentRun);
        }

        /// <summary>
        /// Handles the event when a genetic algorithm has finished executing.
        /// </summary>
        public override void OnAlgorithmCompleted()
        {
            this.context.SaveChanges();
        }

        /// <summary>
        /// Handles the event when a generation has been created.
        /// </summary>
        /// <param name="environment">The environment representing the generation that was created.</param>
        /// <param name="generationIndex">Index value of the generation that was created.</param>
        public override void OnFitnessEvaluated(GeneticEnvironment environment, int generationIndex)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            foreach (Population population in environment.Populations)
            {
                foreach (Statistic stat in this.Algorithm.Statistics)
                {
                    string statVal = stat.GetResultValue(population).ToString();

                    string statName;
                    DisplayNameAttribute attrib = (DisplayNameAttribute)TypeDescriptor.GetAttributes(stat)[typeof(DisplayNameAttribute)];
                    if (attrib != null && !String.IsNullOrEmpty(attrib.DisplayName))
                    {
                        statName = attrib.DisplayName;
                    }
                    else
                    {
                        statName = stat.Configuration.GetType().Name;
                    }

                    StatisticItem statItem;
                    if (!this.stats.TryGetValue(statName, out statItem))
                    {
                        statItem = new StatisticItem { StatisticName = statName };
                        this.context.StatisticItems.AddObject(statItem);

                        this.stats.Add(statName, statItem);
                    }

                    this.context.StatisticLogs.AddObject(new StatisticLog
                    {
                        LogTime = DateTime.Now,
                        Statistic = statItem,
                        StatisticValue = statVal,
                        PopulationIndex = population.Index,
                        GenerationIndex = generationIndex,
                        Run = this.currentRun
                    });
                }
            }
        }

        /// <summary>
        /// Disposes the resources of this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources of this instance.
        /// </summary>
        /// <param name="disposing">Whether a consumer of this object is explicitly disposing it.</param>
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }

    /// <summary>
    /// Configuration for the <see cref="StatisticLogger"/> component.
    /// </summary>
    [Component(typeof(StatisticLogger))]
    public class StatisticLoggerConfiguration : PluginConfiguration
    {
        private string label;

        /// <summary>
        /// Gets or sets the label to be associated with the logging of the statistics.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [RequiredValidator]
        public string Label
        {
            get { return this.label; }
            set { this.SetProperty(ref this.label, value); }
        }
    }
}
