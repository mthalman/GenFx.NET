using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// <see cref="GeneticEntity"/> made up of a fixed-length string of bits.
    /// </summary>
    public class FixedLengthBinaryStringEntity : BinaryStringEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthBinaryStringEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="FixedLengthBinaryStringEntity"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public FixedLengthBinaryStringEntity(GeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Returns the initial length to use for <see cref="FixedLengthBinaryStringEntity"/>.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="FixedLengthBinaryStringEntity"/>.</param>
        /// <returns>Initial length to use for <see cref="FixedLengthBinaryStringEntity"/>.</returns>
        private static int GetLength(GeneticAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            FixedLengthBinaryStringEntityConfiguration config = (FixedLengthBinaryStringEntityConfiguration)algorithm.ConfigurationSet.Entity;
            if (config != null)
            {
                return config.Length;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns a clone of this <see cref="FixedLengthBinaryStringEntity"/>.
        /// </summary>
        /// <returns>A clone of this <see cref="FixedLengthBinaryStringEntity"/>.</returns>
        public override GeneticEntity Clone()
        {
            FixedLengthBinaryStringEntity binaryEntity = new FixedLengthBinaryStringEntity(this.Algorithm);
            this.CopyTo(binaryEntity);
            return binaryEntity;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="FixedLengthBinaryStringEntity"/>.
    /// </summary>
    [Component(typeof(FixedLengthBinaryStringEntity))]
    public class FixedLengthBinaryStringEntityConfiguration : BinaryStringEntityConfiguration
    {
        private const int DefaultLength = 20;

        private int length = FixedLengthBinaryStringEntityConfiguration.DefaultLength;

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
