# Validation
GenFx provides a rich validation system for ensuring the configuration of your genetic algorithm is in the expected state.

## Component Property Validation
All components have the ability to have their public properties validated by GenFx.  Each property can use the GenFx API to validate its own state and each component also has validation logic to validate all of its properties when it is used by a genetic algorithm.

### Validators
GenFx validates properties through the use of validator attributes.  These are attributes that can be placed on a property which indicate how that property should be validated.  For example, the [RequiredValidatorAttribute](../api/GenFx.Validation.RequiredValidatorAttribute.md) ensures that the property is not set to a null value.  GenFx even allows you to author your own validator (and attribute, if you wish) to provide your own custom validation logic (see the [Custom Validator tutorial](../tutorials/custom_validator.md)).  To see which validation attributes are provided by GenFx, check them out at the [Validation namespace](../api/GenFx.Validation.md).

### External Component Validation
GenFx provides a way to validate the configuration of other components when a particular component is being used. This allows you to ensure a component that you depend upon is configured in the way that is required by your commponent.  For example, if you've implemented a crossover operator that requires a binary string entity of a certain length, you can attach an external validator attribute to your crossover operator class that defines this validation requirement.  Every validator attribute that is defined by GenFx also includes a version of that attribute which provides external component validation.  See the [Validation namespace](../api/GenFx.Validation.md).

## Required Component Types
Often times it is necessary to ensure that a specific component is used in conjunction with another component.  For example, if you've defined your own crossover operator for binary strings, you want to ensure that only binary string entities are used in the genetic algorithm.  GenFx provides validation attributes that allow you to define this type of requirement. See the [Validation namespace](../api/GenFx.Validation.md).