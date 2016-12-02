using GenFx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests.Mocks
{
    public class MockPlugin : Plugin
    {
        public MockPlugin(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    [Component(typeof(MockPlugin))]
    public class MockPluginConfiguration : PluginConfiguration
    {
    }

    public class MockPlugin2 : Plugin
    {
        public MockPlugin2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    [Component(typeof(MockPlugin2))]
    public class MockPlugin2Configuration : PluginConfiguration
    {
    }
}
