using GenFx.Contracts;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides a list-based entity with multi-point crossover support.
    /// </summary>
    /// <remarks>
    /// Multi-point crossover chooses multiple list element positions and alternately swaps the elements on
    /// for each of the points between two list-based entities.  For example, if two entities represented
    /// by ADCFBE and BFEACD were to be crossed over at position 2 and 4, the resulting offspring would
    /// be AFEFBE and BDCACD.
    /// 
    /// An option of multi-point crossover is partially-matched crossover support.  With this option, it assumes
    /// each of the elements in the list is unique for that entity and the resulting offspring must share that
    /// unique characteristic.  Using the example above, the two parent entities have unique elements but the 
    /// offspring do not (offspring 1, for example, has 2 F's and 2 E's).  By using the partially matched crossover
    /// option, it would ensure the offspring have unique elements.  It does this by swapping out the original element
    /// that is a duplicate and swapping it out with the element at the same position of the duplicate on the other parent.
    /// So for the example above, this would result in the following offspring: AFEDBC and BDCAEF.  This option is only
    /// available when using two crossover points.
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint")]
    public sealed class MultiPointCrossoverOperator : MultiPointCrossoverOperator<MultiPointCrossoverOperator, MultiPointCrossoverOperatorFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public MultiPointCrossoverOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
