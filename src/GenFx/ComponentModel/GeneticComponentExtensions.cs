namespace GenFx.ComponentModel
{
    internal static class GeneticComponentExtensions
    {
        /// <summary>
        /// Returns an objects that contains the serializable state of this component.
        /// </summary>
        public static KeyValueMap SaveState(this IGeneticComponent component)
        {
            KeyValueMap state = new KeyValueMap();
            component.SetSaveState(state);
            return state;
        }
    }
}
