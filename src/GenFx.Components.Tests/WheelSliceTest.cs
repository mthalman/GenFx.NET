using GenFx.Components.SelectionOperators;
using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="WheelSlice"/> class.
    /// </summary>
    public class WheelSliceTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void WheelSlice_Ctor()
        {
            MockEntity entity = new MockEntity();
            double size = 3;
            WheelSlice slice = new WheelSlice(entity, size);
            Assert.Same(entity, slice.Entity);
            Assert.Equal(size, slice.Size);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity.
        /// </summary>
        [Fact]
        public void WheelSlice_Ctor_NullEntity()
        {
            Assert.Throws<ArgumentNullException>(() => new WheelSlice(null, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity.
        /// </summary>
        [Fact]
        public void WheelSlice_Ctor_InvalidSize()
        {
            Assert.Throws<ArgumentException>(() => new WheelSlice(new MockEntity(), -1));
        }
    }
}
