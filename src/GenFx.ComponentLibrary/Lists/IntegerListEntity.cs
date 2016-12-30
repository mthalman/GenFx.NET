using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Entity made up of a list of integers.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public abstract class IntegerListEntity<TEntity, TConfiguration> : ListEntity<TEntity, TConfiguration, int>, IIntegerListEntity
        where TEntity : IntegerListEntity<TEntity, TConfiguration>
        where TConfiguration : IntegerListEntityConfiguration<TConfiguration, TEntity>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <param name="initialLength">Initial length of the list.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialLength"/> is less than zero.</exception>
        protected IntegerListEntity(IGeneticAlgorithm algorithm, int initialLength)
            : base(algorithm, initialLength)
        {
        }

        /// <summary>
        /// Initializes the list with random values.
        /// </summary>
        /// <remarks>
        /// <b>Notes to inheritors:</b> When overriding this method, you must either call the <b>InitializeCore</b>
        /// method of the base class or call <see cref="ListEntityBase{TEntity, TConfiguration, TItem}.UpdateStringRepresentation"/>
        /// after the list has been initialized.  This is necessary in order to sync the 
        /// string representation of this object with the initialized data.
        /// </remarks>
        protected override void InitializeCore()
        {
            if (this.Configuration.UseUniqueElementValues)
            {
                List<int> availableInts = new List<int>();
                for (int i = this.Configuration.MinElementValue; i <= this.Configuration.MaxElementValue; i++)
                {
                    availableInts.Add(i);
                }

                // randomize the ints
                int n = availableInts.Count;
                while (n > 1)
                {
                    n--;
                    int k = RandomHelper.Instance.GetRandomValue(n);
                    int value = availableInts[k];
                    availableInts[k] = availableInts[n];
                    availableInts[n] = value;
                }

                availableInts.RemoveRange(this.Length, availableInts.Count - this.Length);

                for (int i = 0; i < availableInts.Count; i++)
                {
                    this[i] = availableInts[i];
                }
            }
            else
            {
                for (int i = 0; i < this.Length; i++)
                {
                    this[i] = RandomHelper.Instance.GetRandomValue(this.Configuration.MinElementValue, this.Configuration.MaxElementValue + 1);
                }
            }

            this.UpdateStringRepresentation();
        }
    }
}
