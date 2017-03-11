using System;
using System.Threading.Tasks;

namespace GenFx.UI.ViewModels
{
    /// <summary>
    /// Represents the view model for the <see cref="ExecutionPanel"/> control.
    /// </summary>
    internal class ExecutionPanelViewModel : IDisposable
    {
        private ExecutionContext context;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="context">The <see cref="ExecutionContext"/> that provides the context for executing a <see cref="GeneticAlgorithm"/>.</param>
        public ExecutionPanelViewModel(ExecutionContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns a value indicating whether the associated <see cref="GeneticAlgorithm"/> can have its execution started.
        /// </summary>
        /// <returns>True if execution can be started; otherwise, false.</returns>
        public bool CanStartExecution()
        {
            return (this.context.ExecutionState == ExecutionState.Idle ||
                this.context.ExecutionState == ExecutionState.Paused);
        }

        /// <summary>
        /// Returns a value indicating whether the associated <see cref="GeneticAlgorithm"/> can have its execution paused.
        /// </summary>
        /// <returns>True if execution can be paused; otherwise, false.</returns>
        public bool CanPauseExecution()
        {
            return (this.context.ExecutionState == ExecutionState.Running);
        }

        /// <summary>
        /// Returns a value indicating whether the associated <see cref="GeneticAlgorithm"/> can have its execution stopped.
        /// </summary>
        /// <returns>True if execution can be stopped; otherwise, false.</returns>
        public bool CanStopExecution()
        {
            return (this.context.ExecutionState == ExecutionState.Running ||
                this.context.ExecutionState == ExecutionState.Paused);
        }

        /// <summary>
        /// Starts the execution of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        /// <returns>The <see cref="Task"/> associated with the execution.</returns>
        public Task StartExecutionAsync()
        {
            return this.ExecuteAlgorithmCoreAsync(this.context.GeneticAlgorithm.RunAsync);
        }

        /// <summary>
        /// Executes one generation of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        /// <returns>The <see cref="Task"/> associated with the execution.</returns>
        public Task StepExecutionAsync()
        {
            return this.ExecuteAlgorithmCoreAsync(async () =>
            {
                bool isComplete = true;

                try
                {
                    isComplete = await this.context.GeneticAlgorithm.StepAsync();
                }
                finally
                {
                    if (isComplete)
                    {
                        this.context.ExecutionState = ExecutionState.Idle;
                    }
                    else
                    {
                        this.context.ExecutionState = ExecutionState.Paused;
                    }
                }
            });
        }

        /// <summary>
        /// Stops the execution of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public void StopExecution()
        {
            if (this.context.ExecutionState != ExecutionState.Paused)
            {
                this.context.ExecutionState = ExecutionState.IdlePending;
            }
            else
            {
                this.context.ExecutionState = ExecutionState.Idle;
            }
        }

        /// <summary>
        /// Pauses the execution of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public void PauseExecution()
        {
            this.context.ExecutionState = ExecutionState.PausePending;
        }

        /// <summary>
        /// Disposes the state of this object.
        /// </summary>
        public void Dispose()
        {
            this.UnsubscribeFromAlgorithmEvents();
        }

        private async Task ExecuteAlgorithmCoreAsync(Func<Task> executeAlgorithm)
        {
            ExecutionState state = this.context.ExecutionState;
            if (state == ExecutionState.Idle)
            {
                this.context.GeneticAlgorithm.FitnessEvaluated += this.GeneticAlgorithm_FitnessEvaluated;
                this.context.GeneticAlgorithm.AlgorithmCompleted += this.GeneticAlgorithm_AlgorithmCompleted;

                try
                {
                    await this.context.GeneticAlgorithm.InitializeAsync();
                }
                catch (Exception e)
                {
                    this.context.AlgorithmException = e;
                    throw e;
                }
            }

            this.context.ExecutionState = ExecutionState.Running;

            try
            {
                await executeAlgorithm();
            }
            catch (Exception e)
            {
                this.context.AlgorithmException = e;
                throw e;
            }
        }

        /// <summary>
        /// Handles the event when the associated <see cref="GeneticAlgorithm"/> completes execution.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> associated with the event.</param>
        private void GeneticAlgorithm_AlgorithmCompleted(object sender, EventArgs e)
        {
            this.context.ExecutionState = ExecutionState.Idle;
            this.UnsubscribeFromAlgorithmEvents();
        }

        /// <summary>
        /// Handles the event when the associated <see cref="GeneticAlgorithm"/> has evaluted the fitness of a generation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="EnvironmentFitnessEvaluatedEventArgs"/> associated with the event.</param>
        private void GeneticAlgorithm_FitnessEvaluated(object sender, EnvironmentFitnessEvaluatedEventArgs e)
        {
            if (this.context.ExecutionState == ExecutionState.PausePending ||
                this.context.ExecutionState == ExecutionState.IdlePending)
            {
                e.Cancel = true;

                if (this.context.ExecutionState == ExecutionState.PausePending)
                {
                    this.context.ExecutionState = ExecutionState.Paused;
                }
                else
                {
                    this.context.ExecutionState = ExecutionState.Idle;
                    this.UnsubscribeFromAlgorithmEvents();
                }
            }
        }

        /// <summary>
        /// Unsubscribes from the necessary events on the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        private void UnsubscribeFromAlgorithmEvents()
        {
            this.context.GeneticAlgorithm.FitnessEvaluated -= this.GeneticAlgorithm_FitnessEvaluated;
            this.context.GeneticAlgorithm.AlgorithmCompleted -= this.GeneticAlgorithm_AlgorithmCompleted;
        }
    }
}
