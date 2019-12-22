using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PropertyValidatorExtensions"/> class.
    /// </summary>
    public class PropertyValidatorExtensionsTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null validator is passed to <see cref="PropertyValidatorExtensions.EnsureIsValid"/>.
        /// </summary>
        [Fact]
        public void PropertyValidatorExtensions_EnsureIsValid_NullValidator()
        {
            Assert.Throws<ArgumentNullException>(() => PropertyValidatorExtensions.EnsureIsValid(null, "foo", 2, this));
        }

        /// <summary>
        /// Tests that the proper exception is thrown when the property is invalid.
        /// </summary>
        [Fact]
        public void PropertyValidatorExtensions_EnsureIsValid_ThrowOnInvalid()
        {
            CustomValidator validator = new CustomValidator { IsValidReturnValue = false };
            Assert.Throws<ValidationException>(() => PropertyValidatorExtensions.EnsureIsValid(validator, "foo", 2, this));
        }

        /// <summary>
        /// Tests that no exception is thrown when the property is valid.
        /// </summary>
        [Fact]
        public void PropertyValidatorExtensions_EnsureIsValid_DoNotThrowWhenValid()
        {
            CustomValidator validator = new CustomValidator { IsValidReturnValue = true };
            PropertyValidatorExtensions.EnsureIsValid(validator, "foo", 2, this);
        }

        private class CustomValidator : PropertyValidator
        {
            public bool IsValidReturnValue { get; set; }

            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                return this.IsValidReturnValue;
            }
        }
    }
}
