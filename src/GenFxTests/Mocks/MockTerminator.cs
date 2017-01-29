using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace GenFxTests.Mocks
{
    [DataContract]
    class MockTerminator : Terminator
    {
        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    [DataContract]
    abstract class MockTerminator2Base : Terminator
    {
    }

    [DataContract]
    class MockTerminator2 : MockTerminator2Base
    {
        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    [DataContract]
    class MockTerminator3 : MockTerminator2Base
    {
        public override bool IsComplete()
        {
            throw new NotImplementedException();
        }
    }
}
