using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="MutationOperatorBase{TMutation, TConfiguration}"/>.
    /// </summary>
    public abstract class MutationOperatorConfigurationBase<TConfiguration, TMutation> : ComponentConfiguration<TConfiguration, TMutation>, IMutationOperatorConfiguration
        where TMutation : MutationOperatorBase<TMutation, TConfiguration>
        where TConfiguration : MutationOperatorConfigurationBase<TConfiguration, TMutation>
    {
        private const double DefaultMutationRate = .001;

        private double mutationRate = DefaultMutationRate;

        /// <summary>
        /// Gets or sets the probability that a data segment within a <see cref="IGeneticEntity"/> will become mutated.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DoubleValidator(MinValue = 0, MaxValue = 1)]
        public double MutationRate
        {
            get { return this.mutationRate; }
            set { this.SetProperty(ref this.mutationRate, value); }
        }
    }
}
