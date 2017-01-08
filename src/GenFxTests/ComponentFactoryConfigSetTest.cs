using GenFx;
using GenFx.Contracts;
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
    /// This is a test class for <see cref="ComponentFactoryConfigSet"/>. 
    /// </summary>
    [TestClass]
    public class ComponentFactoryConfigSetTest
    {

        /// <summary>
        /// Tests that ComponentConfigurationSet.CrossoverOperator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void CrossoverOperatorTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockCrossoverOperatorFactoryConfig val = new MockCrossoverOperatorFactoryConfig();
            target.CrossoverOperator = val;

            Assert.AreSame(val, target.CrossoverOperator, "CrossoverOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.CrossoverOperator can be set to a valid null value.
        ///</summary>
        [TestMethod()]
        public void CrossoverOperatorTest_ValidNull()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();

            MockCrossoverOperatorFactoryConfig val = null;

            target.CrossoverOperator = val;

            Assert.AreSame(val, target.CrossoverOperator, "CrossoverOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.ElitismStrategy can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void ElitismStrategyTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockElitismStrategyFactoryConfig val = new MockElitismStrategyFactoryConfig();
            target.ElitismStrategy = val;

            Assert.AreSame(val, target.ElitismStrategy, "ElitismStrategy was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.ElitismStrategy can be set to a valid null value.
        ///</summary>
        [TestMethod()]
        public void ElitismStrategyTest_ValidNull()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockElitismStrategyFactoryConfig val = null;
            target.ElitismStrategy = val;

            Assert.AreSame(val, target.ElitismStrategy, "ElitismStrategy was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.FitnessEvaluator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void FitnessEvaluatorTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockFitnessEvaluatorFactoryConfig val = new MockFitnessEvaluatorFactoryConfig();
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
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            AssertEx.Throws<ArgumentNullException>(() => target.FitnessEvaluator = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.FitnessScalingStrategy can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void FitnessScalingStrategyTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockFitnessScalingStrategyFactoryConfig val = new MockFitnessScalingStrategyFactoryConfig();
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
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockFitnessScalingStrategyFactoryConfig val = null;
            target.FitnessScalingStrategy = val;
            Assert.AreSame(val, target.FitnessScalingStrategy, "FitnessScalingStrategy was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Entity can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void EntityTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockEntityFactoryConfig val = new MockEntityFactoryConfig();
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
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            AssertEx.Throws<ArgumentNullException>(() => target.Entity = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.MutationOperator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void MutationOperatorTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockMutationOperatorFactoryConfig val = new MockMutationOperatorFactoryConfig();
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
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockMutationOperatorFactoryConfig val = null;
            target.MutationOperator = val;
            Assert.AreSame(val, target.MutationOperator, "MutationOperator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Population can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void PopulationTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockPopulationFactoryConfig val = new MockPopulationFactoryConfig();
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
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            AssertEx.Throws<ArgumentNullException>(() => target.Population = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.SelectionOperator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void SelectionOperatorTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockSelectionOperatorFactoryConfig val = new MockSelectionOperatorFactoryConfig();
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
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            AssertEx.Throws<ArgumentNullException>(() => target.SelectionOperator = null);
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Statistics is initialized correctly.
        ///</summary>
        [TestMethod()]
        public void StatisticsTest()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            Assert.AreEqual(0, target.Statistics.Count, "GenFx.ComponentConfigurationSet.Statistics was not initialized correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Terminator can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void TerminatorTest_Valid()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            MockTerminatorFactoryConfig val = new MockTerminatorFactoryConfig();
            target.Terminator = val;
            Assert.AreSame(val, target.Terminator, "Terminator was not set correctly.");
        }

        /// <summary>
        /// Tests that ComponentConfigurationSet.Terminator cannot be set to a null value.
        ///</summary>
        [TestMethod()]
        public void TerminatorTest_ValidNull()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();
            target.Terminator = null;
            Assert.IsNotNull(target.Terminator, "Terminator was not set correctly.");
        }

        /// <summary>
        /// Tests that the state of the configuration set can be saved correctly.
        /// </summary>
        [TestMethod]
        public void SaveState()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();

            MockCrossoverOperatorFactoryConfig crossoverConfig = new MockCrossoverOperatorFactoryConfig
            {
                CrossoverRate = 0.2
            };
            target.CrossoverOperator = crossoverConfig;

            MockElitismStrategyFactoryConfig elitismConfig = new MockElitismStrategyFactoryConfig
            {
                ElitistRatio = 0.2
            };
            target.ElitismStrategy = elitismConfig;

            MockEntityFactoryConfig entityConfig = new MockEntityFactoryConfig();
            target.Entity = entityConfig;

            MockFitnessEvaluatorFactoryConfig fitnessEvalConfig = new MockFitnessEvaluatorFactoryConfig
            {
                EvaluationMode = FitnessEvaluationMode.Maximize
            };
            target.FitnessEvaluator = fitnessEvalConfig;

            MockFitnessScalingStrategyFactoryConfig fitnessScalingConfig = new MockFitnessScalingStrategyFactoryConfig();
            target.FitnessScalingStrategy = fitnessScalingConfig;

            MockGeneticAlgorithmFactoryConfig geneticAlgorithmConfig = new MockGeneticAlgorithmFactoryConfig
            {
                EnvironmentSize = 5,
            };
            target.GeneticAlgorithm = geneticAlgorithmConfig;

            MockMutationOperatorFactoryConfig mutationConfig = new MockMutationOperatorFactoryConfig
            {
                MutationRate = 0.7
            };
            target.MutationOperator = mutationConfig;

            MockPluginFactoryConfig plugin1Config = new MockPluginFactoryConfig();
            MockPlugin2FactoryConfig plugin2Config = new MockPlugin2FactoryConfig();
            target.Plugins.Add(plugin1Config);
            target.Plugins.Add(plugin2Config);

            MockPopulationFactoryConfig populationConfig = new MockPopulationFactoryConfig
            {
                PopulationSize = 10
            };
            target.Population = populationConfig;

            MockSelectionOperatorFactoryConfig selectionConfig = new MockSelectionOperatorFactoryConfig
            {
                SelectionBasedOnFitnessType = FitnessType.Raw
            };
            target.SelectionOperator = selectionConfig;

            MockStatisticFactoryConfig stat1Config = new MockStatisticFactoryConfig();
            MockStatistic2FactoryConfig stat2Config = new MockStatistic2FactoryConfig();
            target.Statistics.Add(stat1Config);
            target.Statistics.Add(stat2Config);

            target.Terminator = null;

            KeyValueMap state = target.SaveState();

            KeyValueMap crossoverState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.CrossoverOperator)];
            Assert.AreEqual(2, crossoverState.Count);
            Assert.AreEqual(crossoverConfig.GetType().AssemblyQualifiedName, crossoverState["$type"]);
            Assert.AreEqual(crossoverConfig.CrossoverRate, crossoverState[nameof(ICrossoverOperatorFactoryConfig.CrossoverRate)]);

            KeyValueMap elitismState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.ElitismStrategy)];
            Assert.AreEqual(2, elitismState.Count);
            Assert.AreEqual(elitismConfig.GetType().AssemblyQualifiedName, elitismState["$type"]);
            Assert.AreEqual(elitismConfig.ElitistRatio, elitismState[nameof(IElitismStrategyFactoryConfig.ElitistRatio)]);

            KeyValueMap entityState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.Entity)];
            Assert.AreEqual(1, entityState.Count);
            Assert.AreEqual(entityConfig.GetType().AssemblyQualifiedName, entityState["$type"]);

            KeyValueMap fitnessEvalState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.FitnessEvaluator)];
            Assert.AreEqual(2, fitnessEvalState.Count);
            Assert.AreEqual(fitnessEvalConfig.GetType().AssemblyQualifiedName, fitnessEvalState["$type"]);
            Assert.AreEqual(fitnessEvalConfig.EvaluationMode.ToString(), fitnessEvalState[nameof(IFitnessEvaluatorFactoryConfig.EvaluationMode)]);

            KeyValueMap fitnessScalingState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.FitnessScalingStrategy)];
            Assert.AreEqual(1, fitnessScalingState.Count);
            Assert.AreEqual(fitnessScalingConfig.GetType().AssemblyQualifiedName, fitnessScalingState["$type"]);

            KeyValueMap geneticAlgorithmState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.GeneticAlgorithm)];
            Assert.AreEqual(2, geneticAlgorithmState.Count);
            Assert.AreEqual(geneticAlgorithmConfig.GetType().AssemblyQualifiedName, geneticAlgorithmState["$type"]);
            Assert.AreEqual(geneticAlgorithmConfig.EnvironmentSize, geneticAlgorithmState[nameof(IGeneticAlgorithmFactoryConfig.EnvironmentSize)]);

            KeyValueMap mutationState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.MutationOperator)];
            Assert.AreEqual(2, mutationState.Count);
            Assert.AreEqual(mutationConfig.GetType().AssemblyQualifiedName, mutationState["$type"]);
            Assert.AreEqual(mutationConfig.MutationRate, mutationState[nameof(IMutationOperatorFactoryConfig.MutationRate)]);

            KeyValueMapCollection pluginsState = (KeyValueMapCollection)state[nameof(ComponentFactoryConfigSet.Plugins)];
            Assert.AreEqual(2, pluginsState.Count);
            KeyValueMap plugin1State = pluginsState[0];
            Assert.AreEqual(plugin1Config.GetType().AssemblyQualifiedName, plugin1State["$type"]);
            KeyValueMap plugin2State = pluginsState[1];
            Assert.AreEqual(plugin2Config.GetType().AssemblyQualifiedName, plugin2State["$type"]);

            KeyValueMap populationState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.Population)];
            Assert.AreEqual(2, populationState.Count);
            Assert.AreEqual(populationConfig.GetType().AssemblyQualifiedName, populationState["$type"]);
            Assert.AreEqual(populationConfig.PopulationSize, populationState[nameof(IPopulationFactoryConfig.PopulationSize)]);

            KeyValueMap selectionState = (KeyValueMap)state[nameof(ComponentFactoryConfigSet.SelectionOperator)];
            Assert.AreEqual(2, selectionState.Count);
            Assert.AreEqual(selectionConfig.GetType().AssemblyQualifiedName, selectionState["$type"]);
            Assert.AreEqual(selectionConfig.SelectionBasedOnFitnessType.ToString(), selectionState[nameof(ISelectionOperatorFactoryConfig.SelectionBasedOnFitnessType)]);

            KeyValueMapCollection statsState = (KeyValueMapCollection)state[nameof(ComponentFactoryConfigSet.Statistics)];
            Assert.AreEqual(2, statsState.Count);
            KeyValueMap stat1State = statsState[0];
            Assert.AreEqual(stat1Config.GetType().AssemblyQualifiedName, stat1State["$type"]);
            KeyValueMap stat2State = statsState[1];
            Assert.AreEqual(stat2Config.GetType().AssemblyQualifiedName, stat2State["$type"]);

            Assert.IsNotNull(state[nameof(ComponentFactoryConfigSet.Terminator)]); // always defaults to non-null
        }

        /// <summary>
        /// Tests that a configuration set can be restored from state that contains all null values.
        /// </summary>
        [TestMethod]
        public void RestoreState_AllNull()
        {
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();

            KeyValueMap state = new KeyValueMap
            {
                { nameof(ComponentFactoryConfigSet.CrossoverOperator), null },
                { nameof(ComponentFactoryConfigSet.ElitismStrategy), null },
                { nameof(ComponentFactoryConfigSet.Entity), null },
                { nameof(ComponentFactoryConfigSet.FitnessEvaluator), null },
                { nameof(ComponentFactoryConfigSet.FitnessScalingStrategy), null },
                { nameof(ComponentFactoryConfigSet.GeneticAlgorithm), null },
                { nameof(ComponentFactoryConfigSet.MutationOperator), null },
                { nameof(ComponentFactoryConfigSet.Plugins), new KeyValueMapCollection() },
                { nameof(ComponentFactoryConfigSet.Population), null },
                { nameof(ComponentFactoryConfigSet.SelectionOperator), null },
                { nameof(ComponentFactoryConfigSet.Statistics), new KeyValueMapCollection() },
                { nameof(ComponentFactoryConfigSet.Terminator), null },
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
            ComponentFactoryConfigSet target = new ComponentFactoryConfigSet();

            KeyValueMap state = new KeyValueMap
            {
                { nameof(ComponentFactoryConfigSet.CrossoverOperator), new KeyValueMap
                    {
                        { "$type", typeof(MockCrossoverOperatorFactoryConfig).AssemblyQualifiedName },
                        { "CrossoverRate", 0.2 }
                    }
                },
                { nameof(ComponentFactoryConfigSet.ElitismStrategy), new KeyValueMap
                    {
                        { "$type", typeof(MockElitismStrategyFactoryConfig).AssemblyQualifiedName },
                        { "ElitistRatio", 0.5 }
                    }
                },
                { nameof(ComponentFactoryConfigSet.Entity), new KeyValueMap
                    {
                        { "$type", typeof(MockEntityFactoryConfig).AssemblyQualifiedName },
                    }
                },
                { nameof(ComponentFactoryConfigSet.FitnessEvaluator), new KeyValueMap
                    {
                        { "$type", typeof(MockFitnessEvaluatorFactoryConfig).AssemblyQualifiedName },
                        { "EvaluationMode", "Maximize" }
                    }
                },
                { nameof(ComponentFactoryConfigSet.FitnessScalingStrategy), new KeyValueMap
                    {
                        { "$type", typeof(MockFitnessScalingStrategyFactoryConfig).AssemblyQualifiedName },
                    }
                },
                { nameof(ComponentFactoryConfigSet.GeneticAlgorithm), new KeyValueMap
                    {
                        { "$type", typeof(MockGeneticAlgorithmFactoryConfig).AssemblyQualifiedName },
                        { "EnvironmentSize", 10 },
                        { "StatisticsEnabled", false }
                    }
                },
                { nameof(ComponentFactoryConfigSet.MutationOperator), new KeyValueMap
                    {
                        { "$type", typeof(MockMutationOperatorFactoryConfig).AssemblyQualifiedName },
                        { "MutationRate", 0.7 },
                    }
                },
                { nameof(ComponentFactoryConfigSet.Plugins), new KeyValueMapCollection
                    {
                        new KeyValueMap
                        {
                            { "$type", typeof(MockPluginFactoryConfig).AssemblyQualifiedName },
                        },
                        new KeyValueMap
                        {
                            { "$type", typeof(MockPlugin2FactoryConfig).AssemblyQualifiedName },
                        }
                    }
                },
                { nameof(ComponentFactoryConfigSet.Population), new KeyValueMap
                    {
                        { "$type", typeof(MockPopulationFactoryConfig).AssemblyQualifiedName },
                        { "PopulationSize", 15 },
                    }
                },
                { nameof(ComponentFactoryConfigSet.SelectionOperator), new KeyValueMap
                    {
                        { "$type", typeof(MockSelectionOperatorFactoryConfig).AssemblyQualifiedName },
                        { "SelectionBasedOnFitnessType", "Raw" },
                    }
                },
                { nameof(ComponentFactoryConfigSet.Statistics), new KeyValueMapCollection
                    {
                        new KeyValueMap
                        {
                            { "$type", typeof(MockStatisticFactoryConfig).AssemblyQualifiedName },
                        },
                        new KeyValueMap
                        {
                            { "$type", typeof(MockStatistic2FactoryConfig).AssemblyQualifiedName },
                        }
                    }
                },
                { nameof(ComponentFactoryConfigSet.Terminator), new KeyValueMap
                    {
                        { "$type", typeof(MockTerminatorFactoryConfig).AssemblyQualifiedName },
                    }
                },
                { "isFrozen", true }
            };

            target.RestoreState(state);

            MockCrossoverOperatorFactoryConfig crossoverConfig = (MockCrossoverOperatorFactoryConfig)target.CrossoverOperator;
            Assert.AreEqual(0.2, crossoverConfig.CrossoverRate);

            MockElitismStrategyFactoryConfig elitismConfig = (MockElitismStrategyFactoryConfig)target.ElitismStrategy;
            Assert.AreEqual(0.5, elitismConfig.ElitistRatio);

            MockEntityFactoryConfig entityConfig = (MockEntityFactoryConfig)target.Entity;
            Assert.IsNotNull(entityConfig);

            MockFitnessEvaluatorFactoryConfig fitnessEvalConfig = (MockFitnessEvaluatorFactoryConfig)target.FitnessEvaluator;
            Assert.AreEqual(FitnessEvaluationMode.Maximize, fitnessEvalConfig.EvaluationMode);

            MockFitnessScalingStrategyFactoryConfig fitnessScalingConfig = (MockFitnessScalingStrategyFactoryConfig)target.FitnessScalingStrategy;
            Assert.IsNotNull(fitnessScalingConfig);

            MockGeneticAlgorithmFactoryConfig mockGeneticAlgorithmConfig = (MockGeneticAlgorithmFactoryConfig)target.GeneticAlgorithm;
            Assert.AreEqual(10, mockGeneticAlgorithmConfig.EnvironmentSize);

            MockMutationOperatorFactoryConfig mutationConfig = (MockMutationOperatorFactoryConfig)target.MutationOperator;
            Assert.AreEqual(0.7, mutationConfig.MutationRate);

            Assert.AreEqual(2, target.Plugins.Count);
            MockPluginFactoryConfig plugin1Config = (MockPluginFactoryConfig)target.Plugins.OfType<MockPluginFactoryConfig>().FirstOrDefault();
            Assert.IsNotNull(plugin1Config);
            MockPlugin2FactoryConfig plugin2Config = (MockPlugin2FactoryConfig)target.Plugins.OfType<MockPlugin2FactoryConfig>().FirstOrDefault();
            Assert.IsNotNull(plugin2Config);

            MockPopulationFactoryConfig populationConfig = (MockPopulationFactoryConfig)target.Population;
            Assert.AreEqual(15, populationConfig.PopulationSize);

            MockSelectionOperatorFactoryConfig selectionConfig = (MockSelectionOperatorFactoryConfig)target.SelectionOperator;
            Assert.AreEqual(FitnessType.Raw, selectionConfig.SelectionBasedOnFitnessType);

            Assert.AreEqual(2, target.Statistics.Count);
            MockStatisticFactoryConfig statistic1Config = (MockStatisticFactoryConfig)target.Statistics.OfType<MockStatisticFactoryConfig>().FirstOrDefault();
            Assert.IsNotNull(statistic1Config);
            MockStatistic2FactoryConfig statistic2Config = (MockStatistic2FactoryConfig)target.Statistics.OfType<MockStatistic2FactoryConfig>().FirstOrDefault();
            Assert.IsNotNull(statistic2Config);

            MockTerminatorFactoryConfig terminatorConfig = (MockTerminatorFactoryConfig)target.Terminator;
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
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();
            AssertEx.Throws<ArgumentNullException>(() => config.Validate(null));
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when passed a component has a mismatch configuration configured on the algorithm.
        /// </summary>
        [TestMethod]
        public async Task ComponentConfigurationSet_ValidateComponentConfiguration_MismatchedConfigurationAsync()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                MutationOperator = new MockMutationOperatorFactoryConfig()
            };
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            await algorithm.InitializeAsync();
            AssertEx.Throws<InvalidOperationException>(() => new MockMutationOperator2(algorithm));
        }
    }
}
