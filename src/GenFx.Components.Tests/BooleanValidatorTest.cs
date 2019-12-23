using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanValidator"/> class.
    /// </summary>
    public class BooleanValidatorTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void BooleanValidator_Ctor()
        {
            BooleanValidator validator = new BooleanValidator(false);
            Assert.False(validator.RequiredValue);

            validator = new BooleanValidator(true);
            Assert.True(validator.RequiredValue);
        }

        /// <summary>
        /// Tests that the <see cref="BooleanValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void BooleanValidator_IsValid()
        {
            BooleanValidator validator = new BooleanValidator(false);
            bool result = validator.IsValid(true, "test", this, out string errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);

            result = validator.IsValid("false", "test", this, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);

            result = validator.IsValid(false, "test", this, out errorMessage);
            Assert.True(result);
            Assert.Null(errorMessage);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null or empty property name to <see cref="BooleanValidator.IsValid"/>.
        /// </summary>
        [Fact]
        public void BooleanValidator_IsValid_NullPropertyName()
        {
            BooleanValidator validator = new BooleanValidator(false);
            string errorMessage;
            Assert.Throws<ArgumentException>(() => validator.IsValid(true, null, this, out errorMessage));
            Assert.Throws<ArgumentException>(() => validator.IsValid(true, String.Empty, this, out errorMessage));
        }
    }
}
