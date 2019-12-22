using GenFx.Validation;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredGeneticAlgorithmAttribute"/> class.
    /// </summary>
    public class RequiredGeneticAlgorithmAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void RequiredGeneticAlgorithmAttribute_Ctor()
        {
            RequiredGeneticAlgorithmAttribute attrib = new RequiredGeneticAlgorithmAttribute(typeof(MockGeneticAlgorithm));
            Assert.Equal(typeof(MockGeneticAlgorithm), attrib.RequiredType);
            Assert.IsType<RequiredGeneticAlgorithmValidator>(attrib.Validator);
        }
    }
}
