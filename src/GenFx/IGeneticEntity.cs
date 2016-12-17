using GenFx.ComponentModel;
using System;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Represents the "organism" which undergoes evolution in the algorithm.
    /// </summary>
    public interface IGeneticEntity : IGeneticComponent
    {
        /// <summary>
        /// Gets or sets the number of generations this entity has survived without being altered.
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// Gets the fitness value of the entity after it has been scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        double ScaledFitnessValue { get; }

        /// <summary>
        /// Gets the fitness value of the entity before being scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        double RawFitnessValue { get; }

        /// <summary>
        /// Returns a clone of this entity.
        /// </summary>
        IGeneticEntity Clone();
        
        /// <summary>
        /// Evaluates the <see cref="IGeneticEntity.RawFitnessValue"/> of the entity.
        /// </summary>
        Task EvaluateFitnessAsync();

        /// <summary>
        /// Returns the appropriate fitness value of the entity based on the the <paramref name="fitnessType"/>.
        /// </summary>
        /// <param name="fitnessType"><see cref="FitnessType"/> indicating which fitness value to
        /// return.</param>
        /// <returns>The appropriate fitness value of the entity.</returns>
        /// <exception cref="ArgumentException"><paramref name="fitnessType"/> value is undefined.</exception>
        double GetFitnessValue(FitnessType fitnessType);
    }
}
