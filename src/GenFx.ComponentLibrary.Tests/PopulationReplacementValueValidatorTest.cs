using GenFx.ComponentLibrary.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PopulationReplacementValueValidator"/> struct.
    /// </summary>
    [TestClass]
    public class PopulationReplacementValueValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValueValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
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
            string errorMessage;
            bool result = validator.IsValid(val, "Prop", this, out errorMessage);
            Assert.AreEqual(isExpectedToBeValid, result);
            if (isExpectedToBeValid)
            {
                Assert.IsNull(errorMessage);
            }
            else
            {
                Assert.IsNotNull(errorMessage);
            }
        }
    }
}
