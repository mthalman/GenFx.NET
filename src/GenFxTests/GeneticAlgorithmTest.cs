using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
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
        /// Tests that the InitializeAsync initializes the state correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_InitializeAsync()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity()
            };
            await algorithm.InitializeAsync();
            Assert.IsNotNull(algorithm.Environment, "Environment not initialized.");
            PrivateObject accessor = new PrivateObject(algorithm.Environment);
            Assert.AreSame(algorithm, accessor.GetField("algorithm"), "Environment should be initialized with the algorithm.");
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoCrossover_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                //CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoElitism_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                //ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessEvaluator_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                //FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());
            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessScalingStrategy_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                //FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoEntityType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                //GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());
            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoMutationOperatorType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                //MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoPopulationType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                //PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());
            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoSelectionOperatorType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                //SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Statistics.Add(new MockStatistic());

            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoStatisticType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            //algorithm.Statistics.Add(new MockStatistic());

            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoTerminatorType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
            };
            algorithm.Statistics.Add(new MockStatistic());

            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting class is missing.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_ValidateRequiredSetting_Async()
        {
            RequiredSettingGeneticAlgorithm algorithm = new RequiredSettingGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new SimplePopulation(),
                GeneticEntitySeed = new MockEntity()
            };

            AssertEx.ThrowsAsync<ArgumentException>(() => algorithm.InitializeAsync());
        }

        /// <summary>
        /// Tests that the Run method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Async()
        {
            int eventHandlerCallCount = 0;
            TestGeneticAlgorithm algorithm = GetAlgorithm();
            await algorithm.InitializeAsync();
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate(object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventHandlerCallCount++;
            });
            await algorithm.RunAsync();

            Assert.AreEqual(3, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(3, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(800, ((MockFitnessEvaluator)algorithm.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(8, ((MockFitnessScalingStrategy)algorithm.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run twice without initializing in between.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Twice_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = GetAlgorithm();

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
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };

            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.RunAsync());
        }

        /// <summary>
        /// Tests that the Step method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Async()
        {
            TestGeneticAlgorithm algorithm = GetAlgorithm();

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
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(400, ((MockFitnessEvaluator)algorithm.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(4, ((MockFitnessScalingStrategy)algorithm.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");

            stepResult = await algorithm.StepAsync();
            Assert.IsFalse(stepResult, "Algorithm should not be complete yet.");

            stepResult = await algorithm.StepAsync();
            Assert.IsTrue(stepResult, "Algorithm should be complete.");

            Assert.AreEqual(3, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(3, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(800, ((MockFitnessEvaluator)algorithm.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(8, ((MockFitnessScalingStrategy)algorithm.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");
        }

        /// <summary>
        /// Tests that an exception is thrown when Step is called too many times with no initialize in between.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Overbounds_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = GetAlgorithm();

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
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };

            AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.StepAsync());
        }

        /// <summary>
        /// Tests that the ApplyElitism method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyElitism()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator
                {
                    EvaluationMode = FitnessEvaluationMode.Maximize
                },
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation()
            };

            TestApplyElitism(algorithm, 0);

            algorithm.ElitismStrategy = new MockElitismStrategy
            {
                ElitistRatio = .1
            };
            algorithm.ElitismStrategy.Initialize(algorithm);
            TestApplyElitism(algorithm, 1);
        }

        private void TestApplyElitism(TestGeneticAlgorithm algorithm, int expectedEliteCount)
        {
            PrivateObject accessor = new PrivateObject(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            for (int i = 0; i < 10; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            IList<GeneticEntity> entities = (IList<GeneticEntity>)accessor.Invoke("ApplyElitism", population);
            Assert.AreEqual(expectedEliteCount, entities.Count);
        }

        /// <summary>
        /// Tests that the SelectGeneticEntitiesAndApplyCrossoverAndMutation method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_SelectGeneticEntitiesAndApplyCrossoverAndMutation()
        {
            GeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                },
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                },
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };

            MockCrossoverOperator crossoverOp = (MockCrossoverOperator)algorithm.CrossoverOperator;
            crossoverOp.Initialize(algorithm);
            algorithm.CrossoverOperator = crossoverOp;
            MockMutationOperator mutationOp = new MockMutationOperator();
            mutationOp.Initialize(algorithm);
            algorithm.MutationOperator = mutationOp;
            MockSelectionOperator selectionOp = new MockSelectionOperator();
            selectionOp.Initialize(algorithm);
            algorithm.SelectionOperator = selectionOp;

            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            for (int i = 0; i < 10; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }

            PrivateObject algAccessor = new PrivateObject(algorithm);

            IList<GeneticEntity> geneticEntities = (IList<GeneticEntity>)algAccessor.Invoke("SelectGeneticEntitiesAndApplyCrossoverAndMutation", population);

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
            GeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };

            MockCrossoverOperator crossoverOp = (MockCrossoverOperator)algorithm.CrossoverOperator;
            crossoverOp.Initialize(algorithm);

            algorithm.CrossoverOperator = crossoverOp;

            PrivateObject algAccessor = new PrivateObject(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            IList<GeneticEntity> geneticEntities = (IList<GeneticEntity>)algAccessor.Invoke("ApplyCrossover", entity1, entity2);

            Assert.AreEqual(2, geneticEntities.Count, "Incorrect number of genetic entities returned.");
            Assert.AreEqual(1, crossoverOp.DoCrossoverCallCount, "Crossover not called correctly.");
        }

        /// <summary>
        /// Tests that the ApplyMutation method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyMutation()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity()
            };

            TestApplyMutation(algorithm, 0);

            algorithm.MutationOperator = new MockMutationOperator
            {
                MutationRate = .01
            };
            TestApplyMutation(algorithm, 3);
        }

        private void TestApplyMutation(TestGeneticAlgorithm algorithm, int expectedMutationCount)
        {
            PrivateObject accessor = new PrivateObject(algorithm);
            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();

            for (int i = 0; i < 3; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                geneticEntities.Add(entity);
            }

            IList<GeneticEntity> mutants = (IList<GeneticEntity>)accessor.Invoke("ApplyMutation", geneticEntities);

            Assert.AreEqual(geneticEntities.Count, mutants.Count, "Incorrect number of genetic entities returned.");
            if (algorithm.MutationOperator != null)
            {
                MockMutationOperator mutationOp = (MockMutationOperator)algorithm.MutationOperator;
                Assert.AreEqual(expectedMutationCount, mutationOp.DoMutateCallCount, "Mutation not called correctly.");
            }
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };
            Type testType = typeof(CrossoverDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };
            Type testType = typeof(CrossoverDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidElitism()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                ElitismStrategy = new MockElitismStrategy
                {
                    ElitistRatio = 1
                }
            };
            Type testType = typeof(ElitismDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidElitism()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                ElitismStrategy = new MockElitismStrategy
                {
                    ElitistRatio = 1
                }
            };

            Type testType = typeof(ElitismDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator()
            };
            Type testType = typeof(FitnessEvaluatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
            };
            Type testType = typeof(FitnessEvaluatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy()
            };
            Type testType = typeof(FitnessScalingStrategyDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy()
            };
            Type testType = typeof(FitnessScalingStrategyDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity()
            };
            Type testType = typeof(EntityDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };
            Type testType = typeof(EntityDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };
            Type testType = typeof(MutationOperatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };
            Type testType = typeof(MutationOperatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };
            Type testType = typeof(PopulationDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };
            Type testType = typeof(PopulationDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };
            Type testType = typeof(SelectionOperatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                }
            };
            Type testType = typeof(SelectionOperatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidStatisticType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
            };
            algorithm.Statistics.Add(new MockStatistic());
            algorithm.Statistics.Add(new MockStatistic2());


            Type testType = typeof(StatisticDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidStatisticType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
            };
            algorithm.Statistics.Add(new MockStatistic());

            Type testType = typeof(StatisticDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                Terminator = new MockTerminator()
            };
            Type testType = typeof(TerminatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                Terminator = new MockTerminator()
            };
            Type testType = typeof(TerminatorDependentClass2);
            AssertEx.Throws<InvalidOperationException>(() => algorithm.ValidateRequiredComponents(testType));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly when overriding a required type of a base class.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_OverrideRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                Terminator = new MockTerminator2()
            };
            Type testType = typeof(TerminatorDependentDerivedClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_UsingBaseTypeAsRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                Terminator = new MockTerminator3() // uses derived type of the required type
            };
            Type testType = typeof(TerminatorDependentClass3);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that GeneticAlgorithmConfiguration.EnvironmentSize can be set to valid value.
        ///</summary>
        [TestMethod()]
        public void EnvironmentSizeTest_Valid()
        {
            SimpleGeneticAlgorithm target = new SimpleGeneticAlgorithm();
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
            SimpleGeneticAlgorithm target = new SimpleGeneticAlgorithm();
            int val = 0;
            AssertEx.Throws<ValidationException>(() => target.EnvironmentSize = val);
        }

        private static async Task<GeneticAlgorithm> TestInitializeAsync(MockGeneticAlgorithm algorithm, bool initializeExceptionExpected = false)
        {
            bool eventCalled = false;
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate(object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventCalled = true;
            });
            
            if (initializeExceptionExpected)
            {
                await AssertEx.ThrowsAsync<ValidationException>(() => algorithm.InitializeAsync());
                return null;
            }
            else
            {
                await algorithm.InitializeAsync();
            }

            Assert.AreEqual(0, algorithm.CurrentGeneration, "Generation should be initialized.");
            
            Assert.AreEqual(algorithm.EnvironmentSize, algorithm.Environment.Populations.Count, "Environment not initialized correctly.");
            Assert.AreEqual(algorithm.PopulationSeed.PopulationSize, algorithm.Environment.Populations[0].Entities.Count, "Population not initialized correctly.");

            MockEntity entity = (MockEntity)algorithm.Environment.Populations[0].Entities[0];
            double entityId = Double.Parse(entity.Identifier);
            Assert.AreEqual(entityId, entity.RawFitnessValue, "Entity fitness was not evaluated.");
            if (algorithm.Statistics.Count > 0)
                Assert.IsTrue(((MockStatistic)algorithm.Statistics.OfType<MockStatistic>().FirstOrDefault()).StatisticEvaluated, "Statistics were not evaluated.");
            Assert.IsTrue(eventCalled, "GenerationCreated event was not raised.");
            return algorithm;
        }

        private TestGeneticAlgorithm GetAlgorithm()
        {
            int environmentSize = 2;
            int populationSize = 100;

            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm();
            algorithm.EnvironmentSize = environmentSize;

            MockPopulation popConfig = new MockPopulation();
            popConfig.PopulationSize = populationSize;
            algorithm.PopulationSeed = popConfig;

            MockCrossoverOperator crossConfig = new MockCrossoverOperator();
            crossConfig.CrossoverRate = .7;
            algorithm.CrossoverOperator = crossConfig;

            MockElitismStrategy eliteConfig = new MockElitismStrategy();
            eliteConfig.ElitistRatio = .1;
            algorithm.ElitismStrategy = eliteConfig;

            MockMutationOperator mutConfig = new MockMutationOperator();
            mutConfig.MutationRate = .01;
            algorithm.MutationOperator = mutConfig;

            MockSelectionOperator selConfig = new MockSelectionOperator();
            selConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.SelectionOperator = selConfig;

            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessScalingStrategy = new MockFitnessScalingStrategy();
            algorithm.GeneticEntitySeed = new MockEntity();
            algorithm.Statistics.Add(new MockStatistic());
            algorithm.Terminator = new TestTerminator();
            return algorithm;
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

        [RequiredTerminator(typeof(MockTerminator2))]
        private class TerminatorDependentClass2
        {
        }

        [RequiredTerminator(typeof(MockTerminator2Base))]
        private class TerminatorDependentClass3
        {
        }

        [RequiredTerminator(typeof(MockTerminator))]
        private class TerminatorDependentBaseClass
        {
        }

        [RequiredTerminator(typeof(MockTerminator2))]
        private class TerminatorDependentDerivedClass : TerminatorDependentBaseClass
        {
        }

        private class RequiredSettingGeneticAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        
        private class TestTerminator : Terminator
        {
            public override bool IsComplete()
            {
                return this.Algorithm.CurrentGeneration == 3;
            }
        }
        
        private class TestGeneticAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                return Task.FromResult(true);
            }
        }

        private class FakeValidationMutationOperator : MutationOperator
        {
            private int value = -1;

            [ConfigurationProperty]
            [IntegerValidator(MinValue = 0)]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeMutationOperator : MutationOperator
        {
            private int value;
            private double value2;

            [ConfigurationProperty]
            public double Value2
            {
                get { return this.value2; }
                set { this.value2 = value; }
            }

            [ConfigurationProperty]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
            
            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        
        [IntegerExternalValidator(typeof(FakeMutationOperator), "Value")]
        [CustomExternalValidator(typeof(FakeValidator), typeof(FakeMutationOperator), "Value")]
        [DoubleExternalValidator(typeof(FakeMutationOperator), "Value2")]
        private class FakeExternalValidatorTerminator : Terminator
        {
            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
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
