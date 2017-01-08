using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby a <see cref="IGeneticEntity"/> object's probability of being
    /// selected is directly proportional to its fitness value compared to the rest of the <see cref="IPopulation"/>
    /// to which it belongs.
    /// </summary>
    public sealed class FitnessProportionateSelectionOperator : FitnessProportionateSelectionOperator<FitnessProportionateSelectionOperator, FitnessProportionateSelectionOperatorFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public FitnessProportionateSelectionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
