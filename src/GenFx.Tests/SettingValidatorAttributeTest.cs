using GenFx.Validation;
using System;
using TestCommon;
using Xunit;

namespace GenFx.Tests
{
    public class ComponentValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the Validator property works correctly.
        /// </summary>
        [Fact]
        public void ComponentValidatorAttribute_Validator()
        {
            TestConfigurationValidatorAttribute attrib = new TestConfigurationValidatorAttribute();

            PropertyValidator validator = attrib.Validator;
            Assert.NotNull(validator);
            Assert.IsType<TestConfigurationValidator>(validator);

            PropertyValidator validator2 = attrib.Validator;
            Assert.Same(validator, validator2);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void IntegerValidatorAttribute_Ctor()
        {
            IntegerValidatorAttribute attrib = new IntegerValidatorAttribute();

            Assert.Equal(Int32.MinValue, attrib.MinValue);
            Assert.Equal(Int32.MaxValue, attrib.MaxValue);
            Assert.IsType<IntegerValidator>(attrib.Validator);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void IntegerExternalValidatorAttribute_Ctor()
        {
            Type configType = typeof(FakeComponent);
            string targetProperty = "Value";
            IntegerExternalValidatorAttribute attrib = new IntegerExternalValidatorAttribute(configType, targetProperty);

            Assert.Equal(Int32.MinValue, attrib.MinValue);
            Assert.Equal(Int32.MaxValue, attrib.MaxValue);
            Assert.IsType<IntegerValidator>(attrib.Validator);
            Assert.Same(configType, attrib.TargetComponentType);
            Assert.Equal(targetProperty, attrib.TargetPropertyName);
        }

        /// <summary>
        /// Tests that the constructor throws when a null arg is passed.
        /// </summary>
        /// <remarks>The rest of the validation tests are handled by <see cref="ExternalValidatorAttributeHelperTest"/>.</remarks>
        [Fact]
        public void IntegerExternalValidatorAttribute_Ctor_NullArg()
        {
            Assert.Throws<ArgumentNullException>(() => new IntegerExternalValidatorAttribute(null, "b"));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void DoubleValidatorAttribute_Ctor()
        {
            DoubleValidatorAttribute attrib = new DoubleValidatorAttribute();

            Assert.Equal(Double.MinValue, attrib.MinValue);
            Assert.Equal(Double.MaxValue, attrib.MaxValue);
            Assert.True(attrib.IsMinValueInclusive);
            Assert.True(attrib.IsMinValueInclusive);
            Assert.True(attrib.IsMaxValueInclusive);
            Assert.IsType<DoubleValidator>(attrib.Validator);
        }

        /// <summary>
        /// Tests that the Min/Max-Value properties work correctly.
        /// </summary>
        [Fact]
        public void DoubleValidatorAttribute_MinMax()
        {
            DoubleValidatorAttribute attrib = new DoubleValidatorAttribute
            {
                MinValue = 10,
                MaxValue = 20,
                IsMaxValueInclusive = false,
                IsMinValueInclusive = false
            };

            PrivateObject accessor = new PrivateObject(attrib.Validator);
            Assert.Equal(attrib.MinValue, accessor.GetField("minValue"));
            Assert.Equal(attrib.MaxValue, accessor.GetField("maxValue"));
            Assert.Equal(attrib.IsMinValueInclusive, accessor.GetField("isMinValueInclusive"));
            Assert.Equal(attrib.IsMaxValueInclusive, accessor.GetField("isMaxValueInclusive"));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void DoubleExternalValidatorAttribute_Ctor()
        {
            Type configType = typeof(FakeComponent);
            string targetProperty = "Value";
            DoubleExternalValidatorAttribute attrib = new DoubleExternalValidatorAttribute(configType, targetProperty);

            Assert.Equal(Double.MinValue, attrib.MinValue);
            Assert.Equal(Double.MaxValue, attrib.MaxValue);
            Assert.IsType<DoubleValidator>(attrib.Validator);
            Assert.Same(configType, attrib.TargetComponentType);
            Assert.Equal(targetProperty, attrib.TargetPropertyName);
        }

        /// <summary>
        /// Tests that the constructor throws when a null arg is passed.
        /// </summary>
        /// <remarks>The rest of the validation tests are handled by <see cref="ExternalValidatorAttributeHelperTest"/>.</remarks>
        [Fact]
        public void DoubleExternalValidatorAttribute_Ctor_NullArg()
        {
            Assert.Throws<ArgumentNullException>(() => new DoubleExternalValidatorAttribute(null, "b"));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void CustomValidatorAttribute_Ctor()
        {
            CustomPropertyValidatorAttribute attrib = new CustomPropertyValidatorAttribute(typeof(TestConfigurationValidator));

            Assert.Same(typeof(TestConfigurationValidator), attrib.ValidatorType);
            Assert.IsType<TestConfigurationValidator>(attrib.Validator);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void CustomExternalValidatorAttribute_Ctor()
        {
            Type configType = typeof(FakeComponent);
            string targetProperty = "Value";
            CustomExternalPropertyValidatorAttribute attrib = new CustomExternalPropertyValidatorAttribute(configType, targetProperty, typeof(TestConfigurationValidator));

            Assert.Same(typeof(TestConfigurationValidator), attrib.ValidatorType);
            Assert.IsType<TestConfigurationValidator>(attrib.Validator);
            Assert.Same(configType, attrib.TargetComponentType);
            Assert.Equal(targetProperty, attrib.TargetPropertyName);
        }

        /// <summary>
        /// Tests that the constructor throws when a null arg is passed.
        /// </summary>
        /// <remarks>The rest of the validation tests are handled by <see cref="ExternalValidatorAttributeHelperTest"/>.</remarks>
        [Fact]
        public void CustomExternalValidatorAttribute_Ctor_NullArg()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(null, "b", typeof(TestConfigurationValidator)));
        }

        private class TestConfigurationValidatorAttribute : PropertyValidatorAttribute
        {
            protected override PropertyValidator CreateValidator()
            {
                return new TestConfigurationValidator();
            }
        }

        private class TestConfigurationValidator : PropertyValidator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeComponent : GeneticComponent
        {
            private int value;

            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }
    }
}
