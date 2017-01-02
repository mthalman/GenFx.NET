using GenFx.ComponentLibrary.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="PopulationBase{TConfiguration, TPopulation}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TPopulation">Type of the associated population class.</typeparam>
    public abstract class PopulationConfigurationBase<TConfiguration, TPopulation> : ConfigurationForComponentWithAlgorithm<TConfiguration, TPopulation>, IPopulationConfiguration
        where TConfiguration : PopulationConfigurationBase<TConfiguration, TPopulation>
        where TPopulation : PopulationBase<TPopulation, TConfiguration>
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
