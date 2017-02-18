using GenFx.Validation;
using TestCommon.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredValidator"/> class.
    /// </summary>
    [TestClass]
    public class RequiredValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="RequiredValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequiredValidator_IsValid()
        {
            RequiredValidator validator = new RequiredValidator();
            string errorMessage;
            bool result = validator.IsValid(2, "Foo", this, out errorMessage);
            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);

            result = validator.IsValid("", "Foo", this, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);

            result = validator.IsValid(null, "Foo", this, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null or empty property name to <see cref="RequiredValidator.IsValid"/>.
        /// </summary>
        [TestMethod]
        public void RequiredValidator_IsValid_NullPropertyName()
        {
            RequiredValidator validator = new RequiredValidator();
            string errorMessage;
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(2, null, this, out errorMessage));
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(2, String.Empty, this, out errorMessage));
        }
    }
}
