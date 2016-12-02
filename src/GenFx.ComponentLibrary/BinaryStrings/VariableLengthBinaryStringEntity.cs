using System;
using System.Collections;
using System.ComponentModel;
using GenFx.ComponentLibrary.Properties;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// <see cref="GeneticEntity"/> made up of a variable-length string of bits.
    /// </summary>
    public class VariableLengthBinaryStringEntity : BinaryStringEntity
    {
        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <remarks>
        /// The length of a <see cref="VariableLengthBinaryStringEntity"/> can be changed
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
        /// Initializes a new instance of the <see cref="VariableLengthBinaryStringEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="VariableLengthBinaryStringEntity"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public VariableLengthBinaryStringEntity(GeneticAlgorithm algorithm)
            : base(algorithm, GetLength(algorithm))
        {
        }

        /// <summary>
        /// Returns the length to use for the <see cref="VariableLengthBinaryStringEntity"/>.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="VariableLengthBinaryStringEntity"/>.</param>
        /// <returns>Length to use for the <see cref="VariableLengthBinaryStringEntity"/>.</returns>
        private static int GetLength(GeneticAlgorithm algorithm)
        {
            VariableLengthBinaryStringEntityConfiguration config = (VariableLengthBinaryStringEntityConfiguration)algorithm.ConfigurationSet.Entity;
            if (config != null)
            {
                int minLength = config.MinimumStartingLength;
                int maxLength = config.MaximumStartingLength;

                if (minLength > maxLength)
                {
                    throw new ValidationException(
                      StringUtil.GetFormattedString(
                        LibResources.ErrorMsg_MismatchedMinMaxValues,
                        nameof(VariableLengthBinaryStringEntityConfiguration.MinimumStartingLength),
                        nameof(VariableLengthBinaryStringEntityConfiguration.MaximumStartingLength)));
                }

                return RandomHelper.Instance.GetRandomValue(minLength, maxLength + 1);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns a clone of this <see cref="VariableLengthBinaryStringEntity"/>.
        /// </summary>
        /// <returns>A clone of this <see cref="VariableLengthBinaryStringEntity"/>.</returns>
        public override GeneticEntity Clone()
        {
            VariableLengthBinaryStringEntity entity = new VariableLengthBinaryStringEntity(this.Algorithm);
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

            // Since we can't remove individual bits elements from the BitArray, 
            // we have to make a new copy that is one bit shorter than the original
            // and translate the old bit values to their new positions.

            BitArray copy = (BitArray)this.Genes.Clone();

            this.Genes.Length--;

            int currentGenesIndex = 0;
            for (int i = 0; i < copy.Length; i++)
            {
                if (i != index)
                {
                    this.Genes[currentGenesIndex] = copy[i];
                    currentGenesIndex++;
                }
            }

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

            if (index == this.Length)
            {
                this.Length++;
                this[this.Length - 1] = value;
                return;
            }

            // Since we can't insert individual bits elements into the BitArray, 
            // we have to make a new copy that is one bit longer than the original
            // and translate the old bit values to their new positions.

            BitArray copy = (BitArray)this.Genes.Clone();

            this.Genes.Length++;

            int copiedGenesIndex = 0;
            for (int i = 0; i < this.Genes.Length; i++)
            {
                if (i == index)
                {
                    this[i] = value;
                }
                else
                {
                    this.Genes[i] = copy[copiedGenesIndex];
                    copiedGenesIndex++;
                }
            }

            this.UpdateStringRepresentation();
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="VariableLengthBinaryStringEntity"/>.
    /// </summary>
    [Component(typeof(VariableLengthBinaryStringEntity))]
    public class VariableLengthBinaryStringEntityConfiguration : BinaryStringEntityConfiguration
    {
        private const int DefaultMaxLength = 20;
        private const int DefaultMinLength = 5;

        private int minimumStartingLength = VariableLengthBinaryStringEntityConfiguration.DefaultMinLength;
        private int maximumStartingLength = VariableLengthBinaryStringEntityConfiguration.DefaultMaxLength;

        /// <summary>
        /// Gets or sets the maximum starting length a new <see cref="VariableLengthBinaryStringEntity"/> can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int MaximumStartingLength
        {
            get { return this.maximumStartingLength; }
            set { this.SetProperty(ref this.maximumStartingLength, value); }
        }

        /// <summary>
        /// Gets or sets the minimum starting length a new <see cref="VariableLengthBinaryStringEntity"/> can have.
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
