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
}
