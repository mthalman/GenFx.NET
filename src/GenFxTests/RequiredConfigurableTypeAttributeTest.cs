using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFx;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.ComponentModel;

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
              typeof(UniformSelectionOperator), typeof(SelectionOperator));

            Assert.AreSame(typeof(UniformSelectionOperator), attrib.RequiredType, "RequiredType not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequiredConfigurableTypeAttribute_Ctor_NullRequiredType()
        {
            TestRequiredConfigurableTypeAttribute attrib = new TestRequiredConfigurableTypeAttribute(
              null, typeof(CrossoverOperator));
        }

        /// <summary>
        /// Tests that an exception is thrown if the base type is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequiredConfigurableTypeAttribute_Ctor_NullBaseType()
        {
            TestRequiredConfigurableTypeAttribute attrib = new TestRequiredConfigurableTypeAttribute(
              typeof(UniformSelectionOperator), null);
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is not a type of the base type.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RequiredConfigurableTypeAttribute_Ctor_InvalidType()
        {
            TestRequiredConfigurableTypeAttribute attrib = new TestRequiredConfigurableTypeAttribute(
              typeof(UniformSelectionOperator), typeof(CrossoverOperator));
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
