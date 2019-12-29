using System;
using GenFx.Wpf.Converters;
using TestCommon.Helpers;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanToDoubleConverter"/> class.
    /// </summary>
    public class BooleanToDoubleConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="BooleanToDoubleConverter.Convert"/> method works correctly.
        /// </summary>
        [Fact]
        public void BooleanToDoubleConverter_Convert()
        {
            BooleanToDoubleConverter converter = new BooleanToDoubleConverter
            {
                ValueForFalse = 2,
                ValueForTrue = 3
            };

            object result = converter.Convert(true, null, null, null);
            Assert.Equal((double)3, result);

            result = converter.Convert(false, null, null, null);
            Assert.Equal((double)2, result);

            result = converter.Convert(null, null, null, null);
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="BooleanToDoubleConverter.ConvertBack"/>.
        /// </summary>
        [Fact]
        public void BooleanToDoubleConverter_ConvertBack()
        {
            BooleanToDoubleConverter converter = new BooleanToDoubleConverter();
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
