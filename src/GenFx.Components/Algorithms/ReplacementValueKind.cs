namespace GenFx.Components.Algorithms
{
    /// <summary>
    /// Indicates the kind of value being used to indicate the number of <see cref="GeneticEntity"/> objects
    /// to replace.
    /// </summary>
    /// <seealso cref="PopulationReplacementValue"/>
    public enum ReplacementValueKind
    {
        /// <summary>
        /// Indicates the <see cref="SteadyStateGeneticAlgorithm.PopulationReplacementValue"/>
        /// property value represents a fixed number of <see cref="GeneticEntity"/> objects to be replaced.
        /// </summary>
        FixedCount = 0,

        /// <summary>
        /// Indicates the <see cref="SteadyStateGeneticAlgorithm.PopulationReplacementValue"/>
        /// property value represents a percentage of the <see cref="GeneticEntity"/> objects to be replaced.
        /// </summary>
        Percentage
    }
}
