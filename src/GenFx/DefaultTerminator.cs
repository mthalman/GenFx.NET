using GenFx.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace GenFx
{
    /// <summary>
    /// Represents a <see cref="ITerminator"/> that never completes.
    /// </summary>
    internal class DefaultTerminator : ITerminator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="DefaultTerminator"/>.</param>
        /// <param name="configuration">The configuration associated with the terminator.</param>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "algorithm")]
        public DefaultTerminator(IGeneticAlgorithm algorithm, DefaultTerminatorConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IComponentConfiguration Configuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns whether the genetic algorithm should stop executing.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public bool IsComplete()
        {
            return false;
        }

        public void RestoreState(KeyValueMap state)
        {
        }

        public void SetSaveState(KeyValueMap state)
        {
        }
    }
}
