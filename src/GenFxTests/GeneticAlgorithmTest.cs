using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
using GenFx.Validation;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.GeneticAlgorithm and is intended
    /// to contain all GenFx.GeneticAlgorithm Unit Tests
    /// </summary>
    [TestClass()]
    public class GeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_Ctor()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Entity = new MockEntityFactoryConfig()
            });
            Assert.IsNotNull(algorithm.Environment, "Environment not initialized.");
            PrivateObject accessor = new PrivateObject(algorithm.Environment);
            Assert.AreSame(algorithm, accessor.GetField("algorithm"), "Environment should be initialized with the algorithm.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoCrossover_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            //config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoElitism_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            //config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessEvaluator_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            //config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessScalingStrategy_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            //config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoEntityType_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            //config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoMutationOperatorType_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            //config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoPopulationType_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            //config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoSelectionOperatorType_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            //config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoStatisticType_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            //config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorFactoryConfig();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoTerminatorType_Async()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.CrossoverOperator = new MockCrossoverOperatorFactoryConfig();
            config.ElitismStrategy = new MockElitismStrategyFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.MutationOperator = new MockMutationOperatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            //config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting class is missing.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_ValidateRequiredSetting_Async()
        {
            ComponentFactoryConfigSet configurationSet = new ComponentFactoryConfigSet();
            configurationSet.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            configurationSet.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            configurationSet.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            configurationSet.Population = new SimplePopulationFactoryConfig();
            configurationSet.Entity = new MockEntityFactoryConfig();

            AssertEx.Throws<ArgumentException>(() => new RequiredSettingGeneticAlgorithm(configurationSet));
        }

        /// <summary>
        /// Tests that the Run method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Async()
        {
            ComponentFactoryConfigSet config = GetConfiguration();

            int eventHandlerCallCount = 0;
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(config);
            await algorithm.InitializeAsync();
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate(object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventHandlerCallCount++;
            });
            await algorithm.RunAsync();

            Assert.AreEqual(3, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(3, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.Operators.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.Operators.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(800, ((MockFitnessEvaluator)algorithm.Operators.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(8, ((MockFitnessScalingStrategy)algorithm.Operators.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.Operators.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.Operators.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run twice without initializing in between.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Twice_NoInitialize_Async()
        {
            ComponentFactoryConfigSet config = GetConfiguration();
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(config);

            await algorithm.InitializeAsync();
            await algorithm.RunAsync();
            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.RunAsync());
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run without first calling Initialize.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig()
            });

            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.RunAsync());
        }

        /// <summary>
        /// Tests that the Step method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Async()
        {
            ComponentFactoryConfigSet config = GetConfiguration();
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(config);

            int eventHandlerCallCount = 0;
            await algorithm.InitializeAsync();
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate(object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventHandlerCallCount++;
            });
            bool stepResult = await algorithm.StepAsync();

            Assert.IsFalse(stepResult, "Algorithm should not be complete yet.");

            Assert.AreEqual(1, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(1, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.Operators.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.Operators.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(400, ((MockFitnessEvaluator)algorithm.Operators.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(4, ((MockFitnessScalingStrategy)algorithm.Operators.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.Operators.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.Operators.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");

            stepResult = await algorithm.StepAsync();
            Assert.IsFalse(stepResult, "Algorithm should not be complete yet.");

            stepResult = await algorithm.StepAsync();
            Assert.IsTrue(stepResult, "Algorithm should be complete.");

            Assert.AreEqual(3, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(3, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.Operators.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.Operators.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(800, ((MockFitnessEvaluator)algorithm.Operators.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(8, ((MockFitnessScalingStrategy)algorithm.Operators.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.Operators.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.Operators.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");
        }

        /// <summary>
        /// Tests that an exception is thrown when Step is called too many times with no initialize in between.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Overbounds_NoInitialize_Async()
        {
            ComponentFactoryConfigSet config = GetConfiguration();
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(config);

            await algorithm.InitializeAsync();
            bool result = await algorithm.StepAsync();
            Assert.IsFalse(result, "Algorithm should not be complete.");

            result = await algorithm.StepAsync();
            Assert.IsFalse(result, "Algorithm should not be complete.");

            result = await algorithm.StepAsync();
            Assert.IsTrue(result, "Algorithm should be complete.");

            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.StepAsync());
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run without first calling Initialize.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig()
            });

            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.StepAsync());
        }

        /// <summary>
        /// Tests that the ApplyElitism method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyElitism()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig
                {
                    EvaluationMode = FitnessEvaluationMode.Maximize
                },
                SelectionOperator = new MockSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig()
            };

            TestApplyElitism(config, 0);

            config.ElitismStrategy = new MockElitismStrategyFactoryConfig
            {
                ElitistRatio = .1
            };
            TestApplyElitism(config, 1);
        }

        private void TestApplyElitism(ComponentFactoryConfigSet config, int expectedEliteCount)
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(config);
            PrivateObject accessor = new PrivateObject(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            for (int i = 0; i < 10; i++)
            {
                population.Entities.Add(new MockEntity(algorithm));
            }
            IList<IGeneticEntity> entity = (IList<IGeneticEntity>)accessor.Invoke("ApplyElitism", population);
            Assert.AreEqual(expectedEliteCount, entity.Count);
        }

        /// <summary>
        /// Tests that the SelectGeneticEntitiesAndApplyCrossoverAndMutation method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_SelectGeneticEntitiesAndApplyCrossoverAndMutation()
        {
            IGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                CrossoverOperator = new MockCrossoverOperatorFactoryConfig
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperatorFactoryConfig
                {
                    MutationRate = 1
                },
                SelectionOperator = new MockSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            });
            
            MockCrossoverOperator crossoverOp = new MockCrossoverOperator(algorithm);
            algorithm.Operators.CrossoverOperator = crossoverOp;
            MockMutationOperator mutationOp = new MockMutationOperator(algorithm);
            algorithm.Operators.MutationOperator = mutationOp;
            MockSelectionOperator selectionOp = new MockSelectionOperator(algorithm);
            algorithm.Operators.SelectionOperator = selectionOp;

            SimplePopulation population = new SimplePopulation(algorithm);
            for (int i = 0; i < 10; i++)
            {
                population.Entities.Add(new MockEntity(algorithm));
            }

            PrivateObject algAccessor = new PrivateObject(algorithm);

            IList<IGeneticEntity> geneticEntities = (IList<IGeneticEntity>)algAccessor.Invoke("SelectGeneticEntitiesAndApplyCrossoverAndMutation", population);

            Assert.AreEqual(2, geneticEntities.Count, "Incorrect number of genetic entities returned.");
            Assert.AreEqual(2, selectionOp.DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(1, crossoverOp.DoCrossoverCallCount, "Crossover not called correctly.");
            Assert.AreEqual(2, mutationOp.DoMutateCallCount, "Mutation not called correctly.");
        }

        /// <summary>
        /// Tests that the ApplyCrossover method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyCrossover()
        {
            IGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                CrossoverOperator = new MockCrossoverOperatorFactoryConfig
                {
                    CrossoverRate = 1
                }
            });

            MockCrossoverOperator crossoverOp = new MockCrossoverOperator(algorithm);

            algorithm.Operators.CrossoverOperator = crossoverOp;

            PrivateObject algAccessor = new PrivateObject(algorithm);
            IList<IGeneticEntity> geneticEntities = (IList<IGeneticEntity>)algAccessor.Invoke("ApplyCrossover", new MockEntity(algorithm), new MockEntity(algorithm));

            Assert.AreEqual(2, geneticEntities.Count, "Incorrect number of genetic entities returned.");
            Assert.AreEqual(1, crossoverOp.DoCrossoverCallCount, "Crossover not called correctly.");
        }

        /// <summary>
        /// Tests that the ApplyMutation method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyMutation()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig()
            };

            TestApplyMutation(config, 0);

            config.MutationOperator = new MockMutationOperatorFactoryConfig
            {
                MutationRate = .01
            };
            TestApplyMutation(config, 3);
        }

        private void TestApplyMutation(ComponentFactoryConfigSet config, int expectedMutationCount)
        {
            IGeneticAlgorithm algorithm = new TestGeneticAlgorithm(config);
            PrivateObject accessor = new PrivateObject(algorithm);
            List<IGeneticEntity> geneticEntities = new List<IGeneticEntity>();
            geneticEntities.Add(new MockEntity(algorithm));
            geneticEntities.Add(new MockEntity(algorithm));
            geneticEntities.Add(new MockEntity(algorithm));

            IList<IGeneticEntity> mutants = (IList<IGeneticEntity>)accessor.Invoke("ApplyMutation", geneticEntities);

            Assert.AreEqual(geneticEntities.Count, mutants.Count, "Incorrect number of genetic entities returned.");
            if (algorithm.Operators.MutationOperator != null)
            {
                MockMutationOperator mutationOp = (MockMutationOperator)algorithm.Operators.MutationOperator;
                Assert.AreEqual(expectedMutationCount, mutationOp.DoMutateCallCount, "Mutation not called correctly.");
            }
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            TestValidateConfiguration(config);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoGeneticAlgorithm()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();
            //config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoSelectionOperator()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            //config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoFitnessEvaluator()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            //config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoPopulation()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            //config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityFactoryConfig();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoEntity()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig();
            config.SelectionOperator = new MockSelectionOperatorFactoryConfig();
            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.Population = new MockPopulationFactoryConfig();
            //config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when a component's configuration property is determined to be invalid.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateComponentConfiguration_InvalidProperty()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                MutationOperator = new FakeValidationMutationOperatorFactoryConfig()
            };

            AssertEx.Throws<ValidationException>(() => new MockGeneticAlgorithm(config));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                CrossoverOperator = new MockCrossoverOperatorFactoryConfig
                {
                    CrossoverRate = 1
                }
            });
            Type testType = typeof(CrossoverDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                CrossoverOperator = new MockCrossoverOperatorFactoryConfig
                {
                    CrossoverRate = 1
                }
            });
            Type testType = typeof(CrossoverDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidElitism()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                ElitismStrategy = new MockElitismStrategyFactoryConfig
                {
                    ElitistRatio = 1
                }
            });
            Type testType = typeof(ElitismDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidElitism()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                ElitismStrategy = new MockElitismStrategyFactoryConfig
                {
                    ElitistRatio = 1
                }
            });

            Type testType = typeof(ElitismDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig()
            });
            Type testType = typeof(FitnessEvaluatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
            });
            Type testType = typeof(FitnessEvaluatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig()
            });
            Type testType = typeof(FitnessScalingStrategyDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig()
            });
            Type testType = typeof(FitnessScalingStrategyDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Entity = new MockEntityFactoryConfig()
            });
            Type testType = typeof(EntityDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig()
            });
            Type testType = typeof(EntityDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                MutationOperator = new MockMutationOperatorFactoryConfig
                {
                    MutationRate = 1
                }
            });
            Type testType = typeof(MutationOperatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                MutationOperator = new MockMutationOperatorFactoryConfig
                {
                    MutationRate = 1
                }
            });
            Type testType = typeof(MutationOperatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig()
            });
            Type testType = typeof(PopulationDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig()
            });
            Type testType = typeof(PopulationDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            });
            Type testType = typeof(SelectionOperatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            });
            Type testType = typeof(SelectionOperatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidStatisticType()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
            };
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Statistics.Add(new MockStatistic2FactoryConfig());
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            Type testType = typeof(StatisticDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidStatisticType()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
            };
            config.Statistics.Add(new MockStatisticFactoryConfig());
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            Type testType = typeof(StatisticDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Terminator = new MockTerminatorFactoryConfig()
            });
            Type testType = typeof(TerminatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Terminator = new MockTerminatorFactoryConfig()
            });
            Type testType = typeof(TerminatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly when overriding a required type of a base class.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_OverrideRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Terminator = new MockTerminator2FactoryConfig()
            });
            Type testType = typeof(TerminatorDependentDerivedClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_UsingBaseTypeAsRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                Terminator = new MockTerminator3FactoryConfig() // uses derived type of the required type
            });
            Type testType = typeof(TerminatorDependentClass3);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that GeneticAlgorithmConfiguration.EnvironmentSize can be set to valid value.
        ///</summary>
        [TestMethod()]
        public void EnvironmentSizeTest_Valid()
        {
            SimpleGeneticAlgorithmFactoryConfig target = new SimpleGeneticAlgorithmFactoryConfig();
            int val = 2;
            target.EnvironmentSize = val;

            Assert.AreEqual(val, target.EnvironmentSize, "EnvironmentSize was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when GeneticAlgorithmConfiguration.EnvironmentSize is set to 
        /// an invalid value.
        ///</summary>
        [TestMethod()]
        public void EnvironmentSizeTest_Invalid()
        {
            SimpleGeneticAlgorithmFactoryConfig target = new SimpleGeneticAlgorithmFactoryConfig();
            int val = 0;
            AssertEx.Throws<ValidationException>(() => target.EnvironmentSize = val);
        }

        private static void TestValidateConfiguration(ComponentFactoryConfigSet config, bool exceptionExpectedOnValidation = false)
        {
            if (exceptionExpectedOnValidation)
            {
                if (config.GeneticAlgorithm == null)
                {
                    AssertEx.Throws<ArgumentException>(() => new MockGeneticAlgorithm(config));
                }
                else
                {
                    AssertEx.Throws<InvalidOperationException>(() => new MockGeneticAlgorithm(config));
                }
            }
            else
            {
                new MockGeneticAlgorithm(config);
            }
        }

        private static async Task<IGeneticAlgorithm> TestInitializeAsync(ComponentFactoryConfigSet config, bool exceptionExpectedOnConstructor = false)
        {
            if (exceptionExpectedOnConstructor)
            {
                AssertEx.Throws<InvalidOperationException>(() => { new MockGeneticAlgorithm(config); });
                return null;
            }

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            bool eventCalled = false;
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate(object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventCalled = true;
            });
            
            await algorithm.InitializeAsync();

            Assert.AreEqual(0, algorithm.CurrentGeneration, "Generation should be initialized.");
            if (config.CrossoverOperator != null)
                Assert.IsInstanceOfType(algorithm.Operators.CrossoverOperator, typeof(MockCrossoverOperator), "CrossoverOperator not initialized correctly.");
            if (config.ElitismStrategy != null)
                Assert.IsInstanceOfType(algorithm.Operators.ElitismStrategy, typeof(MockElitismStrategy), "ElitismStrategy not initialized correctly.");
            if (config.FitnessEvaluator != null)
                Assert.IsInstanceOfType(algorithm.Operators.FitnessEvaluator, typeof(MockFitnessEvaluator), "FitnessEvaluator not initialized correctly.");
            if (config.FitnessScalingStrategy != null)
                Assert.IsInstanceOfType(algorithm.Operators.FitnessScalingStrategy, typeof(MockFitnessScalingStrategy), "FitnessScalingStrategy not initialized correctly.");
            if (config.MutationOperator != null)
                Assert.IsInstanceOfType(algorithm.Operators.MutationOperator, typeof(MockMutationOperator), "MutationOperator not initialized correctly.");
            if (config.SelectionOperator != null)
                Assert.IsInstanceOfType(algorithm.Operators.SelectionOperator, typeof(MockSelectionOperator), "SelectionOperator not initialized correctly.");
            if (config.Terminator != null)
                Assert.IsTrue(algorithm.Operators.Terminator is MockTerminator || algorithm.Operators.Terminator is DefaultTerminator, "Terminator not initialized correctly.");
            else
                Assert.IsInstanceOfType(algorithm.Operators.Terminator, typeof(GeneticAlgorithm<,>).Assembly.GetType("GenFx.EmptyTerminator"), "Terminator not initialized correctly.");

            Assert.AreEqual(config.Statistics.Count, algorithm.Statistics.Count(), "Statistic collection not initialized correctly.");
            if (config.Statistics.Count > 0)
            {
                Assert.IsNotNull(algorithm.Statistics.OfType<MockStatistic>().FirstOrDefault(), "Statistic not initialized correctly.");
            }

            Assert.AreEqual(algorithm.ConfigurationSet.GeneticAlgorithm.EnvironmentSize, algorithm.Environment.Populations.Count, "Environment not initialized correctly.");
            Assert.AreEqual(algorithm.ConfigurationSet.Population.PopulationSize, algorithm.Environment.Populations[0].Entities.Count, "Population not initialized correctly.");

            MockEntity entity = (MockEntity)algorithm.Environment.Populations[0].Entities[0];
            double entityId = Double.Parse(entity.Identifier);
            Assert.AreEqual(entityId, entity.RawFitnessValue, "Entity fitness was not evaluated.");
            if (config.Statistics.Count > 0)
                Assert.IsTrue(((MockStatistic)algorithm.Statistics.OfType<MockStatistic>().FirstOrDefault()).StatisticEvaluated, "Statistics were not evaluated.");
            Assert.IsTrue(eventCalled, "GenerationCreated event was not raised.");
            return algorithm;
        }

        private ComponentFactoryConfigSet GetConfiguration()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet();

            int environmentSize = 2;
            int populationSize = 100;

            TestGeneticAlgorithmFactoryConfig algConfig = new TestGeneticAlgorithmFactoryConfig();
            algConfig.EnvironmentSize = environmentSize;
            config.GeneticAlgorithm = algConfig;

            MockPopulationFactoryConfig popConfig = new MockPopulationFactoryConfig();
            popConfig.PopulationSize = populationSize;
            config.Population = popConfig;

            MockCrossoverOperatorFactoryConfig crossConfig = new MockCrossoverOperatorFactoryConfig();
            crossConfig.CrossoverRate = .7;
            config.CrossoverOperator = crossConfig;

            MockElitismStrategyFactoryConfig eliteConfig = new MockElitismStrategyFactoryConfig();
            eliteConfig.ElitistRatio = .1;
            config.ElitismStrategy = eliteConfig;

            MockMutationOperatorFactoryConfig mutConfig = new MockMutationOperatorFactoryConfig();
            mutConfig.MutationRate = .01;
            config.MutationOperator = mutConfig;

            MockSelectionOperatorFactoryConfig selConfig = new MockSelectionOperatorFactoryConfig();
            selConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            config.SelectionOperator = selConfig;

            config.FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyFactoryConfig();
            config.Entity = new MockEntityFactoryConfig();
            config.Statistics.Add(new MockStatisticFactoryConfig());
            config.Terminator = new TestTerminatorFactoryConfig();
            return config;
        }

        [RequiredCrossoverOperator(typeof(MockCrossoverOperator))]
        private class CrossoverDependentClass
        {
        }

        [RequiredCrossoverOperator(typeof(MockCrossoverOperator2))]
        private class CrossoverDependentClass2
        {
        }

        [RequiredElitismStrategy(typeof(MockElitismStrategy))]
        private class ElitismDependentClass
        {
        }

        [RequiredElitismStrategy(typeof(MockElitismStrategy2))]
        private class ElitismDependentClass2
        {
        }

        [RequiredFitnessEvaluator(typeof(MockFitnessEvaluator))]
        private class FitnessEvaluatorDependentClass
        {
        }

        [RequiredFitnessEvaluator(typeof(MockFitnessEvaluator2))]
        private class FitnessEvaluatorDependentClass2
        {
        }

        [RequiredFitnessScalingStrategy(typeof(MockFitnessScalingStrategy))]
        private class FitnessScalingStrategyDependentClass
        {
        }

        [RequiredFitnessScalingStrategy(typeof(MockFitnessScalingStrategy2))]
        private class FitnessScalingStrategyDependentClass2
        {
        }

        [RequiredGeneticAlgorithm(typeof(MockGeneticAlgorithm))]
        private class GeneticAlgorithmDependentClass
        {
        }

        [RequiredGeneticAlgorithm(typeof(MockGeneticAlgorithm2))]
        private class GeneticAlgorithmDependentClass2
        {
        }

        [RequiredEntity(typeof(MockEntity))]
        private class EntityDependentClass
        {
        }

        [RequiredEntity(typeof(MockEntity2))]
        private class EntityDependentClass2
        {
        }

        [RequiredMutationOperator(typeof(MockMutationOperator))]
        private class MutationOperatorDependentClass
        {
        }

        [RequiredMutationOperator(typeof(MockMutationOperator2))]
        private class MutationOperatorDependentClass2
        {
        }

        [RequiredPopulation(typeof(MockPopulation))]
        private class PopulationDependentClass
        {
        }

        [RequiredPopulation(typeof(MockPopulation2))]
        private class PopulationDependentClass2
        {
        }

        [RequiredSelectionOperator(typeof(MockSelectionOperator))]
        private class SelectionOperatorDependentClass
        {
        }

        [RequiredSelectionOperator(typeof(MockSelectionOperator2))]
        private class SelectionOperatorDependentClass2
        {
        }

        [RequiredStatistic(typeof(MockStatistic))]
        private class StatisticDependentClass
        {
        }

        [RequiredStatistic(typeof(MockStatistic2))]
        private class StatisticDependentClass2 : StatisticDependentClass
        {
        }

        [RequiredTerminator(typeof(MockTerminator))]
        private class TerminatorDependentClass
        {
        }

        [RequiredTerminator(typeof(IMockTerminator2))]
        private class TerminatorDependentClass2
        {
        }

        [RequiredTerminator(typeof(IMockTerminator2))]
        private class TerminatorDependentClass3
        {
        }

        [RequiredTerminator(typeof(MockTerminator))]
        private class TerminatorDependentBaseClass
        {
        }

        [RequiredTerminator(typeof(IMockTerminator2))]
        private class TerminatorDependentDerivedClass : TerminatorDependentBaseClass
        {
        }

        private class RequiredSettingGeneticAlgorithm : GeneticAlgorithm<RequiredSettingGeneticAlgorithm, RequiredSettingGeneticAlgorithmFactoryConfig>
        {
            public RequiredSettingGeneticAlgorithm(ComponentFactoryConfigSet configurationSet)
                : base(configurationSet)
            {
            }

            protected override Task CreateNextGenerationAsync(IPopulation population)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class RequiredSettingGeneticAlgorithmFactoryConfig : GeneticAlgorithmFactoryConfig<RequiredSettingGeneticAlgorithmFactoryConfig, RequiredSettingGeneticAlgorithm>
        {
        }

        private class TestTerminator : TerminatorBase<TestTerminator, TestTerminatorFactoryConfig>
        {
            public TestTerminator(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override bool IsComplete()
            {
                return this.Algorithm.CurrentGeneration == 3;
            }
        }

        private class TestTerminatorFactoryConfig : TerminatorFactoryConfigBase<TestTerminatorFactoryConfig, TestTerminator>
        {
        }

        private class TestGeneticAlgorithm : GeneticAlgorithm<TestGeneticAlgorithm, TestGeneticAlgorithmFactoryConfig>
        {
            public TestGeneticAlgorithm(ComponentFactoryConfigSet config)
                : base(config)
            {
            }

            protected override Task CreateNextGenerationAsync(IPopulation population)
            {
                return Task.FromResult(true);
            }
        }

        private class TestGeneticAlgorithmFactoryConfig : GeneticAlgorithmFactoryConfig<TestGeneticAlgorithmFactoryConfig, TestGeneticAlgorithm>
        {
        }

        private class FakeValidationMutationOperator : MutationOperatorBase<FakeValidationMutationOperator, FakeValidationMutationOperatorFactoryConfig>
        {
            public FakeValidationMutationOperator(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override bool GenerateMutation(IGeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeValidationMutationOperatorFactoryConfig : MutationOperatorFactoryConfigBase<FakeValidationMutationOperatorFactoryConfig, FakeValidationMutationOperator>
        {
            private int value = -1;

            [IntegerValidator(MinValue = 0)]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        private class FakeMutationOperator : MutationOperatorBase<FakeMutationOperator, FakeMutationOperatorFactoryConfig>
        {
            public FakeMutationOperator(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override bool GenerateMutation(IGeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeMutationOperatorFactoryConfig : MutationOperatorFactoryConfigBase<FakeMutationOperatorFactoryConfig, FakeMutationOperator>
        {
            private int value;
            private double value2;

            public double Value2
            {
                get { return this.value2; }
                set { this.value2 = value; }
            }

            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        [IntegerExternalValidator(typeof(FakeMutationOperatorFactoryConfig), "Value")]
        [CustomExternalValidator(typeof(FakeValidator), typeof(FakeMutationOperatorFactoryConfig), "Value")]
        [DoubleExternalValidator(typeof(FakeMutationOperatorFactoryConfig), "Value2")]
        private class FakeExternalValidatorTerminator : TerminatorBase<FakeExternalValidatorTerminator, FakeExternalValidatorTerminatorFactoryConfig>
        {
            public FakeExternalValidatorTerminator(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeExternalValidatorTerminatorFactoryConfig : TerminatorFactoryConfigBase<FakeExternalValidatorTerminatorFactoryConfig, FakeExternalValidatorTerminator>
        {
        }

        private class FakeValidator : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                return true;
            }
        }
    }
}
