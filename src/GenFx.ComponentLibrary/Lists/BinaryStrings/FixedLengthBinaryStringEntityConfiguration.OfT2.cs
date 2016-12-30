using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="FixedLengthBinaryStringEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class FixedLengthBinaryStringEntityConfiguration<TConfiguration, TEntity> : BinaryStringEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : FixedLengthBinaryStringEntityConfiguration<TConfiguration, TEntity>
        where TEntity : FixedLengthBinaryStringEntity<TEntity, TConfiguration>
    {
        private const int DefaultLength = 20;

        private int length = DefaultLength;

        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int Length
        {
            get { return this.length; }
            set { this.SetProperty(ref this.length, value); }
        }
    }
}
