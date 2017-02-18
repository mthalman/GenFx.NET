using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PropertyValidatorExtensions"/> class.
    /// </summary>
    [TestClass]
    public class PropertyValidatorExtensionsTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null validator is passed to <see cref="PropertyValidatorExtensions.EnsureIsValid"/>.
        /// </summary>
        [TestMethod]
        public void PropertyValidatorExtensions_EnsureIsValid_NullValidator()
        {
            AssertEx.Throws<ArgumentNullException>(() => PropertyValidatorExtensions.EnsureIsValid(null, "foo", 2, this));
        }

        /// <summary>
        /// Tests that the proper exception is thrown when the property is invalid.
        /// </summary>
        [TestMethod]
        public void PropertyValidatorExtensions_EnsureIsValid_ThrowOnInvalid()
        {
            CustomValidator validator = new CustomValidator { IsValidReturnValue = false };
            AssertEx.Throws<ValidationException>(() => PropertyValidatorExtensions.EnsureIsValid(validator, "foo", 2, this));
        }

        /// <summary>
        /// Tests that no exception is thrown when the property is valid.
        /// </summary>
        [TestMethod]
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
