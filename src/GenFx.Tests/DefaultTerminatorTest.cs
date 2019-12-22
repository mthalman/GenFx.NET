using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="DefaultTerminator"/> class.
    /// </summary>
    public class DefaultTerminatorTest
    {
        /// <summary>
        /// Tests that the correct value is returned for <see cref="DefaultTerminator.IsComplete"/>.
        /// </summary>
        [Fact]
        public void DefaultTerminator_IsComplete()
        {
            DefaultTerminator terminator = new DefaultTerminator();
            Assert.False(terminator.IsComplete());
        }
    }
}
