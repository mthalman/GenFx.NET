using GenFx.Validation;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredGeneticAlgorithmValidator"/> class.
    /// </summary>
    [TestClass]
    public class RequiredGeneticAlgorithmValidatorTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void RequiredGeneticAlgorithmValidator_Ctor()
        {
            RequiredGeneticAlgorithmValidator validator = new RequiredGeneticAlgorithmValidator(typeof(MockGeneticAlgorithm));
            Assert.AreEqual(typeof(MockGeneticAlgorithm), validator.RequiredComponentType);
        }

        /// <summary>
        /// Tests that the <see cref="RequiredGeneticAlgorithmValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequiredGeneticAlgorithmValidator_IsValid()
        {
            RequiredGeneticAlgorithmValidator validator = new RequiredGeneticAlgorithmValidator(typeof(MockGeneticAlgorithm));
            string errorMessage;
            bool result = validator.IsValid(new MockGeneticAlgorithm(), out errorMessage);
            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);

            result = validator.IsValid(new MockGeneticAlgorithm2(), out errorMessage);
            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
        }
    }
}
