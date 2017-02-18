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
    /// Contains unit tests for the <see cref="RequiredGeneticAlgorithmAttribute"/> class.
    /// </summary>
    [TestClass]
    public class RequiredGeneticAlgorithmAttributeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void RequiredGeneticAlgorithmAttribute_Ctor()
        {
            RequiredGeneticAlgorithmAttribute attrib = new RequiredGeneticAlgorithmAttribute(typeof(MockGeneticAlgorithm));
            Assert.AreEqual(typeof(MockGeneticAlgorithm), attrib.RequiredType);
            Assert.IsInstanceOfType(attrib.Validator, typeof(RequiredGeneticAlgorithmValidator));
        }
    }
}
