using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="GeneticComponentExtensions"/> class.
    /// </summary>
    public class GeneticComponentExtensionsTest
    {
        /// <summary>
        /// Tests that the <see cref="GeneticComponentExtensions.CreateNewAndInitialize"/> works correctly.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CreateNewAndInitialize()
        {
            TestComponent component = new TestComponent();
            component.MyProperty = 5;
            TestComponent newComponent = (TestComponent)GeneticComponentExtensions.CreateNewAndInitialize(component);

            Assert.NotSame(component, newComponent);
            Assert.Equal(component.MyProperty, newComponent.MyProperty);

            TestComponentWithAlgorithm componentWithAlg = new TestComponentWithAlgorithm();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            componentWithAlg.Initialize(algorithm);
            TestComponentWithAlgorithm newComponentWithAlg = (TestComponentWithAlgorithm)GeneticComponentExtensions.CreateNewAndInitialize(componentWithAlg);

            Assert.NotSame(componentWithAlg, newComponentWithAlg);
            Assert.Same(componentWithAlg.Algorithm, newComponentWithAlg.Algorithm);
            Assert.True(componentWithAlg.InitializeCalled);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null component is passed to <see cref="GeneticComponentExtensions.CreateNewAndInitialize" />.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CreateNewAndInitialize_NullComponent()
        {
            Assert.Throws<ArgumentNullException>(() => GeneticComponentExtensions.CreateNewAndInitialize(null));
        }

        /// <summary>
        /// Tests that an exception is thrown a null value is returned from <see cref="GeneticComponent.CreateNew"/>.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CreateNewAndInitialize_NullCreateNewResult()
        {
            TestComponentWithCustomizableCreateNew component = new TestComponentWithCustomizableCreateNew
            {
                CreateNewReturnValue = null
            };

            Assert.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CreateNewAndInitialize(component));
        }

        /// <summary>
        /// Tests that an exception is thrown a value of the wrong type is returned from <see cref="GeneticComponent.CreateNew"/>.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CreateNewAndInitialize_CreateNewWrongType()
        {
            TestComponentWithCustomizableCreateNew component = new TestComponentWithCustomizableCreateNew
            {
                CreateNewReturnValue = new TestComponent()
            };

            Assert.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CreateNewAndInitialize(component));
        }

        /// <summary>
        /// Tests that the <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CopyConfigurationStateTo()
        {
            TestComponent source = new TestComponent
            {
                MyProperty = 3
            };

            TestComponent target = new TestComponent();

            GeneticComponentExtensions.CopyConfigurationStateTo(source, target);

            Assert.Equal(source.MyProperty, target.MyProperty);
        }

        /// <summary>
        /// Tests that an exception is thrown when a component with misconfigured configuration property is passed to <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/>.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CopyConfigurationStateTo_MisconfiguredConfigProperty()
        {
            Assert.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(new MisconfiguredTestComponent(), new MisconfiguredTestComponent()));
            Assert.Throws<InvalidOperationException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(new MisconfiguredTest2Component(), new MisconfiguredTest2Component()));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null source is passed to <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/>.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CopyConfigurationStateTo_NullSource()
        {
            Assert.Throws<ArgumentNullException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(null, new TestComponent()));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null target is passed to <see cref="GeneticComponentExtensions.CopyConfigurationStateTo"/>.
        /// </summary>
        [Fact]
        public void GeneticComponentExtensions_CopyConfigurationStateTo_NullTarget()
        {
            Assert.Throws<ArgumentNullException>(() => GeneticComponentExtensions.CopyConfigurationStateTo(new TestComponent(), null));
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
