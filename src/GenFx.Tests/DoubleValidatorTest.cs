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
    /// Contains unit tests for the <see cref="DoubleValidator"/> class.
    /// </summary>
    [TestClass]
    public class DoubleValidatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_Ctor()
        {
            double min = 50;
            bool isMinInclusive = false;
            double max = 100;
            bool isMaxInclusive = true;
            DoubleValidator validator = new DoubleValidator(min, isMinInclusive, max, isMaxInclusive);
            Assert.AreEqual(min, validator.MinValue, "MinValue was not initialized correctly.");
            Assert.AreEqual(max, validator.MaxValue, "MaxValue was not initialized correctly.");
            Assert.AreEqual(isMinInclusive, validator.IsMinValueInclusive, "IsMinValueInclusive was not initialized correctly.");
            Assert.AreEqual(isMaxInclusive, validator.IsMaxValueInclusive, "IsMaxValueInclusive was not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor initializes the state correctly for equal min/max values.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_Ctor_EqualMinMax()
        {
            double min = 100;
            bool isMinInclusive = true;
            double max = 100;
            bool isMaxInclusive = true;
            DoubleValidator validator = new DoubleValidator(min, isMinInclusive, max, isMaxInclusive);
            Assert.AreEqual(min, validator.MinValue, "MinValue was not initialized correctly.");
            Assert.AreEqual(max, validator.MaxValue, "MaxValue was not initialized correctly.");
            Assert.AreEqual(isMinInclusive, validator.IsMinValueInclusive, "IsMinValueInclusive was not initialized correctly.");
            Assert.AreEqual(isMaxInclusive, validator.IsMaxValueInclusive, "IsMaxValueInclusive was not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor throws when invalid values are used for min/max.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_Ctor_InvalidMinMax()
        {
            int min = 100;
            int max = 99;
            AssertEx.Throws<ArgumentOutOfRangeException>(() => new DoubleValidator(min, true, max, true));
        }

        /// <summary>
        /// Tests that the constructor throws when invalid values are used for min/max.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_Ctor_InvalidEqualMinMax()
        {
            int min = 100;
            int max = 100;
            AssertEx.Throws<InvalidOperationException>(() => new DoubleValidator(min, false, max, true));
        }

        /// <summary>
        /// Tests that the constructor throws when invalid values are used for min/max.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_Ctor_InvalidEqualMinMax2()
        {
            int min = 100;
            int max = 100;
            AssertEx.Throws<InvalidOperationException>(() => new DoubleValidator(min, true, max, false));
        }

        /// <summary>
        /// Tests that the IsValid method works correctly.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_IsValid()
        {
            double min = 50;
            bool isMinInclusive = true;
            double max = 100;
            bool isMaxInclusive = true;
            DoubleValidator validator = new DoubleValidator(min, isMinInclusive, max, isMaxInclusive);

            string temp;
            bool isValid = validator.IsValid(49, "foo", null, out temp);
            Assert.IsFalse(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(49.999, "foo", null, out temp);
            Assert.IsFalse(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(50, "foo", null, out temp);
            Assert.IsTrue(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(75, "foo", null, out temp);
            Assert.IsTrue(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(100, "foo", null, out temp);
            Assert.IsTrue(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(100.0000001, "foo", null, out temp);
            Assert.IsFalse(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(101, "foo", null, out temp);
            Assert.IsFalse(isValid, "IsValid returned incorrect value.");

            PrivateObject accessor = new PrivateObject(validator);
            accessor.SetField("isMinValueInclusive", false);

            isValid = validator.IsValid(50, "foo", null, out temp);
            Assert.IsFalse(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(50.00001, "foo", null, out temp);
            Assert.IsTrue(isValid, "IsValid returned incorrect value.");

            accessor.SetField("isMaxValueInclusive", false);
            isValid = validator.IsValid(100, "foo", null, out temp);
            Assert.IsFalse(isValid, "IsValid returned incorrect value.");

            isValid = validator.IsValid(99.99999, "foo", null, out temp);
            Assert.IsTrue(isValid, "IsValid returned incorrect value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null or empty property name is passed
        /// to <see cref="DoubleValidator.IsValid"/>.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_IsValid_NullPropertyName()
        {
            DoubleValidator validator = new DoubleValidator(1, true, 10, true);

            string errorMessage;
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(2, null, null, out errorMessage));

            AssertEx.Throws<ArgumentException>(() => validator.IsValid(2, String.Empty, null, out errorMessage));
        }

        /// <summary>
        /// Tests the <see cref="DoubleValidator.IsValid"/> method when an object of the wrong type is passed.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_IsValid_WrongType()
        {
            DoubleValidator validator = new DoubleValidator(1, true, 10, true);

            string errorMessage;
            bool result = validator.IsValid("x", "Property", null, out errorMessage);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the <see cref="DoubleValidator.IsValid"/> method when the min and max are equal.
        /// </summary>
        [TestMethod]
        public void DoubleValidator_IsValid_EqualMinMax()
        {
            DoubleValidator validator = new DoubleValidator(1, true, 1, true);

            string errorMessage;
            bool result = validator.IsValid(2, "Property", null, out errorMessage);
            Assert.IsFalse(result);
        }
        }
}
