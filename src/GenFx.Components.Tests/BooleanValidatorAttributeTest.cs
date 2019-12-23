using GenFx.Validation;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="BooleanValidatorAttribute"/> class.
    /// </summary>
    public class BooleanValidatorAttributeTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void BooleanValidatorAttribute_Ctor()
        {
            BooleanValidatorAttribute attrib = new BooleanValidatorAttribute(true);
            Assert.True(attrib.RequiredValue);
            Assert.True(((BooleanValidator)attrib.Validator).RequiredValue);

            attrib = new BooleanValidatorAttribute(false);
            Assert.False(attrib.RequiredValue);
            Assert.False(((BooleanValidator)attrib.Validator).RequiredValue);
        }
    }
}
