using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="IntegerValidator"/> class.
    /// </summary>
    public class IntegerValidatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void IntegerValidator_Ctor()
        {
            int min = 50;
            int max = 100;
            IntegerValidator validator = new IntegerValidator(min, max);
            Assert.Equal(min, validator.MinValue);
            Assert.Equal(max, validator.MaxValue);
        }

        /// <summary>
        /// Tests that the constructor throws when invalid values are used for min/max.
        /// </summary>
        [Fact]
        public void IntegerValidator_Ctor_InvalidMinMax()
        {
            int min = 100;
            int max = 99;
            Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerValidator(min, max));
        }

        /// <summary>
        /// Tests that the IsValid method works correctly.
        /// </summary>
        [Fact]
        public void IntegerValidator_IsValid()
        {
            int min = 50;
            int max = 100;
            IntegerValidator validator = new IntegerValidator(min, max);
            bool isValid = validator.IsValid(49, "foo", null, out _);
            Assert.False(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(49.999, "foo", null, out _);
            Assert.False(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(50, "foo", null, out _);
            Assert.True(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(75, "foo", null, out _);
            Assert.True(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(100, "foo", null, out _);
            Assert.True(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(100.0000001, "foo", null, out _);
            Assert.False(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(101, "foo", null, out _);
            Assert.False(isValid, "IsValid returned incorrect value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null or empty property name is passed to <see cref="IntegerValidator.IsValid"/>.
        /// </summary>
        [Fact]
        public void IntegerValidator_IsValid_NullPropertyName()
        {
            IntegerValidator validator = new IntegerValidator(1, 5);
            string errorMessage;
            Assert.Throws<ArgumentException>(() => validator.IsValid(1, null, this, out errorMessage));
            Assert.Throws<ArgumentException>(() => validator.IsValid(1, String.Empty, this, out errorMessage));
        }

        /// <summary>
        /// Tests that an exception is thrown when <see cref="IntegerValidator.IsValid"/> is called when min and max are the same.
        /// </summary>
        [Fact]
        public void IntegerValidator_IsValid_SameMinMax()
        {
            IntegerValidator validator = new IntegerValidator(5, 5);
            string errorMessage;
            bool result = validator.IsValid(1, "foo", this, out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);
        }
    }
}
