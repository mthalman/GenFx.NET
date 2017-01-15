using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm terminator.
    /// </summary>
    /// <remarks>
    /// The <b>Terminator</b> class defines when a genetic algorithm should stop executing.
    /// </remarks>
    public abstract class TerminatorBase : GeneticComponentWithAlgorithm, ITerminator
    {
        /// <summary>
        /// When overriden in a derived class, returns whether the genetic algorithm should stop
        /// executing.
        /// </summary>
        /// <returns>True if the genetic algorithm is to stop executing; otherwise, false.</returns>
        public abstract bool IsComplete();
    }
}
