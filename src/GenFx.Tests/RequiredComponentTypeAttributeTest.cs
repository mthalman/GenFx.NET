using GenFx;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    /// Summary description for RequiredConfigurableTypeAttributeTest
    /// </summary>
    [TestClass]
    public class RequiredComponentTypeAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor()
        {
            TestRequiredComponentTypeAttribute attrib = new TestRequiredComponentTypeAttribute(
              typeof(MockSelectionOperator), typeof(SelectionOperator));

            Assert.AreSame(typeof(MockSelectionOperator), attrib.RequiredType, "RequiredType not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is null.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor_NullRequiredType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new TestRequiredComponentTypeAttribute(null, typeof(CrossoverOperator)));
        }

        /// <summary>
        /// Tests that an exception is thrown if the base type is null.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor_NullBaseType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new TestRequiredComponentTypeAttribute(typeof(MockSelectionOperator), null));
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is not a type of the base type.
        /// </summary>
        [TestMethod]
        public void RequiredConfigurableTypeAttribute_Ctor_InvalidType()
        {
            AssertEx.Throws<ArgumentException>(() => new TestRequiredComponentTypeAttribute(typeof(MockSelectionOperator), typeof(CrossoverOperator)));
        }

        private class TestRequiredComponentTypeAttribute : RequiredComponentTypeAttribute
        {
            public TestRequiredComponentTypeAttribute(Type requiredType, Type baseType)
                : base(requiredType, baseType)
            {
            }

            protected override ComponentValidator CreateValidator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
