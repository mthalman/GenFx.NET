using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using GenFx;
using System.Reflection;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.StringUtil and is intended
    ///to contain all GenFx.StringUtil Unit Tests
    ///</summary>
    [TestClass()]
    public class StringUtilTest
    {

        /// <summary>
        /// Tests that the GetFormattedString method works correctly.
        /// </summary>
        [TestMethod]
        public void StringUtil_GetFormattedString()
        {
            Type stringUtilType = typeof(GeneticAlgorithm).Assembly.GetType("GenFx.StringUtil");
            MethodInfo method = stringUtilType.GetMethod("GetFormattedString", BindingFlags.Static | BindingFlags.NonPublic);

            object result = method.Invoke(null, new object[] { @"Test\n{0}\t{1}.", new string[] { "1", "2" } });

            Assert.AreEqual("Test\n1\t2.", result, "Incorrect string result.");
        }

    }


}
