using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="GeneticAlgorithm{TConfiguration, TAlgorithm}"/>.
    /// </summary>
    public abstract class GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm> : ComponentConfiguration<TConfiguration, TAlgorithm>, IGeneticAlgorithmConfiguration
        where TConfiguration : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
    {
        private const int DefaultEnvironmentSize = 1;
        private const bool DefaultStatisticsEnabled = true;

        private int environmentSize = DefaultEnvironmentSize;
        private bool statisticsEnabled = DefaultStatisticsEnabled;

        /// <summary>
        /// Gets or sets the number of <see cref="IPopulation"/> objects that are contained by the <see cref="GeneticEnvironment"/>.
        /// </summary>
        /// <value>
        /// The number of populations that are contained by the <see cref="GeneticEnvironment"/>.
        /// This value is defaulted to 1 and must be greater or equal to 1.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int EnvironmentSize
        {
            get { return this.environmentSize; }
            set { this.SetProperty(ref this.environmentSize, value); }
        }

        /// <summary>
        /// Gets or sets whether statistics should be calculated during genetic algorithm execution.
        /// </summary>
        /// <value>True if statistics should be calculated; otherwise, false.</value>
        public bool StatisticsEnabled
        {
            get { return this.statisticsEnabled; }
            set { this.SetProperty(ref this.statisticsEnabled, value); }
        }
    }
}
