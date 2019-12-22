using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanExternalValidatorAttribute"/> class.
    /// </summary>
    public class BooleanExternalValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void BooleanExternalValidatorAttribute_Ctor()
        {
            BooleanExternalValidatorAttribute attrib =
                new BooleanExternalValidatorAttribute(typeof(TestComponent), nameof(TestComponent.MyProperty), true);

            Assert.Equal(typeof(TestComponent), attrib.TargetComponentType);
            Assert.Equal(nameof(TestComponent.MyProperty), attrib.TargetPropertyName);
            Assert.True(attrib.RequiredValue);
            Assert.IsType<BooleanValidator>(attrib.Validator);
        }

        /// <summary>
        /// Tests an exception is thrown when providing a non-existent property to the constructor.
        /// </summary>
        [Fact]
        public void BooleanExternalValidatorAttribute_Ctor_InvalidProperty()
        {
            Assert.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(TestComponent), "Test", true));
        }

        /// <summary>
        /// Tests an exception is thrown when providing a null or empty property to the constructor.
        /// </summary>
        [Fact]
        public void BooleanExternalValidatorAttribute_Ctor_NullProperty()
        {
            Assert.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(TestComponent), null, true));
            Assert.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(TestComponent), String.Empty, true));
        }

        /// <summary>
        /// Tests an exception is thrown when providing a null component type to the constructor.
        /// </summary>
        [Fact]
        public void BooleanExternalValidatorAttribute_Ctor_NullComponentType()
        {
            Assert.Throws<ArgumentNullException>(
                () => new BooleanExternalValidatorAttribute(null, "test", true));
        }

        /// <summary>
        /// Tests an exception is thrown when providing an invalid component type to the constructor.
        /// </summary>
        [Fact]
        public void BooleanExternalValidatorAttribute_Ctor_InvalidComponentType()
        {
            Assert.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(int), "test", true));
        }

        private class TestComponent : GeneticComponent
        {
            public int MyProperty { get; set; }
        }
    }
}
