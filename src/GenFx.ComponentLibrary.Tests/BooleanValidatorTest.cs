using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanValidator"/> class.
    /// </summary>
    [TestClass]
    public class BooleanValidatorTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void BooleanValidator_Ctor()
        {
            BooleanValidator validator = new BooleanValidator(false);
            Assert.IsFalse(validator.RequiredValue);

            validator = new BooleanValidator(true);
            Assert.IsTrue(validator.RequiredValue);
        }

        /// <summary>
        /// Tests that the <see cref="BooleanValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void BooleanValidator_IsValid()
        {
            BooleanValidator validator = new BooleanValidator(false);
            string errorMessage;
            bool result = validator.IsValid(true, "test", this, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);

            result = validator.IsValid("false", "test", this, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);

            result = validator.IsValid(false, "test", this, out errorMessage);
            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null or empty property name to <see cref="BooleanValidator.IsValid"/>.
        /// </summary>
        [TestMethod]
        public void BooleanValidator_IsValid_NullPropertyName()
        {
            BooleanValidator validator = new BooleanValidator(false);
            string errorMessage;
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(true, null, this, out errorMessage));
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(true, String.Empty, this, out errorMessage));
        }
    }
}
