using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using TestCommon.Helpers;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="GeneticComponent"/> class.
    /// </summary>
    [TestClass]
    public class GeneticComponentTest
    {
        /// <summary>
        /// Verifies that the <see cref="GeneticComponent.PropertyChanged"/> event is raised correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_OnPropertyChanged()
        {
            TestComponent component = new TestComponent();

            string propertyName = "foo";

            bool eventRaised = false;
            component.PropertyChanged += (sender, args) =>
            {
                eventRaised = true;
                Assert.AreEqual(propertyName, args.PropertyName);
            };

            component.TestOnPropertyChanged(propertyName);

            Assert.IsTrue(eventRaised);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null or emtpty property name is passed to <see cref="GeneticComponent.OnPropertyChanged"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_OnPropertyChanged_NullOrEmptyPropertyName()
        {
            TestComponent component = new TestComponent();
            AssertEx.Throws<ArgumentException>(() => component.TestOnPropertyChanged(null));
            AssertEx.Throws<ArgumentException>(() => component.TestOnPropertyChanged(String.Empty));
        }

        /// <summary>
        /// Verifies that the <see cref="GeneticComponent.CreateNew"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_CreateNew()
        {
            TestComponent component = new TestComponent();
            GeneticComponent component2 = component.CreateNew();
            Assert.IsInstanceOfType(component2, typeof(TestComponent));
            Assert.AreNotSame(component, component2);
        }

        /// <summary>
        /// Verifies that the <see cref="GeneticComponent.SetProperty"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_SetProperty()
        {
            TestComponent component = new TestComponent();

            bool eventRaised = false;
            component.PropertyChanged += (sender, args) =>
            {
                eventRaised = true;
                Assert.AreEqual(nameof(TestComponent.IntValue), args.PropertyName);
            };

            component.IntValue = 10;
            Assert.IsTrue(eventRaised);

            // Set the property to an invalid value to ensure validation is executed.
            AssertEx.Throws<ValidationException>(() => component.IntValue = 11);
        }

        /// <summary>
        /// Verifies that the <see cref="GeneticComponent.ValidateProperty(object, string)"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_ValidateProperty_Name()
        {
            TestComponent component = new TestComponent();
            
            // is valid
            component.TestValidateProperty(1, nameof(component.IntValue));

            // is not valid
            AssertEx.Throws<ValidationException>(() => component.TestValidateProperty(11, nameof(component.IntValue)));
        }

        /// <summary>
        /// Verifies that an exception is thrown when passing a null or empty property name to
        /// <see cref="GeneticComponent.ValidateProperty(object, string)"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_ValidateProperty_Name_NullOrEmptyPropertyName()
        {
            TestComponent component = new TestComponent();
            AssertEx.Throws<ArgumentException>(() => component.TestValidateProperty(1, (string)null));

            AssertEx.Throws<ArgumentException>(() => component.TestValidateProperty(1, String.Empty));
        }

        /// <summary>
        /// Verifies that an exception is thrown when passing property name of a property that doesn't
        /// exist to <see cref="GeneticComponent.ValidateProperty(object, string)"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_ValidateProperty_Name_NonExistentProperty()
        {
            TestComponent component = new TestComponent();
            AssertEx.Throws<ArgumentException>(() => component.TestValidateProperty(1, "Nothing"));
        }

        /// <summary>
        /// Verifies that the <see cref="GeneticComponent.ValidateProperty(object, PropertyInfo)"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_ValidateProperty_PropertyInfo()
        {
            TestComponent component = new TestComponent();

            // is valid
            component.TestValidateProperty(1, nameof(component.IntValue));

            // is not valid
            AssertEx.Throws<ValidationException>(() => component.TestValidateProperty(11, component.GetType().GetProperty(nameof(component.IntValue))));
        }

        /// <summary>
        /// Verifies that an exception is thrown when passing a null or empty property name to
        /// <see cref="GeneticComponent.ValidateProperty(object, PropertyInfo)"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_ValidateProperty_PropertyInfo_NullOrEmptyPropertyName()
        {
            TestComponent component = new TestComponent();
            AssertEx.Throws<ArgumentNullException>(() => component.TestValidateProperty(1, (PropertyInfo)null));
        }

        /// <summary>
        /// Verifies that the <see cref="GeneticComponent.Validate"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponent_Validate()
        {
            TestComponent component = new TestComponent();
            component.IntValue2 = 2;

            // Should succeed without errors.
            component.Validate();

            component = new TestComponent();
            AssertEx.Throws<ValidationException>(() => component.Validate());

            component = new TestComponent();
            component.IntValue = 5;
            component.IntValue2 = 1;

            AssertEx.Throws<ValidationException>(() => component.Validate());
        }

        [CustomComponentValidator(typeof(TestComponentValidator))]
        private class TestComponent : GeneticComponent
        {
            private int intValue;

            [IntegerValidator(MaxValue = 10)]
            public int IntValue
            {
                get { return this.intValue; }
                set { this.SetProperty(ref this.intValue, value); }
            }

            [IntegerValidator(MinValue = 1)]
            public int IntValue2 { get; set; }

            public void TestOnPropertyChanged(string propertyName)
            {
                this.OnPropertyChanged(propertyName);
            }

            public void TestValidateProperty(object value, string propertyName)
            {
                this.ValidateProperty(value, propertyName);
            }

            public void TestValidateProperty(object value, PropertyInfo propertyInfo)
            {
                this.ValidateProperty(value, propertyInfo);
            }
        }

        private class TestComponentValidator : ComponentValidator
        {
            public override bool IsValid(GeneticComponent component, out string errorMessage)
            {
                if (((TestComponent)component).IntValue == 5)
                {
                    errorMessage = "bad value";
                    return false;
                }

                errorMessage = null;
                return true;
            }
        }
    }
}
