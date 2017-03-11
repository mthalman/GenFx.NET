using GenFx.UI.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequireNonNegativeIntegerConverter"/> class.
    /// </summary>
    [TestClass]
    public class RequireNonNegativeIntegerConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="RequireNonNegativeIntegerConverter.Convert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequireNonNegativeIntegerConverter_Convert()
        {
            RequireNonNegativeIntegerConverter converter = new RequireNonNegativeIntegerConverter();
            object result = converter.Convert(null, null, null, null);
            Assert.IsNull(result);

            result = converter.Convert(-1, null, null, null);
            Assert.IsNull(result);

            result = converter.Convert(0, null, null, null);
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Tests that the <see cref="RequireNonNegativeIntegerConverter.ConvertBack"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequireNonNegativeIntegerConverter_ConvertBack()
        {
            RequireNonNegativeIntegerConverter converter = new RequireNonNegativeIntegerConverter();
            object result = converter.ConvertBack(null, null, null, null);
            Assert.IsNull(result);

            result = converter.ConvertBack(-1, null, null, null);
            Assert.AreEqual(-1, result);

            result = converter.ConvertBack(0, null, null, null);
            Assert.AreEqual(0, result);
        }
    }
}
