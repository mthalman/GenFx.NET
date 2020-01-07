using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Base class for deriving a validator class so that a <see cref="GeneticComponent"/> can be verified.
    /// </summary>
    public abstract class ComponentValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentValidator"/> class.
        /// </summary>
        protected ComponentValidator()
        {
        }

        /// <summary>
        /// When overriden by a derived class, returns whether <paramref name="component"/> is valid.
        /// </summary>
        /// <param name="component"><see cref="GeneticComponent"/> to be validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the component fails validation.</param>
        /// <returns>True if <paramref name="component"/> is valid; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        public abstract bool IsValid(GeneticComponent component, out string? errorMessage);
    }
}
