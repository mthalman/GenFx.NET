using Xunit;

namespace GenFx.Tests
{
    public class RandomUtilTest
    {
        /// <summary>
        /// Tests that the GetRandomValue method works correctly.
        /// </summary>
        [Fact]
        public void RandomUtil_GetRandomValue()
        {
            int num1 = RandomNumberService.Instance.GetRandomValue(100);
            int num2 = RandomNumberService.Instance.GetRandomValue(100);

            if (num1 == num2)
            {
                // Try again for good metric.
                num2 = RandomNumberService.Instance.GetRandomValue(100);
            }

            Assert.NotEqual(num1, num2);
        }

        /// <summary>
        /// Tests that the GetRandomRatio method works correctly.
        /// </summary>
        [Fact]
        public void RandomUtil_GetRandomRatio()
        {
            double num1 = RandomNumberService.Instance.GetDouble();
            double num2 = RandomNumberService.Instance.GetDouble();
            Assert.NotEqual(num1, num2);
        }
    }


}
