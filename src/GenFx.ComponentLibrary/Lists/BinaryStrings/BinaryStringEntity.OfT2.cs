using GenFx.ComponentLibrary.Properties;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// <see cref="IGeneticEntity"/> made up of a string of bits.
    /// </summary>
    /// <remarks>This class uses a <see cref="BitArray"/> data structure to represent the list.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public abstract class BinaryStringEntity<TEntity, TConfiguration> : ListEntityBase<TEntity, TConfiguration, bool>
        where TEntity : BinaryStringEntity<TEntity, TConfiguration>
        where TConfiguration : BinaryStringEntityConfiguration<TConfiguration, TEntity>
    {
        private BitArray genes;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <param name="initialStringLength">Initial length of the binary string.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialStringLength"/> is less than zero.</exception>
        protected BinaryStringEntity(IGeneticAlgorithm algorithm, int initialStringLength)
            : base(algorithm)
        {
            this.genes = new BitArray(initialStringLength);
        }

        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <remarks>
        /// By default, the length of a <see cref="BinaryStringEntity{TEntity, TConfiguration}"/> cannot be changed
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
        public override bool this[int index]
        {
            get { return this.genes[index]; }
            set
            {
                this.genes[index] = value;
                this.UpdateStringRepresentation();
            }
        }

        /// <summary>
        /// Initilizes the binary string with random bit values.
        /// </summary>
        /// <remarks>
        /// <b>Notes to inheritors:</b> When overriding this method, you must either call the <b>InitializeCore</b>
        /// method of the base class or call <see cref="ListEntityBase{TEntity, TConfiguration, TItem}.UpdateStringRepresentation"/>
        /// after the binary string has been initialized.  This is necessary in order to sync the 
        /// string representation of this entity with the initialized
        /// data.
        /// </remarks>
        protected override void InitializeCore()
        {
            for (int i = 0; i < this.Length; i++)
            {
                this[i] = RandomHelper.Instance.GetRandomValue(2) == 1 ? true : false;
            }

            this.UpdateStringRepresentation();
        }

        /// <summary>
        /// Copies the state from this object to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="BinaryStringEntity{TEntity, TConfiguration}"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(TEntity entity)
        {
            base.CopyTo(entity);

            entity.genes = (BitArray)this.genes.Clone();
            entity.UpdateStringRepresentation();
        }

        /// <summary>
        /// Restores the entity's state.
        /// </summary>
        public override void RestoreState(KeyValueMap state)
        {
            base.RestoreState(state);
            this.genes = new BitArray(((string)state[nameof(this.genes)]).Select(c => c == '1' ? true : false).ToArray());
        }

        /// <summary>
        /// Saves the entity's state.
        /// </summary>
        public override void SetSaveState(KeyValueMap state)
        {
            base.SetSaveState(state);

            state[nameof(this.genes)] = this.genes.Cast<bool>().Select(b => b ? "1" : "0").Aggregate((s1, s2) => s1 + s2);
        }

        /// <summary>
        /// Calculates the string representation of the entity.
        /// </summary>
        /// <returns>The string representation.</returns>
        protected override string CalculateStringRepresentation()
        {
            StringBuilder builder = new StringBuilder(this.Length);
            for (int i = 0; i < this.Length; i++)
            {
                builder.Append(this[i] ? "1" : "0");
            }

            return builder.ToString();
        }
    }
}
