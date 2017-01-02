using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Entity made up of a variable-length list of integers.
    /// </summary>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the entity's configuration class.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    public abstract class VariableLengthIntegerListEntity<TEntity, TConfiguration> : IntegerListEntity<TEntity, TConfiguration>
        where TEntity : VariableLengthIntegerListEntity<TEntity, TConfiguration>
        where TConfiguration : VariableLengthIntegerListEntityConfiguration<TConfiguration, TEntity>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected VariableLengthIntegerListEntity(IGeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Gets a value indicating whether the list is a fixed size.
        /// </summary>
        public override bool IsFixedSize { get { return false; } }

        /// <summary>
        /// Gets or sets the length of the integer list.
        /// </summary>
        /// <remarks>
        /// The length of this entity can be changed
        /// from its initial value.  The list will be truncated if the value is less than the current length.
        /// The list will be expanded with zeroes if the value is greater than the current length.
        /// </remarks>
        public override int Length
        {
            get { return base.Length; }
            set
            {
                if (value != this.Length)
                {
                    if (value > this.Length)
                    {
                        for (int i = 0; i < value - this.Length; i++)
                        {
                            this.Add(0);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < this.Length - value; i++)
                        {
                            this.RemoveAt(this.Length - 1);
                        }
                    }

                    this.UpdateStringRepresentation();
                }
            }
        }

        /// <summary>
        /// Returns the length to use for this object.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <returns>Length to use for this object.</returns>
        private static int GetLength(IGeneticAlgorithm algorithm)
        {
            VariableLengthIntegerListEntityConfiguration<TConfiguration, TEntity> config =
                (VariableLengthIntegerListEntityConfiguration<TConfiguration, TEntity>)algorithm.ConfigurationSet.Entity;

            int minLength = config.MinimumStartingLength;
            int maxLength = config.MaximumStartingLength;

            if (minLength > maxLength)
            {
                throw new ValidationException(
                    StringUtil.GetFormattedString(
                    Resources.ErrorMsg_MismatchedMinMaxValues,
                    nameof(config.MinimumStartingLength),
                    nameof(config.MaximumStartingLength)));
            }

            return RandomNumberService.Instance.GetRandomValue(minLength, maxLength + 1);
        }
    }
}
