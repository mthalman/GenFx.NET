using System;
using GenFx.Wpf.Converters;
using TestCommon.Helpers;
using System.Windows;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="NullToVisibilityConverter"/> class.
    /// </summary>
    public class NullToVisibilityConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="NullToVisibilityConverter.Convert"/> method works correctly.
        /// </summary>
        [Fact]
        public void NullToVisibilityConverter_Convert()
        {
            NullToVisibilityConverter converter = new NullToVisibilityConverter
            {
                ValueForNonNull = Visibility.Collapsed,
                ValueForNull = Visibility.Hidden
            };
            
            object result = converter.Convert(null, null, null, null);
            Assert.Equal(Visibility.Hidden, result);

            result = converter.Convert(new object(), null, null, null);
            Assert.Equal(Visibility.Collapsed, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="NullToVisibilityConverter.ConvertBack"/>.
        /// </summary>
        [Fact]
        public void NullToVisibilityConverter_ConvertBack()
        {
            NullToVisibilityConverter converter = new NullToVisibilityConverter();
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
