using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="DefaultTerminator"/> class.
    /// </summary>
    [TestClass]
    public class DefaultTerminatorTest
    {
        /// <summary>
        /// Tests that the correct value is returned for <see cref="DefaultTerminator.IsComplete"/>.
        /// </summary>
        [TestMethod]
        public void DefaultTerminator_IsComplete()
        {
            DefaultTerminator terminator = new DefaultTerminator();
            Assert.IsFalse(terminator.IsComplete());
        }
    }
}
