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
            int num1 = RandomNumberService.Instance.GetRandomValue(100);
            int num2 = RandomNumberService.Instance.GetRandomValue(100);

            if (num1 == num2)
            {
                // Try again for good measure.
                num2 = RandomNumberService.Instance.GetRandomValue(100);
            }

            Assert.AreNotEqual(num1, num2, "Numbers should probably be different.");
        }

        /// <summary>
        /// Tests that the GetRandomRatio method works correctly.
        /// </summary>
        [TestMethod]
        public void RandomUtil_GetRandomRatio()
        {
            double num1 = RandomNumberService.Instance.GetRandomPercentRatio();
            double num2 = RandomNumberService.Instance.GetRandomPercentRatio();
            Assert.AreNotEqual(num1, num2, "Numbers should probably be different.");
        }
    }


}
