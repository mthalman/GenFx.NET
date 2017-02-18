using GenFx.ComponentLibrary.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PopulationReplacementValue"/> struct.
    /// </summary>
    [TestClass]
    public class PopulationReplacementValueTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void PopulationReplacementValue_Ctor()
        {
            PopulationReplacementValue val = new PopulationReplacementValue(3, ReplacementValueKind.Percentage);
            Assert.AreEqual(3, val.Value);
            Assert.AreEqual(ReplacementValueKind.Percentage, val.Kind);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid value to ctor.
        /// </summary>
        [TestMethod]
        public void PopulationReplacementValue_Ctor_InvalidValue()
        {
            AssertEx.Throws<ArgumentException>(() => new PopulationReplacementValue(-1, ReplacementValueKind.Percentage));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid kind to ctor.
        /// </summary>
        [TestMethod]
        public void PopulationReplacementValue_Ctor_InvalidKind()
        {
            AssertEx.Throws<ArgumentException>(() => new PopulationReplacementValue(2, (ReplacementValueKind)3));
        }

        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValue.Equals"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void PopulationReplacementValue_Equals()
        {
            PopulationReplacementValue val1 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val2 = new PopulationReplacementValue(2, ReplacementValueKind.Percentage);
            PopulationReplacementValue val3 = new PopulationReplacementValue(3, ReplacementValueKind.Percentage);
            PopulationReplacementValue val4 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val5 = new PopulationReplacementValue(3, ReplacementValueKind.Percentage);

            Assert.IsFalse(val1.Equals(val2));
            Assert.IsFalse(val1 == val2);
            Assert.IsTrue(val1 != val2);

            Assert.IsFalse(val1.Equals(val3));
            Assert.IsFalse(val1 == val3);
            Assert.IsTrue(val1 != val3);

            Assert.IsTrue(val1.Equals(val4));
            Assert.IsTrue(val1 == val4);
            Assert.IsFalse(val1 != val4);

            Assert.IsFalse(val3.Equals(val2));
            Assert.IsFalse(val3 == val2);
            Assert.IsTrue(val3 != val2);

            Assert.IsTrue(val3.Equals(val5));
            Assert.IsTrue(val3 == val5);
            Assert.IsFalse(val3 != val5);

            Assert.IsFalse(val1.Equals(null));
            Assert.IsFalse(val1 == null);
            Assert.IsTrue(val1 != null);

            Assert.IsFalse(val1.Equals("test"));
        }

        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValue.GetHashCode"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void PopulationReplacementValue_GetHashCode()
        {
            PopulationReplacementValue val1 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val2 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val3 = new PopulationReplacementValue(3, ReplacementValueKind.FixedCount);

            Assert.AreEqual(val1.GetHashCode(), val2.GetHashCode());
            Assert.AreNotEqual(val1.GetHashCode(), val3.GetHashCode());
        }

        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValue.ToString"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void PopulationReplacementValue_ToString()
        {
            PopulationReplacementValue val1 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            Assert.AreEqual("2", val1.ToString());

            PopulationReplacementValue val2 = new PopulationReplacementValue(2, ReplacementValueKind.Percentage);
            Assert.AreEqual("2%", val2.ToString());
        }
    }
}
