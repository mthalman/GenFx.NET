using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanExternalValidatorAttribute"/> class.
    /// </summary>
    [TestClass]
    public class BooleanExternalValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void BooleanExternalValidatorAttribute_Ctor()
        {
            BooleanExternalValidatorAttribute attrib =
                new BooleanExternalValidatorAttribute(typeof(TestComponent), nameof(TestComponent.MyProperty), true);

            Assert.AreEqual(typeof(TestComponent), attrib.TargetComponentType);
            Assert.AreEqual(nameof(TestComponent.MyProperty), attrib.TargetPropertyName);
            Assert.IsTrue(attrib.RequiredValue);
            Assert.IsInstanceOfType(attrib.Validator, typeof(BooleanValidator));
        }

        /// <summary>
        /// Tests an exception is thrown when providing a non-existent property to the constructor.
        /// </summary>
        [TestMethod]
        public void BooleanExternalValidatorAttribute_Ctor_InvalidProperty()
        {
            AssertEx.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(TestComponent), "Test", true));
        }

        /// <summary>
        /// Tests an exception is thrown when providing a null or empty property to the constructor.
        /// </summary>
        [TestMethod]
        public void BooleanExternalValidatorAttribute_Ctor_NullProperty()
        {
            AssertEx.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(TestComponent), null, true));
            AssertEx.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(TestComponent), String.Empty, true));
        }

        /// <summary>
        /// Tests an exception is thrown when providing a null component type to the constructor.
        /// </summary>
        [TestMethod]
        public void BooleanExternalValidatorAttribute_Ctor_NullComponentType()
        {
            AssertEx.Throws<ArgumentNullException>(
                () => new BooleanExternalValidatorAttribute(null, "test", true));
        }

        /// <summary>
        /// Tests an exception is thrown when providing an invalid component type to the constructor.
        /// </summary>
        [TestMethod]
        public void BooleanExternalValidatorAttribute_Ctor_InvalidComponentType()
        {
            AssertEx.Throws<ArgumentException>(
                () => new BooleanExternalValidatorAttribute(typeof(int), "test", true));
        }

        private class TestComponent : GeneticComponent
        {
            public int MyProperty { get; set; }
        }
    }
}
