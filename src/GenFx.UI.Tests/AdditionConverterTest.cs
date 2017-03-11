using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenFx.UI.Converters;
using TestCommon.Helpers;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="AdditionConverter"/> class.
    /// </summary>
    [TestClass]
    public class AdditionConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="AdditionConverter.Convert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void AdditionConverter_Convert()
        {
            AdditionConverter converter = new AdditionConverter();

            converter.Value = 2;
            object result = converter.Convert(1, null, null, null);
            Assert.AreEqual(3, result);

            converter.Value = -5;
            result = converter.Convert(3, null, null, null);
            Assert.AreEqual(-2, result);

            result = converter.Convert(null, null, null, null);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests that an exception is thrown when invoking <see cref="AdditionConverter.ConvertBack"/>.
        /// </summary>
        [TestMethod]
        public void AdditionConverter_ConvertBack()
        {
            AdditionConverter converter = new AdditionConverter();
            AssertEx.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
