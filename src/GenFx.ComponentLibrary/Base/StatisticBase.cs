using GenFx.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides the abstract base class for a statistic.
    /// </summary>
    /// <remarks>
    /// The <b>Statistic</b> class represents any computation of data contained by a <see cref="IPopulation"/>.
    /// After each generation is created, <see cref="GetResultValue(IPopulation)"/> is
    /// invoked with the <see cref="IPopulation"/> of that generation to calculate its data.
    /// </remarks>
    public abstract class StatisticBase : GeneticComponentWithAlgorithm, IStatistic
    {
        private Dictionary<int, ObservableCollection<StatisticResult>> populationResults = new Dictionary<int, ObservableCollection<StatisticResult>>();
        
        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(IGeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

            for (int i = 0; i < this.Algorithm.EnvironmentSize; i++)
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
        /// <param name="population"><see cref="IPopulation"/> from which to derive a statistic.</param>
        /// <returns>Value of the statistic derived from <paramref name="population"/>.</returns>
        /// <remarks>
        /// This method is called once for each generation.
        /// </remarks>
        public abstract object GetResultValue(IPopulation population);

        /// <summary>
        /// Restores the state of this component.
        /// </summary>
        /// <param name="state">The state of the component to restore from.</param>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.populationResults = (Dictionary<int, ObservableCollection<StatisticResult>>)state[nameof(this.populationResults)];
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.populationResults)] = this.populationResults;
        }
    }
}
