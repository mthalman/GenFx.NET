using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnumValidator"/> class.
    /// </summary>
    public class EnumValidatorTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null enum type is passed to the constructor.
        /// </summary>
        [Fact]
        public void EnumValidator_Ctor_NullEnumType()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomEnumValidator(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid enum type is passed to the constructor.
        /// </summary>
        [Fact]
        public void EnumValidator_Ctor_NotEnumType()
        {
            Assert.Throws<ArgumentException>(() => new CustomEnumValidator(typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null or empty property name is passed to <see cref="EnumValidator.IsValid"/>.
        /// </summary>
        [Fact]
        public void EnumValidator_IsValid_NullPropertyName()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            Assert.Throws<ArgumentException>(() => validator.IsValid(FitnessType.Raw, null, this, out errorMessage));
            Assert.Throws<ArgumentException>(() => validator.IsValid(FitnessType.Raw, String.Empty, this, out errorMessage));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null enum value is passed to <see cref="EnumValidator.IsValid"/>.
        /// </summary>
        [Fact]
        public void EnumValidator_IsValid_NullEnumValue()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid(null, "test", this, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);
        }

        /// <summary>
        /// Tests that the <see cref="EnumValidator.IsValid"/> method when an undefined enum value is passed.
        /// </summary>
        [Fact]
        public void EnumValidator_IsValid_UndefinedEnumValue()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid((FitnessType)4, "test", this, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);
        }

        /// <summary>
        /// Tests that the <see cref="EnumValidator.IsValid"/> method when the wrong type of object is passed as an enum value.
        /// </summary>
        [Fact]
        public void EnumValidator_IsValid_EnumValueWrongType()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid("foo", "test", this, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);
        }

        /// <summary>
        /// Tests that the <see cref="EnumValidator.IsValid"/> method when a valid enum value is passed.
        /// </summary>
        [Fact]
        public void EnumValidator_IsValid_ValidEnumValue()
        {
            CustomEnumValidator validator = new CustomEnumValidator(typeof(FitnessType));
            string errorMessage;
            bool result = validator.IsValid(FitnessType.Scaled, "test", this, out errorMessage);
            Assert.True(result);
            Assert.Null(errorMessage);
        }

        private class CustomEnumValidator : EnumValidator
        {
            public CustomEnumValidator(Type enumType) : base(enumType)
            {
            }
        }
    }
}
