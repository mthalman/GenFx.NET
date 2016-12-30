using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// A type of genetic algorithm that maintains multiple isolated populations and then migrates
    /// a select number of genetic entities between the populations after each generation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The number of <see cref="IGeneticEntity"/> objects that migrate each generation is determined by the 
    /// <see cref="MultiDemeGeneticAlgorithmConfiguration{TConfiguration, TAlgorithm}.MigrantCount"/> property value.  Those <see cref="IGeneticEntity"/>
    /// objects with the highest fitness value are the ones chosen to be migrated.
    /// </para>
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    public sealed class MultiDemeGeneticAlgorithm : MultiDemeGeneticAlgorithm<MultiDemeGeneticAlgorithm, MultiDemeGeneticAlgorithmConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configurationSet">Contains the component configuration for the algorithm.</param>
        public MultiDemeGeneticAlgorithm(ComponentConfigurationSet configurationSet)
            : base(configurationSet)
        {
        }
    }
}
