using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="Population{TConfiguration, TPopulation}"/>.
    /// </summary>
    public abstract class PopulationConfiguration<TConfiguration, TPopulation> : ComponentConfiguration<TConfiguration, TPopulation>, IPopulationConfiguration
        where TConfiguration : PopulationConfiguration<TConfiguration, TPopulation>
        where TPopulation : Population<TPopulation, TConfiguration>
    {
        private const int DefaultPopulationSize = 1;

        private int populationSize = DefaultPopulationSize;

        /// <summary>
        /// Gets or sets the number of <see cref="IGeneticEntity"/> objects that are contained by a population.
        /// </summary>
        /// <remarks>
        /// This value is defaulted to 1 and must be greater or equal to 1 to be valid for executing
        /// a genetic algorithm.
        /// </remarks>
        /// <exception cref="ValidationException">Value is invalid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int PopulationSize
        {
            get { return this.populationSize; }
            set { this.SetProperty(ref this.populationSize, value); }
        }
    }
}
