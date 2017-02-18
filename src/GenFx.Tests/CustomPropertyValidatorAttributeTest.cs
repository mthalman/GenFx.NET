using GenFx;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="CustomPropertyValidatorAttribute"/> class.
    /// </summary>
    [TestClass]
    public class CustomPropertyValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void CustomPropertyValidatorAttribute_Ctor_ParameterlessCtor()
        {
            CustomPropertyValidatorAttribute attrib = new CustomPropertyValidatorAttribute(typeof(CustomValidator));
            Assert.AreEqual(typeof(CustomValidator), attrib.ValidatorType);
            Assert.IsInstanceOfType(attrib.Validator, typeof(CustomValidator));
            Assert.AreEqual(0, attrib.ValidatorConstructorArguments.Length);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void CustomPropertyValidatorAttribute_Ctor_ParameterizedCtor()
        {
            CustomPropertyValidatorAttribute attrib =
                new CustomPropertyValidatorAttribute(typeof(CustomValidator2), 1, "test");
            Assert.AreEqual(typeof(CustomValidator2), attrib.ValidatorType);
            Assert.IsInstanceOfType(attrib.Validator, typeof(CustomValidator2));
            CollectionAssert.AreEqual(new object[] { 1, "test" }, attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null validator type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomPropertyValidatorAttribute_Ctor_NullValidatorType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomPropertyValidatorAttribute(null));
            AssertEx.Throws<ArgumentNullException>(() => new CustomPropertyValidatorAttribute(null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid validator type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomPropertyValidatorAttribute_Ctor_InvalidValidatorType()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomPropertyValidatorAttribute(typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown approriately the validator constructor throws an exception.
        /// </summary>
        [TestMethod]
        public void CustomPropertyValidatorAttribute_Validator_ExceptionOnValidatorCtor()
        {
            CustomPropertyValidatorAttribute attrib = new CustomPropertyValidatorAttribute(typeof(CustomValidator3));
            AssertEx.Throws<FormatException>(() => { object x = attrib.Validator; });
        }

        private class CustomValidator : PropertyValidator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator2 : PropertyValidator
        {
            public CustomValidator2(int intVal, string strVal)
            {

            }

            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator3 : PropertyValidator
        {
            public CustomValidator3()
            {
                throw new FormatException();
            }

            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }
    }
}
