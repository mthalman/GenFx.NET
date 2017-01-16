using System;

namespace GenFx
{
    /// <summary>
    /// Represents a customizable component within the framework that is associated with a <see cref="GeneticAlgorithm"/>.
    /// </summary>
    public abstract class GeneticComponentWithAlgorithm : GeneticComponent
    {
        /// <summary>
        /// Gets the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public GeneticAlgorithm Algorithm { get; internal set; }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public virtual void Initialize(GeneticAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }
            this.Algorithm = algorithm;
        }
    }
}
