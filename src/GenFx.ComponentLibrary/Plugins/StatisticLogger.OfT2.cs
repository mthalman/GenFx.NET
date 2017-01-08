using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace GenFx.ComponentLibrary.Plugins
{
    /// <summary>
    /// Logs statistics for each generation.
    /// </summary>
    /// <typeparam name="TPlugin">Type of the deriving plugin class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class StatisticLogger<TPlugin, TConfiguration> : PluginBase<TPlugin, TConfiguration>
        where TPlugin : StatisticLogger<TPlugin, TConfiguration>
        where TConfiguration : StatisticLoggerFactoryConfig<TConfiguration, TPlugin>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected StatisticLogger(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Handles the event when a genetic algorithm is about to start execution.
        /// </summary>
        public override void OnAlgorithmStarting()
        {
            this.WriteTrace(Resources.StatisticLogger_AlgorithmStarted);
        }

        /// <summary>
        /// Handles the event when a genetic algorithm has finished executing.
        /// </summary>
        public override void OnAlgorithmCompleted()
        {
            this.WriteTrace(Resources.StatisticLogger_AlgorithmCompleted);
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

            foreach (IPopulation population in environment.Populations)
            {
                foreach (IStatistic stat in this.Algorithm.Statistics)
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
                        statName = stat.ToString();
                    }

                    this.WriteTrace(String.Format(CultureInfo.CurrentCulture, Resources.StatisticLogger_StatTrace,
                        statName, statVal, population.Index, generationIndex));
                }
            }
        }

        private void WriteTrace(string message)
        {
            Trace.WriteLine(DateTime.Now.ToString(CultureInfo.CurrentCulture) + ": " + message, this.Configuration.TraceCategory);
        }
    }
}
