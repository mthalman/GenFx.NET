using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="StatisticBase{TConfiguration, TStatistic}"/>.
    /// </summary>
    public abstract class StatisticConfigurationBase<TConfiguration, TStatistic> : ComponentConfiguration<TConfiguration, TStatistic>, IStatisticConfiguration
        where TConfiguration : StatisticConfigurationBase<TConfiguration, TStatistic> 
        where TStatistic : StatisticBase<TStatistic, TConfiguration>
    {
    }
}
