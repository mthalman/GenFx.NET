namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a genetic algorithm selection operator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Selection in a genetic algorithm involves choosing a entity from a population to be acted
    /// upon by other operators, such as crossover and mutation, and move to the next generation.  The
    /// general strategy is for a entity to have a higher probability of being selected if it has a higher
    /// fitness value.
    /// </para>
    /// </remarks>
    public interface ISelectionOperator : IGeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the <see cref="FitnessType"/> to base selection of <see cref="IGeneticEntity"/> objects on.
        /// </summary>
        FitnessType SelectionBasedOnFitnessType { get; }

        /// <summary>
        /// Selects a <see cref="IGeneticEntity"/> from <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="IGeneticEntity"/> object that was selected.</returns>
        IGeneticEntity SelectEntity(IPopulation population);
    }
}
