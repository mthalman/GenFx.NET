using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using GenFx;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.GARandom and is intended
    ///to contain all GenFx.GARandom Unit Tests
    ///</summary>
    [TestClass()]
    public class RandomUtilTest
    {
        /// <summary>
        /// Tests that the GetRandomValue method works correctly.
        /// </summary>
        [TestMethod]
        public void RandomUtil_GetRandomValue()
        {
            int num1 = RandomHelper.Instance.GetRandomValue(100);
            int num2 = RandomHelper.Instance.GetRandomValue(100);
            Assert.AreNotEqual(num1, num2, "Numbers should probably be different.");
        }

        /// <summary>
        /// Tests that the GetRandomRatio method works correctly.
        /// </summary>
        [TestMethod]
        public void RandomUtil_GetRandomRatio()
        {
            double num1 = RandomHelper.Instance.GetRandomRatio();
            double num2 = RandomHelper.Instance.GetRandomRatio();
            Assert.AreNotEqual(num1, num2, "Numbers should probably be different.");
        }
    }


}
