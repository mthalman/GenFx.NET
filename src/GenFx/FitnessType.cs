using GenFx.Contracts;

namespace GenFx
{
    /// <summary>
    /// Indicates the kind of fitness value of a <see cref="IGeneticEntity"/>.
    /// </summary>
    public enum FitnessType
    {
        /// <summary>
        /// Indicates a <see cref="IGeneticEntity"/> fitness value that has been scaled by a <see cref="IFitnessScalingStrategy"/> object.
        /// </summary>
        Scaled = 0,

        /// <summary>
        /// Indicates the unmodified, raw fitness value of a <see cref="IGeneticEntity"/>.
        /// </summary>
        Raw
    }
}
