using GenFx;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using TestCommon.Helpers;

namespace GenFx.Tests
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
        public void ExternalValidatorAttributeHelper_ValidateArgs_NullTargetType()
        {
            AssertEx.Throws<ArgumentNullException>(() => ExternalValidatorAttributeHelper.ValidateArguments(null, "c"));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when a null target property is passed.
        /// </summary>
        [TestMethod]
        public void ExternalValidatorAttributeHelper_ValidateArgs_NullTargetProperty()
        {
            AssertEx.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponent), null));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an empty target property is passed.
        /// </summary>
        [TestMethod]
        public void ExternalValidatorAttributeHelper_ValidateArgs_EmptyTargetProperty()
        {
            AssertEx.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponent), String.Empty));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an invalid target type is passed.
        /// </summary>
        [TestMethod]
        public void ExternalValidatorAttributeHelper_ValidateArgs_InvalidTargetType()
        {
            AssertEx.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(InvalidComponent), "Value"));
        }

        /// <summary>
        /// Tests that ValidateArgs method throws when an invalid target property is passed.
        /// </summary>
        [TestMethod]
        public void ExternalValidatorAttributeHelper_ValidateArgs_InvalidTargetProperty()
        {
            AssertEx.Throws<ArgumentException>(() => ExternalValidatorAttributeHelper.ValidateArguments(typeof(FakeComponent), "boo"));
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
