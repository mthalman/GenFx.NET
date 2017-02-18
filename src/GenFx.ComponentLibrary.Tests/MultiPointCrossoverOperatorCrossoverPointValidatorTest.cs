using GenFx.ComponentLibrary.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MultiPointCrossoverOperatorCrossoverPointValidator"/> class.
    /// </summary>
    [TestClass]
    public class MultiPointCrossoverOperatorCrossoverPointValidatorTest
    {
        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperatorCrossoverPointValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
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
            string errorMessage;
            bool result = validator.IsValid(op, out errorMessage);
            Assert.AreEqual(expectedIsValid, result);
            if (expectedIsValid)
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
