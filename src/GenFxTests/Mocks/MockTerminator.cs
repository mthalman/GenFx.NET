using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockTerminator : TerminatorBase<MockTerminator, MockTerminatorConfiguration>
    {
        public MockTerminator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class MockTerminatorConfiguration : TerminatorConfigurationBase<MockTerminatorConfiguration, MockTerminator>
    {
    }

    interface IMockTerminator2 : ITerminator
    {

    }

    abstract class MockTerminator2<TTerminator, TConfiguration> : TerminatorBase<TTerminator, TConfiguration>, IMockTerminator2
        where TTerminator : MockTerminator2<TTerminator, TConfiguration>
        where TConfiguration : MockTerminator2Configuration<TConfiguration, TTerminator>
    {
        public MockTerminator2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    class MockTerminator2 : MockTerminator2<MockTerminator2, MockTerminator2Configuration>
    {
        public MockTerminator2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override bool IsComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class MockTerminator3 : MockTerminator2<MockTerminator3, MockTerminator3Configuration>
    {
        public MockTerminator3(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override bool IsComplete()
        {
            throw new NotImplementedException();
        }
    }

    abstract class MockTerminator2Configuration<TConfiguration, TTerminator> : TerminatorConfigurationBase<TConfiguration, TTerminator>
        where TConfiguration : MockTerminator2Configuration<TConfiguration, TTerminator> 
        where TTerminator : MockTerminator2<TTerminator, TConfiguration>
    {
    }

    class MockTerminator2Configuration : MockTerminator2Configuration<MockTerminator2Configuration, MockTerminator2>
    {
    }

    class MockTerminator3Configuration : MockTerminator2Configuration<MockTerminator3Configuration, MockTerminator3>
    {
    }
}
