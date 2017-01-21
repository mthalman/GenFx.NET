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
            IList<GeneticEntity> geneticEntities = (IList<GeneticEntity>)algAccessor.Invoke("ApplyCrossover", algorithm.PopulationSeed, new GeneticEntity[] { entity1, entity2 });

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
        public void GeneticAlgorithm_ValidateConfiguration_ValidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new CrossoverDependentEntity(),
                PopulationSeed = new MockPopulation(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new CrossoverDependentEntity2(),
                PopulationSeed = new MockPopulation(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidElitism()
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
                },
                CrossoverOperator = new ElitismDependentCrossover()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidElitism()
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
                },
                CrossoverOperator = new ElitismDependentCrossover2()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new FitnessEvaluatorDependentSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new FitnessEvaluatorDependentSelectionOperator2(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                MutationOperator = new FitnessScalingStrategyDependentMutationOperator()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                MutationOperator = new FitnessScalingStrategyDependentMutationOperator2()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new EntityDependentPopulation(),
                GeneticEntitySeed = new MockEntity()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new EntityDependentPopulation2()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MutationOperatorDependentFitnessEvaluation(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MutationOperatorDependentFitnessEvaluator2(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new PopulationDependentFitnessScaling()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new PopulationDependentFitnessScaling2()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                Terminator = new SelectionOperatorDependentTerminator()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                Terminator = new SelectionOperatorDependentTerminator2()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidStatisticType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new StatisticDependentPopulation(),
            };
            algorithm.Statistics.Add(new MockStatistic());
            algorithm.Statistics.Add(new MockStatistic2());

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidStatisticType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new StatisticDependentPopulation2(),
            };
            algorithm.Statistics.Add(new MockStatistic());

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidPluginType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new PluginDependentPopulation(),
            };
            algorithm.Plugins.Add(new MockPlugin());
            algorithm.Plugins.Add(new MockPlugin2());

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidPluginType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new PluginDependentPopulation2(),
            };
            algorithm.Plugins.Add(new MockPlugin());

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentPopulation(),
                Terminator = new MockTerminator()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentPopulation2(),
                Terminator = new MockTerminator()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            AssertEx.Throws<ValidationException>(() => privObj.Invoke("ValidateConfiguration"));
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly when overriding a required type of a base class.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_OverrideRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentDerivedPopulation(),
                Terminator = new MockTerminator2()
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_UsingBaseTypeAsRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentPopulation3(),
                Terminator = new MockTerminator3() // uses derived type of the required type
            };

            PrivateObject privObj = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            privObj.Invoke("ValidateConfiguration");
        }

        /// <summary>
        /// Tests that GeneticAlgorithmConfiguration.EnvironmentSize can be set to valid value.
        ///</summary>
        [TestMethod()]
        public void EnvironmentSizeTest_Valid()
        {
            SimpleGeneticAlgorithm target = new SimpleGeneticAlgorithm();
            int val = 2;
            target.MinimumEnvironmentSize = val;

            Assert.AreEqual(val, target.MinimumEnvironmentSize, "EnvironmentSize was not set correctly.");
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
            AssertEx.Throws<ValidationException>(() => target.MinimumEnvironmentSize = val);
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
            
            Assert.AreEqual(algorithm.MinimumEnvironmentSize, algorithm.Environment.Populations.Count, "Environment not initialized correctly.");
            Assert.AreEqual(algorithm.PopulationSeed.MinimumPopulationSize, algorithm.Environment.Populations[0].Entities.Count, "Population not initialized correctly.");

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
            algorithm.MinimumEnvironmentSize = environmentSize;

            MockPopulation popConfig = new MockPopulation();
            popConfig.MinimumPopulationSize = populationSize;
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
        private class CrossoverDependentEntity : GeneticEntity
        {
            public override string Representation
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        [RequiredCrossoverOperator(typeof(MockCrossoverOperator2))]
        private class CrossoverDependentEntity2 : GeneticEntity
        {
            public override string Representation
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        [RequiredElitismStrategy(typeof(MockElitismStrategy))]
        private class ElitismDependentCrossover : CrossoverOperator
        {
            public ElitismDependentCrossover() : base(2)
            {
            }

            protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredElitismStrategy(typeof(MockElitismStrategy2))]
        private class ElitismDependentCrossover2 : CrossoverOperator
        {
            public ElitismDependentCrossover2() : base(2)
            {
            }

            protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessEvaluator(typeof(MockFitnessEvaluator))]
        private class FitnessEvaluatorDependentSelectionOperator : SelectionOperator
        {
            protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessEvaluator(typeof(MockFitnessEvaluator2))]
        private class FitnessEvaluatorDependentSelectionOperator2 : SelectionOperator
        {
            protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessScalingStrategy(typeof(MockFitnessScalingStrategy))]
        private class FitnessScalingStrategyDependentMutationOperator : MutationOperator
        {
            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessScalingStrategy(typeof(MockFitnessScalingStrategy2))]
        private class FitnessScalingStrategyDependentMutationOperator2 : MutationOperator
        {
            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredGeneticAlgorithm(typeof(MockGeneticAlgorithm))]
        private class GeneticAlgorithmDependentClass
        {
        }

        [RequiredGeneticAlgorithm(typeof(MockGeneticAlgorithm2))]
        private class GeneticAlgorithmDependentClass2
        {
        }

        [RequiredGeneticEntity(typeof(MockEntity))]
        private class EntityDependentPopulation : Population
        {
        }

        [RequiredGeneticEntity(typeof(MockEntity2))]
        private class EntityDependentPopulation2 : Population
        {
        }

        [RequiredMutationOperator(typeof(MockMutationOperator))]
        private class MutationOperatorDependentFitnessEvaluation : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredMutationOperator(typeof(MockMutationOperator2))]
        private class MutationOperatorDependentFitnessEvaluator2 : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredPopulation(typeof(MockPopulation))]
        private class PopulationDependentFitnessScaling : FitnessScalingStrategy
        {
            protected override void UpdateScaledFitnessValues(Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredPopulation(typeof(MockPopulation2))]
        private class PopulationDependentFitnessScaling2 : FitnessScalingStrategy
        {
            protected override void UpdateScaledFitnessValues(Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredSelectionOperator(typeof(MockSelectionOperator))]
        private class SelectionOperatorDependentTerminator : Terminator
        {
            public override bool IsComplete()
            {
                throw new NotImplementedException();
            }
        }

        [RequiredSelectionOperator(typeof(MockSelectionOperator2))]
        private class SelectionOperatorDependentTerminator2 : Terminator
        {
            public override bool IsComplete()
            {
                throw new NotImplementedException();
            }
        }

        [RequiredStatistic(typeof(MockStatistic))]
        private class StatisticDependentPopulation : Population
        {
        }

        [RequiredStatistic(typeof(MockStatistic2))]
        private class StatisticDependentPopulation2 : Population
        {
        }

        [RequiredPlugin(typeof(MockPlugin))]
        private class PluginDependentPopulation : Population
        {
        }

        [RequiredPlugin(typeof(MockPlugin2))]
        private class PluginDependentPopulation2 : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator))]
        private class TerminatorDependentPopulation : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator2))]
        private class TerminatorDependentPopulation2 : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator2Base))]
        private class TerminatorDependentPopulation3 : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator))]
        private class TerminatorDependentBasePopulation : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator2))]
        private class TerminatorDependentDerivedPopulation : TerminatorDependentBasePopulation
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
        [CustomExternalValidator(typeof(FakeMutationOperator), "Value", typeof(FakeValidator))]
        [DoubleExternalValidator(typeof(FakeMutationOperator), "Value2")]
        private class FakeExternalValidatorTerminator : Terminator
        {
            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        
        private class FakeValidator : PropertyValidator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                return true;
            }
        }
    }
}
