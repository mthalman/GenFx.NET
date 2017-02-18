using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ValidationException"/> class.
    /// </summary>
    [TestClass]
    public class ValidationExceptionTest
    {
        /// <summary>
        /// Tests that the ctor intializes the state correctly.
        /// </summary>
        [TestMethod]
        public void ValidationException_Ctor()
        {
            ValidationException exception = new ValidationException();
            Assert.IsNotNull(exception.Message);

            string message = "test";
            exception = new ValidationException(message);
            Assert.AreEqual(message, exception.Message);

            Exception innerException = new ArgumentException();
            exception = new ValidationException(message, innerException);
            Assert.AreEqual(message, exception.Message);
            Assert.AreSame(innerException, exception.InnerException);
        }

        /// <summary>
        /// Tests that the exception can be serialized.
        /// </summary>
        [TestMethod]
        public void ValidationException_Serialization()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(ValidationException));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, new ValidationException("message"));

                stream.Position = 0;
                ValidationException deserialized = (ValidationException)serializer.ReadObject(stream);
                Assert.AreEqual("message", deserialized.Message);
            }
        }
    }
}
