using System;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Plugin component that provides custom extension functionality.
    /// </summary>
    [DataContract]
    public abstract class Plugin : GeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(GeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

#pragma warning disable CA1062 // Validate arguments of public methods
            algorithm.FitnessEvaluated += Algorithm_FitnessEvaluated;
            algorithm.AlgorithmStarting += Algorithm_AlgorithmStarting;
            algorithm.AlgorithmCompleted += Algorithm_AlgorithmCompleted;
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        private void Algorithm_AlgorithmCompleted(object sender, EventArgs e)
        {
            this.OnAlgorithmCompleted();
        }

        private void Algorithm_AlgorithmStarting(object sender, EventArgs e)
        {
            this.OnAlgorithmStarting();
        }

        private void Algorithm_FitnessEvaluated(object sender, EnvironmentFitnessEvaluatedEventArgs e)
        {
            this.OnFitnessEvaluated(e.Environment, e.GenerationIndex);
        }

        /// <summary>
        /// Handles the event when the fitness of an environment has been evaluated.
        /// </summary>
        /// <param name="environment">The environment which has had its fitness evaluated..</param>
        /// <param name="generationIndex">Index value of the current generation in the environment.</param>
        protected virtual void OnFitnessEvaluated(GeneticEnvironment environment, int generationIndex)
        {
        }

        /// <summary>
        /// Handles the event when a genetic algorithm is about to start execution.
        /// </summary>
        protected virtual void OnAlgorithmStarting()
        {
        }

        /// <summary>
        /// Handles the event when a genetic algorithm has finished executing.
        /// </summary>
        protected virtual void OnAlgorithmCompleted()
        {
        }
    }
}
