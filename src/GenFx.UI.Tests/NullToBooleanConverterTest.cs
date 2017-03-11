using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFx.UI.Converters;
using TestCommon.Helpers;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="NullToBooleanConverter"/> class.
    /// </summary>
    [TestClass]
    public class NullToBooleanConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="NullToBooleanConverter.Convert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void NullToBooleanConverter_Convert()
        {
            NullToBooleanConverter converter = new NullToBooleanConverter();

            converter.ValueForNull = true;
            object result = converter.Convert(null, null, null, null);
            Assert.IsTrue((bool)result);

            result = converter.Convert(new object(), null, null, null);
            Assert.IsFalse((bool)result);

            converter.ValueForNull = false;
            result = converter.Convert(null, null, null, null);
            Assert.IsFalse((bool)result);

            result = converter.Convert(new object(), null, null, null);
            Assert.IsTrue((bool)result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="NullToBooleanConverter.ConvertBack"/>.
        /// </summary>
        [TestMethod]
        public void NullToBooleanConverter_ConvertBack()
        {
            NullToBooleanConverter converter = new NullToBooleanConverter();
            AssertEx.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
