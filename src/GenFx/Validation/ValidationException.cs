using System;
using System.Runtime.Serialization;

namespace GenFx.Validation
{
    /// <summary>
    /// The exception that is thrown when a property value is invalid.
    /// </summary>
    [Serializable]
    public sealed class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public ValidationException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The <see cref="Exception"/> that is the cause of the current exception.</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
