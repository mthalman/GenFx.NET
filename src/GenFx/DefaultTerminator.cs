using GenFx.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace GenFx
{
    /// <summary>
    /// Represents a <see cref="ITerminator"/> that never completes.
    /// </summary>
    internal class DefaultTerminator : GeneticComponentWithAlgorithm, ITerminator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTerminator"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "algorithm")]
        public DefaultTerminator()
        {
        }
        
        /// <summary>
        /// Returns whether the genetic algorithm should stop executing.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public bool IsComplete()
        {
            return false;
        }
    }
}
