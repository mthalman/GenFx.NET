using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Elitism
{
    /// <summary>
    /// Provides a basic implementation for elitism in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// Elitism in genetic algorithms is an addition to the selection operator.  It causes the
    /// genetic algorithm to have some number of genetic entities remain unchanged and brought forth to the
    /// next generation.  An <see cref="SimpleElitismStrategy"/> acts upon a <see cref="Population"/> to
    /// select those <see cref="GeneticEntity"/> objects which are determined to be "elite".  The number
    /// of genetic entities chosen is based on the <see cref="ElitismStrategy.ElitistRatio"/> property value.
    /// </remarks>
    [DataContract]
    public sealed class SimpleElitismStrategy : ElitismStrategy
    {
    }
}
