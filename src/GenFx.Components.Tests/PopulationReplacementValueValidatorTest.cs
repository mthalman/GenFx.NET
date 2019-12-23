using GenFx.Components.Algorithms;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PopulationReplacementValueValidator"/> struct.
    /// </summary>
    public class PopulationReplacementValueValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValueValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void PopulationReplacementValueValidator_IsValid()
        {
            PopulationReplacementValue val = new PopulationReplacementValue(100, ReplacementValueKind.FixedCount);
            this.TestValidator(val, true);

            val = new PopulationReplacementValue(100, ReplacementValueKind.Percentage);
            this.TestValidator(val, true);

            val = new PopulationReplacementValue(101, ReplacementValueKind.Percentage);
            this.TestValidator(val, false);

            this.TestValidator("test", false);
            this.TestValidator(1, false);
        }

        private void TestValidator(object val, bool isExpectedToBeValid)
        {
            PopulationReplacementValueValidator validator = new PopulationReplacementValueValidator();
            bool result = validator.IsValid(val, "Prop", this, out string errorMessage);
            Assert.Equal(isExpectedToBeValid, result);
            if (isExpectedToBeValid)
            {
                Assert.Null(errorMessage);
            }
            else
            {
                Assert.NotNull(errorMessage);
            }
        }
    }
}
