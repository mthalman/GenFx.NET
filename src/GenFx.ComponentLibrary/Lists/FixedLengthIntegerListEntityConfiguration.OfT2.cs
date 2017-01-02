using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="FixedLengthIntegerListEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TEntity">Type of the associated entity class.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    public abstract class FixedLengthIntegerListEntityConfiguration<TConfiguration, TEntity> : IntegerListEntityConfiguration<TConfiguration, TEntity>
        where TConfiguration : FixedLengthIntegerListEntityConfiguration<TConfiguration, TEntity>
        where TEntity : FixedLengthIntegerListEntity<TEntity, TConfiguration>
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
