using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="VariableLengthBinaryStringEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    public abstract class VariableLengthBinaryStringEntityFactoryConfig<TConfiguration, TEntity> : BinaryStringEntityFactoryConfig<TConfiguration, TEntity>
        where TConfiguration : VariableLengthBinaryStringEntityFactoryConfig<TConfiguration, TEntity>
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
