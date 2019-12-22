using GenFx.ComponentLibrary.Algorithms;
using System;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PopulationReplacementValue"/> struct.
    /// </summary>
    public class PopulationReplacementValueTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void PopulationReplacementValue_Ctor()
        {
            PopulationReplacementValue val = new PopulationReplacementValue(3, ReplacementValueKind.Percentage);
            Assert.Equal(3, val.Value);
            Assert.Equal(ReplacementValueKind.Percentage, val.Kind);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid value to ctor.
        /// </summary>
        [Fact]
        public void PopulationReplacementValue_Ctor_InvalidValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PopulationReplacementValue(-1, ReplacementValueKind.Percentage));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid kind to ctor.
        /// </summary>
        [Fact]
        public void PopulationReplacementValue_Ctor_InvalidKind()
        {
            Assert.Throws<ArgumentException>(() => new PopulationReplacementValue(2, (ReplacementValueKind)3));
        }

        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValue.Equals"/> method works correctly.
        /// </summary>
        [Fact]
        public void PopulationReplacementValue_Equals()
        {
            PopulationReplacementValue val1 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val2 = new PopulationReplacementValue(2, ReplacementValueKind.Percentage);
            PopulationReplacementValue val3 = new PopulationReplacementValue(3, ReplacementValueKind.Percentage);
            PopulationReplacementValue val4 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val5 = new PopulationReplacementValue(3, ReplacementValueKind.Percentage);

            Assert.False(val1.Equals(val2));
            Assert.False(val1 == val2);
            Assert.True(val1 != val2);

            Assert.False(val1.Equals(val3));
            Assert.False(val1 == val3);
            Assert.True(val1 != val3);

            Assert.True(val1.Equals(val4));
            Assert.True(val1 == val4);
            Assert.False(val1 != val4);

            Assert.False(val3.Equals(val2));
            Assert.False(val3 == val2);
            Assert.True(val3 != val2);

            Assert.True(val3.Equals(val5));
            Assert.True(val3 == val5);
            Assert.False(val3 != val5);

            Assert.False(val1.Equals(null));
            Assert.False(val1 == null);
            Assert.True(val1 != null);

            Assert.False(val1.Equals("test"));
        }

        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValue.GetHashCode"/> method works correctly.
        /// </summary>
        [Fact]
        public void PopulationReplacementValue_GetHashCode()
        {
            PopulationReplacementValue val1 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val2 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            PopulationReplacementValue val3 = new PopulationReplacementValue(3, ReplacementValueKind.FixedCount);

            Assert.Equal(val1.GetHashCode(), val2.GetHashCode());
            Assert.NotEqual(val1.GetHashCode(), val3.GetHashCode());
        }

        /// <summary>
        /// Tests that the <see cref="PopulationReplacementValue.ToString"/> method works correctly.
        /// </summary>
        [Fact]
        public void PopulationReplacementValue_ToString()
        {
            PopulationReplacementValue val1 = new PopulationReplacementValue(2, ReplacementValueKind.FixedCount);
            Assert.Equal("2", val1.ToString());

            PopulationReplacementValue val2 = new PopulationReplacementValue(2, ReplacementValueKind.Percentage);
            Assert.Equal("2%", val2.ToString());
        }
    }
}
