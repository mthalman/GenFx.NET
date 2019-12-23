using GenFx.Components.Lists;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MultiPointCrossoverOperatorCrossoverPointValidator"/> class.
    /// </summary>
    public class MultiPointCrossoverOperatorCrossoverPointValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperatorCrossoverPointValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void MultiPointCrossoverOperatorCrossoverPointValidator_IsValid()
        {
            TestIsValid(2, false, true);
            TestIsValid(2, true, true);
            TestIsValid(3, false, true);
            TestIsValid(3, true, false);
        }

        private static void TestIsValid(int crossoverPointCount, bool requiresUniqueElementValues, bool expectedIsValid)
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = crossoverPointCount
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new IntegerListEntity
                {
                    RequiresUniqueElementValues = requiresUniqueElementValues
                }
            };
            op.Initialize(algorithm);

            MultiPointCrossoverOperatorCrossoverPointValidator validator =
                new MultiPointCrossoverOperatorCrossoverPointValidator();
            bool result = validator.IsValid(op, out string errorMessage);
            Assert.Equal(expectedIsValid, result);
            if (expectedIsValid)
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
