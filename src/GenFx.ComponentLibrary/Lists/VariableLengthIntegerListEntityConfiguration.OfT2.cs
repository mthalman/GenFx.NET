using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="VariableLengthIntegerListEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    public abstract class VariableLengthIntegerListEntityConfiguration<TConfiguration, TEntity> : IntegerListEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : VariableLengthIntegerListEntityConfiguration<TConfiguration, TEntity> 
        where TEntity : VariableLengthIntegerListEntity<TEntity, TConfiguration>
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
