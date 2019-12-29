using System.ComponentModel;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="DisplayNameHelper"/> class.
    /// </summary>
    public class DisplayNameHelperTest
    {
        private const string TestClassDisplayName = "my test class";
        private const string TestClass2DisplayName = "my test class2";

        /// <summary>
        /// Tests that the <see cref="DisplayNameHelper.GetDisplayName"/> method returns the correct value
        /// when the object's type has a <see cref="DisplayNameAttribute"/>.
        /// </summary>
        [Fact]
        public void DisplayNameHelper_GetDisplayName_DisplayNameAttribute()
        {
            string result = DisplayNameHelper.GetDisplayName(new TestClass());
            Assert.Equal(TestClassDisplayName, result);
        }

        /// <summary>
        /// Tests that the <see cref="DisplayNameHelper.GetDisplayName"/> method returns the correct value
        /// when the object's type overrides <see cref="object.ToString"/>.
        /// </summary>
        [Fact]
        public void DisplayNameHelper_GetDisplayName_ToString()
        {
            string result = DisplayNameHelper.GetDisplayName(new TestClass2());
            Assert.Equal(TestClass2DisplayName, result);
        }

        /// <summary>
        /// Tests that the <see cref="DisplayNameHelper.GetDisplayName"/> method returns the correct value
        /// when the object has no explicit definition of display name.
        /// </summary>
        [Fact]
        public void DisplayNameHelper_GetDisplayName_Default()
        {
            string result = DisplayNameHelper.GetDisplayName(new TestClass3());
            Assert.Equal(nameof(TestClass3), result);
        }

        /// <summary>
        /// Tests that the <see cref="DisplayNameHelper.GetDisplayNameWithTypeInfo"/> method returns the correct value.
        /// </summary>
        [Fact]
        public void DisplayNameHelper_GetDisplayNameWithTypeInfo()
        {
            string result = DisplayNameHelper.GetDisplayNameWithTypeInfo(new TestClass());
            Assert.Equal(TestClassDisplayName + " [" + typeof(TestClass).FullName + "]", result);
        }

        [DisplayName(TestClassDisplayName)]
        private class TestClass
        {
        }

        private class TestClass2
        {
            public override string ToString()
            {
                return TestClass2DisplayName;
            }
        }

        private class TestClass3
        {
        }
    }
}
