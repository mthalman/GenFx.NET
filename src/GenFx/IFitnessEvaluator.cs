using GenFx.ComponentModel;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Represents a component which evaluates the fitness of entities.
    /// </summary>
    public interface IFitnessEvaluator : IGeneticComponent
    {
        /// <summary>
        /// Returns the calculated fitness value of the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IGeneticEntity"/> whose calculated fitness value is to be returned.</param>
        /// <returns>
        /// The calculated fitness value of the <paramref name="entity"/>.
        /// </returns>
        Task<double> EvaluateFitnessAsync(IGeneticEntity entity);
    }
}
