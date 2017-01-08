using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="StatisticBase{TConfiguration, TStatistic}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TStatistic">Type of the associated statistic class.</typeparam>
    public abstract class StatisticFactoryConfigBase<TConfiguration, TStatistic> : ConfigurationForComponentWithAlgorithm<TConfiguration, TStatistic>, IStatisticFactoryConfig
        where TConfiguration : StatisticFactoryConfigBase<TConfiguration, TStatistic> 
        where TStatistic : StatisticBase<TStatistic, TConfiguration>
    {
    }
}
