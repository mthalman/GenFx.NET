using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFx.UI.Converters;
using TestCommon.Helpers;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ExecutionStateToStringConverter"/> class.
    /// </summary>
    [TestClass]
    public class ExecutionStateToStringConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="ExecutionStateToStringConverter.Convert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionStateToStringConverter_Convert()
        {
            ExecutionStateToStringConverter converter = new ExecutionStateToStringConverter();

            object result;
            foreach (ExecutionState state in Enum.GetValues(typeof(ExecutionState)))
            {
                result = converter.Convert(state, null, null, null);
                Assert.IsInstanceOfType(result, typeof(string));
            }

            result = converter.Convert((ExecutionState)20, null, null, null);
            Assert.IsNull(result);

            result = converter.Convert(null, null, null, null);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="ExecutionStateToStringConverter.ConvertBack"/>.
        /// </summary>
        [TestMethod]
        public void ExecutionStateToStringConverter_ConvertBack()
        {
            ExecutionStateToStringConverter converter = new ExecutionStateToStringConverter();
            AssertEx.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
