using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredValidator"/> class.
    /// </summary>
    public class RequiredValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="RequiredValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequiredValidator_IsValid()
        {
            RequiredValidator validator = new RequiredValidator();
            bool result = validator.IsValid(2, "Foo", this, out string errorMessage);
            Assert.True(result);
            Assert.Null(errorMessage);

            result = validator.IsValid("", "Foo", this, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);

            result = validator.IsValid(null, "Foo", this, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null or empty property name to <see cref="RequiredValidator.IsValid"/>.
        /// </summary>
        [Fact]
        public void RequiredValidator_IsValid_NullPropertyName()
        {
            RequiredValidator validator = new RequiredValidator();
            string errorMessage;
            Assert.Throws<ArgumentException>(() => validator.IsValid(2, null, this, out errorMessage));
            Assert.Throws<ArgumentException>(() => validator.IsValid(2, String.Empty, this, out errorMessage));
        }
    }
}
