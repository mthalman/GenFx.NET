using System;
using System.ComponentModel;
using GenFx.Properties;

namespace GenFx
{
    /// <summary>
    /// Provides data for the <see cref="IGeneticAlgorithm.FitnessEvaluated"/> event.  This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// If the <see cref="IGeneticAlgorithm"/> is cancelled, it is still safe to execute its <see cref="IGeneticAlgorithm.RunAsync"/> or
    /// <see cref="IGeneticAlgorithm.StepAsync"/> methods. This will cause it to resume execution from where it left off.
    /// </remarks>
    public sealed class EnvironmentFitnessEvaluatedEventArgs : CancelEventArgs
    {
        private GeneticEnvironment environment;
        private int generationIndex;

        /// <summary>
        /// Gets the <see cref="GeneticEnvironment"/> being used by the <see cref="IGeneticAlgorithm"/>.
        /// </summary>
        public GeneticEnvironment Environment
        {
            get { return this.environment; }
        }

        /// <summary>
        /// Gets the index of the generation that has just been created.
        /// </summary>
        public int GenerationIndex
        {
            get { return this.generationIndex; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentFitnessEvaluatedEventArgs"/> class.
        /// </summary>
        /// <param name="environment">The <see cref="GeneticEnvironment"/> being used by the <see cref="IGeneticAlgorithm"/>.</param>
        /// <param name="generationIndex">The index of the generation that has just been created.</param>
        /// <exception cref="ArgumentNullException"><paramref name="environment"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="generationIndex"/> is less than zero.</exception>
        public EnvironmentFitnessEvaluatedEventArgs(GeneticEnvironment environment, int generationIndex)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (generationIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(generationIndex), generationIndex, FwkResources.ErrorMsg_InvalidGenerationIndex);
            }

            this.environment = environment;
            this.generationIndex = generationIndex;
        }
    }
}
