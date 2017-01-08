using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides a basic implementation for elitism in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Elitism in genetic algorithms is an addition to the selection operator.  It causes the
    /// genetic algorithm to have some number of genetic entities remain unchanged and brought forth to the
    /// next generation.  An <see cref="ElitismStrategy"/> acts upon a <see cref="IPopulation"/> to
    /// select those <see cref="IGeneticEntity"/> objects which are determined to be "elite".  The number
    /// of genetic entities chosen is based on the <see cref="ElitismStrategyFactoryConfigBase{TConfiguration, TElitism}.ElitistRatio"/> property value.
    /// </para>
    /// <para>
    /// <b>Notes to inheritors:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentFactoryConfigSet.ElitismStrategy"/> 
    /// property.
    /// </para>
    /// </remarks>
    public sealed class ElitismStrategy : ElitismStrategyBase<ElitismStrategy, ElitismStrategyConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public ElitismStrategy(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
