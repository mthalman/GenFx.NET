using System;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFxTests.Helpers;

namespace GenFxTests
{
    /// <summary>
    /// Unit tests for the <see cref="PropertyValidator"/> class and it's related classes.
    ///</summary>
    [TestClass]
    public class ValidatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void IntegerValidator_Ctor()
        {
            int min = 50;
            int max = 100;
            IntegerValidator validator = new IntegerValidator(min, max);
            Assert.AreEqual(min, validator.MinValue, "MinValue was not initialized correctly.");
            Assert.AreEqual(max, validator.MaxValue, "MaxValue was not initialized correctly.");
        }

        /// <summary>
        /// Tests that the constructor throws when invalid values are used for min/max.
        /// </summary>
        [TestMethod]
        public void IntegerValidator_Ctor_InvalidMinMax()
        {
            int min = 100;
            int max = 99;
            AssertEx.Throws<ArgumentOutOfRangeException>(() => new IntegerValidator(min, max));
        }

        /// <summary>
        /// Tests that the IsValid method works correctly.
        /// </summary>
        [TestMethod]
        public void IntegerValidator_IsValid()
        {
            int min = 50;
            int max = 100;
            IntegerValidator validator = new IntegerValidator(min, max);

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
        }

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
    }
}
