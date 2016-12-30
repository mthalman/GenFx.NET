using GenFx;
using GenFx.ComponentLibrary.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests.Mocks
{
    public class MockPlugin : PluginBase<MockPlugin, MockPluginConfiguration>
    {
        public MockPlugin(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    public class MockPluginConfiguration : PluginConfigurationBase<MockPluginConfiguration, MockPlugin>
    {
    }

    public class MockPlugin2 : PluginBase<MockPlugin2, MockPlugin2Configuration>
    {
        public MockPlugin2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    public class MockPlugin2Configuration : PluginConfigurationBase<MockPlugin2Configuration, MockPlugin2>
    {
    }
}
