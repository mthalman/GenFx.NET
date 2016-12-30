using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using GenFx;
using GenFx.ComponentModel;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFxTests.Helpers;

namespace GenFxTests
{
    /// <summary>
    /// Unit tests for the <see cref="ComponentConfiguration"/> class.
    /// </summary>
    [TestClass]
    public class ComponentConfigurationTest
    {
        private static bool isValidCalled;
        private static bool isValid2Called;
        private static bool isValid3Called;
        private static bool isValid4Called;
        private static bool isValid5Called;
        private static bool isValid6Called;
        private static bool isValidReturnValue;
        private static object isValidValue;
        private static object isValid4Value;
        private static object isValid5Value;
        private static object isValid6Value;

        /// <summary>
        /// Cleans up state after a test method has executed.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            isValidCalled = false;
            isValid2Called = false;
            isValid3Called = false;
            isValid4Called = false;
            isValid5Called = false;
            isValid6Called = false;
            isValidReturnValue = false;
            isValidValue = null;
            isValid4Value = null;
            isValid5Value = null;
            isValid6Value = null;
        }

        /// <summary>
        /// Tests that the ComponentType property works correctly.
        /// </summary>
        [TestMethod]
        public void ComponentConfiguration_ComponentType()
        {
            ComponentConfiguration component = new FakeComponentConfiguration();
            Assert.AreSame(typeof(FakeComponent), component.ComponentType, "Incorrect type returned.");
        }
        
        /// <summary>
        /// Tests that the ValidateProperty method works correctly.
        /// </summary>
        [TestMethod]
        public void ComponentConfiguration_ValidateProperty()
        {
            FakeComponentConfiguration config = new FakeComponentConfiguration();
            int value = 2;
            isValidReturnValue = true;
            PrivateObject accessor = new PrivateObject(config);
            accessor.Invoke("ValidateProperty", value, "Value");
            Assert.IsTrue(isValidCalled, "IsValid should be called.");
            Assert.IsTrue(isValid2Called, "IsValid should be called.");
            Assert.AreEqual(value, isValidValue, "Incorrect value passed to IsValid.");
        }

        /// <summary>
        /// Tests that the ValidateProperty method throws when a config is invalid.
        /// </summary>
        [TestMethod]
        public void ComponentConfiguration_ValidateProperty_InvalidProperty()
        {
            FakeComponentConfiguration config = new FakeComponentConfiguration();
            int value = 3;
            isValidReturnValue = false;
            PrivateObject accessor = new PrivateObject(config);
            AssertEx.Throws<ValidationException>(() => accessor.Invoke("ValidateProperty", value, "Value"));
        }

        /// <summary>
        /// Tests that the ValidateProperty method throws when a null property name is passed.
        /// </summary>
        [TestMethod]
        public void ComponentConfiguration_ValidateProperty_NullPropertyName()
        {
            FakeComponentConfiguration config = new FakeComponentConfiguration();
            PrivateObject accessor = new PrivateObject(config);
            AssertEx.Throws<ArgumentException>(() => accessor.Invoke("ValidateProperty", 3, null));
        }

        /// <summary>
        /// Tests that the ValidateProperty method throws when the property name that is passed doesn't exist.
        /// </summary>
        [TestMethod]
        public void ComponentConfiguration_ValidateProperty_PropertyNameDoesNotExist()
        {
            FakeComponentConfiguration config = new FakeComponentConfiguration();
            PrivateObject accessor = new PrivateObject(config);
            AssertEx.Throws<ArgumentException>(() => accessor.Invoke("ValidateProperty", 5, "test"));
        }

        /// <summary>
        /// Tests that the Validate method works correctly.
        /// </summary>
        [TestMethod]
        public void ComponentConfiguration_Validate()
        {
            FakeComponentConfiguration config = new FakeComponentConfiguration();
            config.Value = 5;
            config.Value2 = 2;
            isValidReturnValue = true;

            Dictionary<PropertyInfo, List<Validator>> mapping = new Dictionary<PropertyInfo, List<Validator>>();
            config.Validate();
            Assert.IsTrue(isValidCalled, "IsValid should have been called for first validator.");
            Assert.IsTrue(isValid2Called, "IsValid should have been called for second validator.");
            Assert.IsTrue(isValid3Called, "IsValid should have been called for third validator.");
            Assert.IsFalse(isValid4Called, "IsValid should not have been called for the external validator.");
            Assert.IsFalse(isValid5Called, "IsValid should not have been called for the external validator.");
            Assert.IsFalse(isValid6Called, "IsValid should not have been called for the external validator.");
            Assert.AreEqual(config.Value, isValidValue, "Incorrect value passed to IsValid.");
        }

        private class FakeComponent : GeneticComponent
        {
            public FakeComponent()
                : base(null)
            {
            }
        }

        private class FakeComponentConfiguration : ComponentConfiguration
        {
            private int value;

            public override Type ComponentType
            {
                get { return typeof(FakeComponent); }
            }

            [CustomValidator(typeof(CustomValidator))]
            [CustomValidator(typeof(CustomValidator2))]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            [CustomValidator(typeof(CustomValidator3))]
            public int Value2
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }
        
        private class CustomValidator : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                ComponentConfigurationTest.isValidCalled = true;
                ComponentConfigurationTest.isValidValue = value;
                return ComponentConfigurationTest.isValidReturnValue;
            }
        }

        private class CustomValidator2 : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                ComponentConfigurationTest.isValid2Called = true;
                return true;
            }
        }

        private class CustomValidator3 : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                ComponentConfigurationTest.isValid3Called = true;
                return true;
            }
        }

        private class CustomValidator4 : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                ComponentConfigurationTest.isValid4Called = true;
                ComponentConfigurationTest.isValid4Value = value;
                return true;
            }
        }

        private class CustomValidator5 : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                ComponentConfigurationTest.isValid5Called = true;
                ComponentConfigurationTest.isValid5Value = value;
                return true;
            }
        }

        private class CustomValidator6 : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                ComponentConfigurationTest.isValid6Called = true;
                ComponentConfigurationTest.isValid6Value = value;
                return true;
            }
        }
    }
}
