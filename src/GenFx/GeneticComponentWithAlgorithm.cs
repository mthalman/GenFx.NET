using System;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Represents a customizable component within the framework that is associated with a <see cref="GeneticAlgorithm"/>.
    /// </summary>
    [DataContract]
    public abstract class GeneticComponentWithAlgorithm : GeneticComponent
    {
        /// <summary>
        /// Gets the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        [DataMember]
        public GeneticAlgorithm? Algorithm { get; internal set; }

        public bool IsInitialized { get; protected set; }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public virtual void Initialize(GeneticAlgorithm algorithm)
        {
            this.Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            this.IsInitialized = true;
        }

        internal void AssertIsInitialized()
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException(Resources.ComponentNotInitialized);
            }
        }
    }
}
