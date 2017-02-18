using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a statistic.
    /// </summary>
    /// <remarks>
    /// The <b>Statistic</b> class represents any computation of data contained by a <see cref="Population"/>.
    /// After each generation is created, <see cref="GetResultValue(Population)"/> is
    /// invoked with the <see cref="Population"/> of that generation to calculate its data.
    /// </remarks>
    [DataContract]
    public abstract class Statistic : GeneticComponentWithAlgorithm
    {
        [DataMember]
        private Dictionary<int, ObservableCollection<StatisticResult>> populationResults = new Dictionary<int, ObservableCollection<StatisticResult>>();
        
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
                this.populationResults.Add(i, new ObservableCollection<StatisticResult>());
            }
        }

        /// <summary>
        /// Returns the statistic results of the indicated population for this statistic.
        /// </summary>
        /// <param name="populationId">ID of the population for which to return statistic results.</param>
        /// <returns>Collection of <see cref="StatisticResult"/> objects.</returns>
        /// <remarks>
        /// Each <see cref="StatisticResult"/> object in the collection is associated
        /// with one generation of the population.
        /// </remarks>
        public ObservableCollection<StatisticResult> GetResults(int populationId)
        {
            ObservableCollection<StatisticResult> results;
            this.populationResults.TryGetValue(populationId, out results);
            return results;
        }

        /// <summary>
        /// When overriden in a derived class, calculates a statistical value from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive a statistic.</param>
        /// <returns>Value of the statistic derived from <paramref name="population"/>.</returns>
        /// <remarks>
        /// This method is called once for each generation.
        /// </remarks>
        public abstract object GetResultValue(Population population);
    }
}
