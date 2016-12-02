using System;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a customizable component within the framework that is associated with a <see cref="GeneticAlgorithm"/>.
    /// </summary>
    public abstract class GeneticComponentWithAlgorithm : GeneticComponent
    {
        /// <summary>
        /// Gets the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        protected GeneticAlgorithm Algorithm { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="GeneticComponentWithAlgorithm"/>.
        /// </summary>
        /// <param name="algorithm">The <see cref="GeneticAlgorithm"/> this component is associated with.</param>
        protected GeneticComponentWithAlgorithm(GeneticAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            Algorithm = algorithm;
            Algorithm.ValidateComponentConfiguration(this);
        }
    }
}
