using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    public abstract class GeneticComponent
    {
        internal const string ConfigurationProperty = "Configuration";

        /// <summary>
        /// When overriden by a derived class, gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        [Browsable(false)]
        public abstract ComponentConfiguration Configuration
        {
            get;
        }

        /// <summary>
        /// Returns an objects that contains the serializable state of this component.
        /// </summary>
        internal KeyValueMap SaveStateCore()
        {
            KeyValueMap state = new KeyValueMap();
            SetSaveState(state);
            return state;
        }

        /// <summary>
        /// Restores the state of the component.
        /// </summary>
        protected internal virtual void RestoreState(KeyValueMap state)
        {
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        protected virtual void SetSaveState(KeyValueMap state)
        {
        }
    }
}
