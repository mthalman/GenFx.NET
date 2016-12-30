using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="IntegerListEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class IntegerListEntityConfiguration<TConfiguration, TEntity> : ListEntityConfiguration<TConfiguration, TEntity, int>, IIntegerListEntityConfiguration
        where TConfiguration : IntegerListEntityConfiguration<TConfiguration, TEntity>
        where TEntity : IntegerListEntity<TEntity, TConfiguration>
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
