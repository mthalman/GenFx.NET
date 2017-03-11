using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFx.UI.Converters;
using TestCommon.Helpers;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanToDoubleConverter"/> class.
    /// </summary>
    [TestClass]
    public class BooleanToDoubleConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="BooleanToDoubleConverter.Convert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void BooleanToDoubleConverter_Convert()
        {
            BooleanToDoubleConverter converter = new BooleanToDoubleConverter
            {
                ValueForFalse = 2,
                ValueForTrue = 3
            };

            object result = converter.Convert(true, null, null, null);
            Assert.AreEqual((double)3, result);

            result = converter.Convert(false, null, null, null);
            Assert.AreEqual((double)2, result);

            result = converter.Convert(null, null, null, null);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="BooleanToDoubleConverter.ConvertBack"/>.
        /// </summary>
        [TestMethod]
        public void BooleanToDoubleConverter_ConvertBack()
        {
            BooleanToDoubleConverter converter = new BooleanToDoubleConverter();
            AssertEx.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
