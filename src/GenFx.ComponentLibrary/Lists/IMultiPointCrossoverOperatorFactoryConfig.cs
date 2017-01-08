namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of a multipoint crossover operator.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint")]
    public interface IMultiPointCrossoverOperatorFactoryConfig
    {
        /// <summary>
        /// Gets or sets the number of crossover points to use.
        /// </summary>
        int CrossoverPointCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use partially matched crossover.
        /// </summary>
        bool UsePartiallyMatchedCrossover { get; set; }
    }
}
