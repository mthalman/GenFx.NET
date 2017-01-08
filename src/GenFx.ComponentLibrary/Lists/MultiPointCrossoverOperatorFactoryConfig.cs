using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="MultiPointCrossoverOperator"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint")]
    public sealed class MultiPointCrossoverOperatorFactoryConfig : MultiPointCrossoverOperatorFactoryConfig<MultiPointCrossoverOperatorFactoryConfig, MultiPointCrossoverOperator>
    {
    }
}
