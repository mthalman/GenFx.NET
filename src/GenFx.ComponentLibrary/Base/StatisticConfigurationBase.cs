using GenFx.ComponentLibrary.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="StatisticBase{TConfiguration, TStatistic}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TStatistic">Type of the associated statistic class.</typeparam>
    public abstract class StatisticConfigurationBase<TConfiguration, TStatistic> : ConfigurationForComponentWithAlgorithm<TConfiguration, TStatistic>, IStatisticConfiguration
        where TConfiguration : StatisticConfigurationBase<TConfiguration, TStatistic> 
        where TStatistic : StatisticBase<TStatistic, TConfiguration>
    {
    }
}
