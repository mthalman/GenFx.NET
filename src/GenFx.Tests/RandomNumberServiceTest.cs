using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RandomNumberService"/> class.
    /// </summary>
    [TestClass]
    public class RandomNumberServiceTest
    {
        /// <summary>
        /// Initializes each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }
        
        /// <summary>
        /// Tests that an exception is thrown when <see cref="RandomNumberService.Instance"/> is set to null.
        /// </summary>
        [TestMethod]
        public void RandomNumberService_Instance_Null()
        {
            AssertEx.Throws<ArgumentNullException>(() => RandomNumberService.Instance = null);
        }

        /// <summary>
        /// Tests that the <see cref="RandomNumberService.GetDouble"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RandomNumberService_GetDouble()
        {
            double result = RandomNumberService.Instance.GetDouble();
            Assert.IsTrue(result >= 0 && result <= 1);
        }

        /// <summary>
        /// Tests that the <see cref="RandomNumberService.GetRandomValue"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RandomNumberService_GetRandomValue_Max()
        {
            for (int i = 0; i < 50; i++)
            {
                int result = RandomNumberService.Instance.GetRandomValue(10);
                Assert.IsTrue(result >= 0 && result < 50);
            } 
        }

        /// <summary>
        /// Tests that the <see cref="RandomNumberService.GetRandomValue"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void RandomNumberService_GetRandomValue_MinMax()
        {
            for (int i = 0; i < 50; i++)
            {
                int result = RandomNumberService.Instance.GetRandomValue(100, 110);
                Assert.IsTrue(result >= 100 && result < 110);
            }
        }
    }
}
