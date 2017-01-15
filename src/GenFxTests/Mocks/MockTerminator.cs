using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockTerminator : TerminatorBase
    {
        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    
    interface IMockTerminator2 : ITerminator
    {

    }

    abstract class MockTerminator2Base : TerminatorBase, IMockTerminator2
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
