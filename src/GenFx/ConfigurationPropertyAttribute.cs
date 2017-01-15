using System;

namespace GenFx
{
    /// <summary>
    /// Indicates that a property represents the configuration of a component.
    /// </summary>
    /// <remarks>
    /// Configuration properties represent the initial state of a component.  They are used
    /// to configure a component prior to execution.  Properties adorned with this attribute
    /// are special in that there state is passed on to new instances of the component.  For example,
    /// each time a genetic entity is instantiated, the state of the configuration properties will be
    /// passed down to the new object.  This is how the configuration state of components are disseminated.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ConfigurationPropertyAttribute : Attribute
    {
    }
}
