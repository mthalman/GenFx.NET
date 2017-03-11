using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFx.UI.Converters;
using TestCommon.Helpers;
using System.Windows;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="NullToVisibilityConverter"/> class.
    /// </summary>
    [TestClass]
    public class NullToVisibilityConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="NullToVisibilityConverter.Convert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void NullToVisibilityConverter_Convert()
        {
            NullToVisibilityConverter converter = new NullToVisibilityConverter
            {
                ValueForNonNull = Visibility.Collapsed,
                ValueForNull = Visibility.Hidden
            };
            
            object result = converter.Convert(null, null, null, null);
            Assert.AreEqual(Visibility.Hidden, result);

            result = converter.Convert(new object(), null, null, null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="NullToVisibilityConverter.ConvertBack"/>.
        /// </summary>
        [TestMethod]
        public void NullToVisibilityConverter_ConvertBack()
        {
            NullToVisibilityConverter converter = new NullToVisibilityConverter();
            AssertEx.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
