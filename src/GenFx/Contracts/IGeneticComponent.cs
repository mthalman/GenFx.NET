namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    public interface IGeneticComponent
    {
        /// <summary>
        /// Creates a new instance of the same component type as this object.
        /// </summary>
        /// <remarks>
        /// This method acts as a factory to create new versions of the component.  For example,
        /// for a genetic entity type named MyEntity, it would override this method like this:
        /// <code>
        /// <![CDATA[
        /// public override IGeneticComponent CreateNew()
        /// {
        ///     return new MyEntity();
        /// }
        /// ]]>
        /// </code>
        /// </remarks>
        /// <returns>A new instance of the same component type as this object.</returns>
        IGeneticComponent CreateNew();

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

        /// <summary>
        /// Validates the state of the component.
        /// </summary>
        void Validate();
    }
}
