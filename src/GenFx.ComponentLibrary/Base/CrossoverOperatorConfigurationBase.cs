using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="CrossoverOperatorBase{TCrossover, TConfiguration}"/>.
    /// </summary>
    public abstract class CrossoverOperatorConfigurationBase<TConfiguration, TCrossover> : ComponentConfiguration<TConfiguration, TCrossover>, ICrossoverOperatorConfiguration
        where TConfiguration : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>
        where TCrossover : CrossoverOperatorBase<TCrossover, TConfiguration>
    {
        private const double DefaultCrossoverRate = .7;
        private const double CrossoverRateMin = 0;
        private const double CrossoverRateMax = 1;

        private double crossoverRate = DefaultCrossoverRate;

        /// <summary>
        /// Gets or sets the probability that two <see cref="IGeneticEntity"/> objects will crossover after being selected.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DoubleValidator(MinValue = CrossoverRateMin, MaxValue = CrossoverRateMax)]
        public double CrossoverRate
        {
            get { return this.crossoverRate; }
            set { this.SetProperty(ref this.crossoverRate, value); }
        }
    }
}
