using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Entity made up of a fixed-length string of bits.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class FixedLengthBinaryStringEntity : BinaryStringEntity
    {
        private const int DefaultLength = 20;

        private int fixedLength = DefaultLength;

        /// <summary>
        /// Gets or sets the fixed length of the binary string.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int FixedLength
        {
            get { return this.fixedLength; }
            set { this.SetProperty(ref this.fixedLength, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the list is a fixed size.
        /// </summary>
        public override bool IsFixedSize { get { return true; } }

        /// <summary>
        /// Returns the initial length to use for the list.
        /// </summary>
        /// <returns>The initial length to use for the list.</returns>
        protected override int GetInitialLength()
        {
            return this.FixedLength;
        }
    }
}
