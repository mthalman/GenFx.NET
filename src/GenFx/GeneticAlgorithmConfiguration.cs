using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="GeneticAlgorithm{TConfiguration, TAlgorithm}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TAlgorithm">Type of the associated algorithm class.</typeparam>
    public abstract class GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm> : ComponentConfiguration<TConfiguration, TAlgorithm>, IGeneticAlgorithmConfiguration
        where TConfiguration : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
    {
        private const int DefaultEnvironmentSize = 1;

        private int environmentSize = DefaultEnvironmentSize;

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
    }
}
