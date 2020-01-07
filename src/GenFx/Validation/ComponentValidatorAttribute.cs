using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how a <see cref="GeneticComponent"/> should be validated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public abstract class ComponentValidatorAttribute : Attribute
    {
        private ComponentValidator? validator;

        /// <summary>
        /// Gets the validator used to verify the value of the property.
        /// </summary>
        public ComponentValidator Validator
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
        /// Initializes a new instance of the <see cref="ComponentValidatorAttribute"/> class.
        /// </summary>
        protected ComponentValidatorAttribute()
        {
        }

        /// <summary>
        /// When overriden by a derived class, returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected abstract ComponentValidator CreateValidator();
    }
}
