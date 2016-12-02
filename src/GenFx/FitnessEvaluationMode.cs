namespace GenFx
{
    /// <summary>
    /// Represents how the fitness value of an entity is to be treated.
    /// </summary>
    public enum FitnessEvaluationMode
    {
        /// <summary>
        /// Indicates that lower fitness values are better.
        /// </summary>
        Minimize = 0,

        /// <summary>
        /// Indicates that higher fitness values are better.
        /// </summary>
        Maximize
    }

    /// <summary>
    /// Contains helper methods for the <see cref="FitnessEvaluationMode"/> enum.
    /// </summary>
    internal static class FitnessEvaluationModeHelper
    {
        /// <summary>
        /// Returns whether the <paramref name="mode"/> is a defined value.
        /// </summary>
        /// <param name="mode">The <see cref="FitnessEvaluationMode"/> to check.</param>
        /// <returns>True if the value is defined; otherwise, false.</returns>
        public static bool IsDefined(FitnessEvaluationMode mode)
        {
            if (mode < FitnessEvaluationMode.Minimize || mode > FitnessEvaluationMode.Maximize)
            {
                return false;
            }

            return true;
        }
    }
}
