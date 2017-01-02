using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFx;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFx.Validation;

namespace GenFxTests
{
    /// <summary>
    /// Summary description for RequiredConfigurableTypeAttributeTest
    /// </summary>
    [TestClass]
    public class RequiredConfigurableTypeAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor()
        {
            TestRequiredConfigurableTypeAttribute attrib = new TestRequiredConfigurableTypeAttribute(
              typeof(UniformSelectionOperator), typeof(ISelectionOperator));

            Assert.AreSame(typeof(UniformSelectionOperator), attrib.RequiredType, "RequiredType not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is null.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor_NullRequiredType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new TestRequiredConfigurableTypeAttribute(null, typeof(ICrossoverOperator)));
        }

        /// <summary>
        /// Tests that an exception is thrown if the base type is null.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor_NullBaseType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new TestRequiredConfigurableTypeAttribute(typeof(UniformSelectionOperator), null));
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is not a type of the base type.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor_InvalidType()
        {
            AssertEx.Throws<ArgumentException>(() => new TestRequiredConfigurableTypeAttribute(typeof(UniformSelectionOperator), typeof(ICrossoverOperator)));
        }

        private class TestRequiredConfigurableTypeAttribute : RequiredComponentAttribute
        {
            public TestRequiredConfigurableTypeAttribute(Type requiredType, Type baseType)
                : base(requiredType, baseType)
            {
            }
        }
    }
}
