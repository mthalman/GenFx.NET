using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// <see cref="GeneticEntity"/> made up of a fixed-length list of integers.
    /// </summary>
    public class FixedLengthIntegerListEntity : IntegerListEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthIntegerListEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="FixedLengthIntegerListEntity"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public FixedLengthIntegerListEntity(GeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Returns the initial length to use for <see cref="FixedLengthIntegerListEntity"/>.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="FixedLengthIntegerListEntity"/>.</param>
        /// <returns>Initial length to use for <see cref="FixedLengthIntegerListEntity"/>.</returns>
        private static int GetLength(GeneticAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            FixedLengthIntegerListEntityConfiguration config = (FixedLengthIntegerListEntityConfiguration)algorithm.ConfigurationSet.Entity;
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
        /// Returns a clone of this <see cref="FixedLengthIntegerListEntity"/>.
        /// </summary>
        /// <returns>A clone of this <see cref="FixedLengthIntegerListEntity"/>.</returns>
        public override GeneticEntity Clone()
        {
            FixedLengthIntegerListEntity binaryEntity = new FixedLengthIntegerListEntity(this.Algorithm);
            this.CopyTo(binaryEntity);
            return binaryEntity;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="FixedLengthIntegerListEntity"/>.
    /// </summary>
    [Component(typeof(FixedLengthIntegerListEntity))]
    public class FixedLengthIntegerListEntityConfiguration : IntegerListEntityConfiguration
    {
        private const int DefaultLength = 20;

        private int length = FixedLengthIntegerListEntityConfiguration.DefaultLength;

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
