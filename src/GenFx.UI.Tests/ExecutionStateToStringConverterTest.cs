using System;
using GenFx.UI.Converters;
using TestCommon.Helpers;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ExecutionStateToStringConverter"/> class.
    /// </summary>
    public class ExecutionStateToStringConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="ExecutionStateToStringConverter.Convert"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionStateToStringConverter_Convert()
        {
            ExecutionStateToStringConverter converter = new ExecutionStateToStringConverter();

            object result;
            foreach (ExecutionState state in Enum.GetValues(typeof(ExecutionState)))
            {
                result = converter.Convert(state, null, null, null);
                Assert.IsType<string>(result);
            }

            result = converter.Convert((ExecutionState)20, null, null, null);
            Assert.Null(result);

            result = converter.Convert(null, null, null, null);
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="ExecutionStateToStringConverter.ConvertBack"/>.
        /// </summary>
        [Fact]
        public void ExecutionStateToStringConverter_ConvertBack()
        {
            ExecutionStateToStringConverter converter = new ExecutionStateToStringConverter();
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
