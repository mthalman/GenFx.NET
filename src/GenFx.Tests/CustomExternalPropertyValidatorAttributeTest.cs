using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="CustomExternalPropertyValidatorAttribute"/> class.
    /// </summary>
    public class CustomExternalPropertyValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor sets the state correctly.
        /// </summary>
        [Fact]
        public void CustomExternalPropertyValidatorAttribute_ParameterlessCtor()
        {
            CustomExternalPropertyValidatorAttribute attrib =
                new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty), typeof(CustomValidator));

            Assert.Equal(nameof(CustomComponent.MyProperty), attrib.TargetPropertyName);
            Assert.Equal(typeof(CustomComponent), attrib.TargetComponentType);
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
            CustomExternalPropertyValidatorAttribute attrib =
                new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty), typeof(CustomValidator2), 1, "test");

            Assert.Equal(nameof(CustomComponent.MyProperty), attrib.TargetPropertyName);
            Assert.Equal(typeof(CustomComponent), attrib.TargetComponentType);
            Assert.Equal(typeof(CustomValidator2), attrib.ValidatorType);
            Assert.IsType<CustomValidator2>(attrib.Validator);
            Assert.Equal(new object[] { 1, "test" }, attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null component type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_NullComponentType()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(null, "x", typeof(CustomValidator)));
            Assert.Throws<ArgumentNullException>(() => new CustomComponentValidatorAttribute(null, "x", typeof(CustomValidator), 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null validator type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_NullValidatorType()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), "x", null));
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), "x", null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null property name is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_NullPropertyName()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), null, null));
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), null, null, 1, 2));
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), String.Empty, null));
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), String.Empty, null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null property name is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_NonExistentPropertyName()
        {
            Assert.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), "x", typeof(CustomValidator)));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid component type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_InvalidComponentType()
        {
            Assert.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(int), nameof(CustomComponent.MyProperty), typeof(CustomValidator)));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid validator type is passed to the constructor.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Ctor_InvalidValidatorType()
        {
            Assert.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty), typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown approriately the validator constructor throws an exception.
        /// </summary>
        [Fact]
        public void CustomComponentValidatorAttribute_Validator_ExceptionOnValidatorCtor()
        {
            CustomExternalPropertyValidatorAttribute attrib = new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty), typeof(CustomValidator3));
            Assert.Throws<FormatException>(() => { object x = attrib.Validator; });
        }

        private class CustomComponent : GeneticComponent
        {
            public int MyProperty { get; set; }
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
