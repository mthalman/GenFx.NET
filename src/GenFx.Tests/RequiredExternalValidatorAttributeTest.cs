using GenFx;
using GenFx.Validation;
using TestCommon.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredExternalValidatorAttribute"/> class.
    /// </summary>
    [TestClass]
    public class RequiredExternalValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void RequiredExternalValidatorAttribute_Ctor()
        {
            RequiredExternalValidatorAttribute attrib =
                new RequiredExternalValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty));

            Assert.AreEqual(typeof(CustomComponent), attrib.TargetComponentType);
            Assert.AreEqual(nameof(CustomComponent.MyProperty), attrib.TargetPropertyName);
            Assert.IsInstanceOfType(attrib.Validator, typeof(RequiredValidator));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null component type to the ctor.
        /// </summary>
        [TestMethod]
        public void RequiredExternalValidatorAttribute_Ctor_NullComponentType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new RequiredExternalValidatorAttribute(null, "x"));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid component type to the ctor.
        /// </summary>
        [TestMethod]
        public void RequiredExternalValidatorAttribute_Ctor_InvalidComponentType()
        {
            AssertEx.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(int), "x"));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid property name to the ctor.
        /// </summary>
        [TestMethod]
        public void RequiredExternalValidatorAttribute_Ctor_InvalidPropertyName()
        {
            AssertEx.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(CustomComponent), null));
            AssertEx.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(CustomComponent), String.Empty));
            AssertEx.Throws<ArgumentException>(() => new RequiredExternalValidatorAttribute(typeof(CustomComponent), "foo"));
        }

        private class CustomComponent : GeneticComponent
        {
            public int MyProperty { get; set; }
        }
    }
}
