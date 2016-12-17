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
}
