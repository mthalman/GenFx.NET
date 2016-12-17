using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="Statistic{TConfiguration, TStatistic}"/>.
    /// </summary>
    public abstract class StatisticConfiguration<TConfiguration, TStatistic> : ComponentConfiguration<TConfiguration, TStatistic>, IStatisticConfiguration
        where TConfiguration : StatisticConfiguration<TConfiguration, TStatistic> 
        where TStatistic : Statistic<TStatistic, TConfiguration>
    {
    }
}
