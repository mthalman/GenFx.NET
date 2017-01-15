using System;
using System.Threading.Tasks;

namespace GenFx.Contracts
{
    /// <summary>
    /// Represents the "organism" which undergoes evolution in the algorithm.
    /// </summary>
    public interface IGeneticEntity : IGeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets or sets the number of generations this entity has survived without being altered.
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// Gets or sets the fitness value of the entity after it has been scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        double ScaledFitnessValue { get; set; }

        /// <summary>
        /// Gets the fitness value of the entity before being scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        double RawFitnessValue { get; }

        /// <summary>
        /// Gets the string representation of the entity.
        /// </summary>
        string Representation { get; }
        
        /// <summary>
        /// Returns a clone of this entity.
        /// </summary>
        IGeneticEntity Clone();

        /// <summary>
        /// Copies the state from this instance to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IGeneticEntity"/> to which state is to be copied.</param>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to copy the state of this instance
        /// to the entity passed in.
        /// </para>
        /// <para>
        /// <b>Notes to inheritors:</b> When overriding this method, it is necessary to call the
        /// <b>CopyTo</b> method of the base class.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        void CopyTo(IGeneticEntity entity);

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
