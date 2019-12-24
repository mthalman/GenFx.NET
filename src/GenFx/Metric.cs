using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a metric.
    /// </summary>
    /// <remarks>
    /// The <b>Metric</b> class represents any computation of data contained by a <see cref="Population"/>.
    /// After each generation is created, <see cref="GetResultValue(Population)"/> is
    /// invoked with the <see cref="Population"/> of that generation to calculate its data.
    /// </remarks>
    [DataContract]
    public abstract class Metric : GeneticComponentWithAlgorithm
    {
        [DataMember]
        private readonly Dictionary<int, ObservableCollection<MetricResult>> populationResults = new Dictionary<int, ObservableCollection<MetricResult>>();
        
        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(GeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

            populationResults.Clear();

            for (int i = 0; i < this.Algorithm.MinimumEnvironmentSize; i++)
            {
                this.populationResults.Add(i, new ObservableCollection<MetricResult>());
            }
        }

        /// <summary>
        /// Returns the metric results of the indicated population for this metric.
        /// </summary>
        /// <param name="populationIndex">Index of the population for which to return metric results.</param>
        /// <returns>Collection of <see cref="MetricResult"/> objects.</returns>
        /// <remarks>
        /// Each <see cref="MetricResult"/> object in the collection is associated
        /// with one generation of the population.
        /// </remarks>
        public ObservableCollection<MetricResult> GetResults(int populationIndex)
        {
            if (!this.populationResults.TryGetValue(populationIndex, out ObservableCollection<MetricResult> results))
            {
                results = new ObservableCollection<MetricResult>();
                this.populationResults.Add(populationIndex, results);
            }
            return results;
        }

        /// <summary>
        /// When overriden in a derived class, calculates a metrical value from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive a metric.</param>
        /// <returns>Value of the metric derived from <paramref name="population"/>.</returns>
        /// <remarks>
        /// This method is called once for each generation.
        /// </remarks>
        public abstract object GetResultValue(Population population);
    }
}
