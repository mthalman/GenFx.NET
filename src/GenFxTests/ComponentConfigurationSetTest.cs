using GenFx;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentConfigurationSet and is intended
    ///to contain all GenFx.ComponentConfigurationSet Unit Tests
    ///</summary>
    [TestClass()]
    public class ComponentConfigurationSetTest
    {

        /// <summary>
        /// Tests that ComponentConfigurationSet.CrossoverOperator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void CrossoverOperatorTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockCrossoverOperatorConfiguration val = new MockCrossoverOperatorConfiguration();
            target.CrossoverOperator = val;

            Assert.AreSame(val, target.CrossoverOperator, "CrossoverOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.CrossoverOperator can be set to a valid null value.
        ///</summary>
        [TestMethod()]
        public void CrossoverOperatorTest_ValidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();

            MockCrossoverOperatorConfiguration val = null;

            target.CrossoverOperator = val;

            Assert.AreSame(val, target.CrossoverOperator, "CrossoverOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.ElitismStrategy can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void ElitismStrategyTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockElitismStrategyConfiguration val = new MockElitismStrategyConfiguration();
            target.ElitismStrategy = val;

            Assert.AreSame(val, target.ElitismStrategy, "ElitismStrategy was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.ElitismStrategy can be set to a valid null value.
        ///</summary>
        [TestMethod()]
        public void ElitismStrategyTest_ValidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockElitismStrategyConfiguration val = null;
            target.ElitismStrategy = val;

            Assert.AreSame(val, target.ElitismStrategy, "ElitismStrategy was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.FitnessEvaluator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void FitnessEvaluatorTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockFitnessEvaluatorConfiguration val = new MockFitnessEvaluatorConfiguration();
            target.FitnessEvaluator = val;

            Assert.AreSame(val, target.FitnessEvaluator, "FitnessEvaluator was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when ComponentConfigurationSet.FitnessEvaluator is set to 
        /// a null value.
        ///</summary>
        [TestMethod()]
        public void FitnessEvaluatorTypeTest_InvalidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            AssertEx.Throws<ArgumentNullException>(() => target.FitnessEvaluator = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.FitnessScalingStrategy can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void FitnessScalingStrategyTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockFitnessScalingStrategyConfiguration val = new MockFitnessScalingStrategyConfiguration();
            target.FitnessScalingStrategy = val;
            Assert.AreSame(val, target.FitnessScalingStrategy, "FitnessScalingStrategy was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.FitnessScalingStrategy can be set to 
        /// a null value.
        ///</summary>
        [TestMethod()]
        public void FitnessScalingStrategyTest_ValidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockFitnessScalingStrategyConfiguration val = null;
            target.FitnessScalingStrategy = val;
            Assert.AreSame(val, target.FitnessScalingStrategy, "FitnessScalingStrategy was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Entity can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void EntityTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockEntityConfiguration val = new MockEntityConfiguration();
            target.Entity = val;
            Assert.AreSame(val, target.Entity, "Entity was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when ComponentConfigurationSet.Entity is set to 
        /// a null value.
        ///</summary>
        [TestMethod()]
        public void EntityTest_InvalidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            AssertEx.Throws<ArgumentNullException>(() => target.Entity = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.MutationOperator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void MutationOperatorTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockMutationOperatorConfiguration val = new MockMutationOperatorConfiguration();
            target.MutationOperator = val;
            Assert.AreSame(val, target.MutationOperator, "MutationOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.MutationOperator can be set to 
        /// a null value.
        ///</summary>
        [TestMethod()]
        public void MutationOperatorTypeTest_ValidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockMutationOperatorConfiguration val = null;
            target.MutationOperator = val;
            Assert.AreSame(val, target.MutationOperator, "MutationOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Population can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void PopulationTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockPopulationConfiguration val = new MockPopulationConfiguration();
            target.Population = val;
            Assert.AreSame(val, target.Population, "Population was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when ComponentConfigurationSet.Population is set to 
        /// a null value.
        ///</summary>
        [TestMethod()]
        public void PopulationTest_InvalidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            AssertEx.Throws<ArgumentNullException>(() => target.Population = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.SelectionOperator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void SelectionOperatorTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockSelectionOperatorConfiguration val = new MockSelectionOperatorConfiguration();
            target.SelectionOperator = val;
            Assert.AreSame(val, target.SelectionOperator, "SelectionOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when ComponentConfigurationSet.SelectionOperator is set to 
        /// a null value.
        ///</summary>
        [TestMethod()]
        public void SelectionOperatorTest_InvalidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            AssertEx.Throws<ArgumentNullException>(() => target.SelectionOperator = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Statistics is initialized correctly.
        ///</summary>
        [TestMethod()]
        public void StatisticsTest()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            Assert.AreEqual(0, target.Statistics.Count, "GenFx.ComponentConfigurationSet.Statistics was not initialized correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Terminator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void TerminatorTest_Valid()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            MockTerminatorConfiguration val = new MockTerminatorConfiguration();
            target.Terminator = val;
            Assert.AreSame(val, target.Terminator, "Terminator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Terminator cannot be set to a null value.
        ///</summary>
        [TestMethod()]
        public void TerminatorTest_ValidNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();
            target.Terminator = null;
            Assert.IsNotNull(target.Terminator, "Terminator was not set correctly.");
        }

        /// <summary>
        /// Tests that the state of the configuration set can be saved correctly.
        /// </summary>
        [TestMethod]
        public void SaveState()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();

            MockCrossoverOperatorConfiguration crossoverConfig = new MockCrossoverOperatorConfiguration
            {
                CrossoverRate = 0.2
            };
            target.CrossoverOperator = crossoverConfig;

            MockElitismStrategyConfiguration elitismConfig = new MockElitismStrategyConfiguration
            {
                ElitistRatio = 0.2
            };
            target.ElitismStrategy = elitismConfig;

            MockEntityConfiguration entityConfig = new MockEntityConfiguration();
            target.Entity = entityConfig;

            MockFitnessEvaluatorConfiguration fitnessEvalConfig = new MockFitnessEvaluatorConfiguration
            {
                EvaluationMode = FitnessEvaluationMode.Maximize
            };
            target.FitnessEvaluator = fitnessEvalConfig;

            MockFitnessScalingStrategyConfiguration fitnessScalingConfig = new MockFitnessScalingStrategyConfiguration();
            target.FitnessScalingStrategy = fitnessScalingConfig;

            MockGeneticAlgorithmConfiguration geneticAlgorithmConfig = new MockGeneticAlgorithmConfiguration
            {
                EnvironmentSize = 5,
            };
            target.GeneticAlgorithm = geneticAlgorithmConfig;

            MockMutationOperatorConfiguration mutationConfig = new MockMutationOperatorConfiguration
            {
                MutationRate = 0.7
            };
            target.MutationOperator = mutationConfig;

            MockPluginConfiguration plugin1Config = new MockPluginConfiguration();
            MockPlugin2Configuration plugin2Config = new MockPlugin2Configuration();
            target.Plugins.Add(plugin1Config);
            target.Plugins.Add(plugin2Config);

            MockPopulationConfiguration populationConfig = new MockPopulationConfiguration
            {
                PopulationSize = 10
            };
            target.Population = populationConfig;

            MockSelectionOperatorConfiguration selectionConfig = new MockSelectionOperatorConfiguration
            {
                SelectionBasedOnFitnessType = FitnessType.Raw
            };
            target.SelectionOperator = selectionConfig;

            MockStatisticConfiguration stat1Config = new MockStatisticConfiguration();
            MockStatistic2Configuration stat2Config = new MockStatistic2Configuration();
            target.Statistics.Add(stat1Config);
            target.Statistics.Add(stat2Config);

            target.Terminator = null;

            KeyValueMap state = target.SaveState();

            KeyValueMap crossoverState = (KeyValueMap)state[nameof(ComponentConfigurationSet.CrossoverOperator)];
            Assert.AreEqual(2, crossoverState.Count);
            Assert.AreEqual(crossoverConfig.GetType().AssemblyQualifiedName, crossoverState["$type"]);
            Assert.AreEqual(crossoverConfig.CrossoverRate, crossoverState[nameof(ICrossoverOperatorConfiguration.CrossoverRate)]);

            KeyValueMap elitismState = (KeyValueMap)state[nameof(ComponentConfigurationSet.ElitismStrategy)];
            Assert.AreEqual(2, elitismState.Count);
            Assert.AreEqual(elitismConfig.GetType().AssemblyQualifiedName, elitismState["$type"]);
            Assert.AreEqual(elitismConfig.ElitistRatio, elitismState[nameof(IElitismStrategyConfiguration.ElitistRatio)]);

            KeyValueMap entityState = (KeyValueMap)state[nameof(ComponentConfigurationSet.Entity)];
            Assert.AreEqual(1, entityState.Count);
            Assert.AreEqual(entityConfig.GetType().AssemblyQualifiedName, entityState["$type"]);

            KeyValueMap fitnessEvalState = (KeyValueMap)state[nameof(ComponentConfigurationSet.FitnessEvaluator)];
            Assert.AreEqual(2, fitnessEvalState.Count);
            Assert.AreEqual(fitnessEvalConfig.GetType().AssemblyQualifiedName, fitnessEvalState["$type"]);
            Assert.AreEqual(fitnessEvalConfig.EvaluationMode.ToString(), fitnessEvalState[nameof(IFitnessEvaluatorConfiguration.EvaluationMode)]);

            KeyValueMap fitnessScalingState = (KeyValueMap)state[nameof(ComponentConfigurationSet.FitnessScalingStrategy)];
            Assert.AreEqual(1, fitnessScalingState.Count);
            Assert.AreEqual(fitnessScalingConfig.GetType().AssemblyQualifiedName, fitnessScalingState["$type"]);

            KeyValueMap geneticAlgorithmState = (KeyValueMap)state[nameof(ComponentConfigurationSet.GeneticAlgorithm)];
            Assert.AreEqual(2, geneticAlgorithmState.Count);
            Assert.AreEqual(geneticAlgorithmConfig.GetType().AssemblyQualifiedName, geneticAlgorithmState["$type"]);
            Assert.AreEqual(geneticAlgorithmConfig.EnvironmentSize, geneticAlgorithmState[nameof(IGeneticAlgorithmConfiguration.EnvironmentSize)]);

            KeyValueMap mutationState = (KeyValueMap)state[nameof(ComponentConfigurationSet.MutationOperator)];
            Assert.AreEqual(2, mutationState.Count);
            Assert.AreEqual(mutationConfig.GetType().AssemblyQualifiedName, mutationState["$type"]);
            Assert.AreEqual(mutationConfig.MutationRate, mutationState[nameof(IMutationOperatorConfiguration.MutationRate)]);

            KeyValueMapCollection pluginsState = (KeyValueMapCollection)state[nameof(ComponentConfigurationSet.Plugins)];
            Assert.AreEqual(2, pluginsState.Count);
            KeyValueMap plugin1State = pluginsState[0];
            Assert.AreEqual(plugin1Config.GetType().AssemblyQualifiedName, plugin1State["$type"]);
            KeyValueMap plugin2State = pluginsState[1];
            Assert.AreEqual(plugin2Config.GetType().AssemblyQualifiedName, plugin2State["$type"]);

            KeyValueMap populationState = (KeyValueMap)state[nameof(ComponentConfigurationSet.Population)];
            Assert.AreEqual(2, populationState.Count);
            Assert.AreEqual(populationConfig.GetType().AssemblyQualifiedName, populationState["$type"]);
            Assert.AreEqual(populationConfig.PopulationSize, populationState[nameof(IPopulationConfiguration.PopulationSize)]);

            KeyValueMap selectionState = (KeyValueMap)state[nameof(ComponentConfigurationSet.SelectionOperator)];
            Assert.AreEqual(2, selectionState.Count);
            Assert.AreEqual(selectionConfig.GetType().AssemblyQualifiedName, selectionState["$type"]);
            Assert.AreEqual(selectionConfig.SelectionBasedOnFitnessType.ToString(), selectionState[nameof(ISelectionOperatorConfiguration.SelectionBasedOnFitnessType)]);

            KeyValueMapCollection statsState = (KeyValueMapCollection)state[nameof(ComponentConfigurationSet.Statistics)];
            Assert.AreEqual(2, statsState.Count);
            KeyValueMap stat1State = statsState[0];
            Assert.AreEqual(stat1Config.GetType().AssemblyQualifiedName, stat1State["$type"]);
            KeyValueMap stat2State = statsState[1];
            Assert.AreEqual(stat2Config.GetType().AssemblyQualifiedName, stat2State["$type"]);

            Assert.IsNotNull(state[nameof(ComponentConfigurationSet.Terminator)]); // always defaults to non-null
        }

        /// <summary>
        /// Tests that a configuration set can be restored from state that contains all null values.
        /// </summary>
        [TestMethod]
        public void RestoreState_AllNull()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();

            KeyValueMap state = new KeyValueMap
            {
                { nameof(ComponentConfigurationSet.CrossoverOperator), null },
                { nameof(ComponentConfigurationSet.ElitismStrategy), null },
                { nameof(ComponentConfigurationSet.Entity), null },
                { nameof(ComponentConfigurationSet.FitnessEvaluator), null },
                { nameof(ComponentConfigurationSet.FitnessScalingStrategy), null },
                { nameof(ComponentConfigurationSet.GeneticAlgorithm), null },
                { nameof(ComponentConfigurationSet.MutationOperator), null },
                { nameof(ComponentConfigurationSet.Plugins), new KeyValueMapCollection() },
                { nameof(ComponentConfigurationSet.Population), null },
                { nameof(ComponentConfigurationSet.SelectionOperator), null },
                { nameof(ComponentConfigurationSet.Statistics), new KeyValueMapCollection() },
                { nameof(ComponentConfigurationSet.Terminator), null },
                { "isFrozen", true },
            };

            target.RestoreState(state);
            Assert.IsNull(target.CrossoverOperator);
            Assert.IsNull(target.ElitismStrategy);
            Assert.IsNull(target.Entity);
            Assert.IsNull(target.FitnessEvaluator);
            Assert.IsNull(target.FitnessScalingStrategy);
            Assert.IsNull(target.GeneticAlgorithm);
            Assert.IsNull(target.MutationOperator);
            Assert.AreEqual(0, target.Plugins.Count);
            Assert.IsNull(target.Population);
            Assert.IsNull(target.SelectionOperator);
            Assert.AreEqual(0, target.Statistics.Count);
            Assert.IsNotNull(target.Terminator); // always defaults to non-null

            PrivateObject configAccessor = new PrivateObject(target);
            Assert.IsTrue((bool)configAccessor.GetField("isFrozen"));
        }

        /// <summary>
        /// Tests that a configuration set can be restored from state that contains all values set.
        /// </summary>
        [TestMethod]
        public void RestoreState_AllSet()
        {
            ComponentConfigurationSet target = new ComponentConfigurationSet();

            KeyValueMap state = new KeyValueMap
            {
                { nameof(ComponentConfigurationSet.CrossoverOperator), new KeyValueMap
                    {
                        { "$type", typeof(MockCrossoverOperatorConfiguration).AssemblyQualifiedName },
                        { "CrossoverRate", 0.2 }
                    }
                },
                { nameof(ComponentConfigurationSet.ElitismStrategy), new KeyValueMap
                    {
                        { "$type", typeof(MockElitismStrategyConfiguration).AssemblyQualifiedName },
                        { "ElitistRatio", 0.5 }
                    }
                },
                { nameof(ComponentConfigurationSet.Entity), new KeyValueMap
                    {
                        { "$type", typeof(MockEntityConfiguration).AssemblyQualifiedName },
                    }
                },
                { nameof(ComponentConfigurationSet.FitnessEvaluator), new KeyValueMap
                    {
                        { "$type", typeof(MockFitnessEvaluatorConfiguration).AssemblyQualifiedName },
                        { "EvaluationMode", "Maximize" }
                    }
                },
                { nameof(ComponentConfigurationSet.FitnessScalingStrategy), new KeyValueMap
                    {
                        { "$type", typeof(MockFitnessScalingStrategyConfiguration).AssemblyQualifiedName },
                    }
                },
                { nameof(ComponentConfigurationSet.GeneticAlgorithm), new KeyValueMap
                    {
                        { "$type", typeof(MockGeneticAlgorithmConfiguration).AssemblyQualifiedName },
                        { "EnvironmentSize", 10 },
                        { "StatisticsEnabled", false }
                    }
                },
                { nameof(ComponentConfigurationSet.MutationOperator), new KeyValueMap
                    {
                        { "$type", typeof(MockMutationOperatorConfiguration).AssemblyQualifiedName },
                        { "MutationRate", 0.7 },
                    }
                },
                { nameof(ComponentConfigurationSet.Plugins), new KeyValueMapCollection
                    {
                        new KeyValueMap
                        {
                            { "$type", typeof(MockPluginConfiguration).AssemblyQualifiedName },
                        },
                        new KeyValueMap
                        {
                            { "$type", typeof(MockPlugin2Configuration).AssemblyQualifiedName },
                        }
                    }
                },
                { nameof(ComponentConfigurationSet.Population), new KeyValueMap
                    {
                        { "$type", typeof(MockPopulationConfiguration).AssemblyQualifiedName },
                        { "PopulationSize", 15 },
                    }
                },
                { nameof(ComponentConfigurationSet.SelectionOperator), new KeyValueMap
                    {
                        { "$type", typeof(MockSelectionOperatorConfiguration).AssemblyQualifiedName },
                        { "SelectionBasedOnFitnessType", "Raw" },
                    }
                },
                { nameof(ComponentConfigurationSet.Statistics), new KeyValueMapCollection
                    {
                        new KeyValueMap
                        {
                            { "$type", typeof(MockStatisticConfiguration).AssemblyQualifiedName },
                        },
                        new KeyValueMap
                        {
                            { "$type", typeof(MockStatistic2Configuration).AssemblyQualifiedName },
                        }
                    }
                },
                { nameof(ComponentConfigurationSet.Terminator), new KeyValueMap
                    {
                        { "$type", typeof(MockTerminatorConfiguration).AssemblyQualifiedName },
                    }
                },
                { "isFrozen", true }
            };

            target.RestoreState(state);

            MockCrossoverOperatorConfiguration crossoverConfig = (MockCrossoverOperatorConfiguration)target.CrossoverOperator;
            Assert.AreEqual(0.2, crossoverConfig.CrossoverRate);

            MockElitismStrategyConfiguration elitismConfig = (MockElitismStrategyConfiguration)target.ElitismStrategy;
            Assert.AreEqual(0.5, elitismConfig.ElitistRatio);

            MockEntityConfiguration entityConfig = (MockEntityConfiguration)target.Entity;
            Assert.IsNotNull(entityConfig);

            MockFitnessEvaluatorConfiguration fitnessEvalConfig = (MockFitnessEvaluatorConfiguration)target.FitnessEvaluator;
            Assert.AreEqual(FitnessEvaluationMode.Maximize, fitnessEvalConfig.EvaluationMode);

            MockFitnessScalingStrategyConfiguration fitnessScalingConfig = (MockFitnessScalingStrategyConfiguration)target.FitnessScalingStrategy;
            Assert.IsNotNull(fitnessScalingConfig);

            MockGeneticAlgorithmConfiguration mockGeneticAlgorithmConfig = (MockGeneticAlgorithmConfiguration)target.GeneticAlgorithm;
            Assert.AreEqual(10, mockGeneticAlgorithmConfig.EnvironmentSize);

            MockMutationOperatorConfiguration mutationConfig = (MockMutationOperatorConfiguration)target.MutationOperator;
            Assert.AreEqual(0.7, mutationConfig.MutationRate);

            Assert.AreEqual(2, target.Plugins.Count);
            MockPluginConfiguration plugin1Config = (MockPluginConfiguration)target.Plugins.OfType<MockPluginConfiguration>().FirstOrDefault();
            Assert.IsNotNull(plugin1Config);
            MockPlugin2Configuration plugin2Config = (MockPlugin2Configuration)target.Plugins.OfType<MockPlugin2Configuration>().FirstOrDefault();
            Assert.IsNotNull(plugin2Config);

            MockPopulationConfiguration populationConfig = (MockPopulationConfiguration)target.Population;
            Assert.AreEqual(15, populationConfig.PopulationSize);

            MockSelectionOperatorConfiguration selectionConfig = (MockSelectionOperatorConfiguration)target.SelectionOperator;
            Assert.AreEqual(FitnessType.Raw, selectionConfig.SelectionBasedOnFitnessType);

            Assert.AreEqual(2, target.Statistics.Count);
            MockStatisticConfiguration statistic1Config = (MockStatisticConfiguration)target.Statistics.OfType<MockStatisticConfiguration>().FirstOrDefault();
            Assert.IsNotNull(statistic1Config);
            MockStatistic2Configuration statistic2Config = (MockStatistic2Configuration)target.Statistics.OfType<MockStatistic2Configuration>().FirstOrDefault();
            Assert.IsNotNull(statistic2Config);

            MockTerminatorConfiguration terminatorConfig = (MockTerminatorConfiguration)target.Terminator;
            Assert.IsNotNull(terminatorConfig);

            PrivateObject configAccessor = new PrivateObject(target);
            Assert.IsTrue((bool)configAccessor.GetField("isFrozen"));
        }


        /// <summary>
        /// Tests that the Validate method throws an exception when a null argument is passed.
        /// </summary>
        [TestMethod]
        public void ComponentConfigurationSet_Validate_NullArg()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            AssertEx.Throws<ArgumentNullException>(() => config.Validate(null));
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when passed a component has a mismatch configuration configured on the algorithm.
        /// </summary>
        [TestMethod]
        public async Task ComponentConfigurationSet_ValidateComponentConfiguration_MismatchedConfigurationAsync()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Population = new MockPopulationConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                MutationOperator = new MockMutationOperatorConfiguration()
            };
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            await algorithm.InitializeAsync();
            AssertEx.Throws<InvalidOperationException>(() => new MockMutationOperator2(algorithm));
        }
    }
}
