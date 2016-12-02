using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using GenFx.ComponentLibrary.Properties;
using GenFx.ComponentLibrary.Lists;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// <see cref="GeneticEntity"/> made up of a string of bits.
    /// </summary>
    public abstract class BinaryStringEntity : ListEntityBase<int>
    {
        private BitArray genes;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryStringEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="BinaryStringEntity"/>.</param>
        /// <param name="initialStringLength">Initial length of the binary string.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialStringLength"/> is less than zero.</exception>
        protected BinaryStringEntity(GeneticAlgorithm algorithm, int initialStringLength)
            : base(algorithm)
        {
            this.genes = new BitArray(initialStringLength);
        }

        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <remarks>
        /// By default, the length of a <see cref="BinaryStringEntity"/> cannot be changed
        /// from its initial value unless the derived class overrides this behavior.
        /// </remarks>
        /// <exception cref="ArgumentException">Value is different from the current length.</exception>
        public override int Length
        {
            get { return this.genes.Length; }
            set
            {
                if (value != this.genes.Length)
                {
                    throw new ArgumentException(LibResources.ErrorMsg_BinaryStringEntityLengthCannotBeChanged, nameof(value));
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="BitArray"/> containing the values of the binary string.
        /// </summary>
        protected BitArray Genes
        {
            get { return this.genes; }
        }

        /// <summary>
        /// Gets or sets the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the bit to get or set.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is not one or zero.</exception>
        public override int this[int index]
        {
            get { return Convert.ToInt32(this.genes[index], CultureInfo.CurrentCulture); }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(LibResources.ErrorMsg_NonBinaryValueUsed);
                }

                this.genes[index] = Convert.ToBoolean(value);
                this.UpdateStringRepresentation();
            }
        }

        /// <summary>
        /// Initilizes the binary string with random bit values.
        /// </summary>
        /// <remarks>
        /// <b>Notes to inheritors:</b> When overriding this method, you must either call the <b>InitializeCore</b>
        /// method of the base class or call <see cref="ListEntityBase.UpdateStringRepresentation"/>
        /// after the binary string has been initialized.  This is necessary in order to sync the 
        /// string representation of <see cref="BinaryStringEntity"/> with the initialized
        /// data.
        /// </remarks>
        protected override void InitializeCore()
        {
            for (int i = 0; i < this.Length; i++)
            {
                this[i] = RandomHelper.Instance.GetRandomValue(2);
            }

            this.UpdateStringRepresentation();
        }

        /// <summary>
        /// Copies the state from this <see cref="BinaryStringEntity"/> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="BinaryStringEntity"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            base.CopyTo(entity);

            BinaryStringEntity bsEntity = (BinaryStringEntity)entity;
            bsEntity.genes = (BitArray)this.genes.Clone();
        }

        /// <summary>
        /// Restores the entity's state.
        /// </summary>
        protected override void RestoreState(KeyValueMap state)
        {
            base.RestoreState(state);
            this.genes = new BitArray(((string)state[nameof(this.genes)]).Select(c => c == '1' ? true : false).ToArray());
        }

        /// <summary>
        /// Saves the entity's state.
        /// </summary>
        protected override void SetSaveState(KeyValueMap state)
        {
            base.SetSaveState(state);

            state[nameof(this.genes)] = this.genes.Cast<bool>().Select(b => b ? "1" : "0").Aggregate((s1, s2) => s1 + s2);
        }

        /// <summary>
        /// Calculates the string representation of the <see cref="ListEntityBase{T}"/>.
        /// </summary>
        /// <returns>The string representation.</returns>
        protected override string CalculateStringRepresentation()
        {
            StringBuilder builder = new StringBuilder(this.Length);
            for (int i = 0; i < this.Length; i++)
            {
                builder.Append(this[i]);
            }

            return builder.ToString();
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="BinaryStringEntity"/>.
    /// </summary>
    [Component(typeof(BinaryStringEntity))]
    public abstract class BinaryStringEntityConfiguration : GeneticEntityConfiguration
    {
    }
}
