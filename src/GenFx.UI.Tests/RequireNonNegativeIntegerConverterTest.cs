using GenFx.UI.Converters;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequireNonNegativeIntegerConverter"/> class.
    /// </summary>
    public class RequireNonNegativeIntegerConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="RequireNonNegativeIntegerConverter.Convert"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequireNonNegativeIntegerConverter_Convert()
        {
            RequireNonNegativeIntegerConverter converter = new RequireNonNegativeIntegerConverter();
            object result = converter.Convert(null, null, null, null);
            Assert.Null(result);

            result = converter.Convert(-1, null, null, null);
            Assert.Null(result);

            result = converter.Convert(0, null, null, null);
            Assert.Equal(0, result);
        }

        /// <summary>
        /// Tests that the <see cref="RequireNonNegativeIntegerConverter.ConvertBack"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequireNonNegativeIntegerConverter_ConvertBack()
        {
            RequireNonNegativeIntegerConverter converter = new RequireNonNegativeIntegerConverter();
            object result = converter.ConvertBack(null, null, null, null);
            Assert.Null(result);

            result = converter.ConvertBack(-1, null, null, null);
            Assert.Equal(-1, result);

            result = converter.ConvertBack(0, null, null, null);
            Assert.Equal(0, result);
        }
    }
}
