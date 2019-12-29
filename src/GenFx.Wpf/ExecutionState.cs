namespace GenFx.Wpf
{
    /// <summary>
    /// Indicates the execution state of a <see cref="GeneticAlgorithm"/>.
    /// </summary>
    public enum ExecutionState
    {
        /// <summary>
        /// Indicates that the <see cref="GeneticAlgorithm"/> is not running and not paused.
        /// </summary>
        Idle,

        /// <summary>
        /// Indicates that the <see cref="GeneticAlgorithm"/> is currently running but will be idle at the next available opportunity.
        /// </summary>
        IdlePending,
        
        /// <summary>
        /// Indicates that the <see cref="GeneticAlgorithm"/> is currently paused.
        /// </summary>
        Paused,

        /// <summary>
        /// Indicates that the <see cref="GeneticAlgorithm"/> is currently running but will be paused at the next available opportunity.
        /// </summary>
        PausePending,

        /// <summary>
        /// Indicates that the <see cref="GeneticAlgorithm"/> is currently running.
        /// </summary>
        Running
    }
}
