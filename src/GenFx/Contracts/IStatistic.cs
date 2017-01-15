using System.Collections.ObjectModel;

namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a component that calculates a statistic during algorithm execution.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>Statistic</b> class represents any computation of data contained by a <see cref="IPopulation"/>.
    /// After each generation is created, <see cref="IStatistic.GetResultValue(IPopulation)"/> is
    /// invoked with the <see cref="IPopulation"/> of that generation to calculate its data.
    /// </para>
    /// </remarks>
    public interface IStatistic : IGeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Calculates a statistical value from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> from which to derive a statistic.</param>
        /// <returns>Value of the statistic derived from <paramref name="population"/>.</returns>
        /// <remarks>
        /// This method is called once for each generation.
        /// </remarks>
        object GetResultValue(IPopulation population);

        /// <summary>
        /// Returns the statistic results of the indicated population for this statistic.
        /// </summary>
        /// <param name="populationId">ID of the population for which to return statistic results.</param>
        /// <returns>Collection of <see cref="StatisticResult"/> objects.</returns>
        /// <remarks>
        /// Each <see cref="StatisticResult"/> object in the collection is associated
        /// with one generation of the population.
        /// </remarks>
        ObservableCollection<StatisticResult> GetResults(int populationId);
    }
}
