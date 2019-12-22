using GenFx.Validation;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredGeneticAlgorithmValidator"/> class.
    /// </summary>
    public class RequiredGeneticAlgorithmValidatorTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void RequiredGeneticAlgorithmValidator_Ctor()
        {
            RequiredGeneticAlgorithmValidator validator = new RequiredGeneticAlgorithmValidator(typeof(MockGeneticAlgorithm));
            Assert.Equal(typeof(MockGeneticAlgorithm), validator.RequiredComponentType);
        }

        /// <summary>
        /// Tests that the <see cref="RequiredGeneticAlgorithmValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequiredGeneticAlgorithmValidator_IsValid()
        {
            RequiredGeneticAlgorithmValidator validator = new RequiredGeneticAlgorithmValidator(typeof(MockGeneticAlgorithm));
            bool result = validator.IsValid(new MockGeneticAlgorithm(), out string errorMessage);
            Assert.True(result);
            Assert.Null(errorMessage);

            result = validator.IsValid(new MockGeneticAlgorithm2(), out errorMessage);
            Assert.False(result);
            Assert.NotNull(errorMessage);
        }
    }
}
