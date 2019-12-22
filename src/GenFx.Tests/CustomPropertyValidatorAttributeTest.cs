using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="CustomPropertyValidatorAttribute"/> class.
    /// </summary>
    public class CustomPropertyValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void CustomPropertyValidatorAttribute_Ctor_ParameterlessCtor()
        {
            CustomPropertyValidatorAttribute attrib = new CustomPropertyValidatorAttribute(typeof(CustomValidator));
            Assert.Equal(typeof(CustomValidator), attrib.ValidatorType);
            Assert.IsType<CustomValidator>(attrib.Validator);
            Assert.Empty(attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void CustomPropertyValidatorAttribute_Ctor_ParameterizedCtor()
        {
            CustomPropertyValidatorAttribute attrib =
                new CustomPropertyValidatorAttribute(typeof(CustomValidator2), 1, "test");
            Assert.Equal(typeof(CustomValidator2), attrib.ValidatorType);
            Assert.IsType<CustomValidator2>(attrib.Validator);
            Assert.Equal(new object[] { 1, "test" }, attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null validator type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomPropertyValidatorAttribute_Ctor_NullValidatorType()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomPropertyValidatorAttribute(null));
            Assert.Throws<ArgumentNullException>(() => new CustomPropertyValidatorAttribute(null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid validator type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomPropertyValidatorAttribute_Ctor_InvalidValidatorType()
        {
            Assert.Throws<ArgumentException>(() => new CustomPropertyValidatorAttribute(typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown approriately the validator constructor throws an exception.
        /// </summary>
        [Fact]
        public void CustomPropertyValidatorAttribute_Validator_ExceptionOnValidatorCtor()
        {
            CustomPropertyValidatorAttribute attrib = new CustomPropertyValidatorAttribute(typeof(CustomValidator3));
            Assert.Throws<FormatException>(() => { object x = attrib.Validator; });
        }

        private class CustomValidator : PropertyValidator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator2 : PropertyValidator
        {
            public CustomValidator2(int intVal, string strVal)
            {

            }

            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator3 : PropertyValidator
        {
            public CustomValidator3()
            {
                throw new FormatException();
            }

            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }
    }
}
