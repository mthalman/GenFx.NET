using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides a basic implementation for elitism in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// Elitism in genetic algorithms is an addition to the selection operator.  It causes the
    /// genetic algorithm to have some number of genetic entities remain unchanged and brought forth to the
    /// next generation.  An <see cref="ElitismStrategy"/> acts upon a <see cref="IPopulation"/> to
    /// select those <see cref="IGeneticEntity"/> objects which are determined to be "elite".  The number
    /// of genetic entities chosen is based on the <see cref="ElitismStrategyBase.ElitistRatio"/> property value.
    /// </remarks>
    public sealed class ElitismStrategy : ElitismStrategyBase
    {
    }
}
