using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="ElitismStrategy{TElitism, TConfiguration}"/>.
    /// </summary>
    public class ElitismStrategyConfiguration<TConfiguration, TElitism> : ComponentConfiguration<TConfiguration, TElitism>, IElitismStrategyConfiguration
        where TConfiguration : ElitismStrategyConfiguration<TConfiguration, TElitism>
        where TElitism : ElitismStrategy<TElitism, TConfiguration>
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
