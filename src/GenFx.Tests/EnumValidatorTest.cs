using GenFx;
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
    /// Contains unit tests for the <see cref="EnumValidator"/> class.
    /// </summary>
    [TestClass]
    public class EnumValidatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null enum type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void EnumValidator_Ctor_NullEnumType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomEnumValidator(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid enum type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void EnumValidator_Ctor_NotEnumType()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomEnumValidator(typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null or empty property name is passed to <see cref="EnumValidator.IsValid"/>.
        /// </summary>
        [TestMethod]
        public void EnumValidator_IsValid_NullPropertyName()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(FitnessType.Raw, null, this, out errorMessage));
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(FitnessType.Raw, String.Empty, this, out errorMessage));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null enum value is passed to <see cref="EnumValidator.IsValid"/>.
        /// </summary>
        [TestMethod]
        public void EnumValidator_IsValid_NullEnumValue()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid(null, "test", this, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
        }

        /// <summary>
        /// Tests that the <see cref="EnumValidator.IsValid"/> method when an undefined enum value is passed.
        /// </summary>
        [TestMethod]
        public void EnumValidator_IsValid_UndefinedEnumValue()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid((FitnessType)4, "test", this, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
        }

        /// <summary>
        /// Tests that the <see cref="EnumValidator.IsValid"/> method when the wrong type of object is passed as an enum value.
        /// </summary>
        [TestMethod]
        public void EnumValidator_IsValid_EnumValueWrongType()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid("foo", "test", this, out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
        }

        /// <summary>
        /// Tests that the <see cref="EnumValidator.IsValid"/> method when a valid enum value is passed.
        /// </summary>
        [TestMethod]
        public void EnumValidator_IsValid_ValidEnumValue()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid(FitnessType.Scaled, "test", this, out errorMessage);
            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);
        }

        private class CustomEnumValidator : EnumValidator
        {
            public CustomEnumValidator(Type enumType) : base(enumType)
            {
            }
        }
    }
}
