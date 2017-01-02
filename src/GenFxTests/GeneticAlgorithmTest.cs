using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentModel;
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Population = new MockPopulationConfiguration(),
                Entity = new MockEntityConfiguration()
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
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoCrossover_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            //config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoElitism_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            //config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessEvaluator_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            //config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessScalingStrategy_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            //config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoEntityType_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            //config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoMutationOperatorType_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            //config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoPopulationType_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            //config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoSelectionOperatorType_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            //config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoStatisticType_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            //config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoTerminatorType_Async()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            config.ElitismStrategy = new MockElitismStrategyConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.MutationOperator = new MockMutationOperatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            //config.Terminator = new MockTerminatorConfiguration();
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting class is missing.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_ValidateRequiredSetting_Async()
        {
            ComponentConfigurationSet configurationSet = new ComponentConfigurationSet();
            configurationSet.SelectionOperator = new MockSelectionOperatorConfiguration();
            configurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            configurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            configurationSet.Population = new SimplePopulationConfiguration();
            configurationSet.Entity = new MockEntityConfiguration();

            AssertEx.Throws<ArgumentException>(() => new RequiredSettingGeneticAlgorithm(configurationSet));
        }

        /// <summary>
        /// Tests that the Run method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Async()
        {
            ComponentConfigurationSet config = GetConfiguration();

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
            ComponentConfigurationSet config = GetConfiguration();
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
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration()
            });

            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.RunAsync());
        }

        /// <summary>
        /// Tests that the Step method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Async()
        {
            ComponentConfigurationSet config = GetConfiguration();
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
            ComponentConfigurationSet config = GetConfiguration();
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
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration()
            });

            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.StepAsync());
        }

        /// <summary>
        /// Tests that the ApplyElitism method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyElitism()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration
                {
                    EvaluationMode = FitnessEvaluationMode.Maximize
                },
                SelectionOperator = new MockSelectionOperatorConfiguration
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration()
            };

            TestApplyElitism(config, 0);

            config.ElitismStrategy = new MockElitismStrategyConfiguration
            {
                ElitistRatio = .1
            };
            TestApplyElitism(config, 1);
        }

        private void TestApplyElitism(ComponentConfigurationSet config, int expectedEliteCount)
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
            IGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
                CrossoverOperator = new MockCrossoverOperatorConfiguration
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperatorConfiguration
                {
                    MutationRate = 1
                },
                SelectionOperator = new MockSelectionOperatorConfiguration
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
            IGeneticAlgorithm algorithm = new TestGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                CrossoverOperator = new MockCrossoverOperatorConfiguration
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
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new TestGeneticAlgorithmConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration()
            };

            TestApplyMutation(config, 0);

            config.MutationOperator = new MockMutationOperatorConfiguration
            {
                MutationRate = .01
            };
            TestApplyMutation(config, 3);
        }

        private void TestApplyMutation(ComponentConfigurationSet config, int expectedMutationCount)
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
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoGeneticAlgorithm()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            //config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoSelectionOperator()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            //config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoFitnessEvaluator()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            //config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoPopulation()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            //config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_NoEntity()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            //config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config, true);
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when a component's configuration property is determined to be invalid.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateComponentConfiguration_InvalidProperty()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                MutationOperator = new FakeValidationMutationOperatorConfiguration()
            };

            AssertEx.Throws<ValidationException>(() => new MockGeneticAlgorithm(config));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                CrossoverOperator = new MockCrossoverOperatorConfiguration
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                CrossoverOperator = new MockCrossoverOperatorConfiguration
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                ElitismStrategy = new MockElitismStrategyConfiguration
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                ElitismStrategy = new MockElitismStrategyConfiguration
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Population = new MockPopulationConfiguration(),
                Entity = new MockEntityConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                MutationOperator = new MockMutationOperatorConfiguration
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                MutationOperator = new MockMutationOperatorConfiguration
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration
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
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
            };
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Statistics.Add(new MockStatistic2Configuration());
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
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
            };
            config.Statistics.Add(new MockStatisticConfiguration());
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                Terminator = new MockTerminatorConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                Terminator = new MockTerminatorConfiguration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                Terminator = new MockTerminator2Configuration()
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                Terminator = new MockTerminator3Configuration() // uses derived type of the required type
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
            SimpleGeneticAlgorithmConfiguration target = new SimpleGeneticAlgorithmConfiguration();
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
            SimpleGeneticAlgorithmConfiguration target = new SimpleGeneticAlgorithmConfiguration();
            int val = 0;
            AssertEx.Throws<ValidationException>(() => target.EnvironmentSize = val);
        }

        private static void TestValidateConfiguration(ComponentConfigurationSet config, bool exceptionExpectedOnValidation = false)
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

        private static async Task<IGeneticAlgorithm> TestInitializeAsync(ComponentConfigurationSet config, bool exceptionExpectedOnConstructor = false)
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

        private ComponentConfigurationSet GetConfiguration()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();

            int environmentSize = 2;
            int populationSize = 100;

            TestGeneticAlgorithmConfiguration algConfig = new TestGeneticAlgorithmConfiguration();
            algConfig.EnvironmentSize = environmentSize;
            config.GeneticAlgorithm = algConfig;

            MockPopulationConfiguration popConfig = new MockPopulationConfiguration();
            popConfig.PopulationSize = populationSize;
            config.Population = popConfig;

            MockCrossoverOperatorConfiguration crossConfig = new MockCrossoverOperatorConfiguration();
            crossConfig.CrossoverRate = .7;
            config.CrossoverOperator = crossConfig;

            MockElitismStrategyConfiguration eliteConfig = new MockElitismStrategyConfiguration();
            eliteConfig.ElitistRatio = .1;
            config.ElitismStrategy = eliteConfig;

            MockMutationOperatorConfiguration mutConfig = new MockMutationOperatorConfiguration();
            mutConfig.MutationRate = .01;
            config.MutationOperator = mutConfig;

            MockSelectionOperatorConfiguration selConfig = new MockSelectionOperatorConfiguration();
            selConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            config.SelectionOperator = selConfig;

            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            config.Entity = new MockEntityConfiguration();
            config.Statistics.Add(new MockStatisticConfiguration());
            config.Terminator = new TestTerminatorConfiguration();
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

        private class RequiredSettingGeneticAlgorithm : GeneticAlgorithm<RequiredSettingGeneticAlgorithm, RequiredSettingGeneticAlgorithmConfiguration>
        {
            public RequiredSettingGeneticAlgorithm(ComponentConfigurationSet configurationSet)
                : base(configurationSet)
            {
            }

            protected override Task CreateNextGenerationAsync(IPopulation population)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class RequiredSettingGeneticAlgorithmConfiguration : GeneticAlgorithmConfiguration<RequiredSettingGeneticAlgorithmConfiguration, RequiredSettingGeneticAlgorithm>
        {
        }

        private class TestTerminator : TerminatorBase<TestTerminator, TestTerminatorConfiguration>
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

        private class TestTerminatorConfiguration : TerminatorConfigurationBase<TestTerminatorConfiguration, TestTerminator>
        {
        }

        private class TestGeneticAlgorithm : GeneticAlgorithm<TestGeneticAlgorithm, TestGeneticAlgorithmConfiguration>
        {
            public TestGeneticAlgorithm(ComponentConfigurationSet config)
                : base(config)
            {
            }

            protected override Task CreateNextGenerationAsync(IPopulation population)
            {
                return Task.FromResult(true);
            }
        }

        private class TestGeneticAlgorithmConfiguration : GeneticAlgorithmConfiguration<TestGeneticAlgorithmConfiguration, TestGeneticAlgorithm>
        {
        }

        private class FakeValidationMutationOperator : MutationOperatorBase<FakeValidationMutationOperator, FakeValidationMutationOperatorConfiguration>
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

        private class FakeValidationMutationOperatorConfiguration : MutationOperatorConfigurationBase<FakeValidationMutationOperatorConfiguration, FakeValidationMutationOperator>
        {
            private int value = -1;

            [IntegerValidator(MinValue = 0)]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        private class FakeMutationOperator : MutationOperatorBase<FakeMutationOperator, FakeMutationOperatorConfiguration>
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

        private class FakeMutationOperatorConfiguration : MutationOperatorConfigurationBase<FakeMutationOperatorConfiguration, FakeMutationOperator>
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

        [IntegerExternalValidator(typeof(FakeMutationOperatorConfiguration), "Value")]
        [CustomExternalValidator(typeof(FakeValidator), typeof(FakeMutationOperatorConfiguration), "Value")]
        [DoubleExternalValidator(typeof(FakeMutationOperatorConfiguration), "Value2")]
        private class FakeExternalValidatorTerminator : TerminatorBase<FakeExternalValidatorTerminator, FakeExternalValidatorTerminatorConfiguration>
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

        private class FakeExternalValidatorTerminatorConfiguration : TerminatorConfigurationBase<FakeExternalValidatorTerminatorConfiguration, FakeExternalValidatorTerminator>
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
