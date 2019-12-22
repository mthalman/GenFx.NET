using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="CustomComponentValidatorAttribute"/> class.
    /// </summary>
    public class CustomComponentValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_ParameterlessCtor()
        {
            CustomComponentValidatorAttribute attrib = new CustomComponentValidatorAttribute(typeof(CustomValidator));
            Assert.Equal(typeof(CustomValidator), attrib.ValidatorType);
            Assert.IsType<CustomValidator>(attrib.Validator);
            Assert.Empty(attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_ParameterizedCtor()
        {
            CustomComponentValidatorAttribute attrib =
                new CustomComponentValidatorAttribute(typeof(CustomValidator2), 1, "test");
            Assert.Equal(typeof(CustomValidator2), attrib.ValidatorType);
            Assert.IsType<CustomValidator2>(attrib.Validator);
            Assert.Equal(new object[] { 1, "test" }, attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null validator type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_NullValidatorType()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomComponentValidatorAttribute(null));
            Assert.Throws<ArgumentNullException>(() => new CustomComponentValidatorAttribute(null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid validator type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_InvalidValidatorType()
        {
            Assert.Throws<ArgumentException>(() => new CustomComponentValidatorAttribute(typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown approriately the validator constructor throws an exception.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Validator_ExceptionOnValidatorCtor()
        {
            CustomComponentValidatorAttribute attrib = new CustomComponentValidatorAttribute(typeof(CustomValidator3));
            Assert.Throws<FormatException>(() => { object x = attrib.Validator; });
        }

        private class CustomValidator : ComponentValidator
        {
            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator2 : ComponentValidator
        {
            public CustomValidator2(int intVal, string strVal)
            {

            }

            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator3 : ComponentValidator
        {
            public CustomValidator3()
            {
                throw new FormatException();
            }

            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }
    }
}
