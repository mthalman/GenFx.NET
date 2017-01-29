using GenFx.Validation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Plugins
{
    /// <summary>
    /// Logs statistics for each generation.
    /// </summary>
    [DataContract]
    public class StatisticLogger : Plugin
    {
        [DataMember]
        private string traceCategory;

        /// <summary>
        /// Gets or sets the category to associate with the tracing information.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [RequiredValidator]
        public string TraceCategory
        {
            get { return this.traceCategory; }
            set { this.SetProperty(ref this.traceCategory, value); }
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
                        statName = stat.ToString();
                    }

                    this.WriteTrace(String.Format(CultureInfo.CurrentCulture, Resources.StatisticLogger_StatTrace,
                        statName, statVal, population.Index, generationIndex));
                }
            }
        }

        private void WriteTrace(string message)
        {
            Trace.WriteLine(DateTime.Now.ToString(CultureInfo.CurrentCulture) + ": " + message, this.TraceCategory);
        }
    }
}
