using GenFx.ComponentLibrary.Base;
using GenFx.Validation;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="MultiPointCrossoverOperator{TCrossover, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TCrossover">Type of the associated crossover operator class.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint")]
    public abstract class MultiPointCrossoverOperatorConfiguration<TConfiguration, TCrossover> : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>, IMultiPointCrossoverOperatorConfiguration
        where TConfiguration : MultiPointCrossoverOperatorConfiguration<TConfiguration, TCrossover>
        where TCrossover : MultiPointCrossoverOperator<TCrossover, TConfiguration>
    {
        private const int DefaultCrossoverPointCount = CrossoverRateMin;
        private const int CrossoverRateMin = 2;

        private int crossoverPointCount = DefaultCrossoverPointCount;
        private bool usePartiallyMatchedCrossover;

        /// <summary>
        /// Gets or sets the number of crossover points to use.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = CrossoverRateMin)]
        [CustomValidator(typeof(MultiPointCrossoverOperatorCrossoverPointValidator))]
        public int CrossoverPointCount
        {
            get { return this.crossoverPointCount; }
            set { this.SetProperty(ref this.crossoverPointCount, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use partially matched crossover.
        /// </summary>
        [CustomValidator(typeof(MultiPointCrossoverOperatorCrossoverPointValidator))]
        public bool UsePartiallyMatchedCrossover
        {
            get { return this.usePartiallyMatchedCrossover; }
            set { this.SetProperty(ref this.usePartiallyMatchedCrossover, value); }
        }
    }
}
