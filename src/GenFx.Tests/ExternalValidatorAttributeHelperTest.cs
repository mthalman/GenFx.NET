using GenFx.Validation;
using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Unit tests for the ExternalValidatorAttributeHelper class.
    /// </summary>
    public class ExternalValidatorAttributeHelperTest
    {
        /// <summary>
        /// Tests that ValidateArgs method throws when a null target type is passed.
        /// </summary>
        [Fact]
        public void ExternalValidatorAttributeHelper_ValidateArgs_NullTargetType()
        {
            Assert.Throws<ArgumentNullException>(() => ExternalValidatorAttributeHelper.ValidateArguments(null, "c"));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when a null target property is passed.
        /// </summary>
        [Fact]
        public void ExternalValidatorAttributeHelper_ValidateArgs_NullTargetProperty()
        {
            Assert.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponent), null));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an empty target property is passed.
        /// </summary>
        [Fact]
        public void ExternalValidatorAttributeHelper_ValidateArgs_EmptyTargetProperty()
        {
            Assert.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponent), String.Empty));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an invalid target type is passed.
        /// </summary>
        [Fact]
        public void ExternalValidatorAttributeHelper_ValidateArgs_InvalidTargetType()
        {
            Assert.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(InvalidComponent), "Value"));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an invalid target property is passed.
        /// </summary>
        [Fact]
        public void ExternalValidatorAttributeHelper_ValidateArgs_InvalidTargetProperty()
        {
            Assert.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponent), "boo"));
        }

        private class FakeComponent : GeneticComponent
        {
            private int value;

            [ConfigurationProperty]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        private class InvalidComponent
        {
            private int value;

            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }
    }
}
