using System;
using System.Reflection;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    ///This is a test class for GenFx.StringUtil and is intended
    ///to contain all GenFx.StringUtil Unit Tests
    ///</summary>
    public class StringUtilTest
    {
        /// <summary>
        /// Tests that the GetFormattedString method works correctly.
        /// </summary>
        [Fact]
        public void StringUtil_GetFormattedString()
        {
            Type stringUtilType = typeof(GeneticAlgorithm).Assembly.GetType("GenFx.StringUtil");
            MethodInfo method = stringUtilType.GetMethod("GetFormattedString", BindingFlags.Static | BindingFlags.NonPublic);

            object result = method.Invoke(null, new object[] { @"Test\n{0}\t{1}.", new string[] { "1", "2" } });

            Assert.Equal("Test\n1\t2.", result);
        }

    }


}
