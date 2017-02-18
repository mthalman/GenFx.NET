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
    /// Contains unit tests for the <see cref="CustomComponentValidatorAttribute"/> class.
    /// </summary>
    [TestClass]
    public class CustomComponentValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_ParameterlessCtor()
        {
            CustomComponentValidatorAttribute attrib = new CustomComponentValidatorAttribute(typeof(CustomValidator));
            Assert.AreEqual(typeof(CustomValidator), attrib.ValidatorType);
            Assert.IsInstanceOfType(attrib.Validator, typeof(CustomValidator));
            Assert.AreEqual(0, attrib.ValidatorConstructorArguments.Length);
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_ParameterizedCtor()
        {
            CustomComponentValidatorAttribute attrib =
                new CustomComponentValidatorAttribute(typeof(CustomValidator2), 1, "test");
            Assert.AreEqual(typeof(CustomValidator2), attrib.ValidatorType);
            Assert.IsInstanceOfType(attrib.Validator, typeof(CustomValidator2));
            CollectionAssert.AreEqual(new object[] { 1, "test" }, attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null validator type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_NullValidatorType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomComponentValidatorAttribute(null));
            AssertEx.Throws<ArgumentNullException>(() => new CustomComponentValidatorAttribute(null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid validator type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_InvalidValidatorType()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomComponentValidatorAttribute(typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown approriately the validator constructor throws an exception.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Validator_ExceptionOnValidatorCtor()
        {
            CustomComponentValidatorAttribute attrib = new CustomComponentValidatorAttribute(typeof(CustomValidator3));
            AssertEx.Throws<FormatException>(() => { object x = attrib.Validator; });
        }

        private class CustomValidator : ComponentValidator
        {
            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator2 : ComponentValidator
        {
            public CustomValidator2(int intVal, string strVal)
            {

            }

            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }

        private class CustomValidator3 : ComponentValidator
        {
            public CustomValidator3()
            {
                throw new FormatException();
            }

            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                throw new NotImplementedException();
            }
        }
    }
}
