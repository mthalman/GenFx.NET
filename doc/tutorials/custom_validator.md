# Custom Validator
GenFx provides a validation model that lets you provide validation for properties and components as a whole.  This ensures a component is using values that it expects which is useful when you are building shareable components for others to use or if you just want to make sure you don't mistakenly configure your own components.

## Property Validator
In the first part of this tutorial, we're going to author a validator for integer properties that ensures the property can only be set to an even number.

Since this validator is meant for properties, we'll derive our class from [PropertyValidator](xref:GenFx.Validation.PropertyValidator) and implement its [IsValid](xref:GenFx.Validation.PropertyValidator.IsValid(System.Object,System.String,System.Object,System.String@)) method:

    public class EvenNumberValidator : PropertyValidator
    {
        public override bool IsValid(object value, string propertyName,
            object owner, out string errorMessage)
        {
            if (!(value is int))
            {
                errorMessage = propertyName + " was not set to an Int32 value.";
                return false;
            }
            else if ((int)value % 2)
            {
                errorMessage = propertyName + " must be set to an even number.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }

Now that we have a validator, we can use it one of two ways:
* Define a custom attribute class that can be used for the properties to which we want to apply this validation.
* Make use of the built-in [CustomPropertyValidatorAttribute](xref:GenFx.Validation.CustomPropertyValidatorAttribute) that can target any kind of property validator.

Let's explore both of these options to see how they work.  First, let's create a custom attribute class.  GenFx provides a convenient base class for property validator attributes to make this a pretty simple implementation:

    [AttributeUsage(AttributeTargets.Property)]
    public class EvenNumberValidatorAttribute : PropertyValidatorAttribute
    {
        protected override PropertyValidator CreateValidator()
        {
            return new EvenNumberValidator();
        }
    }

This is how that attribute would be used on a component property:
   
    private int myProperty;

    [EvenNumberValidator]
    public int MyProperty
    {
        get { return this.myProperty; }
        set { this.SetProperty(ref this.myProperty, value); }
    }

Invocation of the `EvenNumberValidator` would automatically occur when [SetProperty](xref:GenFx.GeneticComponent.SetProperty``1(``0@,``0,System.String)) is called to ensure the value is valid.

The other approach to take is to use [CustomPropertyValidatorAttribute](xref:GenFx.Validation.CustomPropertyValidatorAttribute) instead of defining our own custom attribute.  This approach doesn't yield as elegant of an API, especially if your validator requires arguments, but it's a quick way to get things up and running.  Here is how that would look:

    private int myProperty;

    [CustomPropertyValidator(typeof(EvenNumberValidator)]
    public int MyProperty
    {
        get { return this.myProperty; }
        set { this.SetProperty(ref this.myProperty, value); }
    }

## Component Validator
Let's move on to the next type of validator that you can customize: component validator.  With a component validator, you validate the component as a whole; it's not tied to a specific property as is the case with a property validator.

In this part of the tutorial, we'll define a component validator which validates that one integer property is greater than another integer property.  A component validator is appropriate for this scenario instead of a property validator because a property validator would validate the property immediately when it is set.  Depending on the order in which the properties are set, you could have a validation error before the consuming code was able to set the next property to the appropriate value.

Since this validator is meant for the whole component, we'll derive our class from [ComponentValidator](xref:GenFx.Validation.ComponentValidator) and implement its [IsValid](xref:GenFx.Validation.ComponentValidator.IsValid(GenFx.GeneticComponent,System.String@)) method.  For simplicity of the tutorial, we'll implement it such that the validator knows the type of the component from which it will be used.  Here's the code:

    public class GreaterThanValidator : ComponentValidator
    {
        public override bool IsValid(GeneticComponent component, out string errorMessage)
        {
            MyGeneticEntity entity = (MyGeneticEntity)component;

            if (entity.Value1 <= entity.Value2)
            {
                errorMessage = "Value1 must be greater than Value2.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }

Now that we have a validator, we can use it one of two ways:
* Define a custom attribute class that can be used for the component to which we want to apply this validation.
* Make use of the built-in [CustomComponentValidatorAttribute](xref:GenFx.Validation.CustomComponentValidatorAttribute) that can target any kind of component validator.

Let's explore both of these options to see how they work.  First, let's create a custom attribute class.  GenFx provides a convenient base class for component validator attributes to make this a pretty simple implementation:

    [AttributeUsage(AttributeTargets.Class)]
    public class GreaterThanValidatorAttribute : ComponentValidatorAttribute
    {
        protected override ComponentValidator CreateValidator()
        {
            return new GreaterThanValidator();
        }
    }

This is how that attribute would be used on a component:
   
    [GreaterThanValidator]
    public class MyGeneticEntity : GeneticEntity
    {
        private string value1;
        private string value2;

        public int Value1
        {
            get { return this.value1; }
            set { this.SetProperty(ref this.value1, value); }
        }

        public int Value2
        {
            get { return this.value1; }
            set { this.SetProperty(ref this.value1, value); }
        }
    }

Invocation of the `GreaterThanValidator` would automatically occur when the GA is initialized.

The other approach to take is to use [CustomComponentValidatorAttribute](xref:GenFx.Validation.CustomComponentValidatorAttribute) instead of defining our own custom attribute.  This approach doesn't yield as elegant of an API, especially if your validator requires arguments, but it's a quick way to get things up and running.  Here is how that would look:

    [CustomComponentValidator(typeof(GreaterThanValidator))]
    public class MyGeneticEntity : GeneticEntity
    {
        /* Same code as before... */
    }

To learn more about the validation API, check out the the [Validation namespace](xref:GenFx.Validation).