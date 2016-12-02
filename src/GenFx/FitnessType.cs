namespace GenFx
{
    /// <summary>
    /// Indicates the kind of fitness value of a <see cref="GeneticEntity"/>.
    /// </summary>
    public enum FitnessType
    {
        /// <summary>
        /// Indicates a <see cref="GeneticEntity"/> fitness value that has been scaled by a <see cref="FitnessScalingStrategy"/> object.
        /// </summary>
        Scaled = 0,

        /// <summary>
        /// Indicates the unmodified, raw fitness value of a <see cref="GeneticEntity"/>.
        /// </summary>
        Raw
    }

    /// <summary>
    /// Contains helper methods for the <see cref="FitnessType"/> enum.
    /// </summary>
    internal static class FitnessTypeHelper
    {
        /// <summary>
        /// Returns whether the <paramref name="fitnessType"/> is a defined value.
        /// </summary>
        /// <param name="fitnessType">The <see cref="FitnessType"/> to check.</param>
        /// <returns>True if the value is defined; otherwise, false.</returns>
        public static bool IsDefined(FitnessType fitnessType)
        {
            if (fitnessType < FitnessType.Scaled || fitnessType > FitnessType.Raw)
            {
                return false;
            }

            return true;
        }
    }
}
