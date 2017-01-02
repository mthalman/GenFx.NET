using GenFx.Validation;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the configuration of <see cref="SteadyStateGeneticAlgorithm{TAlgorithm, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TAlgorithm">Type of the associated algorithm class.</typeparam>
    public abstract class SteadyStateGeneticAlgorithmConfiguration<TConfiguration, TAlgorithm> : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm>
        where TConfiguration : GeneticAlgorithmConfiguration<TConfiguration, TAlgorithm> 
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
    {
        private PopulationReplacementValue replacementValue = new PopulationReplacementValue(10, ReplacementValueKind.Percentage);

        /// <summary>
        /// Gets or sets the value indicating how many members of the the <see cref="IPopulation"/> are to 
        /// be replaced with the offspring of the previous generation.
        /// </summary>
        /// <value>
        /// A value representing a fixed amount of <see cref="IGeneticEntity"/> objects to be replaced
        /// or the percentage that is to be replaced.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [CustomValidator(typeof(PopulationReplacementValueValidator))]
        public PopulationReplacementValue PopulationReplacementValue
        {
            get { return this.replacementValue; }
            set { this.SetProperty(ref this.replacementValue, value); }
        }    
    }
}
