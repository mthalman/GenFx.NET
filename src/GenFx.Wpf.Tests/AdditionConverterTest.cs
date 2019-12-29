using System;
using GenFx.Wpf.Converters;
using TestCommon.Helpers;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="AdditionConverter"/> class.
    /// </summary>
    public class AdditionConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="AdditionConverter.Convert"/> method works correctly.
        /// </summary>
        [Fact]
        public void AdditionConverter_Convert()
        {
            AdditionConverter converter = new AdditionConverter();

            converter.Value = 2;
            object result = converter.Convert(1, null, null, null);
            Assert.Equal(3, result);

            converter.Value = -5;
            result = converter.Convert(3, null, null, null);
            Assert.Equal(-2, result);

            result = converter.Convert(null, null, null, null);
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="AdditionConverter.ConvertBack"/>.
        /// </summary>
        [Fact]
        public void AdditionConverter_ConvertBack()
        {
            AdditionConverter converter = new AdditionConverter();
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
