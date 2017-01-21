# Customizing and Extending Components
GenFx features an extensiblity model that allows developers to customize and extend components to meet their needs.

## Configuration Properties
Most built-in components define [configuration properties](component_model.md#configuration).  The components are authored in a way that their behavior is generalized and customizable through the use of configuration properties. The configuration properties allow developers to configure the component according to their needs.  When authoring your own component to be used by other developers, it is suggested to think about how your component implementation can be generalized and configurable through the use of configuration properties.

## Overriding Behavior
If a built-in component doesn't provide the exact behavior you need but it's close to what you want, then consider deriving your own custom component from the existing one through class inheritance. All components are authored to allow overriding some parts of their implementation. You can then implement your own logic and reuse any logic from the base class.