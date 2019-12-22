using System;
using GenFx.UI.Converters;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="NullToBooleanConverter"/> class.
    /// </summary>
    public class NullToBooleanConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="NullToBooleanConverter.Convert"/> method works correctly.
        /// </summary>
        [Fact]
        public void NullToBooleanConverter_Convert()
        {
            NullToBooleanConverter converter = new NullToBooleanConverter();

            converter.ValueForNull = true;
            object result = converter.Convert(null, null, null, null);
            Assert.True((bool)result);

            result = converter.Convert(new object(), null, null, null);
            Assert.False((bool)result);

            converter.ValueForNull = false;
            result = converter.Convert(null, null, null, null);
            Assert.False((bool)result);

            result = converter.Convert(new object(), null, null, null);
            Assert.True((bool)result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="NullToBooleanConverter.ConvertBack"/>.
        /// </summary>
        [Fact]
        public void NullToBooleanConverter_ConvertBack()
        {
            NullToBooleanConverter converter = new NullToBooleanConverter();
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
