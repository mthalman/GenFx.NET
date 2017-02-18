using GenFx.ComponentLibrary.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ListStartingLengthValidator"/> class.
    /// </summary>
    [TestClass]
    public class ListStartingLengthValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="ListStartingLengthValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ListStartingLengthValidator_IsValid()
        {
            ListStartingLengthValidator validator = new ListStartingLengthValidator();

            BinaryStringEntity entity = new BinaryStringEntity
            {
                MinimumStartingLength = 5,
                MaximumStartingLength = 6
            };

            string errorMessage;
            bool result = validator.IsValid(entity, out errorMessage);
            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);

            entity = new BinaryStringEntity
            {
                MinimumStartingLength = 6,
                MaximumStartingLength = 5
            };
            result = validator.IsValid(entity, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
        }
    }
}
