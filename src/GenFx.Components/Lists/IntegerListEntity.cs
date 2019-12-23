using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx.Components.Lists
{
    /// <summary>
    /// Entity made up of a list of integers.
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class IntegerListEntity : ListEntity<int>
    {
        private const int DefaultMinElementValue = 0;
        private const int DefaultMaxElementValue = Int32.MaxValue;

        [DataMember]
        private int minElementValue = DefaultMinElementValue;

        [DataMember]
        private int maxElementValue = DefaultMaxElementValue;

        [DataMember]
        private bool useUniqueElementValues;
        
        /// <summary>
        /// Gets or sets the minimum value an integer in the list can have.
        /// </summary>
        [ConfigurationProperty]
        public int MinElementValue
        {
            get { return this.minElementValue; }
            set { this.SetProperty(ref this.minElementValue, value); }
        }

        /// <summary>
        /// Gets or sets the maximum value an integer in the list can have.
        /// </summary>
        [ConfigurationProperty]
        public int MaxElementValue
        {
            get { return this.maxElementValue; }
            set { this.SetProperty(ref this.maxElementValue, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether each of the element values should be unique for the entity.
        /// </summary>
        [ConfigurationProperty]
        public override bool RequiresUniqueElementValues
        {
            get { return this.useUniqueElementValues; }
            set { this.SetProperty(ref this.useUniqueElementValues, value); }
        }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(GeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

            if (this.RequiresUniqueElementValues)
            {
                List<int> availableInts = new List<int>();
                for (int i = this.MinElementValue; i <= this.MaxElementValue; i++)
                {
                    availableInts.Add(i);
                }

                // randomize the ints
                int n = availableInts.Count;
                while (n > 1)
                {
                    n--;
                    int k = RandomNumberService.Instance.GetRandomValue(n);
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
                    // Normally, we would get a random value like this GetRandomValue(MinElementValue, MaxElementValue + 1),
                    // but if MaxElementValue is equal to Int32.Max, then adding 1 to it would cause an overlflow, so
                    // we shift the range down by 1 to get the random value and then add 1 to it.
                    this[i] = RandomNumberService.Instance.GetRandomValue(this.MinElementValue - 1, this.MaxElementValue) + 1;
                }
            }

            this.UpdateStringRepresentation();
        }
    }
}
