using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnumHelper"/> class.
    /// </summary>
    public class EnumHelperTests
    {
        /// <summary>
        /// Tests that the <see cref="EnumHelper.CreateUndefinedEnumException"/> method returns the
        /// correct result.
        /// </summary>
        [Fact]
        public void EnumHelper_CreateUndefinedEnumException()
        {
            ArgumentException exception = EnumHelper.CreateUndefinedEnumException(typeof(FitnessEvaluationMode), "foo");
            Assert.Equal("foo", exception.ParamName);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null type to <see cref="EnumHelper.CreateUndefinedEnumException"/>.
        /// </summary>
        [Fact]
        public void EnumHelper_CreateUndefinedEnumException_NullType()
        {
            Assert.Throws<ArgumentNullException>(() => EnumHelper.CreateUndefinedEnumException(null, "foo"));
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetInvalidEnumMessage"/> returns a correct value.
        /// </summary>
        [Fact]
        public void EnumHelper_GetInvalidEnumMessage()
        {
            string message = EnumHelper.GetInvalidEnumMessage(typeof(FitnessEvaluationMode));
            Assert.NotNull(message);
        }

        /// <summary>
        /// Test that an exception is thrown when a null type is passed to <see cref="EnumHelper.GetInvalidEnumMessage"/>.
        /// </summary>
        [Fact]
        public void EnumHelper_GetInvalidEnumMessage_NullType()
        {
            Assert.Throws<ArgumentNullException>(() => EnumHelper.GetInvalidEnumMessage(null));
        }
    }
}
