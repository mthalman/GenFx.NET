using System;

namespace GenFx.ComponentLibrary.Plugins
{
    /// <summary>
    /// Logs statistics for each generation.
    /// </summary>
    public sealed class StatisticLogger : StatisticLogger<StatisticLogger, StatisticLoggerConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public StatisticLogger(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
