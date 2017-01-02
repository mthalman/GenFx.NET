using GenFx.ComponentLibrary.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="ElitismStrategyBase{TElitism, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TElitism">Type of the associated elitism strategy class.</typeparam>
    public abstract class ElitismStrategyConfigurationBase<TConfiguration, TElitism> : ConfigurationForComponentWithAlgorithm<TConfiguration, TElitism>, IElitismStrategyConfiguration
        where TConfiguration : ElitismStrategyConfigurationBase<TConfiguration, TElitism>
        where TElitism : ElitismStrategyBase<TElitism, TConfiguration>
    {
        private const double DefaultElitistRatio = .1;
        private double elitistRatio = DefaultElitistRatio;

        /// <summary>
        /// Gets or sets the ratio of <see cref="IGeneticEntity"/> objects that will be selected as elite and move on 
        /// to the next generation unchanged.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DoubleValidator(MinValue = 0, MaxValue = 1)]
        public double ElitistRatio
        {
            get { return this.elitistRatio; }
            set { this.SetProperty(ref this.elitistRatio, value); }
        }
    }
}
