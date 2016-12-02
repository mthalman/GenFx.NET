using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using GenFx.ComponentModel;
using GenFx.Properties;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a statistic.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>Statistic</b> class represents any computation of data contained by a <see cref="Population"/>.
    /// After each generation is created, <see cref="Statistic.GetResultValue(Population)"/> is
    /// invoked with the <see cref="Population"/> of that generation to calculate its data.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by adding to the <see cref="ComponentConfigurationSet.Statistics"/> property
    /// the type of that derived class.
    /// </para>
    /// </remarks>
    public abstract class Statistic : GeneticComponentWithAlgorithm
    {
        private Dictionary<int, ObservableCollection<StatisticResult>> populationResults = new Dictionary<int, ObservableCollection<StatisticResult>>();

        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.Statistics[this.GetType()]; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Statistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="Statistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected Statistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
            for (int i = 0; i < this.Algorithm.ConfigurationSet.GeneticAlgorithm.EnvironmentSize; i++)
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

        /// <summary>
        /// Restores the state of this component.
        /// </summary>
        /// <param name="state">The state of the component to restore from.</param>
        protected internal override void RestoreState(KeyValueMap state)
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
        protected override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.populationResults)] = this.populationResults;
        }

        /// <summary>
        /// Calculates the stats for the <paramref name="environment"/>.
        /// </summary>
        internal void Calculate(GeneticEnvironment environment, int generationIndex)
        {
            foreach (Population population in environment.Populations)
            {
                ObservableCollection<StatisticResult> populationStats = this.GetResults(population.Index);
                StatisticResult result = new StatisticResult(generationIndex, population.Index, this.GetResultValue(population), this);
                populationStats.Add(result);
            }
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="Statistic"/>.
    /// </summary>
    [Component(typeof(Statistic))]
    public abstract class StatisticConfiguration : ComponentConfiguration
    {
    }
}
