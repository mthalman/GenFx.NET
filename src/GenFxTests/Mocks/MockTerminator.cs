using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockTerminator : Terminator
    {
        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    
    abstract class MockTerminator2Base : Terminator
    {
    }

    class MockTerminator2 : MockTerminator2Base
    {
        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class MockTerminator3 : MockTerminator2Base
    {
        public override bool IsComplete()
        {
            throw new NotImplementedException();
        }
    }
}
