using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredExternalValidatorAttribute"/> class.
    /// </summary>
    public class RequiredExternalValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void RequiredExternalValidatorAttribute_Ctor()
        {
            RequiredExternalValidatorAttribute attrib =
                new RequiredExternalValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty));

            Assert.Equal(typeof(CustomComponent), attrib.TargetComponentType);
            Assert.Equal(nameof(CustomComponent.MyProperty), attrib.TargetPropertyName);
            Assert.IsType<RequiredValidator>(attrib.Validator);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null component type to the ctor.
        /// </summary>
        [Fact]
        public void RequiredExternalValidatorAttribute_Ctor_NullComponentType()
        {
            Assert.Throws<ArgumentNullException>(() => new RequiredExternalValidatorAttribute(null, "x"));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid component type to the ctor.
        /// </summary>
        [Fact]
        public void RequiredExternalValidatorAttribute_Ctor_InvalidComponentType()
        {
            Assert.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(int), "x"));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid property name to the ctor.
        /// </summary>
        [Fact]
        public void RequiredExternalValidatorAttribute_Ctor_InvalidPropertyName()
        {
            Assert.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(CustomComponent), null));
            Assert.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(CustomComponent), String.Empty));
            Assert.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(CustomComponent), "foo"));
        }

        private class CustomComponent : GeneticComponent
        {
            public int MyProperty { get; set; }
        }
    }
}
