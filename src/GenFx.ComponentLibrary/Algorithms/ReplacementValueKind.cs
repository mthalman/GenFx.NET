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

    /// <summary>
    /// Contains helper methods for the <see cref="ReplacementValueKind"/> enum.
    /// </summary>
    internal static class ReplacementValueKindHelper
    {
        /// <summary>
        /// Returns whether the <paramref name="valueKind"/> is a defined value.
        /// </summary>
        /// <param name="valueKind">The <see cref="ReplacementValueKind"/> to check.</param>
        /// <returns>True if the value is defined; otherwise, false.</returns>
        public static bool IsDefined(ReplacementValueKind valueKind)
        {
            if (valueKind < ReplacementValueKind.FixedCount || valueKind > ReplacementValueKind.Percentage)
            {
                return false;
            }

            return true;
        }
    }
}
