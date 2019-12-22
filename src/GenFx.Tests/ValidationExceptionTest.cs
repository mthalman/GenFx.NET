using GenFx.Validation;
using System;
using System.IO;
using System.Runtime.Serialization;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ValidationException"/> class.
    /// </summary>
    public class ValidationExceptionTest
    {
        /// <summary>
        /// Tests that the ctor intializes the state correctly.
        /// </summary>
        [Fact]
        public void ValidationException_Ctor()
        {
            ValidationException exception = new ValidationException();
            Assert.NotNull(exception.Message);

            string message = "test";
            exception = new ValidationException(message);
            Assert.Equal(message, exception.Message);

            Exception innerException = new ArgumentException();
            exception = new ValidationException(message, innerException);
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        /// <summary>
        /// Tests that the exception can be serialized.
        /// </summary>
        [Fact]
        public void ValidationException_Serialization()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(ValidationException));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, new ValidationException("message"));

                stream.Position = 0;
                ValidationException deserialized = (ValidationException)serializer.ReadObject(stream);
                Assert.Equal("message", deserialized.Message);
            }
        }
    }
}
