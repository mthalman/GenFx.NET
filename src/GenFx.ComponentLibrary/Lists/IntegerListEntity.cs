using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// <see cref="GeneticEntity"/> made up of a list of integers.
    /// </summary>
    public abstract class IntegerListEntity : ListEntity<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerListEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="IntegerListEntity"/>.</param>
        /// <param name="initialLength">Initial length of the list.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialLength"/> is less than zero.</exception>
        protected IntegerListEntity(GeneticAlgorithm algorithm, int initialLength)
            : base(algorithm, initialLength)
        {
        }

        /// <summary>
        /// Initializes the list with random values.
        /// </summary>
        /// <remarks>
        /// <b>Notes to inheritors:</b> When overriding this method, you must either call the <b>InitializeCore</b>
        /// method of the base class or call <see cref="ListEntityBase.UpdateStringRepresentation"/>
        /// after the list has been initialized.  This is necessary in order to sync the 
        /// string representation of <see cref="IntegerListEntity"/> with the initialized data.
        /// </remarks>
        protected override void InitializeCore()
        {
            IntegerListEntityConfiguration config = (IntegerListEntityConfiguration)this.Algorithm.ConfigurationSet.Entity;

            if (config.UseUniqueElementValues)
            {
                List<int> availableInts = new List<int>();
                for (int i = config.MinElementValue; i <= config.MaxElementValue; i++)
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
                    this[i] = RandomHelper.Instance.GetRandomValue(config.MinElementValue, config.MaxElementValue + 1);
                }
            }

            this.UpdateStringRepresentation();
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="IntegerListEntity"/>.
    /// </summary>
    [Component(typeof(IntegerListEntity))]
    public abstract class IntegerListEntityConfiguration : GeneticEntityConfiguration
    {
        private const int DefaultMinElementValue = 0;
        private const int DefaultMaxElementValue = Int32.MaxValue;

        private int minElementValue = DefaultMinElementValue;
        private int maxElementValue = DefaultMaxElementValue;
        private bool useUniqueElementValues;

        /// <summary>
        /// Gets or sets the minimum value an integer in the list can have.
        /// </summary>
        public int MinElementValue
        {
            get { return this.minElementValue; }
            set { this.SetProperty(ref this.minElementValue, value); }
        }

        /// <summary>
        /// Gets or sets the maximum value an integer in the list can have.
        /// </summary>
        public int MaxElementValue
        {
            get { return this.maxElementValue; }
            set { this.SetProperty(ref this.maxElementValue, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether each of the element values should be unique for the entity.
        /// </summary>
        public bool UseUniqueElementValues
        {
            get { return this.useUniqueElementValues; }
            set { this.SetProperty(ref this.useUniqueElementValues, value); }
        }
    }
}
