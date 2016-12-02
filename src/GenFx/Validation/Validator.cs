using System.Diagnostics.CodeAnalysis;
using GenFx.ComponentModel;

namespace GenFx.Validation
{
    /// <summary>
    /// Base class for deriving a validator class so that a value can be verified.
    /// </summary>
    public abstract class Validator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Validator"/> class.
        /// </summary>
        protected Validator()
        {
        }

        /// <summary>
        /// When overriden by a derived class, returns whether <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="propertyName">Name of the property being validated.</param>
        /// <param name="owner">The object that owns the property being validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the property fails validation.</param>
        /// <returns>True if <paramref name="value"/> is valid; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        public abstract bool IsValid(object value, string propertyName, object owner, out string errorMessage);
    }
}
