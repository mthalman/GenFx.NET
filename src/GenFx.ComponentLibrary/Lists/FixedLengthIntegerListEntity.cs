using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Entity made up of a fixed-length list of integers.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class FixedLengthIntegerListEntity : IntegerListEntity
    {
        private const int DefaultLength = 20;

        private int fixedLength = DefaultLength;

        /// <summary>
        /// Gets or sets the length to which the list will be fixed.
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
