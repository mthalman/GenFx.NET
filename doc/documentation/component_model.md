# GenFx Component Model
GenFx makes use of a component model for the major concepts that make up a genetic algorithm (GA).  This component model provides a consistent API and the ability to customize and extend all components.

## Self-Replication
One of the important features of components is the ability to self-replicate.  Each component has the ability to create a new version of itself, an essential aspect needed when executing a GA.  There is a default implementation that all components have for self-replication but it can be customized if necessary.

## Configuration
The component model has the concept of component configuration properties.  The properties are intended to provide the initial configuration state to the component.  Configuration properties are treated differently from normal public properties because their state is automatically transferred as part of the self-replication logic.  This ensures that all new instances of components have access to the same configuration state that was initially defined for the GA.  

## Validation
All components have built-in validation support.  Any public property that is exposed by the component will be validated, according to how it is configured, when that component is used by the system.  See [Validation](validation.md) to learn more.