using System.Collections.Generic;

namespace GenFx.Contracts
{
    /// <summary>
    /// Represents an operator which crosses over subparts of entities.
    /// </summary>
    public interface ICrossoverOperator : IGeneticComponent
    {
        /// <summary>
        /// Attempts to perform a crossover between <paramref name="entity1"/> and <paramref name="entity2"/>
        /// if a random value is within the range of the <see cref="ICrossoverOperatorFactoryConfig.CrossoverRate"/>.
        /// </summary>
        /// <param name="entity1"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="IGeneticEntity"/> objects resulting from the crossover.  If no
        /// crossover occurred, this collection contains the original values of <paramref name="entity1"/>
        /// and <paramref name="entity2"/>.
        /// </returns>
        IList<IGeneticEntity> Crossover(IGeneticEntity entity1, IGeneticEntity entity2);
    }
}
