using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby the <see cref="IGeneticEntity"/> objects in a <see cref="IPopulation"/>
    /// are selected according to their fitness rank in comparison to the result of the <see cref="IPopulation"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Selection is then based on this ranking rather than on absolute fitness.  This technique avoids 
    /// selecting only a few of highly fit <see cref="IGeneticEntity"/> objects and thus can prevent premature 
    /// convergence.  But it also loses the perhaps important distinguishment of absolute fitness values 
    /// later in a run.  Use of a <see cref="IFitnessScalingStrategy"/> object does not have an impact 
    /// when <b>RankSelectionOperator</b> is being used since absolute differences in fitness are ignored.
    /// </para>
    /// </remarks>
    public sealed class RankSelectionOperator : RankSelectionOperator<RankSelectionOperator, RankSelectionOperatorFactoryConfig>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public RankSelectionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
