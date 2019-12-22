using GenFx.ComponentLibrary.Lists;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ListStartingLengthValidator"/> class.
    /// </summary>
    public class ListStartingLengthValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="ListStartingLengthValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void ListStartingLengthValidator_IsValid()
        {
            ListStartingLengthValidator validator = new ListStartingLengthValidator();

            BinaryStringEntity entity = new BinaryStringEntity
            {
                MinimumStartingLength = 5,
                MaximumStartingLength = 6
            };

            bool result = validator.IsValid(entity, out string errorMessage);
            Assert.True(result);
            Assert.Null(errorMessage);

            entity = new BinaryStringEntity
            {
                MinimumStartingLength = 6,
                MaximumStartingLength = 5
            };
            result = validator.IsValid(entity, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);
        }
    }
}
