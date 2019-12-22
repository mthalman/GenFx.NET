using GenFx.Validation;
using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RequiredComponentTypeValidator"/> class.
    /// </summary>
    public class RequiredComponentTypeValidatorTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_Ctor()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            Assert.Equal(typeof(MockEntity), validator.RequiredComponentType);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null component type to the ctor.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_Ctor_NullComponentType()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomValidator(null, typeof(GeneticEntity)));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null base type to the ctor.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_Ctor_NullBaseType()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomValidator(typeof(MockEntity), null));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid component type to the ctor.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_Ctor_InvalidComponentType()
        {
            Assert.Throws<ArgumentException>(() => new CustomValidator(typeof(int), typeof(GeneticEntity)));
        }

        /// <summary>
        /// Test that the <see cref="RequiredComponentTypeValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_IsValid_WithAlgorithm()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity))
            {
                HasRequiredComponentReturnValue = true,
                ExpectedGeneticAlgorithm = algorithm
            };

            bool result = validator.IsValid(algorithm, out string errorMessage);

            Assert.True(result);
            Assert.Null(errorMessage);
        }

        /// <summary>
        /// Test that the <see cref="RequiredComponentTypeValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_IsValid_WithNonAlgorithmComponent()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity))
            {
                HasRequiredComponentReturnValue = true,
                ExpectedGeneticAlgorithm = algorithm
            };

            MockTerminator terminator = new MockTerminator();
            terminator.Initialize(algorithm);

            bool result = validator.IsValid(terminator, out string errorMessage);

            Assert.True(result);
            Assert.Null(errorMessage);
        }

        /// <summary>
        /// Test that the <see cref="RequiredComponentTypeValidator.IsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_IsValid_NotValid()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity))
            {
                HasRequiredComponentReturnValue = false,
                ExpectedGeneticAlgorithm = algorithm
            };

            bool result = validator.IsValid(algorithm, out string errorMessage);

            Assert.False(result);
            Assert.NotNull(errorMessage);
        }

        /// <summary>
        /// Test that an exception is thrown when passing a null component to the <see cref="RequiredComponentTypeValidator.IsValid"/> method.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_IsValid_NullComponent()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            Assert.Throws<ArgumentNullException>(() => validator.IsValid(null, out string errorMessage));
        }

        /// <summary>
        /// Test that an exception is thrown when passing a non-initialized non-algorithm component to the <see cref="RequiredComponentTypeValidator.IsValid"/> method.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_IsValid_NoAlgorithm()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            Assert.Throws<ArgumentException>(() => validator.IsValid(new MockTerminator(), out string errorMessage));
        }

        /// <summary>
        /// Tests that the <see cref="RequiredComponentTypeValidator.IsEquivalentType"/> method works correctly.
        /// </summary>
        [Fact]
        public void RequiredComponentTypeValidator_IsEquivalentType()
        {
            CustomValidator validator = new CustomValidator(typeof(MockEntity), typeof(GeneticEntity));
            bool result = validator.TestIsEquivalentType(new DerivedMockEntity());
            Assert.True(result);

            result = validator.TestIsEquivalentType(new MockEntity2());
            Assert.False(result);

            result = validator.TestIsEquivalentType(null);
            Assert.False(result);
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
                Assert.Same(this.ExpectedGeneticAlgorithm, algorithmContext);
                return this.HasRequiredComponentReturnValue;
            }

            public bool TestIsEquivalentType(GeneticComponent configuredComponent)
            {
                return this.IsEquivalentType(configuredComponent);
            }
        }
    }
}
