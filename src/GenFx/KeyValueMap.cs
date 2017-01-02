using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Collection of key/value pairs.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [Serializable]
    public class KeyValueMap : Dictionary<string, object>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="KeyValueMap"/>.
        /// </summary>
        public KeyValueMap()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="KeyValueMap"/>.
        /// </summary>
        protected KeyValueMap(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// List of <see cref="KeyValueMap"/> objects.
    /// </summary>
    public class KeyValueMapCollection : List<KeyValueMap>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="KeyValueMapCollection"/>.
        /// </summary>
        public KeyValueMapCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="KeyValueMapCollection"/>.
        /// </summary>
        /// <param name="list">Collection of objects to populate in the list.</param>
        public KeyValueMapCollection(IEnumerable<KeyValueMap> list)
        {
            this.AddRange(list);
        }
    }
}
