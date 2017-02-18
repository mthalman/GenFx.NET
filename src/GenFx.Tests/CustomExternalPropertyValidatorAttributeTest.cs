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
    /// Contains unit tests for the <see cref="CustomExternalPropertyValidatorAttribute"/> class.
    /// </summary>
    [TestClass]
    public class CustomExternalPropertyValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the constructor sets the state correctly.
        /// </summary>
        [TestMethod]
        public void CustomExternalPropertyValidatorAttribute_ParameterlessCtor()
        {
            CustomExternalPropertyValidatorAttribute attrib =
                new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty), typeof(CustomValidator));

            Assert.AreEqual(nameof(CustomComponent.MyProperty), attrib.TargetPropertyName);
            Assert.AreEqual(typeof(CustomComponent), attrib.TargetComponentType);
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
            CustomExternalPropertyValidatorAttribute attrib =
                new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty), typeof(CustomValidator2), 1, "test");

            Assert.AreEqual(nameof(CustomComponent.MyProperty), attrib.TargetPropertyName);
            Assert.AreEqual(typeof(CustomComponent), attrib.TargetComponentType);
            Assert.AreEqual(typeof(CustomValidator2), attrib.ValidatorType);
            Assert.IsInstanceOfType(attrib.Validator, typeof(CustomValidator2));
            CollectionAssert.AreEqual(new object[] { 1, "test" }, attrib.ValidatorConstructorArguments);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null component type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_NullComponentType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(null, "x", typeof(CustomValidator)));
            AssertEx.Throws<ArgumentNullException>(() => new CustomComponentValidatorAttribute(null, "x", typeof(CustomValidator), 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null validator type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_NullValidatorType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), "x", null));
            AssertEx.Throws<ArgumentNullException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), "x", null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null property name is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_NullPropertyName()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), null, null));
            AssertEx.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), null, null, 1, 2));
            AssertEx.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), String.Empty, null));
            AssertEx.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), String.Empty, null, 1, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null property name is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_NonExistentPropertyName()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), "x", null));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid component type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_InvalidComponentType()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(int), "x", typeof(CustomValidator)));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid validator type is passed to the constructor.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Ctor_InvalidValidatorType()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), "x", typeof(int)));
        }

        /// <summary>
        /// Tests that an exception is thrown approriately the validator constructor throws an exception.
        /// </summary>
        [TestMethod]
        public void CustomComponentValidatorAttribute_Validator_ExceptionOnValidatorCtor()
        {
            CustomExternalPropertyValidatorAttribute attrib = new CustomExternalPropertyValidatorAttribute(typeof(CustomComponent), nameof(CustomComponent.MyProperty), typeof(CustomValidator3));
            AssertEx.Throws<FormatException>(() => { object x = attrib.Validator; });
        }

        private class CustomComponent : GeneticComponent
        {
            public int MyProperty { get; set; }
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
