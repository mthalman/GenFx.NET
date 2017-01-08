using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Entity made up of a variable-length string of bits.
    /// </summary>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the entity's configuration class.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public abstract class VariableLengthBinaryStringEntity<TEntity, TConfiguration> : BinaryStringEntity<TEntity, TConfiguration>
        where TEntity : VariableLengthBinaryStringEntity<TEntity, TConfiguration>
        where TConfiguration : VariableLengthBinaryStringEntityFactoryConfig<TConfiguration, TEntity>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected VariableLengthBinaryStringEntity(IGeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Gets a value indicating whether the list is a fixed size.
        /// </summary>
        public override bool IsFixedSize { get { return false; } }

        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <remarks>
        /// The length of an entity can be changed
        /// from its initial value.  The string will be truncated if the value is less than the current length.
        /// The string will be expanded with zeroes if the value is greater than the current length.
        /// </remarks>
        public override int Length
        {
            get { return base.Length; }
            set
            {
                if (value != this.Length)
                {
                    this.Genes.Length = value;
                    this.UpdateStringRepresentation();
                }
            }
        }

        /// <summary>
        /// Returns the length to use for the entity.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <returns>Length to use for the entity.</returns>
        private static int GetLength(IGeneticAlgorithm algorithm)
        {
            VariableLengthBinaryStringEntityFactoryConfig<TConfiguration, TEntity> config = 
                (VariableLengthBinaryStringEntityFactoryConfig<TConfiguration, TEntity>)algorithm.ConfigurationSet.Entity;
            if (config != null)
            {
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
            else
            {
                return 0;
            }
        }
    }
}
