using System;
using System.ComponentModel;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFxTests.Helpers;
using GenFx;

namespace GenFxTests
{
    /// <summary>
    /// 
    ///</summary>
    [TestClass]
    public class ComponentValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the Validator property works correctly.
        /// </summary>
        [TestMethod]
        public void ComponentValidatorAttribute_Validator()
        {
            TestConfigurationValidatorAttribute attrib = new TestConfigurationValidatorAttribute();

            Validator validator = attrib.Validator;
            Assert.IsNotNull(validator, "Validator should be set.");
            Assert.IsInstanceOfType(validator, typeof(TestConfigurationValidator), "Validator should be the test's validator.");

            Validator validator2 = attrib.Validator;
            Assert.AreSame(validator, validator2, "Same instance should be returned.");
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void IntegerValidatorAttribute_Ctor()
        {
            IntegerValidatorAttribute attrib = new IntegerValidatorAttribute();

            Assert.AreEqual(Int32.MinValue, attrib.MinValue, "MinValue not initialized correctly.");
            Assert.AreEqual(Int32.MaxValue, attrib.MaxValue, "MinValue not initialized correctly.");
            Assert.IsInstanceOfType(attrib.Validator, typeof(IntegerValidator), "Validator is not correct type.");
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void IntegerExternalValidatorAttribute_Ctor()
        {
            Type configType = typeof(FakeComponent);
            string targetProperty = "Value";
            IntegerExternalValidatorAttribute attrib = new IntegerExternalValidatorAttribute(configType, targetProperty);

            Assert.AreEqual(Int32.MinValue, attrib.MinValue, "MinValue not initialized correctly.");
            Assert.AreEqual(Int32.MaxValue, attrib.MaxValue, "MinValue not initialized correctly.");
            Assert.IsInstanceOfType(attrib.Validator, typeof(IntegerValidator), "Validator is not correct type.");
            Assert.AreSame(configType, attrib.TargetComponentType, "TargetComponentConfigurationType not initialized correctly.");
            Assert.AreEqual(targetProperty, attrib.TargetProperty, "TargetProperty not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor throws when a null arg is passed.
        /// </summary>
        /// <remarks>The rest of the validation tests are handled by <see cref="ExternalValidatorAttributeHelperTest"/>.</remarks>
        [TestMethod]
        public void IntegerExternalValidatorAttribute_Ctor_NullArg()
        {
            AssertEx.Throws<ArgumentNullException>(() => new IntegerExternalValidatorAttribute(null, "b"));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void DoubleValidatorAttribute_Ctor()
        {
            DoubleValidatorAttribute attrib = new DoubleValidatorAttribute();

            Assert.AreEqual(Double.MinValue, attrib.MinValue, "MinValue not initialized correctly.");
            Assert.AreEqual(Double.MaxValue, attrib.MaxValue, "MinValue not initialized correctly.");
            Assert.IsTrue(attrib.IsMinValueInclusive, "IsMinValueInclusive not initialized correctly."); Assert.IsTrue(attrib.IsMinValueInclusive, "IsMinValueInclusive not initialized correctly.");
            Assert.IsTrue(attrib.IsMaxValueInclusive, "IsMaxValueInclusive not initialized correctly.");
            Assert.IsInstanceOfType(attrib.Validator, typeof(DoubleValidator), "Validator is not correct type.");
        }

        /// <summary>
        /// Tests that the Min/Max-Value properties work correctly.
        /// </summary>
        [TestMethod]
        public void DoubleValidatorAttribute_MinMax()
        {
            DoubleValidatorAttribute attrib = new DoubleValidatorAttribute();
            attrib.MinValue = 10;
            attrib.MaxValue = 20;
            attrib.IsMaxValueInclusive = false;
            attrib.IsMinValueInclusive = false;

            PrivateObject accessor = new PrivateObject(attrib.Validator);
            Assert.AreEqual(attrib.MinValue, accessor.GetField("minValue"), "MinValue not set correctly on Validator.");
            Assert.AreEqual(attrib.MaxValue, accessor.GetField("maxValue"), "MaxValue not set correctly on Validator.");
            Assert.AreEqual(attrib.IsMinValueInclusive, accessor.GetField("isMinValueInclusive"), "IsMinValueInclusive not set correctly on Validator.");
            Assert.AreEqual(attrib.IsMaxValueInclusive, accessor.GetField("isMaxValueInclusive"), "IsMaxValueInclusive not set correctly on Validator.");
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void DoubleExternalValidatorAttribute_Ctor()
        {
            Type configType = typeof(FakeComponent);
            string targetProperty = "Value";
            DoubleExternalValidatorAttribute attrib = new DoubleExternalValidatorAttribute(configType, targetProperty);

            Assert.AreEqual(Double.MinValue, attrib.MinValue, "MinValue not initialized correctly.");
            Assert.AreEqual(Double.MaxValue, attrib.MaxValue, "MinValue not initialized correctly.");
            Assert.IsInstanceOfType(attrib.Validator, typeof(DoubleValidator), "Validator is not correct type.");
            Assert.AreSame(configType, attrib.TargetComponentType, "TargetComponentType not initialized correctly.");
            Assert.AreEqual(targetProperty, attrib.TargetProperty, "TargetProperty not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor throws when a null arg is passed.
        /// </summary>
        /// <remarks>The rest of the validation tests are handled by <see cref="ExternalValidatorAttributeHelperTest"/>.</remarks>
        [TestMethod]
        public void DoubleExternalValidatorAttribute_Ctor_NullArg()
        {
            AssertEx.Throws<ArgumentNullException>(() => new DoubleExternalValidatorAttribute(null, "b"));
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void CustomValidatorAttribute_Ctor()
        {
            CustomValidatorAttribute attrib = new CustomValidatorAttribute(typeof(TestConfigurationValidator));

            Assert.AreSame(typeof(TestConfigurationValidator), attrib.ValidatorType, "ValidatorType not initialized correctly.");
            Assert.IsInstanceOfType(attrib.Validator, typeof(TestConfigurationValidator), "Validator is not correct type.");
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void CustomExternalValidatorAttribute_Ctor()
        {
            Type configType = typeof(FakeComponent);
            string targetProperty = "Value";
            CustomExternalValidatorAttribute attrib = new CustomExternalValidatorAttribute(typeof(TestConfigurationValidator), configType, targetProperty);

            Assert.AreSame(typeof(TestConfigurationValidator), attrib.ValidatorType, "ValidatorType not initialized correctly.");
            Assert.IsInstanceOfType(attrib.Validator, typeof(TestConfigurationValidator), "Validator is not correct type.");
            Assert.AreSame(configType, attrib.TargetComponentType, "TargetComponentConfigurationType not initialized correctly.");
            Assert.AreEqual(targetProperty, attrib.TargetProperty, "TargetProperty not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor throws when a null arg is passed.
        /// </summary>
        /// <remarks>The rest of the validation tests are handled by <see cref="ExternalValidatorAttributeHelperTest"/>.</remarks>
        [TestMethod]
        public void CustomExternalValidatorAttribute_Ctor_NullArg()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomExternalValidatorAttribute(typeof(TestConfigurationValidator), null, "b"));
        }

        private class TestConfigurationValidatorAttribute : ConfigurationValidatorAttribute
        {
            protected override Validator CreateValidator()
            {
                return new TestConfigurationValidator();
            }
        }

        private class TestConfigurationValidator : Validator
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
