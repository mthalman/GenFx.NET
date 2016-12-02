using System;
using System.Collections;
using System.ComponentModel;
using GenFx.ComponentLibrary.Properties;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// <see cref="GeneticEntity"/> made up of a variable-length list of integers.
    /// </summary>
    public class VariableLengthIntegerListEntity : IntegerListEntity
    {
        /// <summary>
        /// Gets or sets the length of the integer list.
        /// </summary>
        /// <remarks>
        /// The length of a <see cref="VariableLengthIntegerListEntity"/> can be changed
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
			                this.Genes.Add(0);
			            }
                    }
                    else
                    {
                        for (int i = 0; i < this.Length - value; i++)
			            {
			                this.Genes.RemoveAt(this.Genes.Count - 1);
			            }
                    }

                    this.UpdateStringRepresentation();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableLengthIntegerListEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="VariableLengthIntegerListEntity"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public VariableLengthIntegerListEntity(GeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Returns the length to use for the <see cref="VariableLengthIntegerListEntity"/>.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="VariableLengthIntegerListEntity"/>.</param>
        /// <returns>Length to use for the <see cref="VariableLengthIntegerListEntity"/>.</returns>
        private static int GetLength(GeneticAlgorithm algorithm)
        {
            VariableLengthIntegerListEntityConfiguration config = (VariableLengthIntegerListEntityConfiguration)algorithm.ConfigurationSet.Entity;
            if (config != null)
            {
                int minLength = config.MinimumStartingLength;
                int maxLength = config.MaximumStartingLength;

                if (minLength > maxLength)
                {
                    throw new ValidationException(
                      StringUtil.GetFormattedString(
                        LibResources.ErrorMsg_MismatchedMinMaxValues,
                        nameof(VariableLengthIntegerListEntityConfiguration.MinimumStartingLength),
                        nameof(VariableLengthIntegerListEntityConfiguration.MaximumStartingLength)));
                }

                return RandomHelper.Instance.GetRandomValue(minLength, maxLength + 1);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns a clone of this <see cref="VariableLengthIntegerListEntity"/>.
        /// </summary>
        /// <returns>A clone of this <see cref="VariableLengthIntegerListEntity"/>.</returns>
        public override GeneticEntity Clone()
        {
            VariableLengthIntegerListEntity entity = new VariableLengthIntegerListEntity(this.Algorithm);
            this.CopyTo(entity);
            return entity;
        }

        /// <summary>
        /// Removes the bit value located at the specified position.
        /// </summary>
        /// <param name="index">Index of the bit to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than or equal to <see cref="Length"/>.</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Length)
            {
                throw new ArgumentOutOfRangeException("index", LibResources.ErrorMsg_VariableLengthListEntity_RemoveBit_OutOfRange);
            }

            this.Genes.RemoveAt(index);

            this.UpdateStringRepresentation();
        }

        /// <summary>
        /// Inserts the bit value at the specified position.
        /// </summary>
        /// <param name="index">Index to insert the bit value at.</param>
        /// <param name="value">Bit value to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than <see cref="Length"/>.</exception>
        public void Insert(int index, int value)
        {
            if (index < 0 || index > this.Length)
            {
                throw new ArgumentOutOfRangeException("index", LibResources.ErrorMsg_VariableLengthListEntity_InsertBit_OutOfRange);
            }

            this.Genes.Insert(index, value);

            this.UpdateStringRepresentation();
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="VariableLengthIntegerListEntity"/>.
    /// </summary>
    [Component(typeof(VariableLengthIntegerListEntity))]
    public class VariableLengthIntegerListEntityConfiguration : IntegerListEntityConfiguration
    {
        private const int DefaultMaxLength = 20;
        private const int DefaultMinLength = 5;

        private int minimumStartingLength = VariableLengthIntegerListEntityConfiguration.DefaultMinLength;
        private int maximumStartingLength = VariableLengthIntegerListEntityConfiguration.DefaultMaxLength;

        /// <summary>
        /// Gets or sets the maximum starting length a new <see cref="VariableLengthIntegerListEntity"/> can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int MaximumStartingLength
        {
            get { return this.maximumStartingLength; }
            set { this.SetProperty(ref this.maximumStartingLength, value); }
        }

        /// <summary>
        /// Gets or sets the minimum starting length a new <see cref="VariableLengthIntegerListEntity"/> can have.
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
