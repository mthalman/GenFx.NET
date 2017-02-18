using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockTerminator : Terminator
    {
        public override bool IsComplete()
        {
            return true;
        }
    }

    [DataContract]
    abstract public class MockTerminator2Base : Terminator
    {
    }

    [DataContract]
    public class MockTerminator2 : MockTerminator2Base
    {
        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    [DataContract]
    public class MockTerminator3 : MockTerminator2Base
    {
        public override bool IsComplete()
        {
            throw new NotImplementedException();
        }
    }
}
