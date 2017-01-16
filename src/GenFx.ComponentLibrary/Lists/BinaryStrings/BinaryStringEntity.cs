using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
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
    public class BinaryStringEntity : ListEntityBase<bool>
    {
        private BitArray genes;
        
        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <remarks>
        /// The length of this entity can be changed
        /// from its initial value.  The list will be truncated if the value is less than the current length.
        /// The list will be expanded with zeroes if the value is greater than the current length.
        /// </remarks>
        public override int Length
        {
            get
            {
                this.EnsureEntityIsInitialized();
                return this.genes.Count;
            }
            set
            {
                if (value != this.Length)
                {
                    if (this.MinimumStartingLength == this.MaximumStartingLength)
                    {
                        throw new ArgumentException(Resources.ErrorMsg_ListEntityLengthCannotBeChanged, nameof(value));
                    }

                    this.genes.Length = value;

                    this.UpdateStringRepresentation();
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
            get
            {
                this.EnsureEntityIsInitialized();
                return this.genes[index];
            }
            set
            {
                this.EnsureEntityIsInitialized();
                this.genes[index] = value;
                this.UpdateStringRepresentation();
            }
        }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(IGeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

            this.genes = new BitArray(this.GetInitialLength());

            for (int i = 0; i < this.Length; i++)
            {
                this[i] = RandomNumberService.Instance.GetRandomValue(2) == 1 ? true : false;
            }

            this.UpdateStringRepresentation();

        }

        /// <summary>
        /// Copies the state from this object to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            this.EnsureEntityIsInitialized();
            base.CopyTo(entity);

            BinaryStringEntity binStrEntity = (BinaryStringEntity)entity;

            binStrEntity.genes = (BitArray)this.genes.Clone();
            binStrEntity.UpdateStringRepresentation();
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

        private void EnsureEntityIsInitialized()
        {
            if (this.genes == null)
            {
                throw new InvalidOperationException(Resources.ErrorMsg_EntityNotInitialized);
            }
        }
    }
}
