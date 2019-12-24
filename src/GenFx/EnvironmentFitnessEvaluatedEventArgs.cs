using System;
using System.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Provides data for the <see cref="GeneticAlgorithm.FitnessEvaluated"/> event.  This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// If the <see cref="GeneticAlgorithm"/> is cancelled, it is still safe to execute its <see cref="GeneticAlgorithm.RunAsync"/> or
    /// <see cref="GeneticAlgorithm.StepAsync"/> methods. This will cause it to resume execution from where it left off.
    /// </remarks>
    public sealed class EnvironmentFitnessEvaluatedEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Gets the <see cref="GeneticEnvironment"/> being used by the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public GeneticEnvironment Environment { get; }

        /// <summary>
        /// Gets the index of the generation that has just been created.
        /// </summary>
        public int GenerationIndex { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentFitnessEvaluatedEventArgs"/> class.
        /// </summary>
        /// <param name="environment">The <see cref="GeneticEnvironment"/> being used by the <see cref="GeneticAlgorithm"/>.</param>
        /// <param name="generationIndex">The index of the generation that has just been created.</param>
        /// <exception cref="ArgumentNullException"><paramref name="environment"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="generationIndex"/> is less than zero.</exception>
        public EnvironmentFitnessEvaluatedEventArgs(GeneticEnvironment environment, int generationIndex)
        {
            if (generationIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(generationIndex), generationIndex, Resources.ErrorMsg_InvalidGenerationIndex);
            }

            this.Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.GenerationIndex = generationIndex;
        }
    }
}
