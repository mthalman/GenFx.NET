using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests.Helpers
{
    internal static class AssertEx
    {
        public static void Throws<T>(Action action)
            where T : Exception
        {
            bool isExceptionThrown = false;
            try
            {
                action();
            }
            catch (T)
            {
                isExceptionThrown = true;
            }

            Assert.IsTrue(isExceptionThrown, "The expected exception {0} was not thrown.", typeof(T).FullName);
        }

        public static async Task ThrowsAsync<T>(Func<Task> func)
            where T : Exception
        {
            bool isExceptionThrown = false;
            try
            {
                await func();
            }
            catch (T)
            {
                isExceptionThrown = true;
            }

            Assert.IsTrue(isExceptionThrown, "The expected exception {0} was not thrown.", typeof(T).FullName);
        }
    }
}
