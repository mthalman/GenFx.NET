using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFxTests
{
    /// <summary>
    /// Unit tests for the ExternalValidatorAttributeHelper class.
    /// </summary>
    [TestClass]
    public class ExternalValidatorAttributeHelperTest
    {
        /// <summary>
        /// Tests that ValidateArgs method throws when a null target type is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExternalValidatorAttributeHelper_ValidateArgs_NullTargetType()
        {
            ExternalValidatorAttributeHelper.ValidateArguments(null, "c");
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when a null target property is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExternalValidatorAttributeHelper_ValidateArgs_NullTargetProperty()
        {
            ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponentConfiguration), null);
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an empty target property is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExternalValidatorAttributeHelper_ValidateArgs_EmptyTargetProperty()
        {
            ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponentConfiguration), String.Empty);
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an invalid target type is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExternalValidatorAttributeHelper_ValidateArgs_InvalidTargetType()
        {
            ExternalValidatorAttributeHelper.ValidateArguments(typeof(InvalidComponentConfiguration), "Value");
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an invalid target property is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExternalValidatorAttributeHelper_ValidateArgs_InvalidTargetProperty()
        {
            ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponentConfiguration), "boo");
        }

        private class FakeComponentConfiguration : ComponentConfiguration
        {
            private int value;

            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        private class InvalidComponentConfiguration
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
