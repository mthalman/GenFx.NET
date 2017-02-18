using GenFx;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ComponentValidatorExtensions"/> class.
    /// </summary>
    [TestClass]
    public class ComponentValidatorExtensionsTest
    {
        /// <summary>
        /// Tests that the <see cref="ComponentValidatorExtensions.EnsureIsValid"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void ComponentValidatorExtensions_EnsureIsValid()
        {
            MockEntity entity = new MockEntity();
            TestValidator validator = new TestValidator(entity, true, null);
            ComponentValidatorExtensions.EnsureIsValid(validator, entity);

            string errorMessage = "my error";
            validator = new TestValidator(entity, false, errorMessage);

            AssertEx.Throws<ValidationException>(() => ComponentValidatorExtensions.EnsureIsValid(validator, entity));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null validator to <see cref="ComponentValidatorExtensions.EnsureIsValid"/>.
        /// </summary>
        [TestMethod]
        public void ComponentValidatorExtensions_EnsureIsValid_NullValidator()
        {
            AssertEx.Throws<ArgumentNullException>(() => ComponentValidatorExtensions.EnsureIsValid(null, new MockEntity()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null component to <see cref="ComponentValidatorExtensions.EnsureIsValid"/>.
        /// </summary>
        [TestMethod]
        public void ComponentValidatorExtensions_EnsureIsValid_NullComponent()
        {
            TestValidator validator = new TestValidator(null, true, null);
            AssertEx.Throws<ArgumentNullException>(() => ComponentValidatorExtensions.EnsureIsValid(validator, null));
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
                Assert.AreSame(this.component, component);
                errorMessage = this.errorMessage;
                return this.isValid;
            }
        }
    }
}
