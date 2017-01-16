namespace GenFx
{
    /// <summary>
    /// Represents a <see cref="Terminator"/> that never completes.
    /// </summary>
    internal class DefaultTerminator : Terminator
    {        
        /// <summary>
        /// Returns whether the genetic algorithm should stop executing.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public override bool IsComplete()
        {
            return false;
        }
    }
}
