namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    public interface IGeneticComponent
    {
        /// <summary>
        /// Gets the <see cref="IComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        IComponentConfiguration Configuration { get; }

        /// <summary>
        /// Restores the state of the component.
        /// </summary>
        /// <param name="state">The state to restore from.</param>
        void RestoreState(KeyValueMap state);

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        void SetSaveState(KeyValueMap state);
    }
}
