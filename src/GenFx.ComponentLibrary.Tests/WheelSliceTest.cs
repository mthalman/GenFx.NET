using GenFx.ComponentLibrary.SelectionOperators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="WheelSlice"/> class.
    /// </summary>
    [TestClass]
    public class WheelSliceTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void WheelSlice_Ctor()
        {
            MockEntity entity = new MockEntity();
            double size = 3;
            WheelSlice slice = new WheelSlice(entity, size);
            Assert.AreSame(entity, slice.Entity);
            Assert.AreEqual(size, slice.Size);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity.
        /// </summary>
        [TestMethod]
        public void WheelSlice_Ctor_NullEntity()
        {
            AssertEx.Throws<ArgumentNullException>(() => new WheelSlice(null, 2));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null entity.
        /// </summary>
        [TestMethod]
        public void WheelSlice_Ctor_InvalidSize()
        {
            AssertEx.Throws<ArgumentException>(() => new WheelSlice(new MockEntity(), -1));
        }
    }
}
