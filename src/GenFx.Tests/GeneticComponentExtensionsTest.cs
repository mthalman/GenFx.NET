using GenFx;
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
    /// Contains unit tests for the <see cref="GeneticComponentExtensions"/> class.
    /// </summary>
    [TestClass]
    public class GeneticComponentExtensionsTest
    {
        /// <summary>
        /// Tests that the <see cref="GeneticComponentExtensions.CreateNewAndInitialize"/> works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CreateNewAndInitialize()
        {
            TestComponent component = new TestComponent();
            component.MyProperty = 5;
            TestComponent newComponent = (TestComponent)GeneticComponentExtensions.CreateNewAndInitialize(component);

            Assert.AreNotSame(component, newComponent);
            Assert.AreEqual(component.MyProperty, newComponent.MyProperty);

            TestComponentWithAlgorithm componentWithAlg = new TestComponentWithAlgorithm();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            componentWithAlg.Initialize(algorithm);
            TestComponentWithAlgorithm newComponentWithAlg = (TestComponentWithAlgorithm)GeneticComponentExtensions.CreateNewAndInitialize(componentWithAlg);

            Assert.AreNotSame(componentWithAlg, newComponentWithAlg);
            Assert.AreSame(componentWithAlg.Algorithm, newComponentWithAlg.Algorithm);
            Assert.IsTrue(componentWithAlg.InitializeCalled);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null component is passed to <see cref="GeneticComponentExtensions.CreateNewAndInitialize" />.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CreateNewAndInitialize_NullComponent()
        {
            AssertEx.Throws<ArgumentNullException>(() => GeneticComponentExtensions.CreateNewAndInitialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown a null value is returned from <see cref="GeneticComponent.CreateNew"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CreateNewAndInitialize_NullCreateNewResult()
        {
            TestComponentWithCustomizableCreateNew component = new TestComponentWithCustomizableCreateNew
            {
                CreateNewReturnValue = null
            };

            AssertEx.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CreateNewAndInitialize(component));
        }

        /// <summary>
        /// Tests that an exception is thrown a value of the wrong type is returned from <see cref="GeneticComponent.CreateNew"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CreateNewAndInitialize_CreateNewWrongType()
        {
            TestComponentWithCustomizableCreateNew component = new TestComponentWithCustomizableCreateNew
            {
                CreateNewReturnValue = new TestComponent()
            };

            AssertEx.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CreateNewAndInitialize(component));
        }

        /// <summary>
        /// Tests that the <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CopyConfigurationStateTo()
        {
            TestComponent source = new TestComponent
            {
                MyProperty = 3
            };

            TestComponent target = new TestComponent();

            GeneticComponentExtensions.CopyConfigurationStateTo(source, target);

            Assert.AreEqual(source.MyProperty, target.MyProperty);
        }

        /// <summary>
        /// Tests that an exception is thrown when a component with misconfigured configuration property is passed to <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CopyConfigurationStateTo_MisconfiguredConfigProperty()
        {
            AssertEx.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(new MisconfiguredTestComponent(), new MisconfiguredTestComponent()));
            AssertEx.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(new MisconfiguredTest2Component(), new MisconfiguredTest2Component()));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null source is passed to <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CopyConfigurationStateTo_NullSource()
        {
            AssertEx.Throws<ArgumentNullException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(null, new TestComponent()));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null target is passed to <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/>.
        /// </summary>
        [TestMethod]
        public void GeneticComponentExtensions_CopyConfigurationStateTo_NullTarget()
        {
            AssertEx.Throws<ArgumentNullException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(new TestComponent(), null));
        }

        private class TestComponent : GeneticComponent
        {
            [ConfigurationProperty]
            public int MyProperty { get; set; }
        }

        private class MisconfiguredTestComponent : GeneticComponent
        {
            [ConfigurationProperty]
            public int MyProperty { get; }
        }

        private class MisconfiguredTest2Component : GeneticComponent
        {
            [ConfigurationProperty]
            public int MyProperty { set { } }
        }

        private class TestComponentWithAlgorithm : GeneticComponentWithAlgorithm
        {
            public bool InitializeCalled;

            [ConfigurationProperty]
            public int MyProperty { get; set; }

            public override void Initialize(GeneticAlgorithm algorithm)
            {
                this.InitializeCalled = true;
                base.Initialize(algorithm);
            }
        }

        private class TestComponentWithCustomizableCreateNew : GeneticComponent
        {
            public GeneticComponent CreateNewReturnValue { get; set; }

            public override GeneticComponent CreateNew()
            {
                return this.CreateNewReturnValue;
            }
        }
    }
}
