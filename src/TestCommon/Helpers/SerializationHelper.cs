using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace TestCommon.Helpers
{
    public static class SerializationHelper
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
