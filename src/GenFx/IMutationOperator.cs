using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents a genetic algorithm mutation operator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Mutations in genetic algorithms involve the altering of a single data component in a entity.
    /// For example, a binary string entity has a chance of mutating one of its bits before progressing
    /// to the next generation.  Genetic algorithm mutation is intended to be similar to gene copying 
    /// errors in nature.  Mutations are the driver of randomness in a population.
    /// </para>
    /// </remarks>
    public interface IMutationOperator : IGeneticComponent
    {
        /// <summary>
        /// Attempts to mutate the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IGeneticEntity"/> to be mutated.</param>
        /// <returns>
        /// A potentially mutated clone of the <paramref name="entity"/>.
        /// </returns>
        IGeneticEntity Mutate(IGeneticEntity entity);
    }
}
