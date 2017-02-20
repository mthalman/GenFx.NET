using System.Collections.ObjectModel;

namespace GenFx
{
    internal static class MetricExtensions
    {
        /// <summary>
        /// Calculates the metrics for the <paramref name="environment"/>.
        /// </summary>
        public static void Calculate(this Metric metric, GeneticEnvironment environment, int generationIndex)
        {
            foreach (Population population in environment.Populations)
            {
                ObservableCollection<MetricResult> populationStats = metric.GetResults(population.Index);
                MetricResult result = new MetricResult(generationIndex, population.Index, metric.GetResultValue(population), metric);
                populationStats.Add(result);
            }
        }
    }
}
