using System.Collections.ObjectModel;

namespace GenFx
{
    internal static class StatisticExtensions
    {
        /// <summary>
        /// Calculates the stats for the <paramref name="environment"/>.
        /// </summary>
        public static void Calculate(this IStatistic statistic, GeneticEnvironment environment, int generationIndex)
        {
            foreach (IPopulation population in environment.Populations)
            {
                ObservableCollection<StatisticResult> populationStats = statistic.GetResults(population.Index);
                StatisticResult result = new StatisticResult(generationIndex, population.Index, statistic.GetResultValue(population), statistic);
                populationStats.Add(result);
            }
        }
    }
}
