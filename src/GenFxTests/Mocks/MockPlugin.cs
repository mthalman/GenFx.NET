using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests.Mocks
{
    public class MockPlugin : PluginBase<MockPlugin, MockPluginFactoryConfig>
    {
        public MockPlugin(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    public class MockPluginFactoryConfig : PluginFactoryConfigBase<MockPluginFactoryConfig, MockPlugin>
    {
    }

    public class MockPlugin2 : PluginBase<MockPlugin2, MockPlugin2FactoryConfig>
    {
        public MockPlugin2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    public class MockPlugin2FactoryConfig : PluginFactoryConfigBase<MockPlugin2FactoryConfig, MockPlugin2>
    {
    }
}
