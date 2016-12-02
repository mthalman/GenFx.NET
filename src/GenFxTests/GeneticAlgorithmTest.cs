using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentModel;
using GenFx.Validation;
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
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
        /// Tests that the Initialize method works correctly for external configuration validator mapping.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_ExternalConfigurationValidatorMapping_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.CrossoverOperator = new MockCrossoverOperatorConfiguration();
            algorithm.ConfigurationSet.ElitismStrategy = new MockElitismStrategyConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            algorithm.ConfigurationSet.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.MutationOperator = new FakeMutationOperatorConfiguration();
            algorithm.ConfigurationSet.Population = new MockPopulationConfiguration();
            algorithm.ConfigurationSet.SelectionOperator = new MockSelectionOperatorConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new MockStatisticConfiguration());
            algorithm.ConfigurationSet.Terminator = new FakeExternalValidatorTerminatorConfiguration();
            await algorithm.InitializeAsync();

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            Dictionary<PropertyInfo, List<Validator>> mapping = (Dictionary<PropertyInfo, List<Validator>>)accessor.GetField("externalValidationMapping");
            Assert.AreEqual(2, mapping.Count, "Incorrect number of mappings.");
            List<Validator> validators = mapping[typeof(FakeMutationOperatorConfiguration).GetProperty("Value")];
            Assert.AreEqual(2, validators.Count, "Incorrect number of validators for property.");
            Assert.IsTrue(validators.Any(v => v is FakeValidator), "FakeSettingValidator not found.");
            Assert.IsTrue(validators.Any(v => v is IntegerValidator), "IntegerValidator not found.");
            validators = mapping[typeof(FakeMutationOperatorConfiguration).GetProperty("Value2")];
            Assert.AreEqual(1, validators.Count, "Incorrect number of validators for property.");
            Assert.IsInstanceOfType(validators[0], typeof(DoubleValidator), "Incorrect type of validator.");
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
        [ExpectedException(typeof(InvalidOperationException))]
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
            await TestInitializeAsync(config);
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
        [ExpectedException(typeof(InvalidOperationException))]
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
            await TestInitializeAsync(config);
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
        [ExpectedException(typeof(InvalidOperationException))]
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
            await TestInitializeAsync(config);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
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
            await TestInitializeAsync(config);
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
        [ExpectedException(typeof(ArgumentException))]
        public async Task GeneticAlgorithm_Initialize_ValidateRequiredSetting_Async()
        {
            GeneticAlgorithm algorithm = new RequiredSettingGeneticAlgorithm();
            algorithm.ConfigurationSet.SelectionOperator = new MockSelectionOperatorConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            await algorithm.InitializeAsync();
        }

        /// <summary>
        /// Tests that the Run method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Async()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

            int eventHandlerCallCount = 0;
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
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GeneticAlgorithm_Run_Twice_NoInitialize_Async()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

            await algorithm.InitializeAsync();
            await algorithm.RunAsync();
            await algorithm.RunAsync();
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run without first calling Initialize.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GeneticAlgorithm_Run_NoInitialize_Async()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

            await algorithm.RunAsync();
        }

        /// <summary>
        /// Tests that the Step method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Async()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

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
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GeneticAlgorithm_Step_Overbounds_NoInitialize_Async()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

            await algorithm.InitializeAsync();
            bool result = await algorithm.StepAsync();
            Assert.IsFalse(result, "Algorithm should not be complete.");

            result = await algorithm.StepAsync();
            Assert.IsFalse(result, "Algorithm should not be complete.");

            result = await algorithm.StepAsync();
            Assert.IsTrue(result, "Algorithm should be complete.");

            await algorithm.StepAsync();
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run without first calling Initialize.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GeneticAlgorithm_Step_NoInitialize_Async()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();

            await algorithm.StepAsync();
        }

        /// <summary>
        /// Tests that the ApplyElitism method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyElitism()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            PrivateObject accessor = new PrivateObject(algorithm);
            Population population = new Population(algorithm);
            for (int i = 0; i < 10; i++)
            {
                population.Entities.Add(new MockEntity(algorithm));
            }
            IList<GeneticEntity> entity = (IList<GeneticEntity>)accessor.Invoke("ApplyElitism", population);
            Assert.AreEqual(0, entity.Count, "No elite genetic entities should be returned.");

            MockElitismStrategyConfiguration config = new MockElitismStrategyConfiguration();
            config.ElitistRatio = .1;
            algorithm.ConfigurationSet.ElitismStrategy = config;
            MockElitismStrategy elitismStrategy = new MockElitismStrategy(algorithm);

            algorithm.Operators.ElitismStrategy = elitismStrategy;

            MockSelectionOperatorConfiguration selectionConfig = new MockSelectionOperatorConfiguration();
            selectionConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = selectionConfig;

            MockFitnessEvaluatorConfiguration fitnessConfig = new MockFitnessEvaluatorConfiguration();
            fitnessConfig.EvaluationMode = FitnessEvaluationMode.Maximize;
            algorithm.ConfigurationSet.FitnessEvaluator = fitnessConfig;

            entity = (IList<GeneticEntity>)accessor.Invoke("ApplyElitism", population);
            Assert.AreEqual(1, entity.Count, "Incorrect number of elite genetic entities returned.");
            Assert.AreEqual(1, elitismStrategy.GetElitistGeneticEntitiesCallCount, "ElitismStrategy not called correctly.");
        }

        /// <summary>
        /// Tests that the SelectGeneticEntitiesAndApplyCrossoverAndMutation method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_SelectGeneticEntitiesAndApplyCrossoverAndMutation()
        {
            GeneticAlgorithm algorithm = new TestGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();

            MockCrossoverOperatorConfiguration crossConfig = new MockCrossoverOperatorConfiguration();
            crossConfig.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = crossConfig;

            MockMutationOperatorConfiguration mutConfig = new MockMutationOperatorConfiguration();
            mutConfig.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = mutConfig;

            MockSelectionOperatorConfiguration selConfig = new MockSelectionOperatorConfiguration();
            selConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = selConfig;

            MockCrossoverOperator crossoverOp = new MockCrossoverOperator(algorithm);
            algorithm.Operators.CrossoverOperator = crossoverOp;
            MockMutationOperator mutationOp = new MockMutationOperator(algorithm);
            algorithm.Operators.MutationOperator = mutationOp;
            MockSelectionOperator selectionOp = new MockSelectionOperator(algorithm);
            algorithm.Operators.SelectionOperator = selectionOp;

            Population population = new Population(algorithm);
            for (int i = 0; i < 10; i++)
            {
                population.Entities.Add(new MockEntity(algorithm));
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
            GeneticAlgorithm algorithm = new TestGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();

            MockCrossoverOperatorConfiguration config = new MockCrossoverOperatorConfiguration();
            config.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = config;
            MockCrossoverOperator crossoverOp = new MockCrossoverOperator(algorithm);

            algorithm.Operators.CrossoverOperator = crossoverOp;

            PrivateObject algAccessor = new PrivateObject(algorithm);
            IList<GeneticEntity> geneticEntities = (IList<GeneticEntity>)algAccessor.Invoke("ApplyCrossover", new MockEntity(algorithm), new MockEntity(algorithm));

            Assert.AreEqual(2, geneticEntities.Count, "Incorrect number of genetic entities returned.");
            Assert.AreEqual(1, crossoverOp.DoCrossoverCallCount, "Crossover not called correctly.");
        }

        /// <summary>
        /// Tests that the ApplyMutation method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyMutation()
        {
            GeneticAlgorithm algorithm = new TestGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();

            PrivateObject accessor = new PrivateObject(algorithm);
            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            geneticEntities.Add(new MockEntity(algorithm));
            geneticEntities.Add(new MockEntity(algorithm));
            geneticEntities.Add(new MockEntity(algorithm));

            IList<GeneticEntity> mutants = (IList<GeneticEntity>)accessor.Invoke("ApplyMutation", geneticEntities);

            Assert.AreEqual(geneticEntities.Count, mutants.Count, "Incorrect number of genetic entities returned.");

            MockMutationOperatorConfiguration config = new MockMutationOperatorConfiguration();
            config.MutationRate = .01;
            algorithm.ConfigurationSet.MutationOperator = config;
            MockMutationOperator mutationOp = new MockMutationOperator(algorithm);
            algorithm.Operators.MutationOperator = mutationOp;

            mutants = (IList<GeneticEntity>)accessor.Invoke("ApplyMutation", geneticEntities);

            Assert.AreEqual(geneticEntities.Count, mutants.Count, "Incorrect number of genetic entities returned.");
            Assert.AreEqual(3, mutationOp.DoMutateCallCount, "Mutation not called correctly.");
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateConfiguration_NoGeneticAlgorithm()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            //config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateConfiguration_NoSelectionOperator()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            //config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateConfiguration_NoFitnessEvaluator()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            //config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateConfiguration_NoPopulation()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            //config.Population = new MockPopulationConfiguration();
            config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config);
        }

        /// <summary>
        /// Tests that the ValidateConfiguration method works correctly.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateConfiguration_NoEntity()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet();
            config.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            config.SelectionOperator = new MockSelectionOperatorConfiguration();
            config.FitnessEvaluator = new MockFitnessEvaluatorConfiguration();
            config.Population = new MockPopulationConfiguration();
            //config.Entity = new MockEntityConfiguration();
            TestValidateConfiguration(config);
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when a null argument is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GeneticAlgorithm_ValidateComponentConfiguration_NullArg()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ValidateComponentConfiguration(null);
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when passed a component that is not configured on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GeneticAlgorithm_ValidateComponentConfiguration_ConfigurationNotSet()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.MutationOperator = new MockMutationOperatorConfiguration();
            MutationOperator mutationOperator = new MockMutationOperator(algorithm);
            algorithm.ConfigurationSet.MutationOperator = null;
            algorithm.ValidateComponentConfiguration(mutationOperator);
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when passed a component has a mismatch configuration configured on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GeneticAlgorithm_ValidateComponentConfiguration_MismatchedConfiguration()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.MutationOperator = new MockMutationOperatorConfiguration();
            MutationOperator mutationOperator = new MockMutationOperator(algorithm);
            algorithm.ConfigurationSet.MutationOperator = new MockMutationOperator2Configuration();
            algorithm.ValidateComponentConfiguration(mutationOperator);
        }

        /// <summary>
        /// Tests that the ValidateComponentConfiguration method throws an exception when a component's configuration property is determined to be invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeneticAlgorithm_ValidateComponentConfiguration_InvalidProperty()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.MutationOperator = new FakeValidationMutationOperatorConfiguration();
            MutationOperator mutationOperator = new FakeValidationMutationOperator(algorithm);
            algorithm.ValidateComponentConfiguration(mutationOperator);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidCrossover()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockCrossoverOperatorConfiguration config = new MockCrossoverOperatorConfiguration();
            config.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = config;
            Type testType = typeof(CrossoverDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidCrossover()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockCrossoverOperatorConfiguration config = new MockCrossoverOperatorConfiguration();
            config.CrossoverRate = 1;
            algorithm.ConfigurationSet.CrossoverOperator = config;
            Type testType = typeof(CrossoverDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidElitism()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockElitismStrategyConfiguration config = new MockElitismStrategyConfiguration();
            config.ElitistRatio = 1;
            algorithm.ConfigurationSet.ElitismStrategy = config;
            Type testType = typeof(ElitismDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidElitism()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            MockElitismStrategyConfiguration config = new MockElitismStrategyConfiguration();
            config.ElitistRatio = 1;
            algorithm.ConfigurationSet.ElitismStrategy = config;
            Type testType = typeof(ElitismDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidFitnessEvaluator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockFitnessEvaluatorConfiguration config = new MockFitnessEvaluatorConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = config;
            Type testType = typeof(FitnessEvaluatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidFitnessEvaluator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockFitnessEvaluatorConfiguration config = new MockFitnessEvaluatorConfiguration();
            algorithm.ConfigurationSet.FitnessEvaluator = config;
            Type testType = typeof(FitnessEvaluatorDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidFitnessScalingStrategy()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            Type testType = typeof(FitnessScalingStrategyDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidFitnessScalingStrategy()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.FitnessScalingStrategy = new MockFitnessScalingStrategyConfiguration();
            Type testType = typeof(FitnessScalingStrategyDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidEntity()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            Type testType = typeof(EntityDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidEntity()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            Type testType = typeof(EntityDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidMutationOperator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockMutationOperatorConfiguration config = new MockMutationOperatorConfiguration();
            config.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = config;
            Type testType = typeof(MutationOperatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidMutationOperator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockMutationOperatorConfiguration config = new MockMutationOperatorConfiguration();
            config.MutationRate = 1;
            algorithm.ConfigurationSet.MutationOperator = config;
            Type testType = typeof(MutationOperatorDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Population = new MockPopulationConfiguration();
            Type testType = typeof(PopulationDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Population = new MockPopulationConfiguration();
            Type testType = typeof(PopulationDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidSelectionOperator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockSelectionOperatorConfiguration config = new MockSelectionOperatorConfiguration();
            config.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = config;
            Type testType = typeof(SelectionOperatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidSelectionOperator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            MockSelectionOperatorConfiguration config = new MockSelectionOperatorConfiguration();
            config.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.ConfigurationSet.SelectionOperator = config;
            Type testType = typeof(SelectionOperatorDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidStatisticType()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Statistics.Add(new MockStatisticConfiguration());
            algorithm.ConfigurationSet.Statistics.Add(new MockStatistic2Configuration());
            Type testType = typeof(StatisticDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidStatisticType()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Statistics.Add(new MockStatisticConfiguration());
            Type testType = typeof(StatisticDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_ValidTerminator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Terminator = new MockTerminatorConfiguration();
            Type testType = typeof(TerminatorDependentClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GeneticAlgorithm_ValidateRequiredComponents_InvalidTerminator()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Terminator = new MockTerminatorConfiguration();
            Type testType = typeof(TerminatorDependentClass2);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly when overriding a required type of a base class.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_OverrideRequiredType()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Terminator = new MockTerminator2Configuration();
            Type testType = typeof(TerminatorDependentDerivedClass);
            algorithm.ValidateRequiredComponents(testType);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateRequiredComponents_UsingBaseTypeAsRequiredType()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Terminator = new MockTerminator3Configuration(); // uses derived type of the required type
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
        [ExpectedException(typeof(ValidationException))]
        public void EnvironmentSizeTest_Invalid()
        {
            SimpleGeneticAlgorithmConfiguration target = new SimpleGeneticAlgorithmConfiguration();
            int val = 0;
            target.EnvironmentSize = val;
        }

        /// <summary>
        /// Tests that GeneticAlgorithmConfiguration.StatisticsEnabled can be set to a valid value.
        ///</summary>
        [TestMethod()]
        public void StatisticsEnabledTest()
        {
            SimpleGeneticAlgorithmConfiguration target = new SimpleGeneticAlgorithmConfiguration();
            bool val = true;
            target.StatisticsEnabled = val;
            Assert.AreEqual(val, target.StatisticsEnabled, "StatisticsEnabled was not set correctly.");

            val = false;
            target.StatisticsEnabled = val;
            Assert.AreEqual(val, target.StatisticsEnabled, "StatisticsEnabled was not set correctly.");
        }

        private static void TestValidateConfiguration(ComponentConfigurationSet config)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            accessor.SetField("config", config);
            accessor.Invoke("ValidateConfiguration");
        }

        private static async Task<GeneticAlgorithm> TestInitializeAsync(ComponentConfigurationSet config)
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            bool eventCalled = false;
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate(object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventCalled = true;
            });
            algorithm.ConfigurationSet.CrossoverOperator = config.CrossoverOperator;
            algorithm.ConfigurationSet.ElitismStrategy = config.ElitismStrategy;
            algorithm.ConfigurationSet.GeneticAlgorithm = config.GeneticAlgorithm;
            if (config.FitnessEvaluator != null)
                algorithm.ConfigurationSet.FitnessEvaluator = config.FitnessEvaluator;
            algorithm.ConfigurationSet.FitnessScalingStrategy = config.FitnessScalingStrategy;
            if (config.Entity != null)
                algorithm.ConfigurationSet.Entity = config.Entity;
            algorithm.ConfigurationSet.MutationOperator = config.MutationOperator;
            if (config.Population != null)
                algorithm.ConfigurationSet.Population = config.Population;
            if (config.SelectionOperator != null)
            {
                algorithm.ConfigurationSet.SelectionOperator = config.SelectionOperator;
            }
            for (int i = 0; i < config.Statistics.Count; i++)
            {
                algorithm.ConfigurationSet.Statistics.Add(config.Statistics[i]);
            }
            if (config.Terminator != null)
                algorithm.ConfigurationSet.Terminator = config.Terminator;

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
                Assert.IsInstanceOfType(algorithm.Operators.Terminator, typeof(MockTerminator), "Terminator not initialized correctly.");
            else
                Assert.IsInstanceOfType(algorithm.Operators.Terminator, typeof(GeneticAlgorithm).Assembly.GetType("GenFx.EmptyTerminator"), "Terminator not initialized correctly.");

            Assert.AreEqual(config.Statistics.Count, algorithm.Statistics.Count, "Statistic collection not initialized correctly.");
            if (config.Statistics.Count > 0)
                Assert.IsInstanceOfType(algorithm.Statistics[0], typeof(MockStatistic), "Statistic not initialized correctly.");

            Assert.AreEqual(algorithm.ConfigurationSet.GeneticAlgorithm.EnvironmentSize, algorithm.Environment.Populations.Count, "Environment not initialized correctly.");
            Assert.AreEqual(algorithm.ConfigurationSet.Population.PopulationSize, algorithm.Environment.Populations[0].Entities.Count, "Population not initialized correctly.");

            MockEntity entity = (MockEntity)algorithm.Environment.Populations[0].Entities[0];
            double entityId = Double.Parse(entity.Identifier);
            Assert.AreEqual(entityId, entity.RawFitnessValue, "Entity fitness was not evaluated.");
            if (config.Statistics.Count > 0)
                Assert.IsTrue(((MockStatistic)algorithm.Statistics[0]).StatisticEvaluated, "Statistics were not evaluated.");
            Assert.IsTrue(eventCalled, "GenerationCreated event was not raised.");
            return algorithm;
        }

        private GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new TestGeneticAlgorithm();
            ComponentConfigurationSet config = algorithm.ConfigurationSet;

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

        [RequiredTerminator(typeof(MockTerminator2))]
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
            public TestTerminator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override bool IsComplete()
            {
                return this.Algorithm.CurrentGeneration == 3;
            }
        }

        [Component(typeof(TestTerminator))]
        private class TestTerminatorConfiguration : TerminatorConfiguration
        {
        }

        private class TestGeneticAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                return Task.FromResult(true);
            }
        }

        [Component(typeof(TestGeneticAlgorithm))]
        private class TestGeneticAlgorithmConfiguration : GeneticAlgorithmConfiguration
        {
        }

        private class FakeValidationMutationOperator : MutationOperator
        {
            public FakeValidationMutationOperator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(FakeValidationMutationOperator))]
        private class FakeValidationMutationOperatorConfiguration : MutationOperatorConfiguration
        {
            private int value = -1;

            [IntegerValidator(MinValue = 0)]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        private class FakeMutationOperator : MutationOperator
        {
            public FakeMutationOperator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(FakeMutationOperator))]
        private class FakeMutationOperatorConfiguration : MutationOperatorConfiguration
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
        private class FakeExternalValidatorTerminator : Terminator
        {
            public FakeExternalValidatorTerminator(GeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(FakeExternalValidatorTerminator))]
        private class FakeExternalValidatorTerminatorConfiguration : TerminatorConfiguration
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
