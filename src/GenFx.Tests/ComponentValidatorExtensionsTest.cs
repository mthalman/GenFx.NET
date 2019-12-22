using GenFx.Validation;
using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ComponentValidatorExtensions"/> class.
    /// </summary>
    public class ComponentValidatorExtensionsTest
    {
        /// <summary>
        /// Tests that the <see cref="ComponentValidatorExtensions.EnsureIsValid"/> method works correctly.
        /// </summary>
        [Fact]
        public void ComponentValidatorExtensions_EnsureIsValid()
        {
            MockEntity entity = new MockEntity();
            TestValidator validator = new TestValidator(entity, true, null);
            ComponentValidatorExtensions.EnsureIsValid(validator, entity);

            string errorMessage = "my error";
            validator = new TestValidator(entity, false, errorMessage);

            Assert.Throws<ValidationException>(() => ComponentValidatorExtensions.EnsureIsValid(validator, entity));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null validator to <see cref="ComponentValidatorExtensions.EnsureIsValid"/>.
        /// </summary>
        [Fact]
        public void ComponentValidatorExtensions_EnsureIsValid_NullValidator()
        {
            Assert.Throws<ArgumentNullException>(() => ComponentValidatorExtensions.EnsureIsValid(null, new MockEntity()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null component to <see cref="ComponentValidatorExtensions.EnsureIsValid"/>.
        /// </summary>
        [Fact]
        public void ComponentValidatorExtensions_EnsureIsValid_NullComponent()
        {
            TestValidator validator = new TestValidator(null, true, null);
            Assert.Throws<ArgumentNullException>(() => ComponentValidatorExtensions.EnsureIsValid(validator, null));
        }

        private class TestValidator : ComponentValidator
        {
            private bool isValid;
            private string errorMessage;
            private GeneticComponent component;

            public TestValidator(GeneticComponent component, bool isValid, string errorMessage)
            {
                this.component = component;
                this.isValid = isValid;
                this.errorMessage = errorMessage;
            }

            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                Assert.Same(this.component, component);
                errorMessage = this.errorMessage;
                return this.isValid;
            }
        }
    }
}
