using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how a property should be validated when set.
    /// </summary>
    public abstract class PropertyValidatorAttribute : Attribute
    {
        private PropertyValidator validator;

        /// <summary>
        /// Gets the validator used to verify the value of the property.
        /// </summary>
        public PropertyValidator Validator
        {
            get
            {
                if (this.validator == null)
                {
                    this.validator = this.CreateValidator();
                }
                return this.validator;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValidatorAttribute"/> class.
        /// </summary>
        protected PropertyValidatorAttribute()
        {
        }

        /// <summary>
        /// When overriden by a derived class, returns the associated <see cref="PropertyValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="PropertyValidator"/> object.</returns>
        protected abstract PropertyValidator CreateValidator();
    }
}
