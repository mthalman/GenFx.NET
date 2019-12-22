using GenFx.Validation;
using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Summary description for RequiredConfigurableTypeAttributeTest
    /// </summary>
    public class RequiredComponentTypeAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void RequiredConfigurableTypeAttribute_Ctor()
        {
            TestRequiredComponentTypeAttribute attrib = new TestRequiredComponentTypeAttribute(
              typeof(MockSelectionOperator), typeof(SelectionOperator));

            Assert.Same(typeof(MockSelectionOperator), attrib.RequiredType);
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is null.
        /// </summary>
        [Fact]
        public void RequiredConfigurableTypeAttribute_Ctor_NullRequiredType()
        {
            Assert.Throws<ArgumentNullException>(() => new TestRequiredComponentTypeAttribute(null, typeof(CrossoverOperator)));
        }

        /// <summary>
        /// Tests that an exception is thrown if the base type is null.
        /// </summary>
        [Fact]
        public void RequiredConfigurableTypeAttribute_Ctor_NullBaseType()
        {
            Assert.Throws<ArgumentNullException>(() => new TestRequiredComponentTypeAttribute(typeof(MockSelectionOperator), null));
        }

        /// <summary>
        /// Tests that an exception is thrown if the required type is not a type of the base type.
        /// </summary>
        [Fact]
        public void RequiredConfigurableTypeAttribute_Ctor_InvalidType()
        {
            Assert.Throws<ArgumentException>(() => new TestRequiredComponentTypeAttribute(typeof(MockSelectionOperator), typeof(CrossoverOperator)));
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
