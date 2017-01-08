using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Populations
{
    /// <summary>
    /// Represents a collection of <see cref="IGeneticEntity"/> objects which interact locally with each other.  A population is 
    /// the unit from which the <see cref="ISelectionOperator"/> selects its genetic entities.
    /// </summary>
    /// <remarks>
    /// Populations can be isolated or interactive with one another through migration depending on
    /// which <see cref="IGeneticAlgorithm"/> is used.
    /// </remarks>
    /// <seealso cref="IGeneticAlgorithm"/>
    public sealed class SimplePopulation : PopulationBase<SimplePopulation, SimplePopulationFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="PopulationBase{TPopulation, TConfiguration}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public SimplePopulation(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
