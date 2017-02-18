using GenFx;
using GenFx.Validation;
using TestCommon.Helpers;
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
    /// Contains unit tests for the <see cref="RequiredComponentTypeValidator"/> class.
    /// </summary>
    [TestClass]
    public class RequiredComponentTypeValidatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_Ctor()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            Assert.AreEqual(typeof(MockEntity), validator.RequiredComponentType);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null component type to the ctor.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_Ctor_NullComponentType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomValidator(null, typeof(GeneticEntity)));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null base type to the ctor.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_Ctor_NullBaseType()
        {
            AssertEx.Throws<ArgumentNullException>(() => new CustomValidator(typeof(MockEntity), null));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid component type to the ctor.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_Ctor_InvalidComponentType()
        {
            AssertEx.Throws<ArgumentException>(() => new CustomValidator(typeof(int), typeof(GeneticEntity)));
        }

        /// <summary>
        /// Test that the <see cref="RequiredComponentTypeValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_IsValid_WithAlgorithm()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            validator.HasRequiredComponentReturnValue = true;
            validator.ExpectedGeneticAlgorithm = algorithm;
            
            string errorMessage;
            bool result = validator.IsValid(algorithm, out errorMessage);

            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);
        }

        /// <summary>
        /// Test that the <see cref="RequiredComponentTypeValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_IsValid_WithNonAlgorithmComponent()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            validator.HasRequiredComponentReturnValue = true;
            validator.ExpectedGeneticAlgorithm = algorithm;

            MockTerminator terminator = new MockTerminator();
            terminator.Initialize(algorithm);

            string errorMessage;
            bool result = validator.IsValid(terminator, out errorMessage);

            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);
        }

        /// <summary>
        /// Test that the <see cref="RequiredComponentTypeValidator.IsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_IsValid_NotValid()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            validator.HasRequiredComponentReturnValue = false;
            validator.ExpectedGeneticAlgorithm = algorithm;

            string errorMessage;
            bool result = validator.IsValid(algorithm, out errorMessage);

            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
        }

        /// <summary>
        /// Test that an exception is thrown when passing a null component to the <see cref="RequiredComponentTypeValidator.IsValid"/> method.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_IsValid_NullComponent()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            string errorMessage;
            AssertEx.Throws<ArgumentNullException>(() => validator.IsValid(null, out errorMessage));
        }

        /// <summary>
        /// Test that an exception is thrown when passing a non-initialized non-algorithm component to the <see cref="RequiredComponentTypeValidator.IsValid"/> method.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_IsValid_NoAlgorithm()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            string errorMessage;
            AssertEx.Throws<ArgumentException>(() => validator.IsValid(new MockTerminator(), out errorMessage));
        }

        /// <summary>
        /// Tests that the <see cref="RequiredComponentTypeValidator.IsEquivalentType"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RequiredComponentTypeValidator_IsEquivalentType()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            bool result = validator.TestIsEquivalentType(new DerivedMockEntity());
            Assert.IsTrue(result);

            result = validator.TestIsEquivalentType(new MockEntity2());
            Assert.IsFalse(result);

            result = validator.TestIsEquivalentType(null);
            Assert.IsFalse(result);
        }

        private class CustomValidator : RequiredComponentTypeValidator
        {
            public bool HasRequiredComponentReturnValue { get; set; }
            public GeneticAlgorithm ExpectedGeneticAlgorithm { get; set; }

            public CustomValidator(Type requiredComponentType, Type baseType)
                : base(requiredComponentType, baseType)
            {
            }

            protected override string ComponentFriendlyName
            {
                get { return ""; }
            }

            protected override bool HasRequiredComponent(GeneticAlgorithm algorithmContext)
            {
                Assert.AreSame(this.ExpectedGeneticAlgorithm, algorithmContext);
                return this.HasRequiredComponentReturnValue;
            }

            public bool TestIsEquivalentType(GeneticComponent configuredComponent)
            {
                return this.IsEquivalentType(configuredComponent);
            }
        }
    }
}
