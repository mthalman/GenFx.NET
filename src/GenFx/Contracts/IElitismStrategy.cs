using System.Collections.Generic;

namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a strategy for calculating elitism in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Elitism in genetic algorithms is an addition to the selection operator.  It causes the
    /// genetic algorithm to have some number of genetic entities remain unchanged and brought forth to the
    /// next generation.  An <see cref="IElitismStrategy"/> acts upon a <see cref="IPopulation"/> to
    /// select those <see cref="IGeneticEntity"/> objects which are determined to be "elite".  The number
    /// of genetic entities chosen is based on the <see cref="IElitismStrategy.ElitistRatio"/> property value.
    /// </para>
    /// </remarks>
    public interface IElitismStrategy : IGeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the ratio of <see cref="IGeneticEntity"/> objects that will be selected as elite and move on 
        /// to the next generation unchanged.
        /// </summary>
        double ElitistRatio { get; }

        /// <summary>
        /// Returns the collection of <see cref="IGeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite and move on to the next generation unchanged.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects
        /// from which to select.</param>
        /// <returns>
        /// The collection of <see cref="IGeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite.
        /// </returns>
        IList<IGeneticEntity> GetEliteEntities(IPopulation population);
    }
}
