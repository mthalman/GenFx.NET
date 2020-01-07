using GenFx.Validation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace GenFx.Components.Plugins
{
    /// <summary>
    /// Logs metrics for each generation.
    /// </summary>
    [DataContract]
    public class MetricLogger : Plugin
    {
        [DataMember]
        private string traceCategory = String.Empty;

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
        protected override void OnAlgorithmStarting()
        {
            this.WriteTrace(Resources.MetricLogger_AlgorithmStarted);
        }

        /// <summary>
        /// Handles the event when a genetic algorithm has finished executing.
        /// </summary>
        protected override void OnAlgorithmCompleted()
        {
            this.WriteTrace(Resources.MetricLogger_AlgorithmCompleted);
        }

        /// <summary>
        /// Handles the event when a generation has been created.
        /// </summary>
        /// <param name="environment">The environment representing the generation that was created.</param>
        /// <param name="generationIndex">Index value of the generation that was created.</param>
        protected override void OnFitnessEvaluated(GeneticEnvironment environment, int generationIndex)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (this.Algorithm == null)
            {
                return;
            }

            foreach (Population population in environment.Populations)
            {
                foreach (Metric metric in this.Algorithm.Metrics)
                {
                    string? metricVal = metric.GetResultValue(population)?.ToString();

                    string metricName;
                    // TypeDescriptor will always provide a DisplayNameAttribute, even if one isn't declared on the type.
                    DisplayNameAttribute attrib = (DisplayNameAttribute)TypeDescriptor.GetAttributes(metric)[typeof(DisplayNameAttribute)];
                    if (!String.IsNullOrEmpty(attrib.DisplayName))
                    {
                        metricName = attrib.DisplayName;
                    }
                    else
                    {
                        metricName = metric.ToString();
                    }

                    if (metricVal == null)
                    {
                        metricVal = "<null>";
                    }

                    this.WriteTrace(String.Format(CultureInfo.CurrentCulture, Resources.MetricLogger_StatTrace,
                        metricName, metricVal, population.Index, generationIndex));
                }
            }
        }

        private void WriteTrace(string message)
        {
            Trace.WriteLine(DateTime.Now.ToString(CultureInfo.CurrentCulture) + ": " + message, this.TraceCategory);
        }
    }
}
