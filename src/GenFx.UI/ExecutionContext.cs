using System;

namespace GenFx.UI
{
    /// <summary>
    /// Represents the UI context for executing a <see cref="GenFx.GeneticAlgorithm"/>.
    /// </summary>
    public class ExecutionContext : ObservableObject
    {
        private ExecutionState executionState = ExecutionState.Idle;
        private Exception algorithmException;

        /// <summary>
        /// Gets the <see cref="GenFx.GeneticAlgorithm"/> associated with this context.
        /// </summary>
        public GeneticAlgorithm GeneticAlgorithm { get; }

        /// <summary>
        /// Gets the <see cref="UI.ExecutionState"/> of the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public ExecutionState ExecutionState
        {
            get { return this.executionState; }
            internal set
            {
                this.SetProperty(ref this.executionState, value);

                if (this.ExecutionState == ExecutionState.Idle)
                {
                    this.AlgorithmException = null;
                }
            }
        }

        /// <summary>
        /// Gets the exception that was thrown, if any, while attempting to execute the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public Exception AlgorithmException
        {
            get { return this.algorithmException; }
            internal set { this.SetProperty(ref this.algorithmException, value); }
        }

        /// <summary>
        /// Initializes the state of this class.
        /// </summary>
        /// <param name="algorithm">The <see cref="GenFx.GeneticAlgorithm"/> associated with this context.</param>
        public ExecutionContext(GeneticAlgorithm algorithm)
        {
            this.GeneticAlgorithm = algorithm;
        }
    }
}
