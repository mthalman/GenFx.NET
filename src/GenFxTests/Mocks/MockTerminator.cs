using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;

namespace GenFxTests.Mocks
{
    class MockTerminator : TerminatorBase<MockTerminator, MockTerminatorFactoryConfig>
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

    class MockTerminatorFactoryConfig : TerminatorFactoryConfigBase<MockTerminatorFactoryConfig, MockTerminator>
    {
    }

    interface IMockTerminator2 : ITerminator
    {

    }

    abstract class MockTerminator2<TTerminator, TConfiguration> : TerminatorBase<TTerminator, TConfiguration>, IMockTerminator2
        where TTerminator : MockTerminator2<TTerminator, TConfiguration>
        where TConfiguration : MockTerminator2FactoryConfig<TConfiguration, TTerminator>
    {
        public MockTerminator2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    class MockTerminator2 : MockTerminator2<MockTerminator2, MockTerminator2FactoryConfig>
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

    class MockTerminator3 : MockTerminator2<MockTerminator3, MockTerminator3FactoryConfig>
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

    abstract class MockTerminator2FactoryConfig<TConfiguration, TTerminator> : TerminatorFactoryConfigBase<TConfiguration, TTerminator>
        where TConfiguration : MockTerminator2FactoryConfig<TConfiguration, TTerminator> 
        where TTerminator : MockTerminator2<TTerminator, TConfiguration>
    {
    }

    class MockTerminator2FactoryConfig : MockTerminator2FactoryConfig<MockTerminator2FactoryConfig, MockTerminator2>
    {
    }

    class MockTerminator3FactoryConfig : MockTerminator2FactoryConfig<MockTerminator3FactoryConfig, MockTerminator3>
    {
    }
}
