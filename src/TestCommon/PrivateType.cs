using System;

namespace TestCommon
{
    public class PrivateType
    {
        public PrivateType(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; }
    }
}
