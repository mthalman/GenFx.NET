using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="VariableLengthBinaryStringEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class VariableLengthBinaryStringEntityConfiguration<TConfiguration, TEntity> : BinaryStringEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : VariableLengthBinaryStringEntityConfiguration<TConfiguration, TEntity>
        where TEntity : VariableLengthBinaryStringEntity<TEntity, TConfiguration>
    {
        private const int DefaultMaxLength = 20;
        private const int DefaultMinLength = 5;

        private int minimumStartingLength = DefaultMinLength;
        private int maximumStartingLength = DefaultMaxLength;

        /// <summary>
        /// Gets or sets the maximum starting length a new entity can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int MaximumStartingLength
        {
            get { return this.maximumStartingLength; }
            set { this.SetProperty(ref this.maximumStartingLength, value); }
        }

        /// <summary>
        /// Gets or sets the minimum starting length a new entity can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int MinimumStartingLength
        {
            get { return this.minimumStartingLength; }
            set { this.SetProperty(ref this.minimumStartingLength, value); }
        }
    }
}
