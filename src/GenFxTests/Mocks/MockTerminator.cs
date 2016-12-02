using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockTerminator : Terminator
    {
        public MockTerminator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    [Component(typeof(MockTerminator))]
    class MockTerminatorConfiguration : TerminatorConfiguration
    {
    }

    class MockTerminator2 : Terminator
    {
        public MockTerminator2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class MockTerminator3 : MockTerminator2
    {
        public MockTerminator3(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    [Component(typeof(MockTerminator2))]
    class MockTerminator2Configuration : TerminatorConfiguration
    {
    }

    [Component(typeof(MockTerminator3))]
    class MockTerminator3Configuration : MockTerminator2Configuration
    {
    }
}
