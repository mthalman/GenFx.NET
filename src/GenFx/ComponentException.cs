using System;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// The exception that is thrown when a component is not defined correctly.
    /// </summary>
    [Serializable]
    public sealed class ComponentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentException"/> class.
        /// </summary>
        public ComponentException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public ComponentException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The <see cref="Exception"/> that is the cause of the current exception.</param>
        public ComponentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private ComponentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
