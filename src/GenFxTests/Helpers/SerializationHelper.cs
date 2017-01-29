using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests.Helpers
{
    internal static class SerializationHelper
    {
        public static object TestSerialization(object obj, IEnumerable<Type> knownTypes)
        {
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType(),
                new DataContractSerializerSettings
                {
                    PreserveObjectReferences = true,
                    KnownTypes = knownTypes
                });

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                stream.Flush();

                stream.Position = 0;

                return serializer.ReadObject(stream);
            }
        }
    }
}
