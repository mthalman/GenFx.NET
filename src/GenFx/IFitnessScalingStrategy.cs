using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents a strategy for genetic algorithms that scales fitness values.
    /// </summary>
    public interface IFitnessScalingStrategy : IGeneticComponent
    {
        /// <summary>
        /// Updates the <see cref="IGeneticEntity.ScaledFitnessValue"/>
        /// of the <see cref="IGeneticEntity"/> objects in the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects.</param>
        void Scale(IPopulation population);
    }
}
