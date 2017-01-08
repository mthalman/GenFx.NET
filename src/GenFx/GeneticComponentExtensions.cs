using GenFx.Contracts;
using System;

namespace GenFx
{
    /// <summary>
    /// Contains extension methods for <see cref="IGeneticComponent"/>.
    /// </summary>
    public static class GeneticComponentExtensions
    {
        /// <summary>
        /// Returns an objects that contains the serializable state of this component.
        /// </summary>
        public static KeyValueMap SaveState(this IGeneticComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }

            KeyValueMap state = new KeyValueMap();
            component.SetSaveState(state);
            return state;
        }
    }
}
