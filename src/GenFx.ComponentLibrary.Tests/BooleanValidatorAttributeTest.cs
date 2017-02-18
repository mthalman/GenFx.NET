using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanValidatorAttribute"/> class.
    /// </summary>
    [TestClass]
    public class BooleanValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void BooleanValidatorAttribute_Ctor()
        {
            BooleanValidatorAttribute attrib = new BooleanValidatorAttribute(true);
            Assert.IsTrue(attrib.RequiredValue);
            Assert.IsTrue(((BooleanValidator)attrib.Validator).RequiredValue);

            attrib = new BooleanValidatorAttribute(false);
            Assert.IsFalse(attrib.RequiredValue);
            Assert.IsFalse(((BooleanValidator)attrib.Validator).RequiredValue);
        }
    }
}
