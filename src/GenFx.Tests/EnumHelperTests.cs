using GenFx;
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
    /// Contains unit tests for the <see cref="EnumHelper"/> class.
    /// </summary>
    [TestClass]
    public class EnumHelperTests
    {
        /// <summary>
        /// Tests that the <see cref="EnumHelper.CreateUndefinedEnumException"/> method returns the
        /// correct result.
        /// </summary>
        [TestMethod]
        public void EnumHelper_CreateUndefinedEnumException()
        {
            ArgumentException exception = EnumHelper.CreateUndefinedEnumException(typeof(FitnessEvaluationMode), "foo");
            Assert.AreEqual("foo", exception.ParamName);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null type to <see cref="EnumHelper.CreateUndefinedEnumException"/>.
        /// </summary>
        [TestMethod]
        public void EnumHelper_CreateUndefinedEnumException_NullType()
        {
            AssertEx.Throws<ArgumentNullException>(() => EnumHelper.CreateUndefinedEnumException(null, "foo"));
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetInvalidEnumMessage"/> returns a correct value.
        /// </summary>
        [TestMethod]
        public void EnumHelper_GetInvalidEnumMessage()
        {
            string message = EnumHelper.GetInvalidEnumMessage(typeof(FitnessEvaluationMode));
            Assert.IsNotNull(message);
        }

        /// <summary>
        /// Test that an exception is thrown when a null type is passed to <see cref="EnumHelper.GetInvalidEnumMessage"/>.
        /// </summary>
        [TestMethod]
        public void EnumHelper_GetInvalidEnumMessage_NullType()
        {
            AssertEx.Throws<ArgumentNullException>(() => EnumHelper.GetInvalidEnumMessage(null));
        }
    }
}
