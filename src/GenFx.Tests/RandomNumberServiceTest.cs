using System;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="RandomNumberService"/> class.
    /// </summary>
    public class RandomNumberServiceTest
    {
        /// <summary>
        /// Initializes each test.
        /// </summary>
        public RandomNumberServiceTest()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }
        
        /// <summary>
        /// Tests that an exception is thrown when <see cref="RandomNumberService.Instance"/> is set to null.
        /// </summary>
        [Fact]
        public void RandomNumberService_Instance_Null()
        {
            Assert.Throws<ArgumentNullException>(() => RandomNumberService.Instance = null);
        }

        /// <summary>
        /// Tests that the <see cref="RandomNumberService.GetDouble"/> method works correctly.
        /// </summary>
        [Fact]
        public void RandomNumberService_GetDouble()
        {
            double result = RandomNumberService.Instance.GetDouble();
            Assert.True(result >= 0 && result <= 1);
        }

        /// <summary>
        /// Tests that the <see cref="RandomNumberService.GetRandomValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void RandomNumberService_GetRandomValue_Max()
        {
            for (int i = 0; i < 50; i++)
            {
                int result = RandomNumberService.Instance.GetRandomValue(10);
                Assert.True(result >= 0 && result < 50);
            } 
        }

        /// <summary>
        /// Tests that the <see cref="RandomNumberService.GetRandomValue"/> method works correctly.
        /// </summary>
        [Fact]
        public void RandomNumberService_GetRandomValue_MinMax()
        {
            for (int i = 0; i < 50; i++)
            {
                int result = RandomNumberService.Instance.GetRandomValue(100, 110);
                Assert.True(result >= 100 && result < 110);
            }
        }
    }
}
