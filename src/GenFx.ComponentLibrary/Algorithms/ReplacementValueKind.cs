namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Indicates the kind of value being used to indicate the number of <see cref="IGeneticEntity"/> objects
    /// to replace.
    /// </summary>
    /// <seealso cref="PopulationReplacementValue"/>
    public enum ReplacementValueKind
    {
        /// <summary>
        /// Indicates the <see cref="SteadyStateGeneticAlgorithmConfiguration{TConfiguration, TAlgorithm}.PopulationReplacementValue"/>
        /// property value represents a fixed number of <see cref="IGeneticEntity"/> objects to be replaced.
        /// </summary>
        FixedCount = 0,

        /// <summary>
        /// Indicates the <see cref="SteadyStateGeneticAlgorithmConfiguration{TConfiguration, TAlgorithm}.PopulationReplacementValue"/>
        /// property value represents a percentage of the <see cref="IGeneticEntity"/> objects to be replaced.
        /// </summary>
        Percentage
    }
}
